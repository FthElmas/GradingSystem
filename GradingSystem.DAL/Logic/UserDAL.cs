using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.Teacher;
using GradingSystem.DAL.DTOs.User;
using GradingSystem.DAL.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class UserDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public UserDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }


        public User AddUser(User user, string password)
        {
            try
            {
                byte[] passHash, passSalt;

                CreatePassword(password, out passHash, out passSalt);
                user.PasswordHash = passHash;
                user.PasswordSalt = passSalt;
                using var conn = _connectionHelper.CreateConnection();
                string query = "INSERT INTO [User] (UserID, Username, PasswordHash, PasswordSalt ,Email, CreatedDate, IsActive) VALUES (@userID, @userName, @passwordHash, @passwordSalt, @email, @createdDate, @isActive)";
                return conn.QueryFirstOrDefault<User>(query, new { userID = user.UserID, userName = user.Username, passwordHash = user.PasswordHash, passwordSalt = user.PasswordSalt, email = user.Email, createdDate = user.CreatedDate, isActive = user.IsActive });
            }
            catch
            {
                return null;
            }
            
        }
        public bool CheckUser(User user)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from [User] where Username = @Username";
            if (conn.QueryFirstOrDefault(query, new { Username = user.Username }) == null)
            {
                return true;
            }
            return false;
        }
        public User LoggedUser(string username)
        {
            try
            {
                using var conn = _connectionHelper.CreateConnection();
                string query = "Select * from [User]\r\nwhere Username = @Username";
                return conn.QueryFirstOrDefault<User>(query, new { Username = username});
            }
            catch
            {
                return null;
            }
        }

        public bool AddUserWithRepo(User user)
        {
            try
            {
                GenericRepository<User> repo = new GenericRepository<User>();
                repo.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
            

        }

        public bool AddUserWithTeacher(User user,string password ,Teacher teacher)
        {
            using var conn = _connectionHelper.CreateConnection();
            using(var transaction = conn.BeginTransaction())
            {
                try
                {
                    byte[] passHash, passSalt;

                    CreatePassword(password, out passHash, out passSalt);
                    user.PasswordHash = passHash;
                    user.PasswordSalt = passSalt;
                    string userQuery = "INSERT INTO [User] (UserID, Username, PasswordHash, PasswordSalt,Email, CreatedDate, IsActive) VALUES (@userID, @userName, @passwordHash, @passwordSalt ,@email, @createdDate, @isActive)";
                    conn.Execute(userQuery, new { userID = user.UserID, userName = user.Username, passwordHash = user.PasswordHash, passwordSalt = user.PasswordSalt ,email = user.Email, createdDate = user.CreatedDate, isActive = user.IsActive });

                    string teacherQuery = "INSERT INTO [Teacher] (TeacherID, TeacherName, TeacherSurname, UserID, CreatedDate, CreatedBy, IsActive) VALUES (@teacherID, @teacherName, @teacherSurname, @userID, @createdDate, @createdBy, @isActive)";
                    conn.Execute(teacherQuery, new { teacherID = teacher.TeacherID, teacherName = teacher.TeacherName, teacherSurname = teacher.TeacherSurname, userID = user.UserID, createdDate = teacher.CreatedDate, createdBy = user.UserID, isActive = teacher.IsActive });
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = LoggedUser(username);
            if(user == null)
            {
                return null;
            }

            if(!ControlPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }
            return user;

        }


        private void CreatePassword(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passSalt = hmac.Key;
            }
        }

        private bool ControlPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < passHash.Length; i++)
                {
                    if (passwordHash[i] != passHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void UpdateUserEmail(Guid _userID, string _email)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "update User set Email = @email where UserID = @userID";
            conn.Execute(query, new { email = _email, userID = _userID });
        }

        public void UpdateUserPassword(Guid _userID, string _password)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "update User set Password = @password where UserID = @userID";
            conn.Execute(query, new { password = _password, userID = _userID });
        }

        public bool UpdateUser(User user)
        {
            try
            {
                GenericRepository<User> repo = new GenericRepository<User>();
                repo.Update(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SoftDelete(User user)
        {
            try
            {
                GenericRepository<User> repo = new GenericRepository<User>();
                repo.SoftDelete(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                GenericRepository<User> repo = new GenericRepository<User>();
                
                return repo.GetAll().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
