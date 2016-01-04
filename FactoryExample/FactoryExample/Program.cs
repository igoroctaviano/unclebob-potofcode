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

namespace FactoryExample
{
    public interface Shape { }
    public class Square : Shape { }
    public class Circle : Shape { }

    /// <summary>
    /// We see the ShapeFactory interface, which has two methods: MakeSquare and MakeCircle.
    /// The MakeSquare method returns an instance of a Square, and the MakeCircle method returns
    /// an istance of a Circle. However, the return type of both functions is Shape.
    /// 
    /// Note that this completely solves the problem of depending on concrete classes.
    /// The application code no longer depends on Circle or Square, yet it still
    /// manages to create instances of them. It manipulates those instances through the
    /// Shape interface and never invokes methods that are specific to Square or Circle.
    /// 
    /// The problem of depending on a concrete class has been removed. Someone must
    /// create ShapeFactoryImplementation, but nobody else ever needs to create Square
    /// or Circle. ShapeFactoryImplementation will most likely be created by Main
    /// or an initialization function attached to Main.
    /// </summary>

    /// <summary>
    /// A Dependency Problem
    /// Astute readers will recognize a problem with this form of the FACTORY pattern. The class
    /// ShapeFactory has a method for each of the derivatives of Shape. This results in a name-only
    /// dependency that makes it difficult to add new derivatives to Shape. Every time we
    /// add a new Shape derivative, we have to add a new method to the ShapeFactory interface.
    /// In most cases, this means that we'll have to recompile and redeploy all the users of 
    /// ShapeFactory.
    /// </summary>
    /* public interface ShapeFactory
    {
        Shape MakeCircle();
        Shape MakeSquare();
    }

    public class ShapeFactoryImplementation : ShapeFactory
    {
        public Shape MakeCircle()
        {
            return new Circle();
        }

        public Shape MakeSquare()
        {
            return new Square();
        }
    } */

    public interface ShapeFactory
    {
        Shape Make(string name);
    }
    
    /// <summary>
    /// We can get rid of this dependency problem by sacrificing a little type safety. Instead
    /// of giving ShapeFactory one method for every Shape derivative, we can give it only one
    /// make function that takes a string. For example:
    /// </summary>
    public class ShapeFactoryImplementation : ShapeFactory
    {
        public Shape Make(string name)
        {
            if (name.Equals("Circle"))
                return new Circle();
            else if (name.Equals("Square"))
                return new Square();
            else
                throw new Exception("ShapeFactory cannot create: " + name);
        }
    }

    /// <summary>
    /// One might argue that this is dangerous, because callers who misspell the name of a
    /// shape will get a runtime error istead of a compile-time error. This is true. However,
    /// if you are writing the appropriate unit tests and are applying test-driven development,
    /// you'll catch these runtime errors long before they become problems.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
