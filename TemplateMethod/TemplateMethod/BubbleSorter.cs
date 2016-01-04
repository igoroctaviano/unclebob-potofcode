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

namespace TemplateMethod
{
    /// <summary>
    /// Bubble Sort
    /// So let's look at a slightly more useful example.
    /// Note that like Application, Bubble Sort is easy to understand,
    /// and so makes a useful teaching tool. However, no one in their
    /// right mind would ever use Bubble Sort if they had any significant
    /// amout of sorting to do. There are much better algorithms.
    /// 
    /// The BubbleSorter class knows how to sort an array of integers, using
    /// the bubble sort algoritm. The Sort method of BubbleSorter contains the
    /// algorithm that knows how to do a bubble sort. The two ancillary methods
    /// - Swap and CompareAndSwap - deal with the details of integers and arrays
    /// and handle the mechanics that the Sort algorithm requires.
    /// </summary>
    class BubbleSorter
    {
        static int operations = 0;
        public static int Sort(int[] array)
        {
            operations = 0;
            if (array.Length <= 1)
                return operations;

            for (int nextToLast = array.Length - 2; nextToLast >= 0; nextToLast--)
                for (int index = 0; index <= nextToLast; index++)
                    CompareAndSwap(array, index);

            return operations;
        }

        private static void Swap(int[] array, int index)
        {
            int temp = array[index];
            array[index] = array[index + 1];
            array[index + 1] = temp;
        }

        private static void CompareAndSwap(int[] array, int index)
        {
            if (array[index] > array[index + 1])
                Swap(array, index);
            operations++;
        }
    }

    /// <summary>
    /// Using the TEMPLATE METHOD pattern, we can separate the bubble sort algorithm
    /// out into an abstract class named AbstractBubbleSorter that contains a Sort
    /// function implementantion that calls an abstract method named OutOfOrder
    /// and another called Swap. The OutOfOrder method compares two adjacent
    /// elements in the array and returns true if the elements are out of order. The
    /// Swap method waps two adjacent cells in the array.
    /// 
    /// Given the AbstractBubbleSorter, we can now create simple derivatives
    /// that can sort any different kind of object. For exemple, we could
    /// create IntAbstractBubbleSorter, which sorts arrays of integers.
    /// 
    /// TEMPLATE METHOD pattern shows one the classic forms of reuse in object-
    /// oriented programming. Generic algorithms are placed in the base class and
    /// inherited into different contexts. But this technique is not wihout its
    /// costs. Inheritance is a very strong relationship. Derivatives are inextricably
    /// bound to their base classes.
    /// </summary>
    abstract class AbstractBubbleSorter
    {
        private int operations = 0;
        protected int length = 0;

        /// <summary>
        /// The Sort method does not know about the array; nor does it care what kinds
        /// of objects are stored in the array. It simply calls OutOfOrder for various
        /// indices into the array and determines whether those indices should be swapped.
        /// </summary>
        /// <returns></returns>
        protected int DoSort()
        {
            operations = 0;
            if (length <= 1)
                return operations;

            for (int nextToLast = length - 2; nextToLast >= 0; nextToLast--)
                for (int index = 0; index <= nextToLast; index++)
                {
                    if (OutOfOrder(index))
                        Swap(index);
                    operations++;
                }

            return operations;
        }

        protected abstract void Swap(int index);

        protected abstract bool OutOfOrder(int index);
    }

    class IntAbstractBubbleSorter : AbstractBubbleSorter
    {
        private int[] array = null;     // Array of integers

        public int Sort(int[] array)
        {
            this.array = array;
            length = array.Length;      // Protected atribute from the base class
            return DoSort();
        }

        protected override void Swap(int index)
        {
            int temp = this.array[index];
            this.array[index] = this.array[index + 1];
            this.array[index + 1] = temp;
        }

        protected override bool OutOfOrder(int index)
        {
            return (this.array[index] > this.array[index + 1]);
        }
    }
}
