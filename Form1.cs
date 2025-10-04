
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace _2212364_PVQHien_Lab03
{
    public class Form1 : Form
    {
        private StudentManager mgr = new StudentManager();
        private string pathTxt  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.txt");
        private string pathXml  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.xml");
        private string pathJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.json");

        private TextBox txtMSSV = new TextBox();
        private TextBox txtHoLot = new TextBox();
        private TextBox txtTen = new TextBox();
        private DateTimePicker dtpNgaySinh = new DateTimePicker();
        private ComboBox cmbLop = new ComboBox();
        private RadioButton rNam = new RadioButton();
        private RadioButton rNu = new RadioButton();
        private TextBox txtCMND = new TextBox();
        private TextBox txtDT = new TextBox();
        private TextBox txtDiaChi = new TextBox();
        private CheckedListBox clbMonHoc = new CheckedListBox();

        private Button btnMoFile = new Button();
        private Button btnTim = new Button();
        private Button btnThem = new Button();
        private Button btnCapNhat = new Button();
        private Button btnThoat = new Button();

        private ListView lv = new ListView();

        public Form1()
        {
            Text = "Nhập thông tin sinh viên — Phùng Võ Quốc Hiển (2212364)";
            Width = 980; Height = 630; StartPosition = FormStartPosition.CenterScreen;

            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";

            cmbLop.DropDownStyle = ComboBoxStyle.DropDown;
            rNam.Text = "Nam"; rNam.Checked = true;
            rNu.Text = "Nữ";
            clbMonHoc.CheckOnClick = true; clbMonHoc.Height = 90;

            btnMoFile.Text = "Mở file...";
            btnTim.Text = "Tìm kiếm";
            btnThem.Text = "Thêm mới";
            btnCapNhat.Text = "Cập nhật";
            btnThoat.Text = "Thoát";

            lv.View = View.Details; lv.CheckBoxes = true; lv.FullRowSelect = true; lv.Dock = DockStyle.Fill;
            lv.Columns.Add("MSSV", 90);
            lv.Columns.Add("Họ và tên", 220);
            lv.Columns.Add("Ngày sinh", 90);
            lv.Columns.Add("Lớp", 80);
            lv.Columns.Add("CMND", 90);
            lv.Columns.Add("SĐT", 100);
            lv.Columns.Add("Địa chỉ", 250);

            txtMSSV.MaxLength = 7;
            txtCMND.MaxLength = 9;
            txtDT.MaxLength   = 10;
            txtHoLot.MaxLength = 20;
            txtTen.MaxLength   = 7;

            txtMSSV.KeyPress += OnlyDigit_KeyPress;
            txtCMND.KeyPress += OnlyDigit_KeyPress;
            txtDT.KeyPress   += OnlyDigit_KeyPress;

            cmbLop.Items.AddRange(new object[] { "CTK46", "CTK47A", "CTK47B", "CTK48" });
            if (cmbLop.Items.Count > 0) cmbLop.SelectedIndex = 0;
            clbMonHoc.Items.AddRange(new object[]
            { "Mạng máy tính", "Hệ điều hành", "Lập trình CSDL", "Lập trình mạng",
              "Đồ án cơ sở", "PP NCKH", "Lập trình thiết bị di động", "An toàn & bảo mật HT" });

            TableLayoutPanel root = new TableLayoutPanel();
            root.Dock = DockStyle.Fill; root.RowCount = 2;
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 260));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);

            GroupBox gb = new GroupBox();
            gb.Text = "Thông tin sinh viên"; gb.Dock = DockStyle.Fill;
            root.Controls.Add(gb, 0, 0);

            TableLayoutPanel grid = new TableLayoutPanel();
            grid.Dock = DockStyle.Fill; grid.ColumnCount = 4; grid.Padding = new Padding(8);
            for (int i = 0; i < 6; i++) grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            gb.Controls.Add(grid);

            grid.Controls.Add(new Label { Text = "MSSV:", TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            grid.Controls.Add(txtMSSV, 1, 0);
            grid.Controls.Add(new Label { Text = "Giới tính:", TextAlign = ContentAlignment.MiddleRight }, 2, 0);
            FlowLayoutPanel pnlSex = new FlowLayoutPanel();
            pnlSex.FlowDirection = FlowDirection.LeftToRight; pnlSex.Dock = DockStyle.Fill;
            pnlSex.Controls.Add(rNam); pnlSex.Controls.Add(rNu);
            grid.Controls.Add(pnlSex, 3, 0);

            grid.Controls.Add(new Label { Text = "Họ và tên lót:", TextAlign = ContentAlignment.MiddleRight }, 0, 1);
            grid.Controls.Add(txtHoLot, 1, 1);
            grid.Controls.Add(new Label { Text = "Tên:", TextAlign = ContentAlignment.MiddleRight }, 2, 1);
            grid.Controls.Add(txtTen, 3, 1);

            grid.Controls.Add(new Label { Text = "Ngày sinh:", TextAlign = ContentAlignment.MiddleRight }, 0, 2);
            grid.Controls.Add(dtpNgaySinh, 1, 2);
            grid.Controls.Add(new Label { Text = "Lớp:", TextAlign = ContentAlignment.MiddleRight }, 2, 2);
            grid.Controls.Add(cmbLop, 3, 2);

            grid.Controls.Add(new Label { Text = "Số CMND:", TextAlign = ContentAlignment.MiddleRight }, 0, 3);
            grid.Controls.Add(txtCMND, 1, 3);
            grid.Controls.Add(new Label { Text = "Số ĐT:", TextAlign = ContentAlignment.MiddleRight }, 2, 3);
            grid.Controls.Add(txtDT, 3, 3);

            grid.Controls.Add(new Label { Text = "Địa chỉ:", TextAlign = ContentAlignment.MiddleRight }, 0, 4);
            grid.Controls.Add(txtDiaChi, 1, 4); grid.SetColumnSpan(txtDiaChi, 3);

            grid.Controls.Add(new Label { Text = "Môn đăng ký:", TextAlign = ContentAlignment.MiddleRight }, 0, 5);
            grid.Controls.Add(clbMonHoc, 1, 5); grid.SetColumnSpan(clbMonHoc, 3);

            FlowLayoutPanel pnlBtns = new FlowLayoutPanel();
            pnlBtns.FlowDirection = FlowDirection.LeftToRight; pnlBtns.Dock = DockStyle.Fill; pnlBtns.Height = 36;
            pnlBtns.Controls.Add(btnMoFile);
            pnlBtns.Controls.Add(btnTim);
            pnlBtns.Controls.Add(btnThem);
            pnlBtns.Controls.Add(btnCapNhat);
            pnlBtns.Controls.Add(btnThoat);
            grid.Controls.Add(pnlBtns, 0, 6); grid.SetColumnSpan(pnlBtns, 4);

            GroupBox gb2 = new GroupBox();
            gb2.Text = "Danh sách sinh viên"; gb2.Dock = DockStyle.Fill;
            root.Controls.Add(gb2, 0, 1);
            gb2.Controls.Add(lv);

            ContextMenuStrip ctxMon = new ContextMenuStrip();
            ToolStripMenuItem miAddMon = new ToolStripMenuItem("Thêm môn...");
            ToolStripMenuItem miDelMon = new ToolStripMenuItem("Xóa môn được chọn");
            ctxMon.Items.Add(miAddMon); ctxMon.Items.Add(miDelMon);
            clbMonHoc.ContextMenuStrip = ctxMon;

            miAddMon.Click += delegate
            {
                using (SimpleInputBox ib = new SimpleInputBox("Thêm môn", "Tên môn học:", ""))
                {
                    if (ib.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(ib.Value))
                    {
                        clbMonHoc.Items.Add(ib.Value.Trim(), true);
                    }
                }
            };
            miDelMon.Click += delegate
            {
                for (int i = clbMonHoc.CheckedIndices.Count - 1; i >= 0; i--)
                    clbMonHoc.Items.RemoveAt(clbMonHoc.CheckedIndices[i]);
            };

            ContextMenuStrip ctxList = new ContextMenuStrip();
            ToolStripMenuItem miDelSelected = new ToolStripMenuItem("Xóa sinh viên đã chọn");
            ToolStripMenuItem miDelAll = new ToolStripMenuItem("Xóa tất cả");
            ctxList.Items.Add(miDelSelected); ctxList.Items.Add(miDelAll);
            lv.ContextMenuStrip = ctxList;

            miDelSelected.Click += delegate { DeleteSelected(); };
            miDelAll.Click += delegate
            {
                if (MessageBox.Show("Xóa toàn bộ danh sách?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mgr.Items.Clear(); SaveAll(); RefreshList();
                }
            };

            Load += Form1_Load;
            btnMoFile.Click += btnMoFile_Click;
            lv.SelectedIndexChanged += delegate
            {
                if (lv.SelectedItems.Count == 0) return;
                int idx = lv.SelectedItems[0].Index;
                if (idx >= 0 && idx < mgr.Items.Count)
                    FillForm(mgr.Items[idx]);
            };
            btnTim.Click += delegate { DoSearch(); };
            btnThem.Click += delegate { DoAddOrUpdate(false); };
            btnCapNhat.Click += delegate { DoAddOrUpdate(true); };
            btnThoat.Click += delegate
            {
                if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
            };
        }

        private void OnlyDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(pathTxt)) File.WriteAllText(pathTxt, "");
                mgr.LoadTxt(pathTxt);
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshList()
        {
            lv.Items.Clear();
            foreach (var s in mgr.Items)
            {
                var it = new ListViewItem(s.MSSV);
                it.SubItems.Add(((s.HoTenLot ?? "") + " " + (s.Ten ?? "")).Trim());
                it.SubItems.Add(s.NgaySinh.ToString("dd/MM/yyyy"));
                it.SubItems.Add(s.Lop ?? "");
                it.SubItems.Add(s.SoCMND ?? "");
                it.SubItems.Add(s.SoDT ?? "");
                it.SubItems.Add(s.DiaChi ?? "");
                lv.Items.Add(it);
            }
        }

        private void FillForm(Student s)
        {
            txtMSSV.Text = s.MSSV ?? "";
            txtHoLot.Text = s.HoTenLot ?? "";
            txtTen.Text = s.Ten ?? "";
            dtpNgaySinh.Value = (s.NgaySinh == default(DateTime)) ? DateTime.Now : s.NgaySinh;
            cmbLop.Text = s.Lop ?? "";
            rNam.Checked = s.GioiTinh == "Nam"; rNu.Checked = s.GioiTinh == "Nữ";
            txtCMND.Text = s.SoCMND ?? "";
            txtDT.Text = s.SoDT ?? "";
            txtDiaChi.Text = s.DiaChi ?? "";

            for (int i = 0; i < clbMonHoc.Items.Count; i++) clbMonHoc.SetItemChecked(i, false);
            if (s.MonHoc != null)
            {
                foreach (var m in s.MonHoc)
                {
                    int idx = clbMonHoc.Items.IndexOf(m);
                    if (idx >= 0) clbMonHoc.SetItemChecked(idx, true);
                    else clbMonHoc.Items.Add(m, true);
                }
            }
        }

        private Student ReadForm()
        {
            Student s = new Student();
            s.MSSV = (txtMSSV.Text ?? "").Trim();
            s.HoTenLot = (txtHoLot.Text ?? "").Trim();
            s.Ten = (txtTen.Text ?? "").Trim();
            s.NgaySinh = dtpNgaySinh.Value.Date;
            s.Lop = (cmbLop.Text ?? "").Trim();
            s.GioiTinh = rNam.Checked ? "Nam" : "Nữ";
            s.SoCMND = (txtCMND.Text ?? "").Trim();
            s.SoDT = (txtDT.Text ?? "").Trim();
            s.DiaChi = (txtDiaChi.Text ?? "").Trim();

            string[] mon = new string[clbMonHoc.CheckedItems.Count];
            for (int i = 0; i < clbMonHoc.CheckedItems.Count; i++)
                mon[i] = clbMonHoc.CheckedItems[i].ToString();
            s.MonHoc = mon;

            if (s.HoTenLot.Length > 20)
            {
                MessageBox.Show("Họ và tên lót không quá 20 ký tự.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (s.Ten.Length > 7)
            {
                MessageBox.Show("Tên không quá 7 ký tự.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (string.IsNullOrWhiteSpace(s.HoTenLot) || string.IsNullOrWhiteSpace(s.Ten))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            string err;
            if (!Student.ValidMSSV(s.MSSV, s.Lop, out err))
            {
                MessageBox.Show(err, "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (!Student.ValidCMND(s.SoCMND))
            {
                MessageBox.Show("Số CMND phải gồm đúng 9 chữ số.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (!Student.ValidSDT(s.SoDT))
            {
                MessageBox.Show("Số điện thoại phải gồm đúng 10 chữ số và bắt đầu bằng 0.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return s;
        }

        private void ClearForm()
        {
            txtMSSV.Clear(); txtHoLot.Clear(); txtTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            txtCMND.Clear(); txtDT.Clear(); txtDiaChi.Clear();
            for (int i = 0; i < clbMonHoc.Items.Count; i++) clbMonHoc.SetItemChecked(i, false);
        }

        private void DoSearch()
        {
            using (SearchForm f = new SearchForm())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    var rs = mgr.SearchExact(f.Mssv, f.Ten, f.Lop).ToList();
                    lv.Items.Clear();
                    foreach (var s in rs)
                    {
                        var it = new ListViewItem(s.MSSV);
                        it.SubItems.Add(((s.HoTenLot ?? "") + " " + (s.Ten ?? "")).Trim());
                        it.SubItems.Add(s.NgaySinh.ToString("dd/MM/yyyy"));
                        it.SubItems.Add(s.Lop ?? "");
                        it.SubItems.Add(s.SoCMND ?? "");
                        it.SubItems.Add(s.SoDT ?? "");
                        it.SubItems.Add(s.DiaChi ?? "");
                        lv.Items.Add(it);
                    }
                }
            }
        }

        private void DoAddOrUpdate(bool isUpdate)
        {
            Student s = ReadForm();
            if (s == null) return;

            bool exists = mgr.Items.Any(x => x.MSSV == s.MSSV);
            if (!isUpdate && exists)
            {
                MessageBox.Show("MSSV đã tồn tại, không thể thêm mới. Hãy dùng Cập nhật.",
                                "Trùng MSSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mgr.AddOrUpdate(s);
            SaveAll();
            RefreshList();
            MessageBox.Show(isUpdate || exists ? "Đã cập nhật." : "Đã thêm mới.");
            ClearForm();
        }

        private void DeleteSelected()
        {
            var mssvs = lv.Items.Cast<ListViewItem>()
                .Where(it => it.Checked)
                .Select(it => it.SubItems[0].Text)
                .ToList();

            if (mssvs.Count == 0)
            {
                MessageBox.Show("Hãy tick chọn vào cột đầu để xóa nhiều sinh viên.", "Thông báo");
                return;
            }
            if (MessageBox.Show("Xóa " + mssvs.Count + " sinh viên đã chọn?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mgr.RemoveByMSSV(mssvs);
                SaveAll();
                RefreshList();
            }
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu";
                ofd.Filter = "Tất cả (*.txt;*.xml;*.json;*.csv;*.xls;*.xlsx)|*.txt;*.xml;*.json;*.csv;*.xls;*.xlsx|TXT (*.txt)|*.txt|XML (*.xml)|*.xml|JSON (*.json)|*.json|CSV (*.csv)|*.csv|Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        LoadFromAny(ofd.FileName);
                        RefreshList();
                        MessageBox.Show("Đã nạp: " + Path.GetFileName(ofd.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không nạp được: " + ex.Message);
                    }
                }
            }
        }

        private void LoadFromAny(string file)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext == ".txt") mgr.LoadTxt(file);
            else if (ext == ".xml") mgr.LoadXml(file);
            else if (ext == ".json") mgr.LoadJson(file);
            else if (ext == ".csv") mgr.LoadCsv(file);
            else if (ext == ".xls" || ext == ".xlsx") mgr.LoadExcel(file, null);
            else throw new NotSupportedException("Không hỗ trợ định dạng: " + ext);
        }

        private void SaveAll()
        {
            try
            {
                mgr.SaveTxt(pathTxt);
                mgr.SaveXml(pathXml);
                mgr.SaveJson(pathJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
