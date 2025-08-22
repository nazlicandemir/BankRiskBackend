using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;






namespace BankRiskTracking.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IGenericRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public IResponse<UserCreateDto> CreateUser(UserCreateDto user)
        {
            if (user == null)
            {
               return ResponseGeneric<UserCreateDto>.Error("Kullanıcı adı bilgileri boş olamaz");
            }

            //Kullanucu adı boş olamaz 
            if (string.IsNullOrEmpty(user.Email))
            {
                return ResponseGeneric<UserCreateDto>.Error("E-posta adresi boş olamaz");
            }

            //email adresı zaten var mı dıye kontrol et
            var existingUser = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            if (existingUser != null)
            {
                return ResponseGeneric<UserCreateDto>.Error("Bu e-posta adresi zaten kullanılıyor.");
            }


            // Gelen şifre alanını hashle 
            var hashedPassword = HashPassword(user.Password); //BCrypt.Net.BCrypt.HashPassword(user.Password);


            //Gelen Dto'yu Entitiye dönüştürüyoruz
            var newUser = new User
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Password = hashedPassword, //şifeyi hashlemeden kaydediyoruz
               

            };

            _userRepository.Create(newUser);
            return ResponseGeneric<UserCreateDto>.Success(null, "Kullanıcı kaydı oluşturuldu");
        }

        public IResponse<string> LoginUser(UserLoginDto user)
        {
            if (user.Email == null || user.Password == null)
            {
                return ResponseGeneric<string>.Error("E-posta asresi ve şifre boş olamaz");
            }

            var checkUser = _userRepository.GetAll()
                .FirstOrDefault(x => x.Email == user.Email && x.Password == HashPassword(user.Password));

            
            if (checkUser == null)
            {
                return ResponseGeneric<string>.Error("E-mail veya şifre hatalı");
            }

            var generatedToken = GenerateJwtToken(checkUser);
            return ResponseGeneric<string>.Success(generatedToken, "Giriş başarılı");
        }


        private string HashPassword(string password)
        {

            //TODO: SecretKey'i confing dosyasindan al 
            string secretKey = "mJykD-8)(.}S=~/#>ElR<7-bn#Yg0!";
            using (var sha256 = SHA256.Create())
            {
                var combinedPassword = password + secretKey;
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
                var hashedPassword = Convert.ToBase64String(bytes);
                return hashedPassword;
            }

        }


        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name ),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email , user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials:creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}

