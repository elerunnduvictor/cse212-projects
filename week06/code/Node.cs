public class Node {
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data) {
        this.Data = data;
    }

    public void Insert(int value) {
        if (value < Data) {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

     public bool Contains(int value) {
        if (value == Data) {
            return true;
        }
        else if (value < Data && Left != null) {
            return Left.Contains(value);
        }
        else if (value > Data && Right != null) {
            return Right.Contains(value);
        }
        return false;
    }
  public int GetHeight() {
        return GetHeight(this);
    }

    private static int GetHeight(Node node) {
        if (node == null) {
            return 0;
        }
        int leftHeight = GetHeight(node.Left);
        int rightHeight = GetHeight(node.Right);
        return Math.Max(leftHeight, rightHeight) + 1;
    }
}