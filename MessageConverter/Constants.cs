using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageConverter
{
    public class Constants
    {
        public static readonly string FHIR_URL = "http://emr.com.vn:8000/R4";

        public static class CodeSystem
        {
            public static readonly string ID_HIS = "https://fhir.emr.vn.com/Idhis";
            public static readonly string MA_YTE = "https://fhir.emr.vn.com/HSBA";
            public static readonly string KHOA_DIEU_TRI = "https://fhir.emr.vn.com/DMKhoaDieuTri-v1.0";
            public static readonly string DICH_VU_KY_THUAT = "https://fhir.emr.com.vn/CodeSystem/DMDVKyThuat";
            public static readonly string ICD_10 = "http://hl7.org/fhir/sid/icd-10";
            public static readonly string DAN_TOC = "https://teminology.emr.com.vn/CodeSystem/DanhMuc-DanToc-v1.0";
            //TODO: To be Continued ...
        }
    }
}
