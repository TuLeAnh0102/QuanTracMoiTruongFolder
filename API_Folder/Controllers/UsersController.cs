using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API_Folder.Dtos;
using API_Folder.Services;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using API_Folder.Model;
using API_Folder.Model.Response;
using API_Folder.Repository;
using System.Data;
using API_Folder.Helpers;
using Microsoft.Extensions.Options;
using API_Folder.Model.NhanVien;

namespace API_Folder.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("api/users/getall")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("api/nhan-vien/login")]
        public JToken Login(string username, string password)
        {
            var response = new ResponseSingle();
            var userResponse = _userService.Login(username, password);
            if (!userResponse.success)
                response = userResponse;
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userResponse.data.ma_nhan_vien_kc.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var data = new
                {
                    token = tokenString
                };

                response.success = true;
                response.data = data;
            }
            return JsonHelper.ToJson(response);
        }

        [AllowAnonymous]
        [HttpPost("api/nhan-vien/cap-nhat-thong-tin")]
        public JToken CapNhatThongTinNhanVien(NV_CUD_NHAN_VIEN_IN obj)
        {
            return _userService.CapNhatThongTin(obj);
        }

        [AllowAnonymous]
        [HttpPost("api/nhan-vien/reset-pass")]
        public JToken ResetPass(string username, string password, string key)
        {
            return _userService.ResetPass(username,password, key);
        }

        [HttpGet("api/nhan-vien/danh-sach-nhan-vien")]
        public JToken DanhSachNhanVien()
        {
            return _userService.DanhSachNhanVien();
        }

        [HttpGet("api/nhan-vien/thong-tin-nhan-vien")]
        public JToken ThongTinNhanVien(int ma_nhan_vien_kc)
        {
            return _userService.ThongTinNhanVien(ma_nhan_vien_kc);
        }
    }
}