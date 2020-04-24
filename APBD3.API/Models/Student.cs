using System;

namespace APBD3.API.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string IndexName { get; }
        public int EnrollmentId { get; set; }
        public DateTime BirthDate { get; set; }
        

        public Student(string id, string firstName, string lastName, string indexName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IndexName = indexName;
        }

        public Student(string firstName, string lastName, string indexName, DateTime birthDate, int enrollmentId)
        {
            var random = new Random();
            Id = $"s{random.Next(int.MaxValue)}";
            FirstName = firstName;
            LastName = lastName;
            IndexName = indexName;
            BirthDate = birthDate;
            EnrollmentId = enrollmentId;
        }
    }
}