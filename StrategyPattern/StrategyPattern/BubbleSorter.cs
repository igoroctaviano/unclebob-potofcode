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

namespace StrategyPattern
{
    /// <summary>
    /// Consider an implementation of the bubble sort that uses the STRATEGY PATTERN.
    /// 
    /// Note that the IntSortHandler class knows nothing whatever of the BubbleSorter,
    /// having no dependency whatever on the bubble sort implementation. This
    /// is not the case with the TEMPLATE METHOD pattern. With TEMPLATE METHOD pattern,
    /// IntBubbleSorter depended directly on BubbleSorter, the class that contains
    /// the bubble sort algorithm.
    /// 
    /// The TEMPLATE METHOD approach partially violtes DIP. The implementation of the
    /// Swap and OutOfOrder methods depends directly on the bubble sort algorithm. 
    /// The STRATEGY approach contains no such dependency. Thus, we can use the
    /// IntSortHandler with Sorter implementations other than BubbleSorter.
    /// 
    /// </summary>
    class BubbleSorter // Bubble sort algorithm class
    {
        private int operations = 0;
        private int length = 0;
        private SortHandler itsSortHandler = null; // SortHandler interface *delegation

        public BubbleSorter(SortHandler handler)
        {
            itsSortHandler = handler;
        }

        public int Sort(object array)
        {
            itsSortHandler.SetArray(array);
            length = itsSortHandler.Length();
            operations = 0;
            if (length <= 1)
                return operations;

            for (int nextToLast = length - 2; nextToLast >= 0; nextToLast--)
                for (int index = 0; index <= nextToLast; index++)
                {
                    if (itsSortHandler.OutOfOrder(index))
                        itsSortHandler.Swap(index);
                    operations++;
                }

            return operations;
        }
    }

    /// <summary>
    /// With the SortHandler, Swap and OutOfOrder does not depend on the BubbleSort
    /// algorithm directly like the TEMPLATE METHOD approach did, violating the DIP
    /// principle partially.
    /// </summary>
    public interface SortHandler
    {
        void Swap(int index);
        bool OutOfOrder(int index);
        int Length();
        void SetArray(object array);
    }

    public class IntSortHandler : SortHandler // Implements SortHandler interface
    {
        private int[] array = null;

        public void Swap(int index)
        {
            int temp = this.array[index];
            this.array[index] = this.array[index + 1];
            this.array[index + 1] = temp;
        }

        public void SetArray(object array)
        {
            this.array = (int[])array;
        }

        public int Length()
        {
            return this.array.Length;
        }

        public bool OutOfOrder(int index)
        {
            return (this.array[index] > this.array[index + 1]);
        }
    }

    /// <summary>
    /// Thus the STRATEGY pattern provides one extra benefit over the TEMPLATE METHOD pattern.
    /// Whereas the TEMPLATE METHOD pattern allows a generic algorithm to manipulate many
    /// possible detailed implemenetations, the STRATEGY pattern, by fully conforming to
    /// DIP, additionally allows each implementation to be manipulated by many dfferent
    /// generic algorithms.
    /// 
    /// CONCLUSION:
    /// TEMPLATE METHOD is simple to write and wimple to use but is also inflexible. STRATEGY
    /// pattern is flexible, but you have to create an extra class, instantiate an extra object,
    /// and wire the extra object into the system. So the choice between TEMPLATE METHOD and
    /// STRATEGY depends on whether you need the flexibility of STRATEGY or can live with the
    /// simplicity of TEMPLATE METHOD. Many times, i have opted for TEMPLATE METHOD simply 
    /// because it is easier to implement and use. For example, i would use the TEMPLATE METHOD
    /// solution to the bubble sort problem unless i was very sure that i needed different sort
    /// algorithms.
    /// </summary>
    public class QuickBubbleSorter
    {
        private int operations = 0;
        private int length = 0;
        private SortHandler itsSortHandler = null;

        public QuickBubbleSorter(SortHandler handler)
        {
            itsSortHandler = handler;
        }

        public int Sort(object array)
        {
            itsSortHandler.SetArray(array);
            length = itsSortHandler.Length();
            operations = 0;
            if (length <= 1)
                return operations;

            bool thisPassInOrder = false;
            for (int nextToLast = length - 2; nextToLast >= 0 && !thisPassInOrder; nextToLast--)
            {
                thisPassInOrder = true; // Potetially.
                for (int index = 0; index <= nextToLast; index++)
                {
                    if (itsSortHandler.OutOfOrder(index))
                    {
                        itsSortHandler.Swap(index);
                        thisPassInOrder = false;
                    }
                    operations++;
                }
            }

            return operations;
        }
    }
}
