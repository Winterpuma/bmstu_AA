using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5_Conveyor
{
    class Conveyor
    {
        Queue<Args> q;

        public Conveyor()
        {
            q = new Queue<Args>();
        }

        public Conveyor(List<Args> elements)
        {
            q = new Queue<Args>(elements);
        }

        public void AddElement(Args arg)
        {
            q.Enqueue(arg);
        }
    }
}
