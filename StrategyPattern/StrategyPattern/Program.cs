///
/// Author: Robert C. Martin and Micah Martin
/// Book: Agile Principles, Practices and Patterns in C#
/// 
/// Igor Octaviano
/// More? access: https://github.com/igoroctaviano/unclebob-potofcode
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace StrategyPattern
{
    class Program
    {
        /// <summary>
        /// The STRATEGY PATTERN solves the problem of inverting the dependencies
        /// of the generic algorithm and the detailed implemenetation in a very
        /// different way of inheritance (TEMPLATE METHOD).
        /// 
        /// Consider once again the pattern-abusing Application problem.
        /// Rather than placing the generic algorithm into an abstract base class, we
        /// place it into a concrete class named ApplicationRunner. We define the abstract
        /// base methods that the generic algorithm must call within an interface named
        /// Application. Wr derive FtoCStrategy from this interface and pass it into
        /// the ApplicationRunner. AplicationRunner then delegates to this interface.
        /// </summary>
        /// <param name="args"></param>
        
        public class ApplicationRunner
        {
            private Application itsApplication = null;

            public ApplicationRunner(Application app)
            {
                itsApplication = app;
            }

            public void Run()
            {
                itsApplication.Init();
                while (!itsApplication.Done())
                    itsApplication.Idle();
                itsApplication.Cleanup();
            }
        }

        public interface Application
        {
            void Init();
            void Idle();
            void Cleanup();
            bool Done();
        }

        public class FtoCStrategy : Application
        {
            private TextReader input;
            private TextWriter output;
            private bool isDone = false;

            public void Init()
            {
                this.input = Console.In;
                this.output = Console.Out;
            }

            public void Idle()
            {
                string fahrString = input.ReadLine();
                if (fahrString == null || fahrString.Length == 0)
                    isDone = true;
                else
                {
                    double fahr = Double.Parse(fahrString);
                        double celcius = 5.0 / 9.0 * (fahr - 32);
                    output.WriteLine("F={0}, C={1}", fahr, celcius);
                }
            }

            public void Cleanup()
            {
                output.WriteLine("ftoc exit");
            }

            public bool Done()
            {
                return isDone;
            }
        }

        static void Main(string[] args)
        {
            /* Bubble sort algorithm class using STRATEGY pattern using delegation,
               passing the IntSortHandler that implements SortHandler interface */
            BubbleSorter bubbleSortAlgorithm = new BubbleSorter(new IntSortHandler());

            // An unordered array to test the implementation.
            int[] unOrderedArray = { 2, 5, 8, 2, 1, 98, 34, 2, 3, 5, 6, 8, 2, 1, 0, 56 };

            bubbleSortAlgorithm.Sort(unOrderedArray);

            for (int i = 0; i < unOrderedArray.Length; i++)
            {
                Console.WriteLine(unOrderedArray[i]);
            }

            // (new ApplicationRunner(new FtoCStrategy())).Run();
        }
    }
}
