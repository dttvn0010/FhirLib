using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightJson;

namespace MessageConverter
{
    public class DanhMuc
    {
        public class ThuocTinh
        {
            public string Ma { get; set; }
            public Object GiaTri { get; set; }

            public ThuocTinh(string ma, Object giaTri)
            {
                Ma = ma;
                GiaTri = giaTri;
            }
        }

        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaNhom { get; set; }
        public List<ThuocTinh> DsThuocTinh { get; set; }

        public DanhMuc()
        {
            DsThuocTinh = new List<ThuocTinh>();
        }

        public static DanhMuc FromCoding(JsonObject coding)
        {
            var dm = new DanhMuc();

            dm.Ten = coding["display"].AsString;
            dm.Ma = coding["code"].AsString;
            dm.MaNhom = coding["system"].AsString;

            return dm;
        }

        public static DanhMuc FromConcept(JsonValue concept)
        {
            var codings = concept["coding"].AsJsonArray;

            if(codings != null && codings.Count > 0)
            {
                return FromCoding(codings[0]);
            }

            return new DanhMuc(); ;
        }

        public static DanhMuc FromParams(string maNhom, JsonArray paramArr)
        {
            var dm = new DanhMuc();
            dm.MaNhom = maNhom;

            foreach (var param in paramArr)
            {
                if(param["name"].AsString == "name")
                {
                    dm.Ma = param["valueString"].AsString;
                }

                if (param["name"].AsString == "display")
                {
                    dm.Ten = param["valueString"].AsString;
                }

                if (param["name"].AsString == "property")
                {
                    var parts = param["part"].AsJsonArray;
                    foreach (var part in parts)
                    {
                        string ma = part["name"].AsString;
                        Object giaTri = null;

                        if (ma == "slug") continue;

                        if (part["valueString"].AsString != null)
                        {
                            giaTri = part["valueString"].AsString;
                        }

                        if (part["valueInteger"].AsString != null)
                        {
                            giaTri = part["valueInteger"].AsInteger;
                        }

                        if (part["valueBoolean"].AsString != null)
                        {
                            giaTri = part["valueBoolean"].AsBoolean;
                        }

                        if (part["valueDecimal"].AsString != null)
                        {
                            giaTri = part["valueDecimal"].AsNumber;
                        }

                        if (part["valueDateTime"].AsString != null)
                        {
                            giaTri = part["valueDateTime"].AsDateTime;
                        }

                        dm.DsThuocTinh.Add(new ThuocTinh(ma, giaTri));
                    }
                }
            }

            return dm;
        }
    }
}
