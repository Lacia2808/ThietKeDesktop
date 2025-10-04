
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace _2212364_PVQHien_Lab03
{
    public class StudentManager
    {
        public List<Student> Items { get; private set; }

        public StudentManager()
        {
            Items = new List<Student>();
        }

        public void AddOrUpdate(Student s)
        {
            int idx = Items.FindIndex(x => x.MSSV == s.MSSV);
            if (idx >= 0) Items[idx] = s; else Items.Add(s);
        }

        public void RemoveByMSSV(IEnumerable<string> keys)
        {
            Items.RemoveAll(s => keys.Contains(s.MSSV));
        }

        public IEnumerable<Student> SearchExact(string mssv, string ten, string lop)
        {
            IEnumerable<Student> q = Items;

            if (!string.IsNullOrWhiteSpace(mssv))
                q = q.Where(s => string.Equals(s.MSSV ?? "", mssv.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(ten))
            {
                string t = ten.Trim();
                q = q.Where(s =>
                    string.Equals(s.Ten ?? "", t, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(((s.HoTenLot ?? "") + " " + (s.Ten ?? "")).Trim(), t, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(lop))
                q = q.Where(s => string.Equals(s.Lop ?? "", lop.Trim(), StringComparison.OrdinalIgnoreCase));

            return q;
        }

        public void LoadTxt(string path)
        {
            Items.Clear();
            if (!File.Exists(path)) return;

            foreach (var line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try { Items.Add(Student.FromTextRow(line)); } catch { }
            }
        }

        public void SaveTxt(string path)
        {
            File.WriteAllLines(path, Items.Select(i => i.ToTextRow()).ToArray());
        }

        public void SaveXml(string path)
        {
            var ser = new XmlSerializer(typeof(List<Student>));
            using (var fs = File.Create(path))
            {
                ser.Serialize(fs, Items);
            }
        }

        public void LoadXml(string path)
        {
            if (!File.Exists(path)) { Items = new List<Student>(); return; }
            var ser = new XmlSerializer(typeof(List<Student>));
            using (var fs = File.OpenRead(path))
            {
                try { Items = (List<Student>)ser.Deserialize(fs); }
                catch { Items = new List<Student>(); }
            }
        }

        public void SaveJson(string path)
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(Items);
            File.WriteAllText(path, json);
        }

        public void LoadJson(string path)
        {
            if (!File.Exists(path)) { Items = new List<Student>(); return; }
            try
            {
                var json = File.ReadAllText(path);
                var js = new JavaScriptSerializer();
                var list = js.Deserialize<List<Student>>(json);
                Items = list ?? new List<Student>();
            }
            catch { Items = new List<Student>(); }
        }

        public void LoadCsv(string path)
        {
            Items.Clear();
            if (!File.Exists(path)) return;

            foreach (var raw in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                var parts = raw.Split(',');
                var cells = new string[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                {
                    var s = parts[i].Trim();
                    if (s.StartsWith("\"")) s = s.Substring(1);
                    if (s.EndsWith("\"")) s = s.Substring(0, s.Length - 1);
                    cells[i] = s;
                }

                var sv = new Student();
                sv.MSSV     = cells.Length > 0 ? cells[0] : "";
                sv.HoTenLot = cells.Length > 1 ? cells[1] : "";
                sv.Ten      = cells.Length > 2 ? cells[2] : "";
                DateTime d; sv.NgaySinh = (cells.Length > 3 && DateTime.TryParse(cells[3], out d)) ? d : DateTime.Now;
                sv.Lop      = cells.Length > 4 ? cells[4] : "";
                sv.GioiTinh = "Nam";
                sv.SoCMND   = cells.Length > 5 ? cells[5] : "";
                sv.SoDT     = cells.Length > 6 ? cells[6] : "";
                sv.DiaChi   = cells.Length > 7 ? cells[7] : "";
                string mon  = cells.Length > 8 ? cells[8] : "";
                sv.MonHoc   = mon.Length == 0 ? new string[0]
                              : mon.Split(new char[] {',',';'}, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(x => x.Trim()).ToArray();

                Items.Add(sv);
            }
        }

        public void LoadExcel(string path, string sheetName)
        {
            Items.Clear();
            if (!File.Exists(path)) return;

            string ext = Path.GetExtension(path).ToLowerInvariant();
            string connStr;

            if (ext == ".xls")
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
            else
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";

            using (var conn = new OleDbConnection(connStr))
            {
                conn.Open();

                if (string.IsNullOrWhiteSpace(sheetName))
                {
                    var schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (schema == null || schema.Rows.Count == 0) return;
                    sheetName = schema.Rows[0]["TABLE_NAME"].ToString();
                }

                using (var da = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow r in dt.Rows)
                    {
                        var sv = new Student();
                        sv.MSSV     = (r.Table.Columns.Contains("MSSV")     ? (r["MSSV"]     ?? "") : "").ToString().Trim();
                        sv.HoTenLot = (r.Table.Columns.Contains("HoTenLot") ? (r["HoTenLot"] ?? "") : "").ToString().Trim();
                        sv.Ten      = (r.Table.Columns.Contains("Ten")      ? (r["Ten"]      ?? "") : "").ToString().Trim();

                        string ns   = (r.Table.Columns.Contains("NgaySinh") ? (r["NgaySinh"] ?? "") : "").ToString();
                        DateTime d; sv.NgaySinh = DateTime.TryParse(ns, out d) ? d : DateTime.Now;

                        sv.Lop      = (r.Table.Columns.Contains("Lop")      ? (r["Lop"]      ?? "") : "").ToString().Trim();
                        sv.GioiTinh = (r.Table.Columns.Contains("GioiTinh") ? (r["GioiTinh"] ?? "Nam") : "Nam").ToString().Trim();
                        sv.SoCMND   = (r.Table.Columns.Contains("SoCMND")   ? (r["SoCMND"]   ?? "") : "").ToString().Trim();
                        sv.SoDT     = (r.Table.Columns.Contains("SoDT")     ? (r["SoDT"]     ?? "") : "").ToString().Trim();
                        sv.DiaChi   = (r.Table.Columns.Contains("DiaChi")   ? (r["DiaChi"]   ?? "") : "").ToString().Trim();

                        string mon = (r.Table.Columns.Contains("MonHoc") ? (r["MonHoc"] ?? "") : "").ToString();
                        sv.MonHoc = mon.Length == 0 ? new string[0]
                                  : mon.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(x => x.Trim()).ToArray();

                        Items.Add(sv);
                    }
                }
            }
        }
    }
}
