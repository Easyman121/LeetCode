using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily
{
    public class CustomStack {
        int maxSize;
        int[] arr;
        int top;
        public CustomStack(int maxSize) {
            arr = new int[maxSize];
            this.maxSize = maxSize;
            top = -1;
        }
    
        public void Push(int x) {
            if (top < maxSize-1)
            {
                arr[++top] = x;
            }
        }
    
        public int Pop() {
        
            if (top < 0) return -1;
            return arr[top--];
        }
    
        public void Increment(int k, int val) {
            int temp = Math.Min(k, top + 1);
            for (int i =0; i < temp; i++)
            {
                arr[i] += val;
            }
        }
    }
}
