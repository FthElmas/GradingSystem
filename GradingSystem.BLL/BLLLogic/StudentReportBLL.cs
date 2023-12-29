using DocumentFormat.OpenXml.Office2010.ExcelAc;
using FluentValidation.Results;
using GradingSystem.BLL.Common.Mapper;
using GradingSystem.BLL.Handler.StudentMark;
using GradingSystem.BLL.Handler.StudentReport;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.Student;
using GradingSystem.DAL.DTOs.StudentMark;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Services
{
    public class StudentReportBLL : IStudentReportDAL
    {
        StudentReportDAL reportDAL;
        ValidateStudentReport validation;
        ValidateStudentMark validationMark;
        public StudentReportBLL()
        {
            reportDAL = new StudentReportDAL();
            validation = new ValidateStudentReport();
            validationMark = new ValidateStudentMark();
        }

        public bool AddReportWithMark(StudentMarkAddDTO studentMark, int selectedWeek ,Guid StudentID)
        {
            MyMapper<StudentMarkAddDTO, StudentMark> mapper = new MyMapper<StudentMarkAddDTO, StudentMark>();
            var data = mapper.Map(studentMark);
            ValidationResult result = validationMark.Validate(data);
            if(result.IsValid)
            {
                return reportDAL.AddReportWithMark(data, selectedWeek, StudentID);
            }
            else
            {
                return false;
            }
        }

        public bool AddStudentReport(StudentReportAddDTO studentReport)
        {
            MyMapper<StudentReportAddDTO, StudentReport> mapper = new MyMapper<StudentReportAddDTO, StudentReport>();
            var data = mapper.Map(studentReport);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid)
            {
                return reportDAL.AddStudentReport(data);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool DeleteStudentReport(StudentReportUpdateDTO studentReport)
        {
            MyMapper<StudentReportUpdateDTO, StudentReport> mapper = new MyMapper<StudentReportUpdateDTO, StudentReport>();
            var data = mapper.Map(studentReport);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid)
            {
                return reportDAL.DeleteStudentReport(data);
            }
            else
            {
                throw new Exception();
            }
        }

        public List<StudentReportSelectDTO> GetAllReportInAllWeeks()
        {
            throw new NotImplementedException();
        }

        public List<StudentReportSelectDTO> GetAllReportInSelectedWeek(int selectedWeek)
        {
            if(selectedWeek != 0)
            {
                var data = reportDAL.GetAllReportInSelectedWeek(selectedWeek);

                return data;
            }
            else
            {
                throw new Exception();
            }
        }

        public List<StudentReportSelectDTO> GetAllReportInSelectedWeekOfStudent(int selectedWeek, Guid studentID)
        {
            
            var data = reportDAL.GetAllReportInSelectedWeekOfStudent(selectedWeek, studentID);
            if(data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
            
        }

        public List<List<StudentReportSelectDTO>> GetAllReportInSelectedWeekOfStudent(List<StudentSelectDTO> students, int selectedWeek)
        {
            var list = new List<List<StudentReportSelectDTO>>();

            foreach (var item in students)
            {

                var data = reportDAL.GetAllReportInSelectedWeekOfStudent(selectedWeek ,item.StudentID);

                list.Add(data);
            }
            return list;

        }

        public List<List<StudentReportSelectDTO>> GetAllReportInThisWeekOfStudent(List<StudentSelectDTO> students)
        {
            var list = new List<List<StudentReportSelectDTO>>();

            foreach (var item in students)
            {

                var data = reportDAL.GetAllReportInThisWeekOfStudent(item.StudentID);
                list.Add(data);
            }
            return list;
        }

        public List<List<StudentReportSelectDTO>> GetAllReportInTodayOfStudent(List<StudentSelectDTO> students)
        {
            var list = new List<List<StudentReportSelectDTO>>();
            MyMapper<StudentReport, StudentReportSelectDTO> mapper = new MyMapper<StudentReport, StudentReportSelectDTO>();
            
            
            foreach (var item in students)
            {
                var data = reportDAL.GetAllReportInTodayOfStudent(item.StudentID);
                list.Add(data);
            }
            return list;
        }

        public List<StudentReportSelectDTO> GetAllReportInTodayOfStudent(Guid studentID)
        {
            if(studentID != Guid.Empty)
            {
                var data = reportDAL.GetAllReportInTodayOfStudent(studentID);
                return data;
            }
            else
            {
                throw new Exception();
            }
        }

        public StudentReportUpdateDTO GetById(int ID)
        {
            var data = reportDAL.GetById(ID);
            if(data != null)
            {
                MyMapper<StudentReport, StudentReportUpdateDTO> mapper = new MyMapper<StudentReport, StudentReportUpdateDTO>();
                return mapper.Map(data);
            }
            else
            {
                return null;
            }
        }

        public bool UpdateStudentReport(StudentReportUpdateDTO student)
        {
            MyMapper<StudentReportUpdateDTO, StudentReport> mapper = new MyMapper<StudentReportUpdateDTO, StudentReport>();
            var data = mapper.Map(student);
            ValidationResult result = validation.Validate(data);
            if(result.IsValid)
            {
                return reportDAL.UpdateStudentReport(data);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
