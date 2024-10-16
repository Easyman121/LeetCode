public class Solution
{
    public static void Main()
    {
        var sol = new Solution();
        sol.MinLength("ABFCACDB");
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


    public long MaxKelements(int[] nums, int k) {

        PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>();
        foreach(int num in nums)
        {
            maxHeap.Enqueue(num , -num);
        }
        long score = 0 ;
        for(int i = 0 ; i < k ; i++)
        {
            int largest = maxHeap.Dequeue();
            score += largest;
            int reduced = (int)Math.Ceiling(largest / 3D);
            maxHeap.Enqueue(reduced , -reduced);
        }
        return score;
    }

    public long MinimumSteps(string s) {
        int pos = 0;
        long swap = 0;
        for (int i =0; i < s.Length; i++){
            if (s[i] == '0'){
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

    public string LongestDiverseString(int a, int b, int c) {
        int maxLength = a + b + c, i = 0;
        List<char> chars = new List<char>();
        int currA, currB, currC;
        currA = currB = currC = 0;
        while (true){
            if (currA != 2 && a>= b && a >= c || a > 0 && (currB == 2 || currC == 2)){
                chars.Add('a');
                a--;
                currA++;
                currB = currC = 0;
            }
            else if (currB != 2 && b>= c && b >= a || b > 0 && (currA == 2 || currC == 2)){
                chars.Add('b');
                b--;
                currB++;
                currA = currC = 0;
            }
            else if (currC != 2 && c>= a && c >= b || c > 0 && (currB == 2 || currA == 2)){
                chars.Add('c');
                c--;
                currC++;
                currA = currB = 0;
            }
            else break;
        }
        return string.Join("", chars);
    }
}
