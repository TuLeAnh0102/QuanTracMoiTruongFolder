using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using API_Folder.Repository;

namespace API_Folder.Controllers
{
    [Authorize]
    [ApiController]
    public class KhachHangController : Controller
    {
        [HttpGet("api/khach-hang/ds-khach-hang")]
        public JToken Index()
        {
            return KhachHangRepository.GET_DANH_SACH_KHACH_HANG();
        }
    }
}
