using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Folder.Model;
using API_Folder.Model.Response;
using API_Folder.Model.Xe;

namespace API_Folder.Repository
{
    public class ThongTinXeRepository
    {
        public static JToken GET_DANH_SACH_XE()
        {
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                var response = baseSQL.GetList("TTXE_GET_DANH_SACH_XE", param);
                return JsonHelper.ToJson(response);
            }
        }

        public static JToken CUD_CauHinhXe(XE_CUD_CAU_HINH_XE_IN obj, int ma_cud)
        {
            ResponseExecute response = new ResponseExecute();
            //insert user
            using (var baseSQL = new BaseSQL())
            {
       
                var param = new SQLDynamicParameters();
                param.Add("P_CUD", ma_cud);
                param.Add("P_MA_XE_KC", obj.ma_xe_kc);
                param.Add("P_BIEN_SO", obj.bien_so);
                param.Add("P_MAU_XE", obj.mau_xe);
                param.Add("P_LOAI_XE", obj.loai_xe);
                param.Add("P_NGAY_MUA", obj.ngay_mua);
                param.Add("P_NGAY_BAN", obj.ngay_ban);
                param.Add("P_GIA_MUA", obj.gia_mua);
                param.Add("P_GIA_BAN", obj.gia_ban);
                param.Add("P_MA_TINH_TRANG_CHO_THUE", obj.ma_tinh_trang_cho_thue);
                param.Add("P_MA_TINH_TRANG_XE", obj.ma_tinh_trang_xe);
                param.Add("P_SO_CHO_NGOI", obj.so_cho_ngoi);
                param.Add("P_MA_HOP_SO", obj.ma_hop_so);
                param.Add("P_TEN_XE", obj.ten_xe);
                param.Add("P_DOI_XE", obj.doi_xe);
                param.Add("P_SO_KHUNG", obj.so_khung);
                param.Add("P_MA_NHIEN_LIEU", obj.ma_nhien_lieu);
                param.Add("P_MA_XUAT_XU", obj.ma_xuat_xu);
                param.Add("P_NHA_SAN_XUAT", obj.nha_san_xuat);

                response = baseSQL.Execute("XE_CUD_THONG_TIN_XE", param);
                return JsonHelper.ToJson(response);
            }
        }

        public static JToken Insert_CauHinhXe(XE_CUD_CAU_HINH_XE_IN obj)
        {
            ResponseExecute response = new ResponseExecute();
            //insert user
            using (var baseSQL = new BaseSQL())
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine(" INSERT INTO tblThongTinXe( bien_so, ten_xe,  mau_xe , loai_xe , so_cho_ngoi, ma_hop_so, doi_xe, so_khung, ma_nhien_lieu, ma_xuat_xu, nha_san_xuat)");
                strSQL.AppendLine(" VALUES ( @bien_so, @ten_xe, @mau_xe , @loai_xe , @so_cho_ngoi, @ma_hop_so, @doi_xe, @so_khung, @ma_nhien_lieu, @ma_xuat_xu, @nha_san_xuat)");

                var param = new SQLDynamicParameters();
                param.Add("@bien_so", obj.bien_so);
                param.Add("@ten_xe", obj.ten_xe);
                param.Add("@mau_xe", obj.mau_xe);
                param.Add("@loai_xe", obj.loai_xe);
                param.Add("@so_cho_ngoi", obj.so_cho_ngoi);
                param.Add("@ma_hop_so", obj.ma_hop_so);
                param.Add("@ten_xe", obj.ten_xe);
                param.Add("@doi_xe", obj.doi_xe);
                param.Add("@so_khung", obj.so_khung);
                param.Add("@ma_nhien_lieu", obj.ma_nhien_lieu);
                param.Add("@ma_xuat_xu", obj.ma_xuat_xu);
                param.Add("@nha_san_xuat", obj.nha_san_xuat);

                response = baseSQL.ExecuteSQL(strSQL.ToString(), param);
                return JsonHelper.ToJson(response);
            }
        }

        public static JToken Update_CauHinhXe(XE_CUD_CAU_HINH_XE_IN obj)
        {
            ResponseExecute response = new ResponseExecute();
            //insert user
            using (var baseSQL = new BaseSQL())
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("UPDATE tblThongTinXe");

                strSQL.AppendLine("SET bien_so = @bien_so ");
                strSQL.AppendLine(", mau_xe = @mau_xe ");
                strSQL.AppendLine(", loai_xe = @loai_xe ");
                strSQL.AppendLine(", so_cho_ngoi = @so_cho_ngoi ");
                strSQL.AppendLine(", ma_hop_so = @ma_hop_so ");
                strSQL.AppendLine(", ten_xe = @ten_xe ");
                strSQL.AppendLine(", doi_xe = @doi_xe ");
                strSQL.AppendLine(", so_khung = @so_khung ");
                strSQL.AppendLine(", ma_nhien_lieu = @ma_nhien_lieu ");
                strSQL.AppendLine(", ma_xuat_xu = @ma_xuat_xu ");
                strSQL.AppendLine(", nha_san_xuat = @nha_san_xuat ");
                strSQL.AppendLine(" WHERE ma_xe_kc = @ma_xe_kc");

                var param = new SQLDynamicParameters();
                param.Add("@ma_xe_kc", obj.ma_xe_kc);
                param.Add("@bien_so", obj.bien_so);
                param.Add("@ten_xe", obj.ten_xe);
                param.Add("@mau_xe", obj.mau_xe);
                param.Add("@loai_xe", obj.loai_xe);
                param.Add("@so_cho_ngoi", obj.so_cho_ngoi);
                param.Add("@ma_hop_so", obj.ma_hop_so);
                param.Add("@ten_xe", obj.ten_xe);
                param.Add("@doi_xe", obj.doi_xe);
                param.Add("@so_khung", obj.so_khung);
                param.Add("@ma_nhien_lieu", obj.ma_nhien_lieu);
                param.Add("@ma_xuat_xu", obj.ma_xuat_xu);
                param.Add("@nha_san_xuat", obj.nha_san_xuat);

                response = baseSQL.ExecuteSQL(strSQL.ToString(), param);
                return JsonHelper.ToJson(response);
            }
        }


        public static JToken Delete_CauHinhXe(int id_xe)
        {
            ResponseExecute response = new ResponseExecute();
            //insert user
            using (var baseSQL = new BaseSQL())
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("UPDATE tblThongTinXe");

                strSQL.AppendLine("SET is_delete = @is_delete ");

                var param = new SQLDynamicParameters();
                param.Add("@is_delete", true);

                response = baseSQL.ExecuteSQL(strSQL.ToString(), param);
                return JsonHelper.ToJson(response);
            }
        }

        public static JToken GET_THONG_TIN_XE(int ma_xe_kc)
        {
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("P_MA_XE_KC", ma_xe_kc);
                var response = baseSQL.GetSingle("XE_GET_THONG_TIN_XE", param);
                return JsonHelper.ToJson(response);
            }
        }
    }
}
