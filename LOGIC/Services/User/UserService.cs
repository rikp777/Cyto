using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.User;
using Domain.Entities;
using Domain.Requests;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.User
{
    public class UserService : IGenericCrudService<UserResource, UserRequest>
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository(new DatabaseContext());
        }

        public UserResource GetById(int id) => UserResource.FromEntity(_userRepository.GetById(id));
        public List<UserResource> GetAll(int size, int page) =>_userRepository.GetAll()
            .Select(UserResource.FromEntity)
            .ToList();

        
        public bool Create(UserRequest user) =>_userRepository
            .Create(UserRequest.ToEntity(user));
        
        
        public bool Update(int id, UserRequest user) =>_userRepository
            .Update(id, UserRequest.ToEntity(user));
        
        
        public bool Delete(int id) => _userRepository
            .Delete(id);
        
        
    }
}