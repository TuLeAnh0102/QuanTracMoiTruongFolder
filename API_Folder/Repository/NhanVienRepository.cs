﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API_Folder.Model;
using API_Folder.Model.NhanVien;
using API_Folder.Model.Response;

namespace API_Folder.Repository
{
    public class NhanVienRepository
    {
        public static bool HT_CHECK_REFRESH_TOKEN(string refresh_token)
        {
            using (var vpdt = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("RS", dbType: SqlDbType.NVarChar, direction: ParameterDirection.Output);
                param.Add("P_REFRESH_TOKEN", refresh_token);
                var Response = vpdt.GetSingle("HT_CHECK_REFRESH_TOKEN", param);
                return (Response.data != null);
            }
        }

        public static ResponseSingle CB_CHECK_LOGIN(string username)
        {
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("P_USERNAME", username);
                var response = baseSQL.GetSingle("NV_GET_THONG_TIN_LOGIN", param);
                return response;
            }
        }
        

    }
}
