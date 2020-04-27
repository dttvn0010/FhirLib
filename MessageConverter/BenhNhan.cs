using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightJson;

namespace MessageConverter
{
    public class BenhNhan
    {
        public string TenDayDu { get; set; }
        public string IdHis { get; set; }

        public BenhNhan(JsonValue patient)
        {
            var names = patient["name"].AsJsonArray;

            if (names != null) {
                foreach (var name in names)
                {
                    string text = name["text"].AsString;
                    if (text != null && text != "")
                    {
                        TenDayDu = text;
                        break;
                    }
                }
            }

            var identifiers = patient["identifier"].AsJsonArray;
            if (identifiers != null)
            {
                foreach (var identifier in identifiers)
                {
                    string system = identifier["system"].AsString;
                    if (system == Constants.CodeSystem.ID_HIS)
                    {
                        IdHis = identifier["value"].AsString;
                        break;
                    }
                }
            }
        }
    }
}
