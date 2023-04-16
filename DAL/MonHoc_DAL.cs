﻿using System.Linq;
using System.Windows.Forms;

namespace DAL
{
    public class MonHoc_DAL
    {
        private PBL3Entities db;
        private static MonHoc_DAL _Instance;
        public static MonHoc_DAL Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new MonHoc_DAL();
                return _Instance;
            }
        }

        private MonHoc_DAL()
        {
            db = new PBL3Entities();
        }
        public MON_HOC GetMonHocById(string MaMH)
        {
            return db.MON_HOC.Where(p => p.MaMH == MaMH).FirstOrDefault();
        }

        public bool UpdateSoTC(string MaMH,int SoTC)
        {
            MON_HOC mh = GetMonHocById(MaMH);
            mh.SoTC = (byte)SoTC;
            return db.SaveChanges() > 0;
        }
    }
}