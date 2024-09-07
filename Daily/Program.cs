public class Solution
{
    public static void Main()
    {
        
    }
    
    public int GetLucky(string s, int k)
    {
        string strnew = "";
        foreach(char c in s)
        {
            strnew += (c - 96);

        }
        int result = 0;
        for (int i =0; i < k; i++)
        {
            result = 0;
            foreach (char c in strnew)
            {
                result += (c - '0');
            }
            strnew = "";
            strnew += result;
        }
        return result;
    }

    public  int RobotSim(int[] commands, int[][] obstacles)
    {
        int[] position = new int[] { 0, 0 };
        byte facing = 0;
        HashSet<(int, int)> obstacleSet = new HashSet<(int, int)>();
        
        int furthestPoint = 0;
        foreach (var obstacle in obstacles)
        {
            obstacleSet.Add((obstacle[0], obstacle[1]));
        }
        for (int i = 0; i < commands.Length; i++)
        {

            switch (commands[i])
            {
                case -2:
                    facing = (byte)((facing + 1) % 4);
                    break;
                case -1:
                    facing = (byte) ((facing + 3) % 4);
                    break;
                case int j when j > 0 && j < 10:

                    for (j = 0; j < commands[i]; j++)
                    {
                        int nextX = position[0];
                        int nextY = position[1];
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
            int newPoint = position[0] * position[0] + position[1] * position[1];
            if (newPoint > furthestPoint)
            {
                furthestPoint = newPoint;
            }
        }
        return furthestPoint;
    }

    public int[] MissingRolls(int[] rolls, int mean, int n)
    {
        int sum = 0;
        foreach ( int roll in rolls ) 
        {
            sum += roll;
        }
        int remainingSum = mean * (n + rolls.Length) - sum;
        if (remainingSum > 6 * n || remainingSum < n) return new int[] { };
        int distributeMean = remainingSum/ n;
        int mod = remainingSum % n;
        int[] elements = new int[n];
        Array.Fill(elements, distributeMean);
        for (int i = 0; i < mod; i++)
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
    public static ListNode ModifiedList(int[] nums, ListNode head)
    {
        HashSet<int> numSet = new HashSet<int>(nums);
        ListNode dummy = new ListNode(0, head); 
        ListNode current = dummy;

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

}