using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API_Folder.Model.File;
using API_Folder.Model;
using System.Text;

namespace API_Folder.Controllers
{
    [Authorize]
    [ApiController]
    public class FileController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("api/file/read-tram-quan-trac")]        
        public JToken ReadTramQuanTrac(string ma_loai_quan_trac, string ma_tram_quan_trac, string max_time)
        {
            List<TimeTramQuanTracModel> lstTimeTramQuanTrac = new List<TimeTramQuanTracModel>();
            List<ConfigTramQuanTracModel> lstTram = Startup.ConnectFolderQuanTrac.Find(x => x.LoaiQuanTrac == ma_loai_quan_trac).ConfigTramQuanTrac;
            if (lstTram.Count == 0)
                return JsonHelper.ToJson(lstTimeTramQuanTrac);

            string pathTramQT = lstTram.Find(x => x.IdTram == ma_tram_quan_trac).PathTram;
            if (String.IsNullOrWhiteSpace(pathTramQT))
                return JsonHelper.ToJson(lstTimeTramQuanTrac);

            // Fetch all files in the Folder(Directory).
            List<string> filesFullPath = Directory.GetFiles(pathTramQT).ToList();

            foreach (string filePath in filesFullPath)
            {
                string thoigian = Path.GetFileNameWithoutExtension(filePath).Split('_').Last();
                if (string.IsNullOrWhiteSpace(max_time))
                    max_time = "0";
                if(Int64.Parse(thoigian) <= Int64.Parse(max_time))
                {
                    continue;
                }    
                TimeTramQuanTracModel fileModel = new TimeTramQuanTracModel();
                fileModel.TenFile = Path.GetFileName(filePath);
                fileModel.ThoiGian = thoigian;
                fileModel.ChiSoDoDac = ReadFileData(filePath);
                lstTimeTramQuanTrac.Add(fileModel);
            }

            return JsonHelper.ToJson(lstTimeTramQuanTrac);
        }

        private string ReadFileData(string filePath)
        {
            string strContent = string.Empty;
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8, true))
                {
                    strContent = streamReader.ReadToEnd();
                };
                return strContent;
            }
            catch
            {
                return strContent;
            }
        }

        //private List<FileChiSoQuanTracModel> ReadFileData(string filePath)
        //{
        //    string line;
        //    List<FileChiSoQuanTracModel> lstResult = new List<FileChiSoQuanTracModel>();
        //    System.IO.StreamReader file;

        //    try
        //    {
        //        file = new System.IO.StreamReader(filePath);
        //        while ((line = file.ReadLine()) != null)
        //        {
        //            string[] lineData = line.Split('	');
        //            FileChiSoQuanTracModel model = new FileChiSoQuanTracModel();
        //            model.TenChiSo = lineData[0];
        //            model.ChiSo = float.Parse(lineData[1], CultureInfo.InvariantCulture.NumberFormat);
        //            model.DonVi = lineData[2];
        //            model.ThoiGian = lineData[3];
        //            model.NoName = lineData[4];
        //            lstResult.Add(model);
        //        }
        //        file.Close();
        //        return lstResult;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
            
        //}
    }
}
