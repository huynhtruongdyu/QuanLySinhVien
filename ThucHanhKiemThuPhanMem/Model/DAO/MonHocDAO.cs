using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem.Model.DAO
{
    public class MonHocDAO
    {
        MyDbContext db;
        List<MonHoc> mh;
        public MonHocDAO()
        {
            db = new MyDbContext();
        }
        public List<MonHoc> GetMonHocs()
        {
            if (mh == null)
            {
                db = new MyDbContext();
                mh = db.MonHoc.ToList();
                return mh;
            }
            return mh;
        }
    }
}
