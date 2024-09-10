public class Solution
{
    public static void Main()
    {
        Console.WriteLine(Compress(new char[] {'a', 'b', 'c'}));
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

    public static string NormalizeWhiteSpace(string input)
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

    public static string ReverseWords(string s)
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

    public static int[] ProductExceptSelf(int[] nums)
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

    public static int Compress(char[] chars)
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
}