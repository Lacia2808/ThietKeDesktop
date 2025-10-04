
using System.Drawing;
using System.Windows.Forms;

namespace _2212364_PVQHien_Lab03
{
    public class SimpleInputBox : Form
    {
        TextBox txt;
        public string Value { get { return txt.Text.Trim(); } }

        public SimpleInputBox(string title, string label, string placeholder)
        {
            Text = title;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(420, 130);

            Label lb = new Label();
            lb.Text = label;
            lb.Dock = DockStyle.Top;
            lb.Padding = new Padding(8);

            txt = new TextBox();
            txt.Dock = DockStyle.Top;
            txt.Text = placeholder ?? "";

            FlowLayoutPanel panelBtn = new FlowLayoutPanel();
            panelBtn.Dock = DockStyle.Bottom;
            panelBtn.Height = 44;
            panelBtn.FlowDirection = FlowDirection.RightToLeft;

            Button ok = new Button();
            ok.Text = "OK";
            ok.DialogResult = DialogResult.OK;
            ok.Width = 90;

            Button cancel = new Button();
            cancel.Text = "Hủy";
            cancel.DialogResult = DialogResult.Cancel;
            cancel.Width = 90;

            panelBtn.Controls.Add(ok);
            panelBtn.Controls.Add(cancel);

            Controls.Add(panelBtn);
            Controls.Add(txt);
            Controls.Add(lb);

            AcceptButton = ok;
            CancelButton = cancel;
        }
    }
}
