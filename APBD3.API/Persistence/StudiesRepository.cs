using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
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

        public async Task<Enrollment> CreateStudentEnrollment(Student student, string studiesName)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var transaction = connection.BeginTransaction();
            Study study = null;
            var enrollment = new Enrollment();
            await using var findStudiesCommand = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM Studies " +
                              "WHERE Studies.Name = @studiesName",
                Parameters =
                {
                    new SqlParameter("studiesName", studiesName)
                },
                Transaction = transaction
            };
            var studies = await findStudiesCommand.ExecuteReaderAsync();
            if (!studies.HasRows)
            {
                await transaction.RollbackAsync();
                throw new StudiesNotFoundException("Studies not found");
            }

            while (studies.Read())
            {
                var name = studies["Name"].ToString();
                var studyId = studies["IdStudy"].ToString();
                study = new Study {Name = name, IdStudy = int.Parse(studyId ?? "-1")};
            }

            await studies.CloseAsync();
            await studies.DisposeAsync();
            await using var findStudentsEnrollments = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM Enrollment " +
                              "WHERE Enrollment.IdStudy = @id " +
                              "AND Enrollment.Semester = 1",
                Parameters =
                {
                    new SqlParameter("id", study.IdStudy)
                },
                Transaction = transaction
            };
            var studentEnrollments = await findStudentsEnrollments.ExecuteReaderAsync();
            if (!studentEnrollments.HasRows)
            {
                await studentEnrollments.CloseAsync();
                await studentEnrollments.DisposeAsync();
                enrollment = new Enrollment {Semester = 1, IdStudy = study.IdStudy, StartDate = DateTime.UtcNow};
                await using var createEnrollment = new SqlCommand
                {
                    Transaction = transaction,
                    Connection = connection,
                    CommandText =
                        "INSERT INTO Enrollment (Semester, IdStudy, StartDate) VALUES (@semester, @studyId, @startDate) SELECT SCOPE_IDENTITY()",
                    Parameters =
                    {
                        new SqlParameter("semester", enrollment.Semester),
                        new SqlParameter("studyId", enrollment.IdStudy),
                        new SqlParameter("startDate", enrollment.StartDate),
                    }
                };
                var enrollmentId = (int) (decimal) await createEnrollment.ExecuteScalarAsync();
                enrollment.IdEnrollment = enrollmentId;
            }
            else
            {
                while (await studentEnrollments.ReadAsync())
                {
                    enrollment.IdEnrollment = (int) studentEnrollments["IdEnrollment"];
                    enrollment.Semester = (int) studentEnrollments["Semester"];
                    enrollment.IdStudy = (int) studentEnrollments["IdStudy"];
                    enrollment.StartDate = (DateTime) studentEnrollments["StartDate"];
                }
            }

            await using var addEnrollmentForStudent = new SqlCommand
            {
                Transaction = transaction,
                Connection = connection,
                CommandText =
                    "INSERT INTO Student VALUES (@indexNumber, @firstName, @lastName, @birthDate, @idEnrollment)"
            };
            addEnrollmentForStudent.Parameters.AddWithValue("indexNumber", student.IndexName);
            addEnrollmentForStudent.Parameters.AddWithValue("firstName", student.FirstName);
            addEnrollmentForStudent.Parameters.AddWithValue("lastName", student.LastName);
            addEnrollmentForStudent.Parameters.AddWithValue("birthDate", student.BirthDate);
            addEnrollmentForStudent.Parameters.AddWithValue("idEnrollment", enrollment.IdEnrollment);
            addEnrollmentForStudent.ExecuteScalar();

            await studentEnrollments.CloseAsync();
            await studentEnrollments.DisposeAsync();
            await transaction.CommitAsync();
            return enrollment;
        }

        public async Task<bool> EnrollmentExists(string studies, int semester)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM Enrollment " +
                              "JOIN Studies " +
                              "ON Enrollment.IdStudy = Studies.IdStudy " +
                              "WHERE Studies.Name = @studies " +
                              "AND Enrollment.Semester = @semester ",
                Parameters =
                {
                    new SqlParameter("studies", studies),
                    new SqlParameter("semester", semester)
                }
            };
            await connection.OpenAsync();
            var enrollments = await command.ExecuteReaderAsync();
            return enrollments.HasRows;
        }

        public async Task<Enrollment> PromoteStudents(string studies, int semester)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand
            {
                CommandText = "sp_promote_students",
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("studies", studies),
                    new SqlParameter("semester", semester),
                    new SqlParameter("updatedEnrollmentId", -1)
                }
            };
            command.Parameters[2].Direction = ParameterDirection.Output;

            await connection.OpenAsync();
            await command.ExecuteReaderAsync();
            if (!int.TryParse(command.Parameters[2].Value.ToString(), out var newEnrollmentId))
            {
                throw new EnrollmentNotFoundException();
            }

            await using var findEnrollmentById = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM Enrollment " +
                              "WHERE Enrollment.IdEnrollment = @id",
                Parameters =
                {
                    new SqlParameter("id", newEnrollmentId)
                }
            };
            var enrollmentReader = await findEnrollmentById.ExecuteReaderAsync();
            var enrollment = new Enrollment();
            while (await enrollmentReader.ReadAsync())
            {
                enrollment.IdEnrollment = (int) enrollmentReader["IdEnrollment"];
                enrollment.Semester = (int) enrollmentReader["Semester"];
                enrollment.IdStudy = (int) enrollmentReader["IdStudy"];
                enrollment.StartDate = (DateTime) enrollmentReader["StartDate"];
            }

            return enrollment;

        }
    }
}