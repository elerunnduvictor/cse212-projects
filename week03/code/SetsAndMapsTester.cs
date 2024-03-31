using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class SetsAndMapsTester
{
    public static void Run()
    {
        // Problem 1: Find Pairs with Sets
        Console.WriteLine("\n=========== Finding Pairs TESTS ===========");
        DisplayPairs(new[] { "am", "at", "ma", "if", "fi" });
        // ma & am
        // fi & if
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "bc", "cd", "de", "ba" });
        // ba & ab
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ba", "ac", "ad", "da", "ca" });
        // ba & ab
        // da & ad
        // ca & ac
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ac" }); // No pairs displayed
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "aa", "ba" });
        // ba & ab
        Console.WriteLine("---------");
        DisplayPairs(new[] { "23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14" });
        // 32 & 23
        // 94 & 49
        // 31 & 13

        // Problem 2: Degree Summary
        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== Census TESTS ===========");
        var degreeSummary = SummarizeDegrees("census.txt");
        foreach (var kvp in degreeSummary)
        {
            Console.WriteLine($"[{kvp.Key}, {kvp.Value}]");
        }

        // Problem 3: Anagrams
        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== Anagram TESTS ===========");
        Console.WriteLine(IsAnagram("CAT", "ACT")); // true
        Console.WriteLine(IsAnagram("DOG", "GOOD")); // false
        Console.WriteLine(IsAnagram("AABBCCDD", "ABCD")); // false
        Console.WriteLine(IsAnagram("ABCCD", "ABBCD")); // false
        Console.WriteLine(IsAnagram("BC", "AD")); // false
        Console.WriteLine(IsAnagram("Ab", "Ba")); // true
        Console.WriteLine(IsAnagram("A Decimal Point", "Im a Dot in Place")); // true
        Console.WriteLine(IsAnagram("tom marvolo riddle", "i am lord voldemort")); // true
        Console.WriteLine(IsAnagram("Eleven plus Two", "Twelve Plus One")); // true
        Console.WriteLine(IsAnagram("Eleven plus One", "Twelve Plus One")); // false

        // Problem 4: Maze
        Console.WriteLine("\n=========== Maze TESTS ===========");
        Dictionary<(int, int), bool[]> map = SetupMazeMap();
        var maze = new Maze(map);
        maze.ShowStatus(); // Should be at (1,1)
        maze.MoveUp(); // Error
        maze.MoveLeft(); // Error
        maze.MoveRight();
        maze.MoveRight(); // Error
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.MoveRight();
        maze.MoveUp();
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveLeft();
        maze.MoveDown(); // Error
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.ShowStatus(); // Should be at (6,6)

        // Problem 5: Earthquake
        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== Earthquake TESTS ===========");
        EarthquakeDailySummary().Wait();
    }

    private static void DisplayPairs(string[] words)
    {
        var seenWords = new HashSet<string>();

        foreach (var word in words)
        {
            var reverse = ReverseString(word);
            if (seenWords.Contains(reverse))
            {
                Console.WriteLine($"{word} & {reverse}");
            }
            else
            {
                seenWords.Add(word);
            }
        }
    }

    private static string ReverseString(string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length >= 4)
            {
                var degree = fields[3].Trim();
                if (!string.IsNullOrEmpty(degree))
                {
                    if (degrees.ContainsKey(degree))
                    {
                        degrees[degree]++;
                    }
                    else
                    {
                        degrees[degree] = 1;
                    }
                }
            }
        }

        return degrees;
    }

    private static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // Create dictionaries to store character frequencies
        var freq1 = new Dictionary<char, int>();
        var freq2 = new Dictionary<char, int>();

        // Update frequencies for word1
        foreach (char c in word1)
        {
            if (freq1.ContainsKey(c))
            {
                freq1[c]++;
            }
            else
            {
                freq1[c] = 1;
            }
        }

        // Update frequencies for word2
        foreach (char c in word2)
        {
            if (freq2.ContainsKey(c))
            {
                freq2[c]++;
            }
            else
            {
                freq2[c] = 1;
            }
        }

        // Compare the dictionaries
        return AreDictionariesEqual(freq1, freq2);
    }

    // Helper function to check if two dictionaries are equal
    private static bool AreDictionariesEqual(Dictionary<char, int> dict1, Dictionary<char, int> dict2)
    {
        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        foreach (var kvp in dict1)
        {
            if (!dict2.ContainsKey(kvp.Key) || dict2[kvp.Key] != kvp.Value)
            {
                return false;
            }
        }

        return true;
    }

    public class Maze
    {
        private readonly Dictionary<(int, int), bool[]> _map;
        private int _currentX;
        private int _currentY;

        public Maze(Dictionary<(int, int), bool[]> map)
        {
            _map = map;
            _currentX = 1; // Initial position
            _currentY = 1;
        }

        public void MoveLeft()
        {
            if (_currentX > 1 && _map[(_currentX, _currentY)][0])
            {
                _currentX--;
                Console.WriteLine($"Moved left to ({_currentX},{_currentY})");
            }
            else
            {
                Console.WriteLine("Cannot move left. Hit a wall or out of bounds.");
            }
        }

        public void MoveRight()
        {
            if (_currentX < 6 && _map[(_currentX, _currentY)][1])
            {
                _currentX++;
                Console.WriteLine($"Moved right to ({_currentX},{_currentY})");
            }
            else
            {
                Console.WriteLine("Cannot move right. Hit a wall or out of bounds.");
            }
        }

        public void MoveUp()
        {
            if (_currentY > 1 && _map[(_currentX, _currentY)][2])
            {
                _currentY--;
                Console.WriteLine($"Moved up to ({_currentX},{_currentY})");
            }
            else
            {
                Console.WriteLine("Cannot move up. Hit a wall or out of bounds.");
            }
        }

        public void MoveDown()
        {
            if (_currentY < 6 && _map[(_currentX, _currentY)][3])
            {
                _currentY++;
                Console.WriteLine($"Moved down to ({_currentX},{_currentY})");
            }
            else
            {
                Console.WriteLine("Cannot move down. Hit a wall or out of bounds.");
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Current position: ({_currentX},{_currentY})");
        }
    }

    private static Dictionary<(int, int), bool[]> SetupMazeMap()
    {
        Dictionary<(int, int), bool[]> map = new()
        {
            { (1, 1), new[] { false, true, false, true } },
            { (1, 2), new[] { false, true, true, false } },
            { (1, 3), new[] { false, false, false, false } },
            { (1, 4), new[] { false, true, false, true } },
            { (1, 5), new[] { false, false, true, true } },
            { (1, 6), new[] { false, false, true, false } },
            { (2, 1), new[] { true, false, false, true } },
            { (2, 2), new[] { true, false, true, true } },
            { (2, 3), new[] { false, false, true, true } },
            { (2, 4), new[] { true, true, true, false } },
            { (2, 5), new[] { false, false, false, false } },
            { (2, 6), new[] { false, false, false, false } },
            { (3, 1), new[] { false, false, false, false } },
            { (3, 2), new[] { false, false, false, false } },
            { (3, 3), new[] { false, false, false, false } },
            { (3, 4), new[] { true, true, false, true } },
            { (3, 5), new[] { false, false, true, true } },
            { (3, 6), new[] { false, false, true, false } },
            { (4, 1), new[] { false, true, false, false } },
            { (4, 2), new[] { false, false, false, false } },
            { (4, 3), new[] { false, true, false, true } },
            { (4, 4), new[] { true, true, true, false } },
            { (4, 5), new[] { false, false, false, false } },
            { (4, 6), new[] { false, false, false, false } },
            { (5, 1), new[] { true, true, false, true } },
            { (5, 2), new[] { false, false, true, true } },
            { (5, 3), new[] { true, true, true, true } },
            { (5, 4), new[] { true, false, true, true } },
            { (5, 5), new[] { false, false, true, true } },
            { (5, 6), new[] { false, true, true, false } },
            { (6, 1), new[] { true, false, false, false } },
            { (6, 2), new[] { false, false, false, false } },
            { (6, 3), new[] { true, false, false, false } },
            { (6, 4), new[] { false, false, false, false } },
            { (6, 5), new[] { false, false, false, false } },
            { (6, 6), new[] { true, false, false, false } }
        };
        return map;
    }

    private static async Task EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

            if (featureCollection?.Features != null)
            {
                foreach (var feature in featureCollection.Features)
                {
                    Console.WriteLine($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
                }
            }
            else
            {
                Console.WriteLine("No earthquake data found.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to fetch earthquake data. Status code: {response.StatusCode}");
        }
    }
}

public class FeatureCollection
{
    public Feature[] Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double Mag { get; set; }
}
