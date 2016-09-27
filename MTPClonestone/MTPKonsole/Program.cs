using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTPKonsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ClonestoneEntities a = new ClonestoneEntities())
            {
                var linqresult = (from c in a.tblcards
                                  where c.life > 6
                                  select c).ToList();
                


            }
            //Hallo
            //Pfeiffer schreibt :)
            //Christian schreibt :)

            Console.WriteLine("Hallo Welt");
            Console.WriteLine("Willkommen in der IN21");
        }

        static void MethodeInEinemBranch()
        {
            Console.WriteLine("Branch");
        }
    }
}
