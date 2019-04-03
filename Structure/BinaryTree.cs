using System.Collections.Generic;

public class BinaryTree
{
    public TreeNode Root { get; private set; }
    public BinaryTree(int[] arr)
    {
        if(arr == null || arr.Length <= 0)
        {
            return;
        }
        Root = new TreeNode(arr[0]);
        BuildTree(Root, arr, 0);
    }

    /// <summary>
    /// NLR
    /// </summary>
    /// <returns></returns>
    public List<int> PreorderTraversal()
    {
        List<int> visit = new List<int>();
        PreorderTraversal(Root, visit);
        return visit;
    }
    private void PreorderTraversal(TreeNode root, List<int> list)
    {
        if(root == null)
        {
            return;
        }
        list.Add(root.Value);
        PreorderTraversal(root.Left, list);
        PreorderTraversal(root.Right, list);
    }

    /// <summary>
    /// LNR
    /// </summary>
    /// <returns></returns>
    public List<int> InorderTraversal()
    {
        List<int> visit = new List<int>();
        InorderTraversal(Root, visit);
        return visit;
    }

    private void InorderTraversal(TreeNode root, List<int> list)
    {
        if (root == null)
        {
            return;
        }
        PreorderTraversal(root.Left, list);
        list.Add(root.Value);
        PreorderTraversal(root.Right, list);
    }

    /// <summary>
    /// LRN
    /// </summary>
    /// <returns></returns>
    public List<int> PostorderTraversal()
    {
        List<int> visit = new List<int>();
        PostorderTraversal(Root, visit);
        return visit;
    }

    private void PostorderTraversal(TreeNode root, List<int> list)
    {
        if (root == null)
        {
            return;
        }
        PreorderTraversal(root.Left, list);
        PreorderTraversal(root.Right, list);
        list.Add(root.Value);
    }

    private void BuildTree(TreeNode root, int[] arr, int index)
    {
        int left = index * 2 + 1;
        int right = left + 1;
        if(left < arr.Length)
        {
            root.Left = new TreeNode(arr[left]);
            BuildTree(root.Left, arr, left);
        }

        if(right < arr.Length)
        {
            root.Right = new TreeNode(arr[right]);
            BuildTree(root.Right, arr, right);
        }
    }

    public class TreeNode
    {
        public TreeNode Left;
        public TreeNode Right;
        public int Value;
        public TreeNode(int v)
        {
            Value = v;
        }
    }
}

