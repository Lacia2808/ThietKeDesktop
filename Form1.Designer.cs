namespace _2212364_PVQHien_Lab03
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Controls used by Form1.cs logic
        private System.Windows.Forms.Label lblMSSV;
        private System.Windows.Forms.TextBox txtMSSV;

        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.RadioButton rdoNam;
        private System.Windows.Forms.RadioButton rdoNu;

        private System.Windows.Forms.Label lblHoLot;
        private System.Windows.Forms.TextBox txtHoLot;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;

        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cmbLop;

        private System.Windows.Forms.Label lblCMND;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtDT; // note: named txtDT to match logic

        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;

        private System.Windows.Forms.GroupBox grpMonHoc;
        private System.Windows.Forms.CheckedListBox clbMonHoc; // important: matches logic
        private System.Windows.Forms.ContextMenuStrip ctxMonHoc;
        private System.Windows.Forms.ToolStripMenuItem ctxAddMon;
        private System.Windows.Forms.ToolStripMenuItem ctxRemoveMon;

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExit;

        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.ListView lvStudents;
        private System.Windows.Forms.ColumnHeader colMSSV;
        private System.Windows.Forms.ColumnHeader colHoLot;
        private System.Windows.Forms.ColumnHeader colTen;
        private System.Windows.Forms.ColumnHeader colNgaySinh;
        private System.Windows.Forms.ColumnHeader colLop;
        private System.Windows.Forms.ColumnHeader colCMND;
        private System.Windows.Forms.ColumnHeader colSDT;
        private System.Windows.Forms.ColumnHeader colDiaChi;

        private System.Windows.Forms.ContextMenuStrip ctxLv;
        private System.Windows.Forms.ToolStripMenuItem ctxDeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem ctxDeleteOne;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }
        #endregion
    }
}
