using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Folder.Model;

namespace API_Folder.Repository
{
    public class CommonRepository
    {
        public static JToken GET_LOAI_HOP_SO()
        {
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                var response = baseSQL.GetList("GET_LOAI_HOP_SO", param);
                return JsonHelper.ToJson(response);
            }
        }
    }
}
