using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MessageConverter;


namespace FhirEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("bundle.txt");
            String json = reader.ReadToEnd();
            reader.Close();

            DichVuKyThuat dvkt = JsonParser.parseDVKTMessage(json);
            Console.WriteLine("Benh nhan:" + dvkt.BenhNhan.IdHis);
            Console.WriteLine("Ma benh an:" + dvkt.DotDieuTri.MaYte);
            Console.WriteLine("Bac si yeu cau:" + dvkt.BacSiYeuCau.Ten + "(" + dvkt.BacSiYeuCau.ChungChiHanhNghe + ")");
            Console.WriteLine("Noi dung yeu cau:" + dvkt.NoiDungYeuCau);
            Console.WriteLine("Ngay yeu cau:" + dvkt.NgayYeuCau);
            Console.WriteLine("Dich vu ky thuat:" + dvkt.DmDichVu.Ten + "(" + dvkt.DmDichVu.Ma + ")");
        }
    }
}
