using System.Collections.Generic;
using System.Threading.Tasks;
using APBD.EF.CodeFirst.Commands;
using APBD.EF.CodeFirst.DTO;

namespace APBD.EF.CodeFirst.Services
{
    public interface IDoctorsService
    {
        Task CreateDoctor(CreateDoctorCommand createDoctorCommand);
        Task UpdateDoctor(UpdateDoctorCommand updateDoctorCommand);
        Task DeleteDoctor(DeleteDoctorCommand deleteDoctorCommand);
        Task<IEnumerable<DoctorDto>> GetDoctors(GetDoctorsCommand getDoctorsCommand);
        Task<DoctorDto> GetDoctor(GetDoctorCommand getDoctorCommand);
    }
}