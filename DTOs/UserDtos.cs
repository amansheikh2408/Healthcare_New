namespace Healthcare.DTOs
{
    public class UserDtos
    {
        public record UserDto(int Id, string Email, string Role);
    }
}
