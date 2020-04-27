using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightJson;
using MessageConverter;

namespace FhirClient
{
    class DanhMucClient
    {
        private string GetCodeSystemId(string codeSystemUrl)
        {
            var param = new Dictionary<string, string>() {
                { "url", codeSystemUrl },
                {"status", "active" }
            };
            string url = Constants.FHIR_URL + "/CodeSystem";
            string json = FhirClientUtils.get(url, param);

            if(json != null)
            {
                var obj = JsonValue.Parse(json);
                var entry = obj["entry"].AsJsonArray;
                if (entry.Count > 0)
                {
                    return entry[0]["resource"]["id"].AsString;
                }
            }

            return null;
        }

        /// - Lấy danh mục theo mã nhóm và mã
        /// - Input:
        ///     + maNhom: string - mã nhóm danh mục, lấy trong Constants.CodeSystem
        ///     + ma: string - mã của danh mục trong nhóm
        ///  - Output:
        ///     + danhMuc: DanhMuc - Danh mục nếu tìm thấy, null nếu không tìm thấy

        public DanhMuc GetDanhMuc(string maNhom, string ma)
        {
            var param = new Dictionary<string, string>() {
                    { "system", maNhom },
                    { "code", ma }
            };
            string url = Constants.FHIR_URL + "/CodeSystem/$lookup";
            var json = FhirClientUtils.get(url, param);
            if(json != null)
            {
                var obj = JsonValue.Parse(json);
                return DanhMuc.FromParams(maNhom, obj["parameter"].AsJsonArray);
            }
            return null;
        }

        /// - Tìm kiếm danh mục theo mã nhóm và tên/mã
        /// - Input:
        ///     maNhom: string - mã nhóm danh mục, lấy trong Constants.CodeSystem
        ///     keyword: string - từ khóa tìm kiếm
        ///  - Output:
        ///     dsDanhMuc: List<DanhMuc> - Danh sách các danh mục trong nhóm có tên/mã chứa keyword tìm kiếm
          
        public List<DanhMuc> SearchDanhMuc(string maNhom, string keyword)
        {
            string url = Constants.FHIR_URL + "/CodeSystem/$find-matches";

            var obj = new JsonObject();
            obj["resourceType"] = "Parameters";
            var parameters = new JsonArray();

            var systemParam = new JsonObject();
            systemParam["name"] = "system";
            systemParam["valueUri"] = maNhom;
            parameters.Add(systemParam);

            var exactParam = new JsonObject();
            exactParam["name"] = "exact";
            exactParam["valueBoolean"] = false;
            parameters.Add(exactParam);

            var propParam = new JsonObject();
            propParam["name"] = "property";

            var parts = new JsonArray();
            var codePart = new JsonObject();
            codePart["name"] = "code";
            codePart["valueCode"] = "slug";
            parts.Add(codePart);

            var valuePart = new JsonObject();
            valuePart["name"] = "value";
            valuePart["valueString"] = keyword;
            parts.Add(valuePart);

            propParam["part"] = parts;
            parameters.Add(propParam);

            obj["parameter"] = parameters;

            var json = FhirClientUtils.post(url, obj.ToString());

            obj = JsonValue.Parse(json);

            foreach(var param in obj["parameter"].AsJsonArray)
            {
                if(param["name"] == "match")
                {
                    parts = param["part"].AsJsonArray;
                    return parts.ToList().ConvertAll(x => DanhMuc.FromCoding(x["valueCoding"]));
                }
            }

            return new List<DanhMuc>();
        }
    }
}
