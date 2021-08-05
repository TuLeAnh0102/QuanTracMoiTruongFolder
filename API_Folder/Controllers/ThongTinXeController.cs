using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using API_Folder.Model;
using API_Folder.Model.Xe;
using API_Folder.Model.Response;
using API_Folder.Repository;

namespace API_Folder.Controllers
{
    [Authorize]
    [ApiController]
    public class ThongTinXeController : Controller
    {
        [HttpGet("api/xe/ds-xe")]
        public JToken DanhSachXe()
        {
            return ThongTinXeRepository.GET_DANH_SACH_XE();
        }

        [HttpPost("api/xe/them-cau-hinh-xe")]
        public JToken ThemCauHinhXe(XE_CUD_CAU_HINH_XE_IN obj)
        {
            return ThongTinXeRepository.Insert_CauHinhXe(obj);
        }

        [HttpPost("api/xe/cap-nhat-cau-hinh-xe")]
        public JToken CapNhatCauHinhXe(XE_CUD_CAU_HINH_XE_IN obj)
        {
            return ThongTinXeRepository.Update_CauHinhXe(obj);
        }

        //[HttpPost("api/xe/xoa-cau-hinh-xe")]
        //public JToken XoaCauHinhXe(int id)
        //{
        //    return ThongTinXeRepository.Update_CauHinhXe(obj);
        //}

        [HttpGet("api/xe/cau-hinh-xe")]
        public JToken GetCauHinhXe(int ma_xe_kc)
        {
            return ThongTinXeRepository.GET_THONG_TIN_XE(ma_xe_kc);
        }
    }
}