using System;
using System.Collections.Generic;
using UnityEngine;

public class AVLTestTree<TKey, TValue> : AVLTree<TKey, TValue>
    where TKey : IComparable<TKey>
{
    public TreeNode<TKey, TValue> GetRoot()
    {
        return root;
    }
}


public class AVLTree<TKey, TValue> : BinarySearchTestTree<TKey, TValue> where TKey : IComparable<TKey>
{

    protected override TreeNode<TKey, TValue> Add(TreeNode<TKey, TValue> node, TKey key, TValue value)
    {
        node = base.Add(node, key, value);
        return Balance(node);
    }

    protected override TreeNode<TKey, TValue> AddOrUpdate(TreeNode<TKey, TValue> node, TKey key, TValue value)
    {
        node = base.AddOrUpdate(node, key, value);
        return Balance(node);
    }

    protected override TreeNode<TKey, TValue> Remove(TreeNode<TKey, TValue> node, TKey key)
    {
        node = base.Remove(node, key);
        return Balance(node);
    }

    protected int BalanceFactor(TreeNode<TKey, TValue> node)
    {
        return node == null ? 0 : Height(node.Left) - Height(node.Right);
    }

    protected TreeNode<TKey, TValue> Balance(TreeNode<TKey, TValue> root)
    {
        int balanceFactor = BalanceFactor(root);
        //LL LR RR RL
        if (balanceFactor > 1)
        {
            var leftNode = root.Left;

            if (BalanceFactor(leftNode) >= 0)
            {
                return RotateRight(root);
            }
            else
            {
                root.Left = RotateLeft(leftNode);
                return RotateRight(root);
            }
        }

        if (balanceFactor < -1)
        {
            var rightNode = root.Right;

            if (BalanceFactor(rightNode) <= 0)
            {
                return RotateLeft(root);
            }
            else
            {
                root.Right = RotateRight(rightNode);
                return RotateLeft(root);
            }
        }

        return root;
    }

    protected TreeNode<TKey, TValue> RotateLeft(TreeNode<TKey, TValue> node)
    {
        var current = node.Right;
        var temp = current.Left;

        current.Left = node;
        node.Right = temp;




        UpdateHeight(node);
        UpdateHeight(current);
        node = current;


        return node;
    }

    protected TreeNode<TKey, TValue> RotateRight(TreeNode<TKey, TValue> node)
    {
        var current = node.Left;
        var temp = current.Right;

        current.Right = node;
        node.Left = temp;


        UpdateHeight(node);
        UpdateHeight(current);
        node = current;

        return node;
    }



    // 모든 노드의 BF ∈ {-1, 0, +1}인지 검증 (AVL 불변식).
    public bool IsBalanced()
    {
        return IsBalanced(root);
    }

    private bool IsBalanced(TreeNode<TKey, TValue> node)
    {
        if (node == null)
        {
            return true;
        }
        if (Mathf.Abs(BalanceFactor(node)) > 1)
        {
            return false;
        }
        return IsBalanced(node.Left) && IsBalanced(node.Right);
    }
}

