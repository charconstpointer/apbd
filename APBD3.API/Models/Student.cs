using System;

namespace APBD3.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string IndexName { get; }

        public Student(string firstName, string lastName, string indexName)
        {
            var random = new Random();
            Id = random.Next(int.MaxValue);
            FirstName = firstName;
            LastName = lastName;
            IndexName = indexName;
        }
    }
}