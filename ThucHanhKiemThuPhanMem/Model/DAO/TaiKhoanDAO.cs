using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem.Model.DAO
{
    public class TaiKhoanDAO
    {
        MyDbContext db;

        public TaiKhoanDAO()
        {
            db = new MyDbContext();
        }
        bool checkUsername(string username)
        {
            username = username.Trim();
            if (username.Length < 6 || username.Length > 15)
                return false;
            else if (char.IsDigit(username[0]))
                return false;
            else if (username.Any(c => !char.IsLetter(c)))
                return false;
            else
                return true;
        }
        bool checkPassword(string password)
        {
            password = password.Trim();
            if (password.Length < 8 || password.Length > 20)
                return false;
            else if (!password.Any(c => !char.IsLetter(c)))
                return false;
            else if (!password.Any(c => !char.IsUpper(c)))
                return false;
            else if (!password.Any(c => !char.IsDigit(c)))
                return false;
            else
                return true;
        }
        public bool Add(string username, string password)
        {
            if (checkUsername(username))
            {
                if (checkPassword(password))
                {
                    db = new MyDbContext();
                    if (db.TaiKhoan.Any(x => x.Username == username) == false)
                    {
                        TaiKhoan tk = new TaiKhoan();
                        tk.Username = username;
                        tk.Password = password;
                        db.TaiKhoan.Add(tk);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false; ;
        }
    }
}
