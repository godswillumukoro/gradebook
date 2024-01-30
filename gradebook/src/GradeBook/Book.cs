using System.Collections.Generic;

namespace GradeBook
{
    public class Book
    {
        private List<double> grades; // a field
        public string Name;

        // explicit constructor
        public Book(string name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public Statistics GetStatistics()
        {
            var result = new Statistics();

            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            foreach (double grade in grades)
            {
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
                result.Average += grade;
            }

            result.Average /= grades.Count;
            result.Average = Math.Round(result.Average, 1);


            return result;
        }
    }
}