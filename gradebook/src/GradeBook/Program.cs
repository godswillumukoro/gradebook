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

            var stats = book.GetStatistics();

            Console.WriteLine($"Highest Grade: {stats.High} \nLowest Grade: {stats.Low}  \nAverage: {Math.Round(stats.Average, 1)}");

            // var grades = new List<double>() { 10.5, 5.5, 5.4, 200, 0.2, -50 };
        }
    }
}