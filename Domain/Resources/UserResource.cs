using System;
using Domain.Entities;

namespace Domain.Resources
{
    public class UserResource
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static UserResource FromEntity(UserEntity user)
        {
            Console.WriteLine(user + "222");
            return new UserResource()
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}