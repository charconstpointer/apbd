namespace APBD.EF.CodeFirst.Commands
{
    public class UpdateDoctorCommand
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}