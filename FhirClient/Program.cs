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
            Console.WriteLine(string.Format("Get danhmuc with maNhom={0} and ma={1}", maNhomDVKT, maDVKT));
            DanhMuc danhmuc = danhMucClient.GetDanhMuc(maNhomDVKT, "1157");
            if (danhmuc != null)
            {
                string result = string.Format("Result : ma={0}, ten={1}", danhmuc.Ma, danhmuc.Ten);
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
                Console.WriteLine("Not found");
            }

            // Tìm kiếm danh mục theo mã nhóm và tên/mã, trả về danh sách danh mục có mã/tên chứa cụm từ tìm kiếm
            Console.WriteLine();
            string maNhomDanToc = Constants.CodeSystem.DAN_TOC;
            string keyword = "Kinh";
            Console.WriteLine(string.Format("Search danhmuc with maNhom={0} and keyword={1}", maNhomDanToc, keyword));

            List<DanhMuc> dsDanhMuc = danhMucClient.SearchDanhMuc(maNhomDanToc, keyword);
            if (dsDanhMuc.Count > 0)
            {
                Console.WriteLine("List results :");
                foreach (var dm in dsDanhMuc)
                {
                    Console.WriteLine(string.Format("ma={0}, ten={1}", dm.Ma, dm.Ten));
                }
            }
            else
            {
                Console.WriteLine("No result found");
            }
        }
    }
}
