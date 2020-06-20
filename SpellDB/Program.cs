using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpellDB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            DDragonParser.ParseAndBuildSpellDB();

            Console.ReadKey();
        }
    }
}
