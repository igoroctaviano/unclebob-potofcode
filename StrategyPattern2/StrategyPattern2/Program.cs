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

namespace StrategyPattern2
{
    class Program
    {
        public interface DataHandler
        {
            void Send(object message);
            object Receive();
        }

        public class IntData : DataHandler
        {
            private int data;

            public void Send(object data)
            {
                try
                {
                    this.data = (int)data;
                }
                catch (Exception e)
                {
                  
                }
            }

            public object Receive()
            {
                return this.data;
            }
        }

        public class StringData : DataHandler
        {
            private string data;

            public void Send(object data)
            {
                this.data = data.ToString();
            }

            public object Receive()
            {
                return this.data;
            }
        }

        public class DataService
        {
            private DataHandler itsDataHandler = null;

            public DataService(DataHandler handler)
            {
                this.itsDataHandler = handler;
            }

            public void SendAndReceive()
            {
                this.itsDataHandler.Send(Console.ReadLine());
                Console.WriteLine(this.itsDataHandler.Receive());
                Console.WriteLine(this.itsDataHandler.GetType().ToString() + " message sent.");
            }

            public void SetData(DataHandler handler)
            {
                this.itsDataHandler = handler;
            }
        }

        static void Main(string[] args)
        {
            DataService dataService = new DataService(new IntData());
            dataService.SendAndReceive();

            dataService.SetData(new StringData());
            dataService.SendAndReceive();
        }
    }
}
