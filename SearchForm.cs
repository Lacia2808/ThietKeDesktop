
using System.Drawing;
using System.Windows.Forms;

namespace _2212364_PVQHien_Lab03
{
    public class SearchForm : Form
    {
        public string Mssv { get { return txtMssv.Text.Trim(); } }
        public string Ten  { get { return txtTen.Text.Trim(); } }
        public string Lop  { get { return txtLop.Text.Trim(); } }

        private TextBox txtMssv = new TextBox();
        private TextBox txtTen  = new TextBox();
        private TextBox txtLop  = new TextBox();

        public SearchForm()
        {
            Text = "Tìm kiếm sinh viên";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(440, 190);

            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.Dock = DockStyle.Fill;
            tbl.ColumnCount = 2;
            tbl.Padding = new Padding(10);
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
            for (int i = 0; i < 3; i++) tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Label lb1 = new Label(); lb1.Text = "MSSV:"; lb1.TextAlign = ContentAlignment.MiddleRight;
            Label lb2 = new Label(); lb2.Text = "Tên:";  lb2.TextAlign = ContentAlignment.MiddleRight;
            Label lb3 = new Label(); lb3.Text = "Lớp:";  lb3.TextAlign = ContentAlignment.MiddleRight;

            tbl.Controls.Add(lb1, 0, 0); tbl.Controls.Add(txtMssv, 1, 0);
            tbl.Controls.Add(lb2, 0, 1); tbl.Controls.Add(txtTen,  1, 1);
            tbl.Controls.Add(lb3, 0, 2); tbl.Controls.Add(txtLop,  1, 2);

            FlowLayoutPanel panelBtn = new FlowLayoutPanel();
            panelBtn.Dock = DockStyle.Fill;
            panelBtn.FlowDirection = FlowDirection.RightToLeft;

            Button btnFind  = new Button(); btnFind.Text  = "Tìm";  btnFind.Width  = 90; btnFind.DialogResult  = DialogResult.OK;
            Button btnClose = new Button(); btnClose.Text = "Đóng"; btnClose.Width = 90; btnClose.DialogResult = DialogResult.Cancel;
            panelBtn.Controls.Add(btnFind);
            panelBtn.Controls.Add(btnClose);

            tbl.Controls.Add(panelBtn, 0, 3); tbl.SetColumnSpan(panelBtn, 2);

            Controls.Add(tbl);
            AcceptButton = btnFind; CancelButton = btnClose;
        }
    }
}
