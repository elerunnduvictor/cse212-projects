public static class Priority {
    public static void Test() {
        // Test Cases

        // Test 1
        // Scenario: Enqueue three items with different priorities and dequeue them
        // Expected Result: The items dequeued in the order of their priorities
        Console.WriteLine("Test 1");
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item 1", 2);
        priorityQueue.Enqueue("Item 2", 5);
        priorityQueue.Enqueue("Item 3", 3);
        Console.WriteLine("Dequeuing items:");
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 2"
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 3"
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 1"
        Console.WriteLine("---------");

        // Test 2
        // Scenario: Enqueue items with the same highest priority and dequeue them
        // Expected Result: The items dequeued in the order they were enqueued
        Console.WriteLine("Test 2");
        priorityQueue.Enqueue("Item 4", 5);
        priorityQueue.Enqueue("Item 5", 5);
        priorityQueue.Enqueue("Item 6", 5);
        Console.WriteLine("Dequeuing items:");
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 4"
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 5"
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 6"
        Console.WriteLine("---------");

        // Test 3
        // Scenario: Try to dequeue from an empty queue
        // Expected Result: Error message should be displayed
        Console.WriteLine("Test 3");
        Console.WriteLine("Dequeuing from an empty queue:");
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Error message
        Console.WriteLine("---------");

        // Test 4
        // Scenario: Enqueue items with descending priorities and dequeue them
        // Expected Result: The items dequeued in the order of their priorities
        Console.WriteLine("Test 4");
        priorityQueue.Enqueue("Item 7", 10);
        priorityQueue.Enqueue("Item 8", 7);
        priorityQueue.Enqueue("Item 9", 3);
        Console.WriteLine("Dequeuing items:");
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 7"
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 8"
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: "Item 9"
        Console.WriteLine("---------");

    }
}

public class PriorityQueue {
    private SortedDictionary<int, Queue<Item>> priorityQueues = new SortedDictionary<int, Queue<Item>>();

    private class Item {
        public string Data { get; }
        public int Priority { get; }

        public Item(string data, int priority) {
            Data = data;
            Priority = priority;
        }
    }

    public void Enqueue(string data, int priority) {
        if (!priorityQueues.ContainsKey(priority)) {
            priorityQueues[priority] = new Queue<Item>();
        }
        priorityQueues[priority].Enqueue(new Item(data, priority));
    }

    public string Dequeue() {
        if (priorityQueues.Count == 0) {
            Console.WriteLine("Error: Queue is empty.");
            return null;
        }

        var highestPriority = priorityQueues.Keys.Max();
        var queueWithHighestPriority = priorityQueues[highestPriority];
        var highestPriorityItem = queueWithHighestPriority.Dequeue();
        
        if (queueWithHighestPriority.Count == 0) {
            priorityQueues.Remove(highestPriority);
        }

        return highestPriorityItem.Data;
    }
}
