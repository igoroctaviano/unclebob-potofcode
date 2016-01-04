///
/// Author: Robert C. Martin and Micah Martin
/// Book: Agile Principles, Practices and Patterns in C#
/// 
/// Igor Octaviano
/// More? access: https://github.com/igoroctaviano/unclebob-potofcode
/// 
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.IO;

namespace TemplateMethod
{
    class Program // FtoCRaw
    {
        /// <summary>
        /// A Main loop reads Fahrenheit readings from the Console and prints out Celsius conversions.
        /// At the end, an exit message is printed.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool done = false;
            while (!done)
            {
                string fahrString = Console.In.ReadLine();
                if (fahrString == null || fahrString.Length == 0)
                    done = true;
                else
                {
                    double fahr = Double.Parse(fahrString);
                    double celcius = 5.0 / 9.0 * (fahr - 32);
                    Console.WriteLine("F={0}, C={1}", fahr, celcius);
                }
            }
            Console.WriteLine("ftoc exit");
        }
    }

    /// <summary>
    /// We can separate this fundamental structure from the ftoc program by using 
    /// the method of an abstract base class. The implemented method captures the
    /// generic algorithm but defers all details to abstract methods of the base
    /// class.
    /// </summary>
    public abstract class Application
    {
        private bool isDone = false;

        /// <summary>
        /// We can capture the main loop structure in an abstract base class called Application.
        /// This class describes a generic main-loop application. We can see the main loop in
        /// the implemented Run function. We can also see that all the work is being deferred
        /// to the abstract methods Init, Idle and Cleanup. The init method takes care of any
        /// initialization we need done. The Idle method does the main work of the program and
        /// will be called repeatedly until SetDone is called. The Cleanup method does whatever
        /// needs to be done before we exit.
        /// </summary>
        protected abstract void Init();

        protected abstract void Idle();

        protected abstract void Cleanup();

        protected void SetDone()
        {
            isDone = true;
        }

        protected bool Done()
        {
            return isDone;
        }

        public void Run()
        {
            Init();
            while (!Done())
                Idle();
            Cleanup();
        }
    }

    /// <summary>
    /// We can rewrite the ftoc class by inheriting from Application and simply filling in
    /// the abstract methods. Its easy to see how the old ftoc Application has been fit into
    /// the TEMPLATE METHOD pattern.
    /// </summary>
    public class FtoCTemplateMethod : Application
    {
        private TextReader input;
        private TextWriter output;

        // public static void Main(string[] args)
        // {
        //     new FtoCTemplateMethod().Run();
        // }

        protected override void Init()
        {
            input = Console.In;
            output = Console.Out;
        }

        protected override void Idle()
        {
            string fahrString = Console.In.ReadLine();
            if (fahrString == null || fahrString.Length == 0)
                SetDone();
            else
            {
                double fahr = Double.Parse(fahrString);
                double celcius = 5.0 / 9.0 * (fahr - 32);
                Console.WriteLine("F={0}, C={1}", fahr, celcius);
            }
        }

        protected override void Cleanup()
        {
            output.WriteLine("ftoc exit");
        }
    }
}
