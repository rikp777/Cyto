using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Resources;

namespace Domain.Requests
{
    public class UserRequest
    {
        [Required(ErrorMessage = "A user name must be provided!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "An email of user must be provided!")]
        [EmailAddress]
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