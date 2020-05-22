using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using APBD3.API.Persistence;
using APBD3.API.Services.Interfaces;

namespace APBD3.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly StudentRepository _studentRepository;

        public AuthService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<dynamic> Login(string index, string password)
        {
            var student = await _studentRepository.FindById(index);
            if (student is null)
            {
                throw new Exception("Student not found");
            }

            //if first time login (password is empty in the database)
            if (string.IsNullOrEmpty(student.Password) && string.IsNullOrEmpty(student.Salt))
            {
                var hashedPassword = HashPassword(128, student.Password);
                await _studentRepository.SetPassword(index, hashedPassword.Hash, hashedPassword.Salt);
                //Generate and return token
                return "token";
            }

            if (CredentialsAreCorrect(password, student.Password, student.Salt))
            {
                return "token";
            }
            throw new Exception(":(");
        }

        private Password HashPassword(int size, string password, string salt = null)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            salt ??= Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            var hashSalt = new Password {Hash = hashPassword, Salt = salt};
            return hashSalt;
        }

        private bool CredentialsAreCorrect(string password, string hash, string salt)
        {
            var requestedPasswordHash = HashPassword(128, password, salt);
            return requestedPasswordHash.Hash == hash;
        }
    }
}