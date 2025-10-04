namespace _2212364_PVQHien_Lab03
{
    partial class SearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMSSV;
        private System.Windows.Forms.TextBox txtMSSV;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.TextBox txtLop;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SearchForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SearchForm";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.ResumeLayout(false);

        }
    }
}
