namespace Project2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}

public interface ISortedSet<T> where T : IComparable<T>
{
    int Size();
    void Add(T item);
    bool Remove(T item);
    T Find(T item);
}

public class SortedSet<T> : ISortedSet<T> where T : IComparable<T>
{
    private SortedSet<T> sortedSet;

    public SortedSet()
    {
        sortedSet = new SortedSet<T>();
    }

    public int Size()
    {
        return sortedSet.Count;
    }

    public void Add(T item)
    {
        sortedSet.Add(item);
    }

    public bool Remove(T item)
    {
        return sortedSet.Remove(item);
    }

    public T Find(T item)
    {
        if (sortedSet.Contains(item))
        {
            return item;
        }

        foreach (var element in sortedSet)
        {
            if (element.CompareTo(item) >= 0)
            {
                return element;
            }
        }

        throw new KeyNotFoundException("Item not found in the sorted set.");
    }
}
public interface ITraversable<T>
{
    IEnumerable<T> PreOrder();
    IEnumerable<T> InOrder();
    IEnumerable<T> PostOrder();
}
public class BSTNode<T>
{
    public T value;
    public BSTNode<T> left;
    public BSTNode<T> right;
    public T GetValue { get; set; }
    public BSTNode(T value, BSTNode<T> left, BSTNode<T> right)
    {
        this.value = value;
        this.left = left;
        this.right = right;
    }

    public List<T> PreOrderVisitor()
    {
        List<T> toReturn = new List<T>();

        // PreOrderNonVisitor(new Accumulator<T>(toReturn)); 
        return toReturn;
    }

    public List<T> PreOrderNonVisitor(List<T>? toReturn = null)
    {
        if (toReturn is not null)
        {
            toReturn = new List<T>();
        }

        if (left is not null)

        {
            left.PreOrderNonVisitor(toReturn);
        }
        if (right is not null)

        {
            right.PreOrderNonVisitor(toReturn);
        }

        return toReturn;

    }
    public void Add(T value)
    {

    }

}
public class TreeNode<T>
{
    public T Value { get; set; }
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }

    public TreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BinaryTree<T> : ITraversable<T>
{
    private TreeNode<T> root;

    public BinaryTree(BSTNode<T> rootNode)
    {
        this.rootNode = rootNode;
    }

    public void Add(T value)
    {
        root = AddRec(root, value);
    }

    private TreeNode<T> AddRec(TreeNode<T> node, T value)
    {
        if (node == null)
            return new TreeNode<T>(value);

        int comparisonResult = Comparer<T>.Default.Compare(value, node.Value);

        if (comparisonResult < 0)
            node.Left = AddRec(node.Left, value);
        else if (comparisonResult > 0)
            node.Right = AddRec(node.Right, value);

        return node;
    }

    public IEnumerable<T> PreOrder()
    {
        return PreOrderTraversal(root);
    }

    private IEnumerable<T> PreOrderTraversal(TreeNode<T> node)
    {
        if (node != null)
        {
            yield return node.Value;
            foreach (var item in PreOrderTraversal(node.Left))
                yield return item;
            foreach (var item in PreOrderTraversal(node.Right))
                yield return item;
        }
    }

    public IEnumerable<T> InOrder()
    {
        return InOrderTraversal(root);
    }

    private IEnumerable<T> InOrderTraversal(TreeNode<T> node)
    {
        if (node != null)
        {
            foreach (var item in InOrderTraversal(node.Left))
                yield return item;
            yield return node.Value;
            foreach (var item in InOrderTraversal(node.Right))
                yield return item;
        }
    }

    public IEnumerable<T> PostOrder()
    {
        return PostOrderTraversal(root);
    }

    private IEnumerable<T> PostOrderTraversal(TreeNode<T> node)
    {
        if (node != null)
        {
            foreach (var item in PostOrderTraversal(node.Left))
                yield return item;
            foreach (var item in PostOrderTraversal(node.Right))
                yield return item;
            yield return node.Value;
        }
    }
}
public class Tree<T> : ISortedSet<T>, ITraversable<T> where T : IComparable<T>
{
    private SortedSet<T> sortedSet;

    public Tree()
    {
        sortedSet = new SortedSet<T>();
    }

    public int Size()
    {
        return sortedSet.Count;
    }

    public void Add(T item)
    {
        sortedSet.Add(item);
    }

    public bool Remove(T item)
    {
        return sortedSet.Remove(item);
    }

    public T Find(T item)
    {
        if (sortedSet.Contains(item))
        {
            return item;
        }

        foreach (var element in sortedSet)
        {
            if (element.CompareTo(item) >= 0)
            {
                return element;
            }
        }

        throw new KeyNotFoundException("Item not found in the sorted set.");
    }

    public IEnumerable<T> PreOrder()
    {
        return PreOrderTraversal();
    }

    private IEnumerable<T> PreOrderTraversal()
    {
        foreach (var item in sortedSet)
            yield return item;
    }

    public IEnumerable<T> InOrder()
    {
        return InOrderTraversal();
    }

    private IEnumerable<T> InOrderTraversal()
    {
        foreach (var item in sortedSet)
            yield return item;
    }

    public IEnumerable<T> PostOrder()
    {
        return PostOrderTraversal();
    }

    private IEnumerable<T> PostOrderTraversal()
    {
        foreach (var item in sortedSet)
            yield return item;
    }
}

public static class Spelling
{
    public static List<string> Fix(List<string> inputWords)
    {
        Tree<string> spellingTree = new Tree<string>();
        List<string> outputWords = new List<string>();
        int nextWordInTreeCount = 0;
        int nextWordNotInTreeCount = 0;

        foreach (var word in inputWords)
        {
            if (spellingTree.Find(word).Equals(word)) // Word is in the tree
            {
                nextWordInTreeCount++;
                outputWords.Add(word);

                if (nextWordInTreeCount % 3 == 0)
                {
                    var successor = spellingTree.Find(word);
                    if (!successor.Equals(word))
                        outputWords[outputWords.Count - 1] = successor;
                    spellingTree.Remove(word);
                }
            }
            else // Word is not in the tree
            {
                nextWordNotInTreeCount++;
                outputWords.Add(word);

                if (nextWordNotInTreeCount % 3 == 0)
                {
                    var successor = spellingTree.Find(word);
                    if (!successor.Equals(word))
                        outputWords[outputWords.Count - 1] = successor;
                }

                if (nextWordNotInTreeCount % 3 != 0)
                    spellingTree.Add(word);
            }
        }

        return outputWords;
    }
}