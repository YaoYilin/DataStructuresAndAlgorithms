using System;

/// <summary>
/// 左倾红黑树 （LLRB Trees, Left-leaning Red-Black Trees）
/// 
/// 左倾红黑树实际上是 2-3 树的进一步优化。
/// 红链接连接的是两个 2 节点，等价于 2-3 树的 3 节点；
/// 黑链接连接的是两个普通的 2 节点。
/// 
/// 特点：
/// 1.节点是红色或黑色。
/// 2.根是黑色。
/// 3.所有叶子都是黑色（叶子是NIL节点）。
/// 4.每个红色节点必须有两个黑色的子节点。（从每个叶子到根的所有路径上不能有两个连续的红色节点。）
/// 5.从任一节点到其每个叶子的所有简单路径都包含相同数目的黑色节点。
/// 
/// <seealso cref="https://algs4.cs.princeton.edu/33balanced/RedBlackBST.java.html"/>
/// 
/// Robert Sedgewick
/// Kevin Wayne
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
public class RedBlackTree<K, V> where K : IComparable
{
    public enum Color : byte
    {
        Black = 0,
        Red,
    }

    public class Node<_K, _V> where _K : IComparable
    {
        public Color Color;
        public Node<_K, _V> Left;
        public Node<_K, _V> Right;
        public int NodeCount;
        public _K Key;
        public _V Value;
    }

    public Node<K, V> Root = null;

    public int Count
    {
        get
        {
            return Size(Root);
        }
    }

    /// <summary>
    /// 对于插入节点，有三种情况：左旋、右旋、变色
    /// 
    /// 新插入的节点为红色节点
    /// 如果插入完树不平衡，则优先左子树，如果插入到了右节点需要进行左旋操作。（右节点红，左节点黑）
    /// 如果插入完变成了 左（Red）-- 左（Red）的情况，需要进行右旋操作。（左子节点红，左孙节点红）
    /// 如果操作完成后，左右子节点都为红，那么进行颜色变换。
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Insert(K key, V value)
    {
        if (key == null)
        {
            throw new Exception("Invalid key.");
        }
        if(value == null)
        {
            Delete(key);
            return;
        }
        Root = Insert(Root, key, value);
        Root.Color = Color.Black;
    }

    private Node<K, V> Insert(Node<K, V> root, K key, V value)
    {
        if (root == null)
        {
            return new Node<K, V>() { Key = key, Value = value, NodeCount = 1, Color = Color.Red };
        }
        int cmp = key.CompareTo(root.Key);
        if(cmp < 0)
        {
            root.Left = Insert(root.Left, key, value);
        }
        else if(cmp > 0)
        {
            root.Right = Insert(root.Right, key, value);
        }
        else
        {
            root.Value = value;
        }

        return Balance(root);
    }

    private Node<K,V> Balance(Node<K,V> root)
    {
        if (IsRed(root.Right) && !IsRed(root.Left))
        {
            root = RotateLeft(root);
        }

        if (IsRed(root.Left) && IsRed(root.Left.Left))
        {
            root = RotateRight(root);
        }

        if (IsRed(root.Left) && IsRed(root.Right))
        {
            FlipColors(root);
        }
        root.NodeCount = Size(root.Left) + Size(root.Right) + 1;
        return root;
    }

    public V Get(K key)
    {
        if(key == null)
        {
            throw new Exception("Invalid key.");
        }
        return Get(Root, key);
    }

    public bool Contains(K key)
    {
        return Get(Root, key) != null;
    }

    /// <summary>
    /// 最小 Key
    /// </summary>
    /// <returns></returns>
    public K Min()
    {
        if(IsEmpty())
        {
            throw new Exception("Tree is null.");
        }
        return Min(Root).Key;
    }

    private Node<K, V> Min(Node<K, V> root)
    {
        if (root.Left == null)
        {
            return root;
        }
        else
        {
            return Min(root.Left);
        }
    }

    /// <summary>
    /// 最大 Key
    /// </summary>
    /// <returns></returns>
    public K Max()
    {
        if (IsEmpty())
        {
            throw new Exception("Tree is null.");
        }
        return Max(Root).Key;
    }

    private Node<K, V> Max(Node<K, V> root)
    {
        if(root.Right == null)
        {
            return root;
        }
        else
        {
            return Max(root.Right);
        }
    }

    public bool IsEmpty()
    {
        return Root == null;
    }

    private V Get(Node<K, V> root, K key)
    {
        if(root == null)
        {
            return default(V);
        }
        int cmp = key.CompareTo(root.Key);
        if(cmp < 0)
        {
            return Get(root.Left, key);
        }
        else if (cmp > 0)
        {
            return Get(root.Right, key);
        }
        else
        {
            return root.Value;
        }
    }

    public void Delete(K key)
    {
        if(key == null)
        {
            throw new Exception("Invalid key.");
        }

        if(!Contains(key))
        {
            return;
        }

        if(!IsRed(Root.Left) &&!IsRed(Root.Right))
        {
            Root.Color = Color.Red;
        }

        Root = Delete(Root, key);
        if(!IsEmpty())
        {
            Root.Color = Color.Black;
        }
    }

    private Node<K, V> Delete(Node<K, V> root, K key)
    {
        if(key.CompareTo(root.Key) < 0)
        {
            if(!IsRed(root.Left) && !IsRed(root.Left.Left))
            {
                root = MoveRedLeft(root);
            }
            root.Left = Delete(root.Left, key);
        }
        else
        {
            if(IsRed(root.Left))
            {
                root = RotateRight(root);
            }

            if(key.CompareTo(root.Key) == 0 && root.Right == null)
            {
                return null;
            }
            if(!IsRed(root.Right) && !IsRed(root.Right.Left))
            {
                root = MoveRedRight(root);
            }

            if(key.CompareTo(root.Key) == 0)
            {
                Node<K, V> x = Min(root.Right);
                root.Key = x.Key;
                root.Value = x.Value;
                root.Right = DeleteMin(root.Right);
            }
            else
            {
                root.Right = Delete(root.Right, key);
            }
        }
        return Balance(root);
    }

    public void DeleteMin()
    {
        if(IsEmpty())
        {
            throw new Exception("Tree is null.");
        }
        if(!IsRed(Root.Left) && !IsRed(Root.Right))
        {
            Root.Color = Color.Red;
        }
        Root = DeleteMin(Root);
        if(!IsEmpty())
        {
            Root.Color = Color.Black;
        }
    }

    private Node<K, V> DeleteMin(Node<K, V> root)
    {
        if(root.Left == null)
        {
            return null;
        }

        if(!IsRed(root.Left) && !IsRed(root.Left.Left))
        {
            root = MoveRedLeft(root);
        }

        root.Left = DeleteMin(root.Left);
        return Balance(root);
    }

    public void DeleteMax()
    {
        if (IsEmpty())
        {
            throw new Exception("Tree is null.");
        }
        if(!IsRed(Root.Left) && !IsRed(Root.Right))
        {
            Root.Color = Color.Red;
        }
        Root = DeleteMax(Root);
        if(!IsEmpty())
        {
            Root.Color = Color.Black;
        }
    }

    private Node<K, V> DeleteMax(Node<K, V> root)
    {
        if(IsRed(root.Left))
        {
            root = RotateRight(root);
        }
        if(root.Right == null)
        {
            return null;
        }
        if(!IsRed(root.Right) && !IsRed(root.Right.Left))
        {
            root = MoveRedRight(root);
        }
        root.Right = DeleteMax(root.Right);
        return Balance(root);
    }

    /// <summary>
    /// Assuming that root is red and both root.Right and root.Right.Left
    /// are black, make root.Right or one of its children red.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    private Node<K, V> MoveRedRight(Node<K, V> root)
    {
        FlipColors(root);
        if(IsRed(root.Left.Left))
        {
            root = RotateRight(root);
            FlipColors(root);
        }
        return root;
    }

    /// <summary>
    /// Assuming that root is red and both root.Left and root.Left.Left
    /// are black, make root.Left or one of its children red.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    private Node<K, V> MoveRedLeft(Node<K, V> root)
    {
        FlipColors(root);
        if (IsRed(root.Right.Left))
        {
            root.Right = RotateRight(root.Right);
            root = RotateLeft(root);
            FlipColors(root);
        }
        return root;
    }

    private bool IsRed(Node<K, V> node)
    {
        return node != null && node.Color == Color.Red;
    }

    private Node<K, V> RotateLeft(Node<K, V> node)
    {
        Node<K, V> x = node.Right;
        node.Right = x.Left;
        x.Left = node;
        x.Color = node.Color;
        node.Color = Color.Red;
        x.NodeCount = node.NodeCount;
        node.NodeCount = 1 + Size(node.Left) + Size(node.Right);
        return x;
    }

    private Node<K, V> RotateRight(Node<K, V> node)
    {
        Node<K, V> x = node.Left;
        node.Left = x.Right;
        x.Right = node;
        x.Color = node.Color;
        node.Color = Color.Red;
        x.NodeCount = node.NodeCount;
        node.NodeCount = 1 + Size(node.Left) + Size(node.Right);
        return x;
    }

    private void FlipColors(Node<K, V> node)
    {
        node.Color = Color.Red;
        node.Left.Color = Color.Black;
        node.Right.Color = Color.Black;
    }

    private int Size(Node<K, V> node)
    {
        if (node == null)
        {
            return 0;
        }
        else
        {
            return node.NodeCount;
        }
    }
}
