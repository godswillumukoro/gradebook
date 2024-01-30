# C# .NET

## Starting a console project
`dotnet new console`

## Running a project
`dotnet run --project src\GradeBook`

Implicitly, the above code does this:

`dotnet restore`: Checks the csproj file to see if there are external packages referenced from the nuget package feed

`dotnet build`: Compiles the source code into binary format(.dll).

The binary file is stored inside the `bin\Debug` folder

The `obj` folder contains temp files for restore and build process.

The important files to keep:
- source code. eg. Program.cs
- csproj file. eg. GradeBook.csproj

With those files, you can build your program at any time and run it.

## Strongly Typed Language
C# is a strongly typed language, meaning every variable and method has a type

## Exceptions
An exception represents an error condition. When an error occurs:
1. A program can handle it and say "Yes I expected the error to occur"
2. The excption goes unhandled, making the program to crash

The .NET runtime would not allow the program to continue executing if there is an unhandled exception because the program might be in a faulty state, its operating under some erroneous condition.
```
static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            // Console.WriteLine("Hello, " + args[0] + "!");
            Console.WriteLine($"Hello, {args[0]}!");
        }
        else
        {
            Console.WriteLine("Hello there!");
        }
    }
```

## Implicit Typing
Using the `var`  it will figure out the type from the value. Note, `var` is not the same as it is in Javascript, as its a strongly typed variable, you cannot reassign a type `string` to a variable initially assigned the `double` type implicitly

## Array Initialization
```
var numbers = new double[3];

        numbers[0] = 10.5;
        numbers[1] = 5.5;
        numbers[2] = 5.0;
```

With the array initialization syntax:
```
var numbers = new double[] { 10.5, 5.5, 5.4 };
```

## Using the foreach loop
```
var numbers = new double[] { 10.5, 5.5, 5.4 };

var result = 0.0;
foreach (double number in numbers)
{
    result += number;
}
Console.WriteLine(result);
```

## List
A data structure like a stack or queue. Its stored in `System.Collections.Generic` namespace.
A List allows you keep a list of things around, like a list of floating point numbers.

## Classes and Objects
In C#, a class defines a new type which makes it possible to get work done.

Types are needed to get anything done in C#
`Console`, `List<double></double>`, `String` are all types. Types have different behaviours and store different information

## When to create a new class
When your method, variable has too much code inside it making it overly complex. A new class provides encapsulation inside a better abstraction for the complexity in one class.

Having your class defined in a namespace helps avoid conflits with similar names of other types
```
using System;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static void Main(string[] args)
        {
            var grades = new List<double>() { 10.5, 5.5, 5.4 };
            grades.Add(56.1);

            var result = 0.0;
            foreach (double number in grades)
            {
                result += number;
            }
            result /= (grades.Count);
            Console.WriteLine(Math.Round(result, 1));
        }
    }
}
```

## Things to consider when creating a class
1. What are the operations / behaviour of the class. ie. things it can do
2. What is the state / data that would be stored inside the class

## Access Modifiers
- public: code outside the class have access
- private: opposite of public
- static: Not associated with an object instance, but with the types they are defined inside of.

## Project Requirement
We need an electronic grade book to read the scores of an individual student and then compute some simple statistics from the scores.

The grades are entered as floating point numbers from 0 to 100 and the statistics should show us the highest grade, the lowest grade, and the average grade

### Solution
```
// Program.cs

using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            var book = new Book("Paul School");
            book.AddGrade(45.9);
            book.AddGrade(25.9);
            book.AddGrade(105.9);

            book.ShowStatistics();

            // var grades = new List<double>() { 10.5, 5.5, 5.4, 200, 0.2, -50 };
        }
    }
}
```

```
// Book.cs

using System.Collections.Generic;

namespace GradeBook
{
    class Book
    {
        private List<double> grades; // a field
        private string name;

        // explicit constructor
        public Book(string name)
        {
            grades = new List<double>();
            this.name = name;
        }
        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public void ShowStatistics()
        {
            var average = 0.0;
            var highGrade = double.MinValue;
            var lowGrade = double.MaxValue;

            foreach (double number in grades)
            {
                highGrade = Math.Max(number, highGrade);
                lowGrade = Math.Min(number, lowGrade);
                average += number;
            }

            average /= (grades.Count);
            Console.WriteLine($"Highest Grade: {highGrade} \nLowest Grade: {lowGrade}  \nAverage: {Math.Round(average, 1)}");
        }
    }
}
```

## How to write Unit Tests
We write tests for our code to see if it behaves correctly, to learn about software design and write better software and to experiment and explore a language and environment.

- Verify: Edge conditions
- Investigate: Explore software behaviour when things go wrong
- Units: Test individual methods
- Automated: Test runner executes your tests and provides reports if the tets passed or failed.

### Configuring Unit Tests
Create dir/file:
```
cd test

mkdir GradeBook.Tests

cd GradeBook.Tests

dotnet new xunit

dotnet test

dotnet add reference ..\..\src\GradeBook\GradeBook.csproj
```

### Test File
```
using System;
using Xunit;

namespace GradeBook.Tests;

public class BookTests
{
    [Fact] //attribute
    public void Test1()
    {
        // arrange
        var book = new Book("");
        book.AddGrade(89.1);
        book.AddGrade(90.5);
        book.AddGrade(77.3);

        // act
        var result = book.GetStatistics();

        // assert
        Assert.Equal(85.6, result.Average, 1);
        Assert.Equal(90.5, result.High, 1);
        Assert.Equal(77.3, result.Low, 1);
    }
}
```

## Working with Reference and Value Types
| Reference Type         | Value Type           |
| ---------------------- | :-------------------:|
| Stores a value that represents a location in memory | Simply stores the value |

### Reference Type
`var b = new Book("Grades)`

The code above creates a space in memory for the variable `b`

By the time that statement finishes executing, there would be a value in the variable `b`. This value represents a memory location (eg. 1072).

So the variable `b` really is a reference to where the `book` object leaves inside the memory of your computer.

### Value Type
`var x = 3`

`3` is an integer which isn't a reference type, but a `value type`

Just like reference types, value types still create space in memory for varibale `x`. But instead of storing a reference, its value `3` is whats being stored.

## Solution File
Makes a system to build and run tests for different projects

### Adding a solution file
```
dotnet new sln

dotnet sln add src\GradeBook\GradeBook.csproj

dotnet sln add test\GradeBook.Tests\GradeBook.Tests.csproj

dotnet build //builds and test the program
```