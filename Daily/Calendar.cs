using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily
{
    class MyCalendar
    {
        List<(int, int)> list;
        public MyCalendar() {
            list = new List<(int, int)> ();
        }
    
        public bool Book(int start, int end) {
            if (list.Count == 0) { list.Add((start, end)); return true; } 
            foreach(var item in list) {
                if (start < item.Item2 && end > item.Item1) {
                    return false; 
                }
            }
            
            list.Add((start, end));
            return true;
        }
    }
}
