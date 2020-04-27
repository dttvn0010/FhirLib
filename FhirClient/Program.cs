using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageConverter;

namespace FhirClient
{
    class Program
    {
        static DanhMucClient danhMucClient = new DanhMucClient();

        static void Main(string[] args)
        {
            // Lấy danh mục theo mã nhóm và mã (tìm chính xác, chỉ trả về 1 kết quả có mã trùng khớp hoàn toàn , hoặc null nếu không tìm được)
            string maNhomDVKT = Constants.CodeSystem.DICH_VU_KY_THUAT;
            string maDVKT = "11579";
            Console.WriteLine(string.Format("Lấy danh mục theo maNhom={0} và ma={1}", maNhomDVKT, maDVKT));
            DanhMuc danhmuc = danhMucClient.GetDanhMuc(maNhomDVKT, "1157");
            if (danhmuc != null)
            {
                string result = string.Format("Kết quả : ma={0}, ten={1}", danhmuc.Ma, danhmuc.Ten);
                if(danhmuc.DsThuocTinh != null)
                {
                    foreach(var thuocTinh in danhmuc.DsThuocTinh)
                    {
                        result += string.Format(",{0}={1}", thuocTinh.Ma, thuocTinh.GiaTri);
                    }
                }
                Console.WriteLine(result);
            }else
            {
                Console.WriteLine("Không tìm thấy");
            }

            // Tìm kiếm danh mục theo mã nhóm và tên/mã, trả về danh sách danh mục có mã/tên chứa cụm từ tìm kiếm
            Console.WriteLine();
            string maNhomDanToc = Constants.CodeSystem.DAN_TOC;
            string keyword = "Kinh";
            Console.WriteLine(string.Format("Tìm danh mục theo maNhom={0} và keyword={1}", maNhomDanToc, keyword));

            List<DanhMuc> dsDmDanToc = danhMucClient.SearchDanhMuc(maNhomDanToc, keyword);
            if (dsDmDanToc.Count > 0)
            {
                Console.WriteLine("Danh sách kết quả :");
                foreach (var dm in dsDmDanToc)
                {
                    Console.WriteLine(string.Format("ma={0}, ten={1}", dm.Ma, dm.Ten));
                }
            }
            else
            {
                Console.WriteLine("Không có kết quả nào");
            }

            // Lấy toàn bộ danh mục trong một nhóm
            Console.WriteLine("Lấy toàn bộ danh sách danh mục dịch vụ kỹ thuật ...");
            List<DanhMuc> dsDmDVKT = danhMucClient.GetDanhMucByGroup(maNhomDVKT);
            Console.WriteLine("Tổng số danh mục dịch vụ kỹ thuật:" + dsDmDVKT.Count);
            
        }
    }
}
