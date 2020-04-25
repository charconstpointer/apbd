using System.Threading.Tasks;
using APBD3.API.Models;
using APBD3.API.Persistence.Interfaces;

namespace APBD3.API.Persistence
{
    public class StudiesRepository : IStudiesRepository
    {
        public async Task CreateStudentEnrollment(Student student, string studiesName)
        {
            throw new System.NotImplementedException();
        }
    }
}