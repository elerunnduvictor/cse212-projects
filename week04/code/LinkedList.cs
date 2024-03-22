using System;
using System.Collections;

public class Node {
    public int Data { get; }
    public Node? Next { get; set; }
    public Node? Prev { get; set; }

    public Node(int data) {
        Data = data;
        Next = null;
        Prev = null;
    }
}

public class LinkedList : IEnumerable<int> {
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value) {
        Node newNode = new Node(value);
        if (_head is null) {
            _head = newNode;
            _tail = newNode;
        } else {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value) {
        Node newNode = new Node(value);
        if (_tail is null) {
            _head = newNode;
            _tail = newNode;
        } else {
            _tail.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
        }
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead() {
        if (_head == _tail) {
            _head = null;
            _tail = null;
        } else if (_head is not null) {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail() {
        if (_head == _tail) {
            _head = null;
            _tail = null;
        } else if (_tail is not null) {
            _tail.Prev!.Next = null;
            _tail = _tail.Prev;
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value) {
        Node? curr = _head;
        while (curr is not null) {
            if (curr.Data == value) {
                if (curr == _head) {
                    RemoveHead();
                } else if (curr == _tail) {
                    RemoveTail();
                } else {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value with 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue) {
        Node? curr = _head;
        while (curr is not null) {
            if (curr.Data == oldValue) {
                curr.Data = newValue;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    public IEnumerator<int> GetEnumerator() {
        var curr = _head;
        while (curr is not null) {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable<int> Reverse() {
        var curr = _tail;
        while (curr is not null) {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString() {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public bool HeadAndTailAreNull() {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public bool HeadAndTailAreNotNull() {
        return _head is not null && _tail is not null;
    }

    public void TestLinkedListOperations() {
        LinkedList myList = new LinkedList();
        
        // Test InsertHead
        myList.InsertHead(1);
        myList.InsertHead(2);
        myList.InsertHead(3);
        Console.WriteLine("After InsertHead: " + myList.ToString()); // Expected output: <LinkedList>{3, 2, 1}

        // Test InsertTail
        myList.InsertTail(4);
        myList.InsertTail(5);
        Console.WriteLine("After InsertTail: " + myList.ToString()); // Expected output: <LinkedList>{3, 2, 1, 4, 5}

        // Test RemoveHead
        myList.RemoveHead();
        Console.WriteLine("After RemoveHead: " + myList.ToString()); // Expected output: <LinkedList>{2, 1, 4, 5}

        // Test RemoveTail
        myList.RemoveTail();
        Console.WriteLine("After RemoveTail: " + myList.ToString()); // Expected output: <LinkedList>{2, 1, 4}

        // Test Remove
        myList.Remove(1);
        Console.WriteLine("After Remove: " + myList.ToString()); // Expected output: <LinkedList>{2, 4}

        // Test Replace
        myList.InsertTail(2);
        myList.InsertTail(2);
        myList.Replace(2, 6);
        Console.WriteLine("After Replace: " + myList.ToString()); // Expected output: <LinkedList>{6, 4, 6, 6}

        // Test Reverse
        Console.WriteLine("Reverse:");
        foreach(var item in myList.Reverse()) {
            Console.WriteLine(item);
        }
        // Expected output: 
        // 6
        // 4
        // 6
        // 6
    }

    static void Main(string[] args) {
        LinkedList list = new LinkedList();
        list.TestLinkedListOperations();
    }
}
