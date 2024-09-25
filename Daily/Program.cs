using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Solution
{
    public static void Main()
    {
        Console.WriteLine("llohe".Contains("he").ToString());
    }


    public int GetLucky(string s, int k)
    {
        var strnew = "";
        foreach (var c in s)
        {
            strnew += c - 96;
        }

        var result = 0;
        for (var i = 0; i < k; i++)
        {
            result = 0;
            foreach (var c in strnew)
            {
                result += c - '0';
            }

            strnew = "";
            strnew += result;
        }

        return result;
    }

    public int RobotSim(int[] commands, int[][] obstacles)
    {
        var position = new int[] { 0, 0 };
        byte facing = 0;
        var obstacleSet = new HashSet<(int, int)>();

        var furthestPoint = 0;
        foreach (var obstacle in obstacles)
        {
            obstacleSet.Add((obstacle[0], obstacle[1]));
        }

        for (var i = 0; i < commands.Length; i++)
        {
            switch (commands[i])
            {
            case -2:
                facing = (byte)((facing + 1) % 4);
                break;
            case -1:
                facing = (byte)((facing + 3) % 4);
                break;
            case int j when j > 0 && j < 10:

                for (j = 0; j < commands[i]; j++)
                {
                    var nextX = position[0];
                    var nextY = position[1];
                    switch (facing)
                    {
                    case 0:
                        nextY++;
                        break;
                    case 1:
                        nextX--;
                        break;
                    case 3:
                        nextX++;
                        break;
                    case 2:
                        nextY--;
                        break;
                    }

                    if (obstacleSet.Contains((nextX, nextY)))
                    {
                        break;
                    }

                    position[0] = nextX;
                    position[1] = nextY;
                }

                break;
            }

            var newPoint = position[0] * position[0] + position[1] * position[1];
            if (newPoint > furthestPoint)
            {
                furthestPoint = newPoint;
            }
        }

        return furthestPoint;
    }

    public int[] MissingRolls(int[] rolls, int mean, int n)
    {
        var sum = 0;
        foreach (var roll in rolls)
        {
            sum += roll;
        }

        var remainingSum = mean * (n + rolls.Length) - sum;
        if (remainingSum > 6 * n || remainingSum < n)
        {
            return new int[] { };
        }

        var distributeMean = remainingSum / n;
        var mod = remainingSum % n;
        var elements = new int[n];
        Array.Fill(elements, distributeMean);
        for (var i = 0; i < mod; i++)
        {
            elements[i]++;
        }

        return elements;
    }

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public ListNode ModifiedList(int[] nums, ListNode head)
    {
        var numSet = new HashSet<int>(nums);
        var dummy = new ListNode(0, head);
        var current = dummy;

        while (current.next != null)
        {
            if (numSet.Contains(current.next.val))
            {
                current.next = current.next.next;
            }
            else
            {
                current = current.next;
            }
        }

        return dummy.next;
    }

    public bool IsSubPath(ListNode head, TreeNode root)
    {
        bool DFS(ListNode head, TreeNode root)
        {
            if (head == null)
            {
                return true;
            }

            if (root == null)
            {
                return false;
            }

            if (root.val != head.val)
            {
                return false;
            }

            return DFS(head.next, root.left) ||
                   DFS(head.next, root.right);
        }

        if (root == null)
        {
            return false;
        }

        return DFS(head, root) || IsSubPath(head, root.left) || IsSubPath(head, root.right);
    }

    public ListNode[] SplitListToParts(ListNode head, int k)
    {
        var cur = head;
        var count = 0;
        while (cur != null)
        {
            count++;
            cur = cur.next;
        }

        var size = count / k;
        var extra = count % k;
        ListNode[] result = new ListNode[k];
        cur = head;
        for (var i = 0; i < k && cur != null; i++)
        {
            result[i] = cur;
            var ext = size + (i < extra ? 1 : 0);
            for (var j = 1; j < ext; j++)
            {
                cur = cur.next;
            }

            var next = cur.next;
            cur.next = null;
            cur = next;
        }

        return result;
    }

    public string ShortestPalindrome(string s)
    {
        int[] MakeKMP(string s)
        {
            var table = new int[s.Length];
            var j = 0;
            for (var i = 0; i < s.Length; i++)
            {
                while (j > 0 && s[i] != s[j])
                {
                    j = table[j - 1];
                }

                if (s[i] == s[j])
                {
                    j++;
                }

                table[i] = j;
            }

            return table;
        }

        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        var reversed = new string(s.Reverse().ToArray());
        var combined = s + "#" + reversed;
        var kmpTable = MakeKMP(combined);
        var charactersToAdd = s.Length - kmpTable[combined.Length - 1];

        return reversed.Substring(0, charactersToAdd) + s;
    }

    public int[][] SpiralMatrix(int m, int n, ListNode head)
    {
        void assignarr(ref int arr, ListNode head)
        {
            if (head != null)
            {
                arr = head.val;
                head = head.next;
            }
        }

        int[][] array = new int[m][];
        for (var i = 0; i < m; i++)
        {
            array[i] = new int[n];
            for (var j = 0; j < n; j++)
            {
                array[i][j] = -1;
            }
        }

        int top = 0, bottom = m - 1, left = 0, right = n - 1;
        while (top <= bottom && left <= right)
        {
            for (var i = left; i <= right; i++)
            {
                assignarr(ref array[top][i], head);
            }

            top++;

            for (var i = top; i <= bottom; i++)
            {
                assignarr(ref array[i][right], head);
            }

            right--;

            if (top <= bottom)
            {
                for (var i = right; i >= left; i--)
                {
                    assignarr(ref array[bottom][i], head);
                }

                bottom--;
            }

            if (left <= right)
            {
                for (var i = bottom; i >= top; i--)
                {
                    assignarr(ref array[i][left], head);
                }

                left++;
            }
        }

        return array;
    }

    public ListNode InsertGreatestCommonDivisors(ListNode head)
    {
        int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a | b;
        }

        if (head.next == null)
        {
            return head;
        }

        var prev = head;
        var next = head.next;
        while (next != null)
        {
            var GCDNode = new ListNode(GCD(prev.val, next.val), next);
            prev.next = GCDNode;
            prev = next;
            next = next.next;
        }

        return head;
    }

    public int MinBitFlips(int start, int goal)
    {
        var strstart = Convert.ToString(start, 2);
        var strgoal = Convert.ToString(goal, 2);
        if (strstart.Length < strgoal.Length)
        {
            strstart = string.Concat(Enumerable.Repeat("0", strgoal.Length - strstart.Length)) + strstart;
        }
        else if (strstart.Length > strgoal.Length)
        {
            strgoal = string.Concat(Enumerable.Repeat("0", strstart.Length - strgoal.Length)) + strgoal;
        }

        var counter = 0;
        for (var i = 0; i < strstart.Length; i++)
        {
            if (strstart[i] != strgoal[i])
            {
                counter++;
            }
        }

        return counter;
    }

    public int CountConsistentStrings(string allowed, string[] words)
    {
        HashSet<char> chars = [..allowed];


        var count = 0;
        foreach (var s in words)
        {
            var isGood = true;
            for (var i = 0; i < s.Length; i++)
            {
                if (!chars.Contains(s[i]))
                {
                    isGood = false;
                }
            }

            if (isGood)
            {
                count++;
            }
        }

        return count;
    }

    public int[] XorQueries(int[] arr, int[][] queries)
    {
        var n = arr.Length;
        var prefixXOR = new int[n];
        prefixXOR[0] = arr[0];

        for (var i = 1; i < n; i++)
        {
            prefixXOR[i] = prefixXOR[i - 1] ^ arr[i];
        }

        var answer = new int[queries.Length];

        for (var i = 0; i < queries.Length; i++)
        {
            var left = queries[i][0];
            var right = queries[i][1];

            if (left == 0)
            {
                answer[i] = prefixXOR[right];
            }
            else
            {
                answer[i] = prefixXOR[right] ^ prefixXOR[left - 1];
            }
        }

        return answer;
    }

    public int FindMinDifference(IList<string> timePoints)
    {
        var minutes = new List<int>();

        foreach (var time in timePoints)
        {
            string[] split = time.Split(':');
            minutes.Add(int.Parse(split[0]) * 60 + int.Parse(split[1]));
        }

        minutes.Sort();

        var minDifference = int.MaxValue;

        for (var i = 1; i < minutes.Count; i++)
        {
            minDifference = Math.Min(minDifference, minutes[i] - minutes[i - 1]);
        }

        var circularDifference = 1440 - minutes[minutes.Count - 1] + minutes[0];
        minDifference = Math.Min(minDifference, circularDifference);

        return minDifference;
    }

    public string[] UncommonFromSentences(string s1, string s2)
    {
        string[] ss1 = s1.Split(' ');
        string[] ss2 = s2.Split(' ');
        List<string> list = new();
        foreach (var ss in ss1)
        {
            if (ss1.Count(x => x == ss) == 1 && !ss2.Contains(ss))
            {
                list.Add(ss);
            }
        }

        foreach (var ss in ss2)
        {
            if (ss2.Count(x => x == ss) == 1 && !ss1.Contains(ss))
            {
                list.Add(ss);
            }
        }


        return list.ToArray();
    }

    public string LargestNumber(int[] nums)
    {
        Array.Sort(nums, (a, b) =>
            StringComparer.Ordinal.Compare(b.ToString() + a.ToString()
                , a.ToString() + b.ToString()));
        if (nums[0] == 0)
        {
            return "0";
        }

        return string.Concat(nums);
    }

    public IList<int> DiffWaysToCompute(string expression)
    {
        IList<int> list = new List<int>();

        for (var i = 0; i < expression.Length; i++)
        {
            var oper = expression[i];
            if (oper == '+' || oper == '-' || oper == '*')
            {
                var ilist1 = DiffWaysToCompute(expression.Substring(0, i));
                var ilist2 = DiffWaysToCompute(expression.Substring(i + 1));
                foreach (var a in ilist1)
                {
                    foreach (var b in ilist2)
                    {
                        if (oper == '+')
                        {
                            list.Add(a + b);
                        }
                        else if (oper == '-')
                        {
                            list.Add(a - b);
                        }
                        else if (oper == '*')
                        {
                            list.Add(a * b);
                        }
                    }
                }
            }
        }

        if (list.Count == 0)
        {
            list.Add(int.Parse(expression));
        }

        return list;
    }

    public IList<int> LexicalOrder(int n)
    {
        var result = new List<int>();
        var current = 1;

        for (var i = 0; i < n; i++)
        {
            result.Add(current);

            // Try to go deeper (multiply by 10)
            if (current * 10 <= n)
            {
                current *= 10;
            }
            // Increment current, or handle boundary and end digits
            else if (current % 10 != 9 && current + 1 <= n)
            {
                current++;
            }
            // Backtrack until we can increment to next valid number
            else
            {
                while (current / 10 % 10 == 9)
                {
                    current /= 10;
                }

                current = current / 10 + 1;
            }
        }

        return result;
    }

    public int MinExtraChar(string s, string[] dictionary) {
        var sentenceLength = s.Length;
        var dp = new List<int>() {0};
        for (var i = 0; i < sentenceLength; ++i)
        {
            var match = dp[i];
            foreach (var word in dictionary)
            {
                var wordLength = word.Length;
                var offset = i - wordLength + 1;
                var maxLength = Math.Min(wordLength, sentenceLength - offset);

                if ( offset >= 0&& s.Substring(offset, maxLength) == word ) // if match
                {
                    match = Math.Max(match, dp[offset] + wordLength);
                }
            }
            dp.Add(match);
        }

        return sentenceLength - dp[^1];
    }

   

    public int LongestCommonPrefix(int[] arr1, int[] arr2) {
       
        HashSet<int> prefixes = [];
for (var i = 0; i < arr1.Length; i++){
    var num = arr1[i];
    while (num > 0){
        prefixes.Add(num);
        num/=10;
    }
}
var max = 0;
for (var i =0 ; i < arr2.Length; i++){
    var num = arr2[i];
    while (num > 0 && !prefixes.Contains(num)){
        num/=10;
    }
    if (prefixes.Contains(num)) max = int.Max(max, num.ToString().Length);
}
return max;
    }

    public class TrieNode {
        public Dictionary<char, TrieNode> Children;
        public int Count; // keeps track of how many words share this prefix
        
        public TrieNode() {
            Children = new Dictionary<char, TrieNode>();
            Count = 0;
        }
    }

    private void Insert(TrieNode root, string word) {
        TrieNode currentNode = root;
        foreach (char c in word) {
            if (!currentNode.Children.ContainsKey(c)) {
                currentNode.Children[c] = new TrieNode();
            }
            currentNode = currentNode.Children[c];
            currentNode.Count++;  // increase the prefix count
        }
    }

    private int GetScore(TrieNode root, string word) {
        TrieNode currentNode = root;
        int score = 0;
        foreach (char c in word) {
            currentNode = currentNode.Children[c];
            score += currentNode.Count;
        }
        return score;
    }
    public int[] SumPrefixScores(string[] words) {
        TrieNode root = new TrieNode();
        
        // Step 1: Insert all words into the Trie
        foreach (string word in words) {
            Insert(root, word);
        }
        
        // Step 2: Calculate the score for each word
        int[] result = new int[words.Length];
        for (int i = 0; i < words.Length; i++) {
            result[i] = GetScore(root, words[i]);
        }
        
        return result;
    }
}