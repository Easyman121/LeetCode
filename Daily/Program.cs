﻿using System.Text;

public class Solution
{
    public static void Main()
    {
        Solution solution = new Solution();
        solution.CountFairPairs(new int[]{0, 1, 7, 4, 4, 5}, 3, 6);
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

    public int MinExtraChar(string s, string[] dictionary)
    {
        var sentenceLength = s.Length;
        var dp = new List<int>() { 0 };
        for (var i = 0; i < sentenceLength; ++i)
        {
            var match = dp[i];
            foreach (var word in dictionary)
            {
                var wordLength = word.Length;
                var offset = i - wordLength + 1;
                var maxLength = Math.Min(wordLength, sentenceLength - offset);

                if (offset >= 0 && s.Substring(offset, maxLength) == word) // if match
                {
                    match = Math.Max(match, dp[offset] + wordLength);
                }
            }

            dp.Add(match);
        }

        return sentenceLength - dp[^1];
    }


    public int LongestCommonPrefix(int[] arr1, int[] arr2)
    {
        HashSet<int> prefixes = [];
        for (var i = 0; i < arr1.Length; i++)
        {
            var num = arr1[i];
            while (num > 0)
            {
                prefixes.Add(num);
                num /= 10;
            }
        }

        var max = 0;
        for (var i = 0; i < arr2.Length; i++)
        {
            var num = arr2[i];
            while (num > 0 && !prefixes.Contains(num))
            {
                num /= 10;
            }

            if (prefixes.Contains(num))
            {
                max = int.Max(max, num.ToString().Length);
            }
        }

        return max;
    }

    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children;
        public int Count; // keeps track of how many words share this prefix

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
            Count = 0;
        }
    }

    private void Insert(TrieNode root, string word)
    {
        var currentNode = root;
        foreach (var c in word)
        {
            if (!currentNode.Children.ContainsKey(c))
            {
                currentNode.Children[c] = new TrieNode();
            }

            currentNode = currentNode.Children[c];
            currentNode.Count++; // increase the prefix count
        }
    }

    private int GetScore(TrieNode root, string word)
    {
        var currentNode = root;
        var score = 0;
        foreach (var c in word)
        {
            currentNode = currentNode.Children[c];
            score += currentNode.Count;
        }

        return score;
    }

    public int[] SumPrefixScores(string[] words)
    {
        var root = new TrieNode();

        // Step 1: Insert all words into the Trie
        foreach (var word in words)
        {
            Insert(root, word);
        }

        // Step 2: Calculate the score for each word
        var result = new int[words.Length];
        for (var i = 0; i < words.Length; i++)
        {
            result[i] = GetScore(root, words[i]);
        }

        return result;
    }

    public bool CanArrange(int[] arr, int k)
    {
        var size = arr.Length;
        if (size % 2 != 0)
        {
            return false;
        }

        var freq = new int[k];

        foreach (var num in arr)
        {
            var remainder = num % k;
            if (remainder < 0)
            {
                remainder += k;
            }

            freq[remainder]++;
        }

        if (freq[0] % 2 != 0)
        {
            return false;
        }

        for (var i = 1; i <= k / 2; i++)
        {
            if (i == k - i)
            {
                if (freq[i] % 2 != 0)
                {
                    return false;
                }
            }
            else
            {
                if (freq[i] != freq[k - i])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public long DividePlayers(int[] skill)
    {
        Array.Sort(skill);
        var slen = skill.Length;
        var temp = skill[0] + skill[slen - 1];
        long result = 0;
        for (var i = 0; i < slen / 2; i++)
        {
            var a = skill[i] + skill[slen - 1 - i];
            if (a != temp)
            {
                return -1;
            }

            result += skill[i] * skill[slen - 1 - i];
            temp = a;
        }

        return result;
    }

    public int[] ArrayRankTransform(int[] arr)
    {
        if (arr.Length == 0)
        {
            return new int[] { };
        }

        var el = arr.Distinct().OrderBy(x => x).ToArray();
        var dict = new Dictionary<int, int>();
        for (var i = 0; i < el.Length; i++)
        {
            dict[el[i]] = i + 1;
        }

        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] = dict[arr[i]];
        }

        return arr;
    }

    public int MinSwaps(string s)
    {
        var open = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '[')
            {
                open++;
            }
            else if (open > 0)
            {
                open--;
            }
        }

        return (open + 1) / 2;
    }


    public bool CheckInclusion(string s1, string s2)
    {
        if (s1.Length > s2.Length)
        {
            return false;
        }

        var s1count = new int[26];
        var s2count = new int[26];
        for (var i = 0; i < s1.Length; i++)
        {
            s1count[s1[i] - 'a']++;
            s2count[s2[i] - 'a']++;
        }

        for (var i = 0; i < s2.Length - s1.Length; i++)
        {
            if (matches(s1count, s2count))
            {
                return true;
            }

            // Update the window
            s2count[s2[i] - 'a']--;
            s2count[s2[i + s1.Length] - 'a']++;
        }

        return matches(s1count, s2count);

        bool matches(int[] s1Count, int[] s2Count)
        {
            for (var i = 0; i < 26; i++)
            {
                if (s1Count[i] != s2Count[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public bool AreSentencesSimilar(string sentence1, string sentence2)
    {
        if (sentence1 == sentence2)
        {
            return true;
        }

        string[] split1 = sentence1.Split(' '), split2 = sentence2.Split(' ');

        int n = split1.Length, m = split2.Length;
        var min = Math.Min(n, m);

        var prefixLength = 0;
        while (prefixLength < min && split1[prefixLength] == split2[prefixLength])
        {
            prefixLength++;
        }

        var suffixLength = 0;
        while (suffixLength < min - prefixLength && split1[n - suffixLength - 1] == split2[m - suffixLength - 1])
        {
            suffixLength++;
        }

        return prefixLength + suffixLength >= n || prefixLength + suffixLength >= m;
    }

    public int MinLength(string s)
    {
        var stack = new Stack<char>();

        foreach (var c in s)
        {
            if (stack.Count > 0)
            {
                var top = stack.Peek();
                if ((top == 'A' && c == 'B') || (top == 'C' && c == 'D'))
                {
                    stack.Pop();
                    continue;
                }
            }

            stack.Push(c);
        }

        return stack.Count;
    }

    public int MinAddToMakeValid(string s)
    {
        int l = 0, miss = 0;
        foreach (var c in s)
        {
            if (c == '(')
            {
                l++;
            }
            else
            {
                if (l > 0)
                {
                    l--;
                }
                else
                {
                    miss++;
                }
            }
        }

        return l + miss;
    }

    public int MaxWidthRamp(int[] nums)
    {
        var n = nums.Length;
        var stack = new Stack<int>();
        for (var i = 0; i < n; i++)
        {
            if (stack.Count == 0 || nums[stack.Peek()] > nums[i])
            {
                stack.Push(i);
            }
        }

        var width = 0;
        for (var i = n - 1; i > 0; i--)
        {
            if (stack.Count == 0)
            {
                break;
            }

            while (stack.Count > 0 && nums[stack.Peek()] <= nums[i])
            {
                width = Math.Max(width, i - stack.Pop());
            }
        }

        return width;
    }

    public int SmallestChair(int[][] times, int targetFriend)
    {
        var targetTime = times[targetFriend];

        Array.Sort(times, (a, b) => a[0].CompareTo(b[0]));

        var chairsQueue = new PriorityQueue<int, int>();
        var freeChairs = new SortedSet<int>();

        var lastUnoccupiedChair = 0;
        var chair = -1;

        for (var i = 0; i < times.Length; i++)
        {
            var leaveTime = -1;

            while (chairsQueue.TryPeek(out chair, out leaveTime) && leaveTime <= times[i][0])
            {
                freeChairs.Add(chairsQueue.Dequeue());
            }

            if (freeChairs.Count > 0)
            {
                chair = freeChairs.Min;
                freeChairs.Remove(chair);
            }
            else
            {
                chair = lastUnoccupiedChair++;
            }

            if (times[i] == targetTime)
            {
                break;
            }

            if (times[i][1] <= targetTime[0])
            {
                chairsQueue.Enqueue(chair, times[i][1]);
            }
        }

        return chair;
    }

    public int MinGroups(int[][] intervals)
    {
        var max = intervals.Max(x => x[1]);

        var line = new int[max + 2];
        foreach (var i in intervals)
        {
            line[i[0]]++;
            line[i[1] + 1]--;
        }

        var overlap = 0;
        max = 0;
        for (var i = 0; i < line.Length; i++)
        {
            overlap += line[i];
            max = Math.Max(max, overlap);
        }

        return max;
    }


    public long MaxKelements(int[] nums, int k)
    {
        var maxHeap = new PriorityQueue<int, int>();
        foreach (var num in nums)
        {
            maxHeap.Enqueue(num, -num);
        }

        long score = 0;
        for (var i = 0; i < k; i++)
        {
            var largest = maxHeap.Dequeue();
            score += largest;
            var reduced = (int)Math.Ceiling(largest / 3D);
            maxHeap.Enqueue(reduced, -reduced);
        }

        return score;
    }

    public long MinimumSteps(string s)
    {
        var pos = 0;
        long swap = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '0')
            {
                swap += i - pos;

                pos++;
            }
        }

        return swap;
    }

    public int[] SmallestRange(IList<IList<int>> nums)
    {
        var elements = new List<(int value, int listIndex)>();

        for (var i = 0; i < nums.Count; i++)
        {
            foreach (var num in nums[i])
            {
                elements.Add((num, i));
            }
        }

        elements.Sort((a, b) => a.value.CompareTo(b.value));

        var count = new int[nums.Count];
        var totalCovered = 0;
        var left = 0;
        var minRange = int.MaxValue;
        int start = 0, end = 0;

        for (var right = 0; right < elements.Count; right++)
        {
            var listIndex = elements[right].listIndex;
            count[listIndex]++;

            if (count[listIndex] == 1)
            {
                totalCovered++;
            }

            while (totalCovered == nums.Count)
            {
                var currentRange = elements[right].value - elements[left].value;

                if (currentRange < minRange)
                {
                    minRange = currentRange;
                    start = elements[left].value;
                    end = elements[right].value;
                }

                var leftListIndex = elements[left].listIndex;
                count[leftListIndex]--;

                if (count[leftListIndex] == 0)
                {
                    totalCovered--;
                }

                left++;
            }
        }

        return new int[] { start, end };
    }

    public string LongestDiverseString(int a, int b, int c)
    {
        int maxLength = a + b + c, i = 0;
        var chars = new List<char>();
        int currA, currB, currC;
        currA = currB = currC = 0;
        while (true)
        {
            if ((currA != 2 && a >= b && a >= c) || (a > 0 && (currB == 2 || currC == 2)))
            {
                chars.Add('a');
                a--;
                currA++;
                currB = currC = 0;
            }
            else if ((currB != 2 && b >= c && b >= a) || (b > 0 && (currA == 2 || currC == 2)))
            {
                chars.Add('b');
                b--;
                currB++;
                currA = currC = 0;
            }
            else if ((currC != 2 && c >= a && c >= b) || (c > 0 && (currB == 2 || currA == 2)))
            {
                chars.Add('c');
                c--;
                currC++;
                currA = currB = 0;
            }
            else
            {
                break;
            }
        }

        return string.Join("", chars);
    }

    public int MaximumSwap(int num)
    {
        var s = num.ToString();
        var st = s.ToCharArray();
        int res = num, n = s.Length;
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                (st[i], st[j]) = (st[j], st[i]);
                var x = int.Parse(st);
                res = Math.Max(res, x);
                st = s.ToCharArray();
            }
        }

        return res;
    }

    public int MaxUniqueSplit(string s)
    {
        int DFS(string s, int start, HashSet<string> unique)
        {
            if (start == s.Length)
            {
                return 0;
            }

            var max = 0;
            for (var i = start + 1; i <= s.Length; i++)
            {
                var curr = s.Substring(start, i - start);

                if (!unique.Contains(curr))
                {
                    unique.Add(curr);
                    max = Math.Max(max, 1 + DFS(s, i, unique));
                    unique.Remove(curr);
                }
            }

            return max;
        }

        var unique = new HashSet<string>();
        return DFS(s, 0, unique);
    }

    public int MaximumSwap2(int num)
    {
        var s = num.ToString();
        var st = s.ToCharArray();
        int swapcount = num, n = s.Length;
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                (st[i], st[j]) = (st[j], st[i]);
                var x = int.Parse(st);
                swapcount = Math.Max(swapcount, x);
                st = s.ToCharArray();
            }
        }

        return swapcount;
    }

    public int CountMaxOrSubsets(int[] nums)
    {
        var max = nums[0];
        for (var i = 1; i < nums.Length; i++)
        {
            max = max | nums[i];
        }

        var count = 0;
        for (var i = 1; i < 1 << nums.Length; i++)
        {
            var current = 0;
            for (var j = 0; j < nums.Length; j++)
            {
                if ((i & (1 << j)) != 0)
                {
                    current |= nums[j];
                }
            }

            if (current == max)
            {
                count++;
            }
        }

        return count;
    }

    public char FindKthBit(int n, int k)
    {
        string invert(string a)
        {
            var b = a.ToCharArray();
            for (var i = 0; i < b.Length; i++)
            {
                if (b[i] == '1')
                {
                    b[i] = '0';
                }
                else
                {
                    b[i] = '1';
                }
            }

            return new string(b);
        }

        string reverse(string a)
        {
            var charArray = a.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        var iteration = "0";

        for (var i = 1; i < n; i++)
        {
            iteration = iteration + "1" + reverse(invert(iteration));
        }

        return iteration[k - 1];
    }

    public bool ParseBoolExpr(string expression)
    {
        bool Parse(string expression, bool and)
        {
            int n = expression.Length, j = 0, par = 0;
            var sub = "";
            while (j < n)
            {
                if (expression[j] == ',' && par == 0)
                {
                    var temp = ParseBoolExpr(sub);
                    if (and)
                    {
                        if (!temp)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (temp)
                        {
                            return true;
                        }
                    }

                    sub = "";
                    j++;
                    continue;
                }

                if (expression[j] == '(')
                {
                    par++;
                }
                else if (expression[j] == ')')
                {
                    par--;
                }

                sub += expression[j++];
            }

            return ParseBoolExpr(sub);
        }


        var n = expression.Length;
        var ch = expression[0];
        if (n == 1)
        {
            return ch == 't';
        }

        if (ch == '!')
        {
            return !ParseBoolExpr(expression.Substring(2, n - 3));
        }

        if (ch == '&')
        {
            return Parse(expression.Substring(2, n - 3), true);
        }

        if (ch == '|')
        {
            return Parse(expression.Substring(2, n - 3), false);
        }

        return false;
    }


    public long KthLargestLevelSum(TreeNode root, int k)
    {
        /*
        List<int> sums = new List<int>();
        void Walk(int level, TreeNode node){
            if (sums.Count <= level){
                sums.Add(0);
            }
            sums[level] += node.val;
            if (node.left != null) Walk(level+1, node.left);
            if (node.right != null) Walk(level+1, node.right);

        }

        Walk(0, root);
        if (sums.Count < k) return -1;
        else {
            sums.OrderByDescending(x => x);
            return sums[k-1];

        }*/
        long returned = 0;
        var maxHeap = new PriorityQueue<long, long>(Comparer<long>.Create((x, y) => y.CompareTo(x)));
        if (root == null)
        {
            return -1;
        }

        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            long levelSize = queue.Count;
            long currSum = 0;

            for (var i = 0; i < levelSize; i++)
            {
                var currNode = queue.Dequeue();
                currSum += currNode.val;

                if (currNode.left != null)
                {
                    queue.Enqueue(currNode.left);
                }


                if (currNode.right != null)
                {
                    queue.Enqueue(currNode.right);
                }
            }


            maxHeap.Enqueue(currSum, currSum);
        }

        for (var i = 0; i < k; i++)
        {
            if (maxHeap.Count != 0)
            {
                returned = maxHeap.Dequeue();
            }
            else
            {
                return -1;
            }
        }

        return returned;
    }


    public TreeNode ReplaceValueInTree(TreeNode root)
    {
        var depthSum = new List<int>();

        void Fin(TreeNode root, int val, int d)
        {
            if (root == null)
            {
                return;
            }

            root.val = val;

            var c = d + 1 < depthSum.Count ? depthSum[d + 1] : 0;

            c -= root.left != null ? root.left.val : 0;
            c -= root.right != null ? root.right.val : 0;

            if (root.left != null)
            {
                Fin(root.left, c, d + 1);
            }

            if (root.right != null)
            {
                Fin(root.right, c, d + 1);
            }
        }

        void Walk(TreeNode root, int d)
        {
            if (root == null)
            {
                return;
            }

            if (d >= depthSum.Count)
            {
                depthSum.Add(root.val);
            }
            else
            {
                depthSum[d] += root.val;
            }

            Walk(root.left, d + 1);
            Walk(root.right, d + 1);
        }

        Walk(root, 0);
        Fin(root, 0, 0);
        return root;
    }

    public bool FlipEquiv(TreeNode root1, TreeNode root2)
    {
        if (root1 == null && root2 == null)
        {
            return true;
        }

        if (root1 == null || root2 == null || root1.val != root2.val)
        {
            return false;
        }

        var noFlip = FlipEquiv(root1.left, root2.left) && FlipEquiv(root1.right, root2.right);
        var flip = FlipEquiv(root1.left, root2.right) && FlipEquiv(root1.right, root2.left);
        return noFlip || flip;
    }

    public IList<string> RemoveSubfolders(string[] folder)
    {
        if (folder.Length <= 1)
        {
            return folder.ToList();
        }

        IList<string> list = new List<string>();
        folder = folder.OrderBy(x => x).ToArray();

        list.Add(folder[0]);
        for (var i = 1; i < folder.Length; i++)
        {
            var lastFolder = list[list.Count - 1] + "/";
            if (!folder[i].StartsWith(lastFolder))
            {
                list.Add(folder[i]);
            }
        }

        return list;
    }

    public int CountSquares(int[][] matrix)
    {
        int m = matrix.Length, n = matrix[0].Length;
        var dp = new int[m, n];
        var count = 0;

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (matrix[i][j] == 1)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1])) + 1;
                    }

                    count += dp[i, j];
                }
            }
        }

        return count;
    }

    public int LongestSquareStreak(int[] nums) {
        /*var set = new HashSet<int>(nums);
        Array.Sort(nums);
        int res = 0;
        foreach (int num in nums){
            int len = 0;
            long curr = num;
            while (curr <= int.MaxValue && set.Contains((int) curr)){
                len++;
                curr *= curr;
            }

            if (len > 1) res = Math.Max(res, len);
        }
        GC.Collect();
        return res > 1 ? res : -1;*/
        if(nums.Length <2)
       {
        return -1;
       }

       int count = -1;

       HashSet<int> set = new HashSet<int>(nums);

       foreach(int x in nums){
        int steak = 0;
        long currVal = x;

        while(set.Contains((int)currVal)){
            steak++;

            if(currVal * currVal > 1e5){
                break;
            }
            currVal = currVal * currVal;
        }
        count = Math.Max(count,steak);
       }
       GC.Collect();
       return count < 2 ? -1 : count; 
    }




    public int MaxMoves(int[][] grid) {
        int[][] dp;
        int[] moves;
        int DFS(int[][] grid, int row, int col, int prev){
            if(row < 0 || col < 0 || row >= grid.Length || col >= grid[row].Length || grid[row][col] <= prev){
                return 0;
            }

            if(dp[row][col] != -1) return dp[row][col];
            
            dp[row][col] = 0;
            int nextCol = col+1;
            
            for(int i=0; i<3; i++)
                dp[row][col] = Math.Max(dp[row][col], 1+DFS(grid, row+moves[i], nextCol, grid[row][col]));

            return dp[row][col];
         }
         
        int rows = grid.Length, cols = grid[0].Length;
        moves = new int[3] { -1, 0, 1 };
        dp = new int[rows][];

        for(int i=0; i<rows; i++){
            dp[i] = new int[cols];
            Array.Fill(dp[i], -1);
        }

        int res = 0;

        for(int i=0; i<rows; i++){
            res = Math.Max(res, DFS(grid, i, 0, -1));
        }

        return res-1;
    }

    public int MinimumMountainRemovals(int[] nums) {
        int n = nums.Length;
        int[] dp1 = new int[n];
        int[] dp2 = new int[n];
        
        Array.Fill(dp1, 1);
        Array.Fill(dp2, 1);

        for (int i = 1; i < n; i++)
        {
            for (int prev = 0; prev < i; prev++)
            {
                if (nums[i] > nums[prev] && dp1[prev] + 1 > dp1[i])
                {
                    dp1[i] = dp1[prev] + 1;
                }
            }
        }

        for (int i = n - 2; i >= 0; i--)
        {
            for (int next = i + 1; next < n; next++)
            {
                if (nums[i] > nums[next] && dp2[next] + 1 > dp2[i])
                {
                    dp2[i] = dp2[next] + 1;
                }
            }
        }

        int max = int.MinValue;
        for (int i = 0; i < n; i++)
        {
            
            if (dp1[i] > 1 && dp2[i] > 1)
            {
                max = Math.Max(max, dp1[i] + dp2[i] - 1);
            }
        }

        return n - max;
    } 

    public string MakeFancyString(string s) {
        if (s.Length == 1 || s.Length == 0) return s;
        Queue<char> queue = new Queue<char>();
        int count = 1;
        queue.Enqueue(s[0]);
        for (int i = 1; i < s.Length; i++){
            if (s[i-1] == s[i]){
                count++;
            }
            else count = 1;
            if (count < 3) queue.Enqueue(s[i]);

        }
        return new string(queue.ToArray());
    }

    public bool IsCircularSentence(string sentence) {
        int i = 0;
        while (i<sentence.Length-1) {
            if (sentence[i]==' ')
                if (sentence[i-1]!=sentence[i+1]) return false;
            i++;
        }
        return sentence[0]==sentence[i];
    }

    public bool RotateString(string s, string goal) =>
    s.Length == goal.Length && (goal + goal).Contains(s);

    public string CompressedString(string word) {
        StringBuilder sb = new StringBuilder();
        char ch = word[0];
        int count = 1;
        for (int i =1; i < word.Length; i++){
            if (ch != word[i] || count == 9){
                sb.Append(count+ch.ToString());
                ch = word[i];
                count = 1;
            }
            else {
                count++;
            }
        }
         sb.Append(count+ch.ToString());
        return sb.ToString();
    }

    public int MinChanges(string s) {
         char lastCh = s[0];
        int freq = 1, n = s.Length, changes = 0;
        for(int i=1;i<n;i++)        // O(n)
            if(s[i]==lastCh)
                freq++;
            else if(freq%2==0)
            {
                lastCh = s[i];
                freq = 1;
            }
            else
            {
                changes++;
                lastCh = '-';
                freq = 0;
            }
        return changes;
    }

    public long MinEnd(int n, int x) {
        long result = x;
        for (var i = 0; i < n - 1; i++){
            result = (result+1)|x;
        }

        return result;
    }

    public int[] MaximumBeauty(int[][] items, int[] queries) {
        
        var priceToBeauty = new Dictionary<int, int>();
        foreach (var item in items) {
            int price = item[0], beauty = item[1];
            if (!priceToBeauty.ContainsKey(price)) {
                priceToBeauty[price] = beauty;
            } else {
                priceToBeauty[price] = Math.Max(priceToBeauty[price], beauty);
            }
        }

        var sortedPrices = new List<int>(priceToBeauty.Keys);
        sortedPrices.Sort();

        for (int i = 1; i < sortedPrices.Count; i++) {
            priceToBeauty[sortedPrices[i]] = Math.Max(priceToBeauty[sortedPrices[i]], priceToBeauty[sortedPrices[i - 1]]);
        }

        int[] results = new int[queries.Length];
        for (int i = 0; i < queries.Length; i++) {
            int queryPrice = queries[i];
            int maxBeauty = GetMaxBeauty(sortedPrices, priceToBeauty, queryPrice);
            results[i] = maxBeauty;
        }

        return results;

        int GetMaxBeauty(List<int> sortedPrices, Dictionary<int, int> priceToBeauty, int queryPrice) {
            int left = 0, right = sortedPrices.Count - 1;
            int result = 0; 

            while (left <= right) {
                int mid = left + (right - left) / 2;
                if (sortedPrices[mid] <= queryPrice) {
                    result = priceToBeauty[sortedPrices[mid]]; 
                    left = mid + 1; 
                } else {
                    right = mid - 1; 
                }
            }

            return result;
        }
    }

    public long CountFairPairs(int[] nums, int lower, int upper) {
        Array.Sort(nums); 
        long count = 0;
        
        for (int i = 0; i < nums.Length - 1; i++) {
            int left = i+1;
            int right = nums.Length;
            int leftBound = Array.BinarySearch(nums, left, right - left, lower - nums[i]);
            int rightBound = Array.BinarySearch(nums, left, right - left, upper - nums[i]);
            if (leftBound < 0) leftBound = ~leftBound;
            if (rightBound < 0) rightBound = ~rightBound - 1;
            
            if (leftBound <= rightBound) {
                count += rightBound - leftBound + 1;
            }
        }
        
        return count;
    }

    public bool CheckIfExist(int[] arr) {
        HashSet<int> hash = new ();
        for(int i =0; i < arr.Length; i++){
            if (hash.Contains(2*arr[i]) || (arr[i] %2==0 && hash.Contains(arr[i]/2))){
                return true;
            }
            hash.Add(arr[i]);
        }
        return false;
    }

    public string AddSpaces(string s, int[] spaces) {
        StringBuilder sb = new StringBuilder();
        int pointer = 0;
        for (int i =0; i < s.Length; i++){
            if (spaces[pointer] == i){
                if (pointer < spaces.Length) pointer++;
                sb.Append(' ');
            }
            sb.Append(s[i]);
        }
        return sb.ToString();
    }

    public bool CanChange(string start, string target) {
        int waitL =0, waitR =0;
        for (int i =0; i < target.Length; i++){
            char current = start[i];
            char goal = target[i];
            if (current == 'R') {
                if (waitL > 0) return false;
                waitR++;
            }
            if (goal == 'L') {
                if (waitR > 0) return false;
                waitL++;
            }
            if (goal == 'R') {
                if (waitR == 0) return false;
                waitR--;
            }
            if (current == 'L') {
                if (waitL == 0) return false;
                waitL--;
            }
        }
        return waitL == 0 && waitR == 0;
    }

    public bool[] IsArraySpecial(int[] nums, int[][] queries) {
        int n = nums.Length;
        int[] prefix = new int[n]; // Prefix array to count special pairs
        bool[] result = new bool[queries.Length]; // Result array

        // Build the prefix array
        for (int i = 1; i < n; i++) {
            prefix[i] = prefix[i - 1];
            if ((nums[i - 1] % 2 == 0 && nums[i] % 2 == 0) || 
                (nums[i - 1] % 2 != 0 && nums[i] % 2 != 0)) {
                prefix[i]++;
            }
        }

        // Process each query
        for (int i = 0; i < queries.Length; i++) {
            int left = queries[i][0];
            int right = queries[i][1];
            
            // Calculate the number of special pairs in the range
            int specialCount = prefix[right] - (left > 0 ? prefix[left] : 0);
            
            // If no special pairs, the result is true; otherwise, false
            result[i] = (specialCount == 0);
        }

        return result;
    }

    public int MaximumLength(string s) {
        Dictionary<string, int> counts = new Dictionary<string, int>();

        int longest = 0;

        char prevChar = '0';
        int currentConsecutive = 0;

        for(int i = 0; i < s.Length; i++)
        {
            if(s[i] == prevChar) currentConsecutive++;
            else currentConsecutive = 1;
            prevChar = s[i];

            for(int j = Math.Max(currentConsecutive - 2, 1); j <= currentConsecutive; j++) 
            {
                if(!counts.ContainsKey(s[i]+j.ToString())) counts[s[i]+j.ToString()]  = 0;

                counts[s[i]+j.ToString()] += 1;
                if(counts[s[i]+j.ToString()] >= 3) longest = Math.Max(longest, j);
            }
        }

        return longest == 0? -1 : longest;
    }

    public int MaximumBeauty(int[] nums, int k) {
        Array.Sort(nums);
        int n = nums.Length;
         int maxLength = 0;
        int start = 0;

        for (int end = 0; end < n; end++)
        {
            while (nums[end] - nums[start] > 2 * k)
            {
                start++;
            }

            maxLength = Math.Max(maxLength, end - start + 1);
        }

        return maxLength;
    }

    public int[] GetFinalState(int[] nums, int k, int multiplier) {
        PriorityQueue<int, int> priority = new PriorityQueue<int, int> ();
        for (int i = 0; i < nums.Length; i++){
            priority.Enqueue(i, nums[i]);
        } 
        for (int i = 0; i < k; i++){
            priority.TryDequeue(out int index, out int num);
            num *= multiplier;
            priority.Enqueue(index, num);
        } 
        for (int i =0; i < nums.Length; i++){
            priority.TryDequeue(out int index, out int num);
            nums[index] = num;
        }  
        return nums;
        /*
        for (int i=0; i<k; i++)
		{
			int min = Int32.MaxValue;
			int minIndex = 0;
			
			for (int j=0; j<nums.Length; j++)
			{
				if (nums[j] < min)
				{
					minIndex = j;
					min = nums[j];
				}
			}
			
			nums[minIndex] *= multiplier;
		}
		
		return nums;
        */
    }

    public string RepeatLimitedString(string s, int repeatLimit) {
        int[] counts = new int[26];
        foreach (char c in s) {
            counts[c - 'a']++;
        }

        var builder = new StringBuilder();
        int cur = 'z' - 'a';
        while (cur >= 0) {
            if (counts[cur] == 0) {
                cur--;
                continue;
            }

            int take = Math.Min(counts[cur], repeatLimit);
            counts[cur] -= take;
            for (int i = 0; i < take; i++) {
                builder.Append((char)('a' + cur));
            }
            if (take < repeatLimit || counts[cur] == 0) {
                continue;
            }

            int fill = cur - 1;
            while (fill >= 0 && counts[fill] == 0) {
                fill--;
            }
            if (fill < 0) {
                break;
            }

            builder.Append((char)('a' + fill));
            counts[fill]--;
        }

        return builder.ToString();
    }

    public int[] FinalPrices(int[] prices) {
        for (int i =0; i < prices.Length; i++){
            for (int j = i+1; j < prices.Length; j++){
                if (prices[j] <= prices[i]){
                    prices[i]-=prices[j];
                    break;
                }
            }
        }   
        return prices;
    }

    public int MaxChunksToSorted(int[] arr) {
        int topValue=0;
        int totalChunks=0;
        for(int i=0;i<arr.Length-1;i++){
            topValue=Math.Max(topValue,arr[i]);
            if(topValue == i) totalChunks++;
        }
        return totalChunks+1;
    }

    
    public IList<int> LargestValues(TreeNode root) {
        List<int> maxVals = new List<int>();

        void Traverse(int index, TreeNode node) {
            if (node == null) return;

            if (maxVals.Count <= index) {
                maxVals.Add(node.val);
            } else {
                maxVals[index] = Math.Max(maxVals[index], node.val);
            }

            Traverse(index + 1, node.left);
            Traverse(index + 1, node.right);
        }

        if (root == null) return maxVals;

        maxVals.Add(root.val);
        Traverse(1, root.left);
        Traverse(1, root.right);
        return maxVals;
    }
}