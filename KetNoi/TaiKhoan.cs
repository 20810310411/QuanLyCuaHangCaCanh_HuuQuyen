using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangCaCanh_HuuQuyen.KetNoi
{
    internal class TaiKhoan
    {
        private string hoten;
        private string tentaikhoan;
        private string matkhau;
        public TaiKhoan() { }
        public TaiKhoan(string hotenn, string tentk, string mk)
        {
            hoten = hotenn;
            tentaikhoan = tentk;
            matkhau = mk;
        }
        public string Tentaikhoan { get => tentaikhoan; set => tentaikhoan = value; }
        public string MatKhau { get => matkhau; set => matkhau = value; }
        public string HoTen { get => hoten; set => hoten = value; }
    }
}
