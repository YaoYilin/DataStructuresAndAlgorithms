/// <summary>
/// Binary Search Tree
/// </summary>
public class BSTree
{
    public class Node
    {
        public Node(int data)
        {
            Data = data;
        }

        public int Data { get; set; }
        public Node Left;
        public Node Right;
    }

    public BSTree() : this(null)
    {

    }

    public BSTree(Node root)
    {
        Root = root;
    }

    public Node Root = null;

    public void Insert(int data)
    {
        Root = Insert(Root, data);
    }

    public void Delete(int data)
    {
        Root = Delete(Root, data);
    }

    public bool Search(int data)
    {
        return Search(Root, data);
    }

    private Node Insert(Node root, int data)
    {
        if (root == null)
        {
            root = new Node(data);
        }
        else if (root.Data <= data)
        {
            root.Right = Insert(root.Right, data);
        }
        else
        {
            root.Left = Insert(root.Left, data);
        }

        return root;
    }

    private bool Search(Node root, int data)
    {
        if(root == null)
        {
            return false;
        }
        if(data == root.Data)
        {
            return true;
        }
        if(data <= root.Data)
        {
            return Search(root.Left, data);
        }
        else
        {
            return Search(root.Right, data);
        }
    }

    private Node Delete(Node root, int data)
    {
        if (root == null)
        {
            return null;
        }
        else if (data < root.Data)
        {
            // 从左节点找
            root.Left = Delete(root.Left, data);
        }
        else if (data > root.Data)
        {
            // 从右节点找
            root.Right = Delete(root.Right, data);
        }
        else
        {
            // 找到了
            if (root.Left == null && root.Right == null)
            {
                // 没有子节点
                root = null;
            }
            else if (root.Left == null)
            {
                // 只有右节点
                root = root.Right;
            }
            else if (root.Right == null)
            {
                // 只有左节点
                root = root.Left;
            }
            else
            {
                // 有两个节点， 找到右边最小的节点替换掉
                Node node = FindMin(root.Right);
                root.Data = node.Data;
                root.Right = Delete(root.Right, node.Data);
            }
        }
        return root;
    }

    private Node FindMin(Node node)
    {
        if (node == null)
        {
            return node;
        }

        if (node.Left == null)
        {
            return node;
        }

        return FindMin(node.Left);
    }
}
