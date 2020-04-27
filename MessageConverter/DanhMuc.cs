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

        public static DanhMuc FromCodeableConcept(JsonValue concept)
        {
            var codings = concept["coding"].AsJsonArray;

            if(codings != null && codings.Count > 0)
            {
                return FromCoding(codings[0]);
            }

            return new DanhMuc(); ;
        }

        private static Object GetPropertyValue(JsonObject obj)
        {
            if (obj["valueString"].AsString != null)
            {
                return obj["valueString"].AsString;
            }

            if (obj["valueInteger"].AsString != null)
            {
                return obj["valueInteger"].AsInteger;
            }

            if (obj["valueBoolean"].AsString != null)
            {
                return obj["valueBoolean"].AsBoolean;
            }

            if (obj["valueDecimal"].AsString != null)
            {
                return obj["valueDecimal"].AsNumber;
            }

            if (obj["valueDateTime"].AsString != null)
            {
                return obj["valueDateTime"].AsDateTime;
            }

            return null;
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
                        Object giaTri = GetPropertyValue(part);

                        if (ma != "slug")
                        {
                            dm.DsThuocTinh.Add(new ThuocTinh(ma, giaTri));
                        }
                    }
                }
            }

            return dm;
        }

        public static DanhMuc FromConcept(String maNhom, JsonObject concept)
        {
            var dm = new DanhMuc();
            dm.MaNhom = maNhom;
            dm.Ma = concept["code"].AsString;
            dm.Ten = concept["display"].AsString;

            var properties = concept["property"].AsJsonArray;
            foreach(var property in properties)
            {
                string ma = property["code"].AsString;
                Object giaTri = GetPropertyValue(property);
                if(ma != "slug")
                {
                    dm.DsThuocTinh.Add(new ThuocTinh(ma, giaTri));
                }
            }

            return dm;
        }
    }
}
