using GradingSystem.Core.Entity;
using GradingSystem.DAL.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IProjectDAL
    {
        bool AddProject(ProjectAddDTO project);
        bool UpdateProject(ProjectUpdateDTO project);
        bool DeleteProject(ProjectUpdateDTO project);
        List<ProjectSelectDTO> GetAll();
        ProjectUpdateDTO GetById(int ID);
    }
}
