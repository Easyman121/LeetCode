
public class Solution
{
    public static void Main()
    {
        
        Console.ReadLine();
    }
    public string MergeAlternately(string word1, string word2)
    {
        string result = "";
        int i = 0;
        int j = 0;
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
        string prefix = "";
        string result ="";
        int str1l = str1.Length;
        int str2l = str2.Length;
        for (int i = 0; i < str1l && i < str2l; i++)
        {
            prefix += str1[i].ToString();
              
            int prefl = prefix.Length;
            if (str1l % prefl == 0 && str2l % prefl == 0)
            {
                if (str1 == string.Concat(Enumerable.Repeat(prefix, str1l/ prefl))
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
        
        int len = candies.Length;
        IList<bool> result = new List<bool>(len);
        int maxCandies = candies.Max();
        for (int i = 0; i < len; i++)
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
        /*
         int len = candies.Length;
        IList<bool> result = new List<bool>(len);
for (int i = 0; i < len; i++)
{
    result.Add(false);
}
int maxCandies = candies.Max();
for (int i = 0; i < len; i++)
{
    if (candies[i] + extraCandies >= maxCandies)
    {
        result[i] = true;
    }
}

return result;
         */
    }

    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        for (int i = 0; i < flowerbed.Length; i++)
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
        int result = 0;
        for (int i = 0; i < s.Length-1; i++)
        {
            int temp = (int)s[i] - (int)s[i + 1];
            result += (temp + (temp >> 31)) ^ (temp >> 31);
        }
        return result;
    }
}
