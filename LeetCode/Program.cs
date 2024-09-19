using System.Collections;

public class Solution
{
    public static void Main()
    {
        Console.WriteLine(FindMaxAverage(new int[] {0, 1,1 ,3,3}, 4));
        Console.ReadLine();
    }

    public string MergeAlternately(string word1, string word2)
    {
        var result = "";
        var i = 0;
        var j = 0;
        while (i < word1.Length && j < word2.Length)
        {
            result += word1[i].ToString() + word2[j].ToString();
            i++;
            j++;
        }

        if (j < word2.Length)
        {
            result += word2.Substring(j, word2.Length - j);
        }
        else if (i < word1.Length)
        {
            result += word1.Substring(i, word1.Length - i);
        }

        return result;
    }

    public string GcdOfStrings(string str1, string str2)
    {
        var prefix = "";
        var result = "";
        var str1l = str1.Length;
        var str2l = str2.Length;
        for (var i = 0; i < str1l && i < str2l; i++)
        {
            prefix += str1[i].ToString();

            var prefl = prefix.Length;
            if (str1l % prefl == 0 && str2l % prefl == 0)
            {
                if (str1 == string.Concat(Enumerable.Repeat(prefix, str1l / prefl))
                    && str2 == string.Concat(Enumerable.Repeat(prefix, str2l / prefl)))
                {
                    result = prefix;
                }
            }
        }

        return result;
    }

    public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        var len = candies.Length;
        IList<bool> result = new List<bool>(len);
        var maxCandies = candies.Max();
        for (var i = 0; i < len; i++)
        {
            if (candies[i] + extraCandies >= maxCandies)
            {
                result.Add(true);
            }
            else
            {
                result.Add(false);
            }
        }

        return result;
    }

    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        for (var i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] == 0 && n != 0)
            {
                bool check1 = true, check2 = true;
                if (i - 1 >= 0)
                {
                    if (flowerbed[i - 1] != 0)
                    {
                        check1 = false;
                    }
                }

                if (i + 1 <= flowerbed.Length)
                {
                    if (flowerbed[i + 1] != 0)
                    {
                        check2 = false;
                    }
                }


                if (check1 && check2)
                {
                    flowerbed[i] = 1;
                    n--;
                }
            }
        }

        return n == 0 ? true : false;
    }

    public int ScoreOfString(string s)
    {
        var result = 0;
        for (var i = 0; i < s.Length - 1; i++)
        {
            var temp = (int)s[i] - (int)s[i + 1];
            result += (temp + (temp >> 31)) ^ (temp >> 31);
        }

        return result;
    }

    public string ReverseVowels(string s)
    {
        int strt = 0, end = s.Length - 1;
        var a = new HashSet<char>() { 'a', 'A', 'e', 'E', 'o', 'O', 'u', 'U', 'i', 'I' };
        var arr = s.ToCharArray();
        bool b = false, c = false;
        while (strt < end)
        {
            if (!a.Contains(s[strt]))
            {
                strt++;
            }
            else
            {
                b = true;
            }

            if (strt < end)
            {
                if (a.Contains(s[end]))
                {
                    c = true;
                }
                else
                {
                    end--;
                }
            }

            if (b && c)
            {
                (arr[strt], arr[end]) = (arr[end], arr[strt]);
                strt++;
                end--;
                b = c = false;
            }
        }

        return new string(arr);
    }

    public string NormalizeWhiteSpace(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        var current = 0;
        var output = new char[input.Length];
        var skipped = false;

        foreach (var c in input.ToCharArray())
        {
            if (char.IsWhiteSpace(c))
            {
                if (!skipped)
                {
                    if (current > 0)
                    {
                        output[current++] = ' ';
                    }

                    skipped = true;
                }
            }
            else
            {
                skipped = false;
                output[current++] = c;
            }
        }

        return new string(output, 0, current);
    }

    public string ReverseWords(string s)
    {
        string[] arr = s.Split(' ');
        s = "";
        for (var i = arr.Length - 1; i > -1; i--)
        {
            if (arr[i] != "")
            {
                s += arr[i] + " ";
            }
        }

        if (s[s.Length - 1] == ' ')
        {
            s = s.Remove(s.Length - 1);
        }

        return s;
    }

    public int[] ProductExceptSelf(int[] nums)
    {
        int numl = nums.Length;
        int[] result = new int[numl];

        for (int i =0; i < numl; i++)
        {
            result[i] = 1;
        }
        int prefix = 1;
        for (int i = 0; i < numl; i++)
        {
            result[i]*= prefix;
            prefix *= nums[i];
        }

        int postfix = 1;
        for (int i = numl - 1; i >= 0; i--)
        {
            result[i] *= postfix;
            postfix *= nums[i];
        }
        return result;
    }

    public bool IncreasingTriplet(int[] nums)
    {
        if (nums.Length < 3) return false;
        int point1, point2;
        point1 = point2 = int.MaxValue;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] <= point2)
            {
                point2 = nums[i];
                continue;
            }
            else if (nums[i] <= point1)
            {
                point1 = nums[i];
                continue;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public int Compress(char[] chars)
    {
        string s = "";
        int j = 0;
        
        for (int i = 0; i < chars.Length; i++)
        {
            char a = chars[i];
            int counter = 0;
            while (j < chars.Length && a == chars[j])
            {
                j++;
                counter++;
            }
            if (counter == 1)
            {
                s += a;
            }
            else
            {
                s += a;
                s += counter.ToString();
                i = j - 1;
            }
            
        }
        Array.Resize(ref chars, s.Length);
        for(int i = 0; i < s.Length; i++)
        {
            chars[i] = s[i];
        }
        return s.Length;
    }

    public void MoveZeroes(int[] nums)
    {
        int i = 0, j = 0;
        while (j < nums.Length)
        {
            if (nums[j] != 0)
            {
                (nums[i], nums[j]) = (nums[j], nums[i]);
                i++;
            }
            j++;
        }
    }

    public bool IsSubsequence(string s, string t)
    {
        int i = 0, j = 0;
        if (s.Length == 0) return true;
        while (i < t.Length)
        {
            if (j == s.Length) return true;
            if (s[j] == t[i])
            {
                j++;
            }
            i++;
        }
        return j == s.Length ? true : false;
    }

    public int MaxArea(int[] height)
    {
            int i = 0, j = height.Length - 1;
            int MaxArea = 0;
        
            while (i != j)
            {
                MaxArea = int.Max(MaxArea,
                            int.Min(height[i], height[j]) * (j - i));

                if (height[i] < height[j])
                    i += 1;

                else
                    j -= 1;
            }
            return MaxArea;
    }

    public int MaxOperations(int[] nums, int k)
    {
        Array.Sort(nums);
        int i = 0, j = nums.Length - 1;
        int counter = 0;
        while (i < j && i < nums.Length - 1 && j >= 0)
        {
            if (nums[i] + nums[j] == k)
            {
                counter++;
                i++;
                j--;
            }
            else if (nums[i] + nums[j] < k)
            {
                i++;
            }
            else
            {
                j--;
            }

        }
        return counter;
    }

    public double FindMaxAverage(int[] nums, int k) {
        
        double sum = 0;
        if (nums.Length == 1 && k == 1) return nums[0];
        for( int i =0; i <k; i++)
        {
            sum+= nums[i];
        }
        double Max = sum/k;
        for (int i = k; i < nums.Length; i++) {
            sum += nums[i];
            sum -= nums[i-k];
            Max = Math.Max(Max, sum/k);
        }
        return Max;
    }

    public int MaxVowels(string s, int k) {
        bool isVowel(char a)
        {
            return a == 'a' || a == 'e' || a=='i' || a=='o' || a=='u';
        }
        // func faster than hashset? wtf?
        int count = 0;
        for (int i = 0; i < k; i++)
        {
            if (isVowel(s[i]))
            {
                count++;
            }
        }
        int Max = count;
        for (int i = k; i < s.Length; i++) {
            if (isVowel(s[i])) count++;
            if ( isVowel(s[i-k])) count--;
            Max = int.Max(Max, count);
            if (Max == k) break;
        }
        return Max;
    }

    public int LongestOnes(int[] nums, int k) {
        int max = 0;
        int i=0, j=0, zero = 0;
        while(j <  nums.Length) {
            if (nums[j] == 0) zero++;
            while (k < zero) {
                if (nums[i] == 0) zero--;
                i++;
            }
            max = int.Max(max, j-i+1);
            j++;
        }
        return max;
    }

     public int LongestSubarray(int[] nums) {
        int max=0, curr=0, prev = 0;
        for (int i = 0;  i < nums.Length; i++) {
            if (nums[i] ==0) {
                max = Math.Max(max, prev+curr);
                prev = curr; curr = 0;
            }
            else
            {
                curr++;
            }
        }
        if (curr == nums.Length) return curr-1;
        max = Math.Max(max, prev+curr);
        return max;
    }

    public int LargestAltitude(int[] gain) {
        int[]a = new int[gain.Length+1];
        a[0] = 0;
        int Max = 0;
        for (int i =1; i < a.Length; i++) { 
            a[i] = a[i-1]+gain[i-1];
            Max = Math.Max(Max, a[i]);
        }
        return Max;
    }

    public int PivotIndex(int[] nums) {
        List<int> psum = new List<int>();
        psum.Add(0);
        foreach(int i in nums)
        {
            psum.Add(psum[psum.Count-1]+i);
        }
        for (int i =0; i < psum.Count-1; i++)
        {
            if (psum[i] == psum[nums.Length] - psum[i+1]) return i;
        }
        return -1;
    }

    public IList<IList<int>> FindDifference(int[] nums1, int[] nums2) {
        HashSet<int> a = new HashSet<int>(nums1);
        HashSet<int> b = new HashSet<int>(nums2);
        IList<IList<int>> ints = new List<IList<int>>() { new List<int>, new List<int>};

        for(int i =0; i < nums1.Length; i++)
        {
            if (!b.Contains(nums1[i])) { if (!ints[0].Contains(nums1[i])) ints[0].Add(nums1[i]);}
        }
        for(int i =0; i < nums2.Length; i++)
        {
            if (!a.Contains(nums2[i]))
            {
                if (!ints[1].Contains(nums2[i])) ints[1].Add(nums2[i]);
            }
        }
        return ints;
    }

    public bool UniqueOccurrences(int[] arr) {
        HashSet <int> a = new HashSet<int>(arr);
        Dictionary<int, int> countMap = new Dictionary<int, int>();
        
        foreach (int num in arr)
        {
            if (countMap.ContainsKey(num))
                countMap[num]++;
            else
                countMap[num] = 1;
        }

        return countMap.Values.Distinct().Count() == countMap.Count;
    }

    public bool CloseStrings(string word1, string word2) {
        Dictionary<char, int> CreateDict(string word)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(char c in word) {
                if (dict.TryGetValue(c, out int frequency))
                {
                    dict[c] = frequency+1;
                }
                else
                {
                    dict[c]=1;
                }
            }
            return dict;
        }
        bool Check(Dictionary<char, int> a, Dictionary<char, int> b)
        {
            foreach(char c in a.Keys) {
                if (!b.ContainsKey(c)) return false;
            }
            return true;
        }
        if (word1.Length != word2.Length) return false;

        Dictionary<char, int> freq1 = CreateDict(word1);
        Dictionary<char, int> freq2 = CreateDict(word2);

        if (!Check(freq1, freq2)) return false;
        if (!Check(freq2, freq1)) return false;
        return freq1.Values.Order().SequenceEqual(freq2.Values.Order());
     }
}