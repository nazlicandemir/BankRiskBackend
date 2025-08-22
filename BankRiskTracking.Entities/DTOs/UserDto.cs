namespace BankRiskTracking.Entities.DTOs
{
    public  class UserCreateDto
    {
        
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
