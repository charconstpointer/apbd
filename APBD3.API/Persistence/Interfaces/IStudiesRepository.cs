using System;
using System.Threading.Tasks;
using APBD3.API.Models;

namespace APBD3.API.Persistence.Interfaces
{
    public interface IStudiesRepository
    {
        Task CreateStudentEnrollment(Student student, string studiesName);
    }
}