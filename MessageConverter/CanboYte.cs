using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightJson;

namespace MessageConverter
{
    public class CanboYte
    {
        public string Ten { get; set; }
        public string ChungChiHanhNghe { get; set; }

        public CanboYte(JsonValue practitioner)
        {
            Ten = practitioner["display"].AsString;
            var identifier = practitioner["identifier"].AsJsonObject;

            if (identifier != null)
            {
                ChungChiHanhNghe = identifier["value"].AsString;
            }
        }
    }
}
