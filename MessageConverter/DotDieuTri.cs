using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightJson;

namespace MessageConverter
{
    public class DotDieuTri
    {
        public string MaYte { get; set; }
        public DanhMuc DmKhoaDieuTri;

        public DotDieuTri(JsonValue[] encounters)
        {
            foreach (var encounter in encounters)
            {
                var identifiers = encounter["identifier"].AsJsonArray;
                if (identifiers != null)
                {
                    foreach (var identifier in identifiers)
                    {
                        if (identifier["system"].AsString == Constants.CodeSystem.MA_YTE)
                        {
                            MaYte = identifier["value"].AsString;
                        }
                    }
                }

                var types = encounter["type"].AsJsonArray;
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        if (type["system"].AsString == Constants.CodeSystem.KHOA_DIEU_TRI)
                        {
                            DmKhoaDieuTri = DanhMuc.FromCodeableConcept(type);
                        }
                    }
                }
            }
        }
    }
}
