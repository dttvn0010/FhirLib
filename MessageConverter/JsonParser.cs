using System;
using System.Linq;
using LightJson;

namespace MessageConverter
{
    public class JsonParser
    {
        public static DichVuKyThuat parseDVKTMessage(String json)
        {
            var dvkt = new DichVuKyThuat();

            var obj = JsonValue.Parse(json);
            var entries = obj["entry"].AsJsonArray;
            var resources = entries.Select(x => x["resource"]).ToArray();

            var encounters = resources.Where(x => x["resourceType"].AsString == "Encounter")
                                      .ToArray();                                    

            dvkt.DotDieuTri = new DotDieuTri(encounters);
            dvkt.DmKhoaDieuTri = dvkt.DotDieuTri.DmKhoaDieuTri;

            var patient = resources.Where(x => x["resourceType"].AsString == "Patient")
                                    .First();

            dvkt.BenhNhan = new BenhNhan(patient);

            var serviceRequest = resources.Where(x => x["resourceType"].AsString == "ServiceRequest")
                                        .First();

            dvkt.DmDichVu = DanhMuc.FromCodeableConcept(serviceRequest.AsJsonObject["code"]);
            var orderDetails = serviceRequest["orderDetail"].AsJsonArray;
            if (orderDetails != null && orderDetails.Count > 0)
            {
                dvkt.NoiDungYeuCau = orderDetails[0]["text"].AsString;
            }
            dvkt.BacSiYeuCau = new CanboYte(serviceRequest["requester"]);
            dvkt.NgayYeuCau = DateTime.Parse(serviceRequest["authoredOn"]);            

            return dvkt;
        }
    }
}
