using Dapper;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Concrete.DapperGenericRepository.Repository;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Logic
{
    public class ProjectDAL
    {
        private readonly ConnectionHelper _connectionHelper;
        public ProjectDAL()
        {
            _connectionHelper = new ConnectionHelper();
        }
        public bool AddProject(Project project)
        {
            try
            {
                GenericRepository<Project> repo = new GenericRepository<Project>();
                repo.Add(project);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Project GetById(int ID)
        {
            GenericRepository<Project> repository = new GenericRepository<Project>();
            return repository.GetById(ID);
        }

        public bool UpdateProject(Project project)
        {
            try
            {
                GenericRepository<Project> repo = new GenericRepository<Project>();
                repo.Update(project);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckProject(Project project)
        {
            using var conn = _connectionHelper.CreateConnection();
            string query = "select * from Project where ProjectName = @ProjectName";
            if(conn.QueryFirstOrDefault(query, new {ProjectName = project.ProjectName}))
            {
                return true;
            }
            return false;
        }

        public bool DeleteProject(Project project)
        {
            try
            {
                GenericRepository<Project> repo = new GenericRepository<Project>();
                repo.SoftDelete(project);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Project> GetAll()
        {
            GenericRepository<Project> repo = new GenericRepository<Project>();
            return repo.GetAll();
        }
    }
}
