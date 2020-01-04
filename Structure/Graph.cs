using System;
using System.Collections.Generic;

public class Graph
{
    public List<Vertex> Vertexs = new List<Vertex>();

    public void AddVertex(Vertex v)
    {
        Vertexs.Add(v);
    }

    public int Dijkstra(Vertex v1, Vertex v2)
    {
        v1.MinDistance = 0;
        PriorityQueue<Vertex> queue = new PriorityQueue<Vertex>();
        queue.Enqueue(v1);
        Dictionary<int, Vertex> map = new Dictionary<int, Vertex>();
        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            foreach (var edge in v.Edges)
            {
                int distance = edge.Distance + v.MinDistance;
                if (edge.To.MinDistance > distance)
                {
                    edge.To.MinDistance = distance;
                    int i;
                    if (queue.Contains(edge.To, out i))
                    {
                        queue.ShiftUp(i);
                    }
                    else
                    {
                        queue.Enqueue(edge.To);
                    }
                }
            }
            map.Add(v.Index, v);
        }

        return map[v2.Index].MinDistance;
    }

    public void Tarjan()
    {
        Stack<Vertex> stack = new Stack<Vertex>();
        int index = 0;
        foreach (var v in Vertexs)
        {
            if (!v.IsSearched)
            {
                Tarjan(stack, v, ref index);
            }
        }
    }

    private void Tarjan(Stack<Vertex> stack, Vertex v, ref int index)
    {
        v.Link = index;
        v.LowerLink = index;
        index++;
        stack.Push(v);
        v.IsSearched = true;
        foreach (var edge in v.Edges)
        {
            var w = edge.To;
            if (!w.IsSearched)
            {
                Tarjan(stack, w, ref index);
                v.LowerLink = System.Math.Min(v.LowerLink, w.LowerLink);
            }
            else if (stack.Contains(edge.To))
            {
                v.LowerLink = System.Math.Min(v.LowerLink, w.Link);
            }
        }

        if (v.IsSearched && v.Link == v.LowerLink)
        {
            Console.WriteLine("---");
            Vertex x = null;
            while (x != v)
            {
                x = stack.Pop();
                Console.Write(x.Index);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public enum State
    {
        NotVisited,
        Visiting,
        Visited
    }

    public class Edge
    {
        public Edge(Vertex from, Vertex to, int distance = 1)
        {
            From = from;
            To = to;
            Distance = distance;
        }

        public Vertex From
        {
            get;
            private set;
        }

        public Vertex To
        {
            get;
            private set;
        }

        public int Distance
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return "From " + From.Index + " To " + To.Index + " Distance = " + Distance;
        }

    }

    public class Vertex : IPriority
    {
        public Vertex(int index)
        {
            Index = index;
            MinDistance = int.MaxValue;
            State = State.NotVisited;
            IsSearched = false;
            Link = -1;
            LowerLink = -1;
        }

        public List<Edge> Edges = new List<Edge>();

        public void AddEdge(Vertex to, int distance = 1)
        {
            Edges.Add(new Edge(this, to, distance));
        }

        public int MinDistance
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public State State
        {
            get;
            set;
        }

        public bool IsSearched { get; set; }
        public int LowerLink { get; set; }
        public int Link { get; set; }
        public int Priority { get { return MinDistance; } }

        public override string ToString()
        {
            //return Index.ToString() + " " + MinDistance;
            return $"Index {Index}, Link {Link}, LowerLink {LowerLink}";
        }
    }
}

