using FluentValidation;
using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.Project;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Project;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class ProjectBLL : IProjectDAL
    {
        ProjectDAL projectDAL;
        ValidateProject addValidation;
        ValidateProjectUpdate updaateValidation;
        public ProjectBLL()
        {
            projectDAL = new ProjectDAL();
            addValidation = new ValidateProject();
            updaateValidation = new ValidateProjectUpdate();
        }

        public bool AddProject(ProjectAddDTO project)
        {
            ValidationResult result = addValidation.Validate(project);
            MyMapper<ProjectAddDTO, Project> mapper = new MyMapper<ProjectAddDTO, Project>();
            return projectDAL.AddProject(mapper.Map(project));
        }

        public bool UpdateProject(ProjectUpdateDTO project)
        {
            ValidationResult result = updaateValidation.Validate(project);
            MyMapper<ProjectUpdateDTO, Project> mapper = new MyMapper<ProjectUpdateDTO, Project>();
            if (result.IsValid)
            {
                return projectDAL.UpdateProject(mapper.Map(project));
            }
            else
            {
                throw new Exception();
            }
        }

        public bool DeleteProject(ProjectUpdateDTO project)
        {
            MyMapper<ProjectUpdateDTO, Project> mapper = new MyMapper<ProjectUpdateDTO, Project>();
            if (project.ProjectID != 0)
            {
                return projectDAL.DeleteProject(mapper.Map(project));
            }
            else
            {
                throw new Exception();
            }
        }

        public List<ProjectSelectDTO> GetAll()
        {
            MyMapper<Project, ProjectSelectDTO> mapper = new MyMapper<Project, ProjectSelectDTO>();
            List<ProjectSelectDTO> project = new List<ProjectSelectDTO>();
            var data = projectDAL.GetAll().ToList();
            data.ForEach(a => project.Add(mapper.Map(a)));
            return project;
        }

        public ProjectUpdateDTO GetById(int ID)
        {
            MyMapper<Project, ProjectUpdateDTO> mapper = new MyMapper<Project, ProjectUpdateDTO>();
            var data = projectDAL.GetById(ID);
            if(data != null)
            {
                return mapper.Map(data);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
