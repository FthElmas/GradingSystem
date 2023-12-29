using GradingSystem.DTO.DTOs.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using GradingSystem.DAL.DTOs.StudentReport;
using GradingSystem.DTO.DTOs.StudentProject;
using GradingSystem.DTO.DTOs.StudentQuiz;

namespace GradingSystem.BLL.Common
{
    public static class MainReportPageToExcel
    {
        public static void ConvertToExcelAndSendEmail(MainReportPage reportPage)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Rapor");

                
                worksheet.Cells[1, 1].Value = "Adı";
                worksheet.Cells[1, 2].Value = "Soyadı";
                worksheet.Cells[1, 3].Value = "Raporlar";
                worksheet.Cells[1, 4].Value = "Projeler";
                worksheet.Cells[1, 5].Value = "Quiz'ler";

                int rowIndex = 2;

                foreach (var student in reportPage.Student)
                {
                    worksheet.Cells[rowIndex, 1].Value = student.StudentName;
                    worksheet.Cells[rowIndex, 2].Value = student.StudentSurname;

                    var report = reportPage.StudentReport[reportPage.Student.IndexOf(student)];
                    if (report != null)
                    {
                        var reportColumn = worksheet.Cells[rowIndex, 3];
                        foreach (var reportItem in report)
                        {
                            reportColumn.Value += $"{reportItem.ReportName} {reportItem.Report}\n";
                        }
                    }

                    var project = reportPage.StudentProject[reportPage.Student.IndexOf(student)];
                    if (project != null)
                    {
                        var projectColumn = worksheet.Cells[rowIndex, 4];
                        foreach (var projectItem in project)
                        {
                            projectColumn.Value += $"{projectItem.Description}\n";
                        }
                    }

                    var quiz = reportPage.StudentQuiz[reportPage.Student.IndexOf(student)];
                    if (quiz != null)
                    {
                        var quizColumn = worksheet.Cells[rowIndex, 5];
                        foreach (var quizItem in quiz)
                        {
                            quizColumn.Value += $"{quizItem.Description}\n";
                        }
                    }

                    rowIndex++;
                }

                var fileName = "rapor.xlsx";
                var filePath = Path.Combine("C:\\Users\\Fatih\\OneDrive\\Resimler\\Ekran Görüntüleri", fileName);
                package.SaveAs(new FileInfo(filePath));

                //SendEmail(toAddresses, filePath, email, password);
            }
        }

        public static void SendEmail(string[] toAddresses, string attachmentPath, string email, string password)
        {
            var fromAddress = new MailAddress(email, "Gönderen Adı");

            var toAddressList = new List<MailAddress>();
            foreach (var toAddress in toAddresses)
            {
                toAddressList.Add(new MailAddress(toAddress));
            }

            const string subject = "Rapor Dosyası";
            const string body = "İyi günler, rapor dosyanız ektedir.";

            using (var message = new MailMessage()
            {
                From = fromAddress,
                Subject = subject,
                Body = body
            })
            {

                message.Attachments.Add(new Attachment(attachmentPath));

                foreach (var toAddress in toAddressList)
                {
                    message.To.Add(toAddress);
                }

                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(email, password);
                    smtp.EnableSsl = true;

                    smtp.Send(message);
                }
            }
        }

    }
}
