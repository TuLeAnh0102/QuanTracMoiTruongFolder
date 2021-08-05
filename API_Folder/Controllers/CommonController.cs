using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using API_Folder.Model;
using API_Folder.Model.Common;
using API_Folder.Model.Response;
using API_Folder.Repository;

namespace API_Folder.Controllers
{
    [Authorize]
    [ApiController]
    public class CommonController : Controller
    {
        [HttpGet("api/common/ds-loai-hop-so")]
        public JToken DanhSachXe()
        { 
            return CommonRepository.GET_LOAI_HOP_SO();
        }
    }
}
