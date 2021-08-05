using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Folder.Model;

namespace API_Folder.Repository
{
    public class KhachHangRepository
    {
        public static JToken GET_DANH_SACH_KHACH_HANG()
        {
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                var response = baseSQL.GetList("KH_DS_KHACH_HANG", param);
                return JsonHelper.ToJson(response);
            }
        }
    }
}
