using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Folder.Entities;
using API_Folder.Helpers;
using API_Folder.Model;
using API_Folder.Model.NhanVien;
using API_Folder.Model.Response;
using API_Folder.Repository;

namespace API_Folder.Services
{
    public interface IUserService
    {
        User GetById(int id);
        ResponseSingle Login(string username, string password);
        IEnumerable<User> GetAll();
        JToken CapNhatThongTin(NV_CUD_NHAN_VIEN_IN obj);
        JToken ResetPass(String username, string password, String key);
        JToken DanhSachNhanVien();
        JToken ThongTinNhanVien(int ma_nhan_vien_kc);
    }
    public class UserService : IUserService
    {
        public User GetById(int id)
        {
            User user = new User();
            if (id == 1)
            {
                user.ma_can_bo = id;
                user.ten_can_bo = "LastName";
            }
            return user;
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            ResponseSingle response = NhanVienRepository.CB_CHECK_LOGIN(user.ten_dang_nhap);

            if (response.data != null)
                throw new AppException("Cán bộ \"" + user.ten_dang_nhap + "\" đã tồn tại");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.password_hash = passwordHash;
            user.password_salt = passwordSalt;

            //NhanVienRepository.NV_CUD_NHAN_VIEN(user);
            //_context.Users.Add(user);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> lstUser = new List<User>();
            User user = new User();
            user.ma_can_bo = 1;
            user.ten_can_bo = "LastName";
            lstUser.Add(user);
            return lstUser;
        }

        public ResponseSingle Login(string username, string password)
        {
            ResponseSingle response = new ResponseSingle();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.SetError("Username hoặc password đang để trống");
                return response;
            }

            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("P_USERNAME", username);
                response = baseSQL.GetSingle("NV_GET_THONG_TIN_LOGIN", param);
                if (!response.success || response.data == null)
                {
                    response.SetError("User không tồn tại");
                    return response;
                }
                var user = response.data;
                // check if password is correct
                if (!VerifyPasswordHash(password, user.password_hash, user.password_salt))
                {
                    response.SetError("Password không đúng");
                    return response;
                }

                return response;
            }
        }

        public JToken CapNhatThongTin(NV_CUD_NHAN_VIEN_IN obj)
        {
            ResponseExecute response = new ResponseExecute();

            if (string.IsNullOrWhiteSpace(obj.password))
            {
                response.SetError("Password không được để trống");
                return JsonHelper.ToJson(response);
            }

            //check username exist
            var checkLogin = NhanVienRepository.CB_CHECK_LOGIN(obj.username);
            if(!checkLogin.success || checkLogin.data != null)
            {
                response.SetError("Nhân viên \"" + obj.username + "\" đã tồn tại");
                return JsonHelper.ToJson(response);
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(obj.password, out passwordHash, out passwordSalt);

            //insert user
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("P_MA_NHAN_VIEN_KC", obj.ma_nhan_vien_kc);
                param.Add("P_USERNAME", obj.username);
                param.Add("P_HO_VA_TEN_NHAN_VIEN", obj.ho_va_ten_nhan_vien);
                param.Add("P_PASSWORD_HASH", passwordHash);
                param.Add("P_PASSWORD_SALT", passwordSalt);
                param.Add("P_DON_VI", obj.don_vi);
                param.Add("P_TRANG_THAI", obj.trang_thai);
                param.Add("P_DI_DONG_NHAN_VIEN", obj.di_dong_nhan_vien);
                param.Add("P_EMAIL_NHAN_VIEN", obj.email_nhan_vien);
                param.Add("P_DIA_CHI_NHAN_VIEN", obj.dia_chi_nhan_vien);
                param.Add("P_GIOI_TINH", obj.gioi_tinh);
                param.Add("P_STT_NHAN_VIEN", obj.stt_nhan_vien);
                param.Add("P_AVARTAR", obj.avartar);

                response = baseSQL.Execute("NV_CUD_NHAN_VIEN", param);
                return JsonHelper.ToJson(response);
            }
        }

        public JToken ResetPass(String username, String password, String key)
        {
            ResponseExecute response = new ResponseExecute();

            if (string.IsNullOrWhiteSpace(password))
            {
                response.SetError("Password không được để trống");
                return JsonHelper.ToJson(response);
            }

            if(key != "Admin12345!@#")
            {
                response.SetError("Key sai");
                return JsonHelper.ToJson(response);
            }

            //check username exist
            var checkLogin = NhanVienRepository.CB_CHECK_LOGIN(username);
            if (!checkLogin.success && checkLogin.data == null)
            {
                response.SetError("Nhân viên \"" + username + "\" không tồn tại");
                return JsonHelper.ToJson(response);
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //insert user
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("P_USERNAME", username);
                param.Add("P_PASSWORD_HASH", passwordHash);
                param.Add("P_PASSWORD_SALT", passwordSalt);

                response = baseSQL.Execute("NV_RESET_PASS", param);
                return JsonHelper.ToJson(response);
            }
        }

        public JToken DanhSachNhanVien()
        {
            using (var baseSQl = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                var response = baseSQl.GetList("NV_GET_DANH_SACH_NHAN_VIEN", param);
                return JsonHelper.ToJson(response);
            }
        }

        public JToken ThongTinNhanVien(int ma_nhan_vien_kc)
        {
            using (var baseSQL = new BaseSQL())
            {
                var param = new SQLDynamicParameters();
                param.Add("P_MA_NHAN_VIEN_KC", ma_nhan_vien_kc);
                var response = baseSQL.GetSingle("NV_GET_THONG_TIN_NHAN_VIEN", param);
                return JsonHelper.ToJson(response);
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
