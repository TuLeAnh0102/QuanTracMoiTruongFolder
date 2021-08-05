using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Folder.Model.NhanVien
{
    public class NV_CUD_NHAN_VIEN_IN
    {
        public int ma_nhan_vien_kc { get; set; }
        public string username { get; set; }
        public string ho_va_ten_nhan_vien { get; set; }
        public string password { get; set; }
        public int don_vi { get; set; }
        public bool trang_thai { get; set; }
        public string di_dong_nhan_vien { get; set; }
        public string email_nhan_vien { get; set; }
        public string dia_chi_nhan_vien { get; set; }
        public bool gioi_tinh { get; set; }
        public int stt_nhan_vien { get; set; }
        public string avartar { get; set; }

    }
}
