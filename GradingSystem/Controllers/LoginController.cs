using GradingSystem.BLL.Services;
using GradingSystem.Core.Entity;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.DTOs.User;
using GradingSystem.DAL.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GradingSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _conf;
        private readonly IUserDAL _dal;
        public LoginController(IConfiguration conf, IUserDAL dal)
        {
            _conf = conf;
            _dal = dal;
        }
        public IActionResult Index()
        {
            var data = new UserBLL().GetAll();
            return View();
        }



        public async Task<IActionResult> Login([FromBody] UserSelectDTO dto)
        {
            var doExist = await _dal.Login(dto.Username, dto.Password);

            if (doExist == null)
            {
                return null;
            }

            var issuer = _conf["JwtIssuer"];
            var audience = _conf["JwtAudience"];

            var desc = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddMinutes(20),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dto.Username),
                    new Claim(ClaimTypes.Email, dto.Email)
                }),
                Issuer = issuer,
                Audience = audience,
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["apikey"])), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(desc);
            var kullaniciIcinUretilmisTokenDegeri = tokenhandler.WriteToken(token);

            return Ok(kullaniciIcinUretilmisTokenDegeri);
        }

        //public async Task<IActionResult> Register([FromBody] UserAddDTO dto)
        //{
        //    var kaydedilmisKullanici = await _dal.AddUserWithTeacher(new User() { UserName = dto.UserName }, dto.Password);
        //    return Ok(kaydedilmisKullanici);
        //}
    }
}
