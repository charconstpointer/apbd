using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using APBD3.API.Exceptions;
using APBD3.API.Models;
using APBD3.API.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APBD3.API.Persistence
{
    public class StudiesRepository : IStudiesRepository
    {
        private readonly string _connectionString;

        public StudiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("apbd");
        }

        public async Task CreateStudentEnrollment(Student student, string studiesName)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM Studies " +
                              "WHERE Studies.Name = @studiesName"
            };
            await connection.OpenAsync();
            var transaction =  connection.BeginTransaction();
            command.Transaction = transaction;
            command.Parameters.AddWithValue("studiesName", studiesName);

            var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                throw new StudiesNotFoundException("Studies not found");
            }

            while (await reader.ReadAsync())
            {
                var name = reader["Name"]?.ToString();
                var study = new Study {Name = name};
                Console.WriteLine(study);
            }
        }
    }
}