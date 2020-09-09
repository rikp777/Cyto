using Domain.Entities;
using Domain.Resources;

namespace Domain.Requests
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static UserEntity ToEntity(UserRequest user)
        {
            return new UserEntity()
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}