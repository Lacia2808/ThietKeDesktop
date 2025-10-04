
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2212364_PVQHien_Lab03
{
    [Serializable]
    public class Student
    {
        public string MSSV { get; set; }
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public string GioiTinh { get; set; }
        public string SoCMND { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string[] MonHoc { get; set; }

        public Student()
        {
            MonHoc = new string[0];
        }

        public string ToTextRow()
        {
            return string.Join("|", new[]
            {
                MSSV ?? "", HoTenLot ?? "", Ten ?? "",
                NgaySinh.ToString("yyyy-MM-dd"),
                Lop ?? "", GioiTinh ?? "",
                SoCMND ?? "", SoDT ?? "", DiaChi ?? "",
                string.Join(",", MonHoc ?? new string[0])
            });
        }

        public static Student FromTextRow(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return new Student();
            string[] p;

            if (line.IndexOf('|') >= 0)
            {
                p = line.Split('|');
            }
            else
            {
                var parts = line.Split(',');
                p = new string[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                {
                    string s = parts[i].Trim();
                    if (s.StartsWith("\"")) s = s.Substring(1);
                    if (s.EndsWith("\"")) s = s.Substring(0, s.Length - 1);
                    p[i] = s;
                }
            }

            var sv = new Student();
            sv.MSSV     = p.Length > 0 ? p[0] : "";
            sv.HoTenLot = p.Length > 1 ? p[1] : "";
            sv.Ten      = p.Length > 2 ? p[2] : "";
            DateTime d; sv.NgaySinh = (p.Length > 3 && DateTime.TryParse(p[3], out d)) ? d : DateTime.Now;
            sv.Lop      = p.Length > 4 ? p[4] : "";
            sv.GioiTinh = p.Length > 5 && (p[5] == "Nam" || p[5] == "Nữ") ? p[5] : "Nam";
            sv.SoCMND   = p.Length > 6 ? p[6] : "";
            sv.SoDT     = p.Length > 7 ? p[7] : "";
            sv.DiaChi   = p.Length > 8 ? p[8] : "";
            string mon  = p.Length > 9 ? p[9] : "";
            sv.MonHoc   = mon.Length == 0 ? new string[0]
                          : mon.Split(new char[] {',',';'}, StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => x.Trim()).ToArray();
            return sv;
        }

        public static bool ValidMSSV(string mssv, string lop, out string err)
        {
            err = null;
            if (string.IsNullOrWhiteSpace(mssv) || mssv.Length != 7 || !mssv.All(char.IsDigit))
            { err = "MSSV phải gồm đúng 7 chữ số (AABBCCC)."; return false; }

            if (mssv.Substring(2, 2) != "10")
            { err = "MSSV phải có dạng AABBCCC, trong đó BB = 10."; return false; }

            var m = Regex.Match(lop ?? "", @"(\d{2})(?!.*\d)");
            if (!m.Success)
            { err = "Không suy ra được 2 số cuối năm nhập học từ Lớp (ví dụ: CTK47A → 47)."; return false; }

            string yy = m.Groups[1].Value;
            string aa = mssv.Substring(0, 2);
            if (aa != yy)
            { err = "AA của MSSV phải trùng 2 số cuối năm nhập học suy từ Lớp (VD: CTK47 → AA=47)."; return false; }

            return true;
        }

        public static bool ValidCMND(string v)
        {
            return !string.IsNullOrWhiteSpace(v) && v.Length == 9 && v.All(char.IsDigit);
        }

        public static bool ValidSDT(string v)
        {
            return !string.IsNullOrWhiteSpace(v) && v.Length == 10 && v.All(char.IsDigit) && v.StartsWith("0");
        }

        public override string ToString()
        {
            return (MSSV ?? "") + " - " + (HoTenLot ?? "") + " " + (Ten ?? "") + " - " + (Lop ?? "");
        }
    }
}
