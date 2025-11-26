namespace CURD_API.Models.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class AuthDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
