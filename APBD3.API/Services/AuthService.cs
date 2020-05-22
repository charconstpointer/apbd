using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using APBD3.API.Persistence;
using APBD3.API.Persistence.Interfaces;
using APBD3.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APBD3.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly string _key;

        public AuthService(IStudentRepository studentRepository, IConfiguration configuration)
        {
            _studentRepository = studentRepository;
            _key = configuration["Key"];
        }

        /// <summary>
        /// Metoda zwraca dynamica, pozniewaz przy pierwszym logowaniu tworze rowniez refresh token dla danego klienta,
        /// Jest to brzydkie i niesemantyczne, ale kto by sie tym przejmowal
        /// </summary>
        /// <param name="index"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                var refreshToken = Guid.NewGuid();
                var hashedPassword = HashPassword(128, student.Password);
                await _studentRepository.SetPassword(index, hashedPassword.Hash, hashedPassword.Salt, refreshToken);
                return new {Token = CreateToken(student.IndexName, _key), RefreshToken = refreshToken};
            }

            if (CredentialsAreCorrect(password, student.Password, student.Salt))
            {
                return new {Token = CreateToken(student.IndexName, _key)};
            }

            throw new Exception(":(");
        }

        public async Task<dynamic> RefreshToken(string index, Guid refreshToken)
        {
            var student = await _studentRepository.FindById(index);
            if (student is null)
            {
                throw new Exception("Student not found");
            }

            if (student.RefreshToken == refreshToken)
            {
                return CreateToken(student.IndexName, _key);
            }

            throw new Exception("Refresh token is not valid");
        }

        private string CreateToken(string username, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "apbd",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var wt = tokenHandler.WriteToken(token);
            return wt;
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