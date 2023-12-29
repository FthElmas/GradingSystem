using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Teacher;
using GradingSystem.DAL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IUserDAL
    {
        bool AddUserWithTeacher(User user, string password, Teacher teacher);
        Task<User> Login(string username, string password);
    }
}
