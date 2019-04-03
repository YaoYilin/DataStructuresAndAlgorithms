using System.Collections.Generic;
/// <summary>
/// An example of recursive elimination
/// sum of preceding n terms 
/// </summary>
public class RecursiveEliminate
{
    // address 0
    public static int Recursion(int n)
    {
        // address 1
        if(n == 0)
        {
            // address 2
            return 0;
        }
        // address 2 + address 3
        return n + Recursion(n - 1);
        // address 4
    }

    public static int NonRecurision(int n)
    {
        Stack<Frame> stack = new Stack<Frame>();
        int address = 0;
        int returnValue = 0;
        while (true)
        {
            switch (address)
            {
                case 0:
                    {
                        stack.Push(new Frame(n, 4));
                        address = 1;
                    }
                    break;
                case 1:
                    {
                        Frame frame = stack.Peek();
                        if(frame.N == 0)
                        {
                            returnValue = 0;
                            address = 2;
                        }
                        else
                        {
                            stack.Push(new Frame(frame.N - 1, 3));
                            address = 1;
                        }
                    }
                    break;
                case 2:
                    {
                        address = stack.Pop().Address;
                    }
                    break;
                case 3:
                    {
                        Frame frame = stack.Peek();
                        returnValue += frame.N;
                        address = 2;
                    }
                    break;
                case 4:
                    return returnValue;
            }
        }
        
    }

    private class Frame
    {
        public int N;
        public int Address;
        public Frame(int n, int address)
        {
            N = n;
            Address = address;
        }
    }
}
