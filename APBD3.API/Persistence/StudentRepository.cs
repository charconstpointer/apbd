﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using APBD3.API.Models;
using Microsoft.Extensions.Configuration;

namespace APBD3.API.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("apbd");
        }

        public async Task<Student> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> FindEnrollments(string id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT Enrollment.* FROM Enrollment " +
                              "JOIN Student " +
                              "ON Student.IdEnrollment = Enrollment.IdEnrollment " +
                              "WHERE Student.IndexNumber = @id "
            };
            command.Parameters.AddWithValue("id", id);
            var enrollments = new List<Enrollment>();
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var semester = int.Parse(reader["Semester"].ToString());
                var idStudy = int.Parse(reader["IdStudy"].ToString());
                var identifier = int.Parse(reader["IdEnrollment"].ToString());
                var startDate = DateTime.Parse(reader["StartDate"].ToString());
                var enrollment = new Enrollment(identifier, semester, idStudy, startDate);
                enrollments.Add(enrollment);
            }

            return enrollments;
        }

        public async Task<IEnumerable<Student>> FindAll()
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM Student"
            };
            var students = new List<Student>();
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var firstName = reader["FirstName"].ToString();
                var lastName = reader["LastName"].ToString();
                var identifier = reader["IndexNumber"].ToString();
                var birthDate = reader["BirthDate"].ToString();
                var student = new Student(identifier, firstName, lastName, birthDate);
                students.Add(student);
            }

            return students;
        }

        public async Task Add(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> Find(Func<Student, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}