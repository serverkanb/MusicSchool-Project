using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using AppCore.Results;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services
{
    public interface IUserService : IService<UserModel>
    {

    }

    public class UserService : IUserService
    {
        private readonly UserRepoBase _userRepo;

        public UserService(UserRepoBase userRepo)
        {
            _userRepo = userRepo;
        }

        public Result Add(UserModel model)
        {
            if (_userRepo.Exists(u => u.UserName.ToLower().Contains(model.UserName.ToLower())))
            {
                return new ErrorResult("The user with same name exists!");
            }
            User entity = new User()
            {
                IsActive = model.IsActive,
                Password = model.Password,
                UserName = model.UserName,
                RoleId = model.RoleId,

            };
            _userRepo.Add(entity);
            return new SuccessResult();


        }


        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IQueryable<UserModel> Query()
        {
            return _userRepo.Query(u => u.Role).Select(u => new UserModel()
            {
                
                IsActive = u.IsActive,
                Id = u.Id,
                Password = u.Password,
                RoleId = u.RoleId,
                UserName = u.UserName,
                RoleName = u.Role.Name,
            });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
