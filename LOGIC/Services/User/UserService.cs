using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Context;
using DAL.Interfaces;
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

        public UserService(IDatabaseContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public UserResource GetById(int id)
        {
            var userEntity = _userRepository.GetById(id);
            return userEntity == null ? null : UserResource.FromEntity(_userRepository.GetById(id));
        }


        public List<UserResource> GetAll() => _userRepository.GetAll()
            .Select(UserResource.FromEntity)
            .ToList();


        public bool Create(UserRequest user, HttpContext current = null) => _userRepository
            .Create(UserRequest.ToEntity(user));


        public bool Update(int id, UserRequest user, HttpContext current = null) => _userRepository
            .Update(id, UserRequest.ToEntity(user));


        public bool Delete(int id, HttpContext current = null) => _userRepository
            .Delete(id);

        public UserResource GetByName(string name)
        {
            var userEntity = _userRepository.GetByName(name);
            return userEntity == null ? null : UserResource.FromEntity(userEntity);
        }

        public UserResource GetByEmail(string email)
        {
            var userEntity = _userRepository.GetByEmail(email);
            return userEntity == null ? null : UserResource.FromEntity(_userRepository.GetByEmail(email));
        }
    }
}