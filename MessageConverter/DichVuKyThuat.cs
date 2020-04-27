using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageConverter
{
    public class DichVuKyThuat
    {
        public BenhNhan BenhNhan { get; set; }
        public DotDieuTri DotDieuTri { get; set; }
        public DanhMuc DmKhoaDieuTri { get; set; }
        public DanhMuc DmDichVu { get; set; }
        public string NoiDungYeuCau { get; set; }
        public DateTime NgayYeuCau { get; set; }
        public CanboYte BacSiYeuCau { get; set; }        
    }
}
