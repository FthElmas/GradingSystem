using FluentValidation.Results;
using GradingSystem.BLL.Common;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.Teacher;
using GradingSystem.BLL.Handler.User;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Teacher;
using GradingSystem.DAL.DTOs.User;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class UserBLL : IUserDAL
    {
        UserDAL userDAL;
        ValidateUser validation;
        ValidateTeacher validateTeacher;
        public UserBLL()
        {
            userDAL = new UserDAL();
            validation = new ValidateUser();
            validateTeacher = new ValidateTeacher();
        }

        public bool AddUserWithRepo(UserAddDTO user)
        {
            MyMapper<UserAddDTO, User> mapper = new MyMapper<UserAddDTO, User>();
            return userDAL.AddUserWithRepo(mapper.Map(user));
        }
        public bool UpdateUser(UserUpdateDTO user)
        {
            MyMapper<UserUpdateDTO, User> mapper = new MyMapper<UserUpdateDTO, User>();
            return userDAL.UpdateUser(mapper.Map(user));
        }
        public bool SoftDelete(UserUpdateDTO user)
        {
            MyMapper<UserUpdateDTO, User> mapper = new MyMapper<UserUpdateDTO, User>();
            return userDAL.SoftDelete(mapper.Map(user));
        }
        public List<UserSelectDTO> GetAll()
        {
            var data = userDAL.GetAll().ToList();
            MyMapper<User, UserSelectDTO> mapper = new MyMapper<User, UserSelectDTO>();
            var list = new List<UserSelectDTO>();
            data.ForEach(a => list.Add(mapper.Map(a)));
            return list;
        }
        public UserSelectDTO LoggedUser(string username)
        {
            var data = userDAL.LoggedUser(username);
            MyMapper<User, UserSelectDTO> mapper = new MyMapper<User, UserSelectDTO>();
            return mapper.Map(data);
        }
        public GeneralReturnType<User> AddUser(UserAddDTO user)
        {
            try
            {
                MyMapper<UserAddDTO, User> mapper = new MyMapper<UserAddDTO, User>();
                return new GeneralReturnType<User>()
                {
                    Datas = userDAL.AddUser(mapper.Map(user), user.Password),
                    Message = "Success",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new GeneralReturnType<User>()
                {
                    Datas = null,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }

        public bool AddUserWithTeacher(User user, string password, Teacher teacher)
        {
            ValidationResult result = validation.Validate(user);
            ValidationResult resultTeacher = validateTeacher.Validate(teacher);
            if(result.IsValid && resultTeacher.IsValid)
            {
                return userDAL.AddUserWithTeacher(user, password, teacher);
            }
            else
            {
                return false;
            }
        }

        public Task<User> Login(string username, string password)
        {
            if (username != null && password != null)
                return userDAL.Login(username, password);
            else
                return null;
        }
    }
}
