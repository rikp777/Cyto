using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Context;
using DAL.Repository.Interfaces;
using Domain.Entities;

namespace DAL.Repository.User
{
    public class UserProjectRepository : IGenericRelationshipRepository<ProjectEntity>
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserEntity> _dbSetUsers;
        private readonly DbSet<ProjectEntity> _dbSetProjects;

        public UserProjectRepository(DatabaseContext context)
        {
            _context = context;
            _dbSetUsers = context.Set<UserEntity>();
            _dbSetProjects = context.Set<ProjectEntity>();
        }


        public bool Attach(int userId, int projectId)
        {
            var user = _dbSetUsers.Find(userId);
            var project = _dbSetProjects.Find(projectId);

            user?.Projects.Add(project);

            return _context.Save();
        }

        public bool Detach(int userId, int projectId)
        {
            var user = _dbSetUsers.Find(userId);
            var project = _dbSetProjects.Find(projectId);

            user?.Projects.Remove(project);

            return _context.Save();
        }

        public ProjectEntity GetById(int userId, int projectId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.Projects.First(p => p.Id == projectId);
        }

        public List<ProjectEntity> GetAll(int userId)
        {
            var user = _dbSetUsers.Find(userId);

            return user?.Projects.ToList();
        }
    }
}