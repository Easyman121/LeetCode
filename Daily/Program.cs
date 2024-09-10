
public class Solution
{
    public static void Main()
    {
        int[][] array = new int[5][];
        for (int i = 0; i < 5; i++)
        {
            array[i] = new int[3];
            for (int j = 0; j < 3; j++)
            {
                array[i][j] = -1;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(array[i][j] + " ");
            }
            Console.Write("\n");
        }
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

    public static ListNode[] SplitListToParts(ListNode head, int k)
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
        for (int i = 0; i < m; i++)
        {
            array[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                array[i][j] = -1;
            }
        }
        int top = 0, bottom = m - 1, left = 0, right = n - 1;
        while (top <= bottom && left <= right)
        {
            for (int i = left; i <= right; i++)
            {
                assignarr(ref array[top][i], head);
                
            }
            top++; 

            for (int i = top; i <= bottom; i++)
            {
                assignarr(ref array[i][right], head);
            }
            right--; 

            if (top <= bottom)
            {
                for (int i = right; i >= left; i--)
                {
                    assignarr(ref array[bottom][i], head);
                }
                bottom--; 
            }

            if (left <= right)
            {
                
                for (int i = bottom; i >= top; i--)
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
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }
        if (head.next == null) return head;
        ListNode prev = head;
        ListNode next = head.next;
        while (next != null)
        {
            ListNode GCDNode = new ListNode(GCD(prev.val, next.val), next);
            prev.next = GCDNode;
            prev = next;
            next = next.next;
        }

        return head;
        
    }

   

}