using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    // templates
    class Template
    {
        // for generating labels
        protected Label GenerateLabel(Control parent,string text="",Color color=default(Color), Font font = default(Font), Size size = default(Size))
        {

            Label label = new Label();
            label.Text = text;
            label.ForeColor = color;
            label.Size = size;
            label.Font = font;
            label.AutoSize = true;
            label.Parent = parent;
            return label;
        }
        // for generating Textbox
        protected TextBox GenerateTextBox(Control parent, string text = "", Color color = default(Color), Font font = default(Font), Size size = default(Size))
        {

            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.ForeColor = color;
            textBox.Size = size;
            textBox.Font = font;
            textBox.AutoSize = true;
            textBox.Parent = parent;
            return textBox;
        }
        // for generating Button
        protected Button GenerateButton(Control parent, string text = "", Color color = default(Color), Size size = default(Size))
        {

            Button btn = new Button();
            btn.Text = text;
            btn.ForeColor = color;
            btn.Size = size;
            btn.Parent = parent;
            return btn;
        }

        // for generating Panel Buffered without flickering and only visible when completed 
        protected Panel GeneratePanel(Control parent=null)
        {
            BufferedPanel panel = new BufferedPanel();
            if(parent!=null)
                panel.Parent = parent;
           
            return panel;
        }

        protected PictureBox GeneratePictureBox(Control parent,Image pic, Size size = default(Size))
        {
            PictureBox pictureBox = new PictureBox();
            try
            {
                pictureBox.Size = size;
                pictureBox.Image =  pic;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                pictureBox.Image = null;
            }
            pictureBox.Parent = parent;
            return pictureBox;
        }


        protected Size GetSize(float widthPercent, float heightPercent)
        {
            return new Size((int)Math.Ceiling(widthPercent), (int)Math.Ceiling(heightPercent));
        }

        protected void CenterControl(Control control,bool alignY=false)
        {
            control.Left = (control.Parent.Width - control.Width) / 2;
            if(alignY)
                control.Top = (control.Parent.Height - control.Height) / 2;
        }

        protected void CenterControl(Control control, Control old,bool alignY = false)
        {
            control.Left = (control.Parent.Width - control.Width) / 2;
            if (alignY)
                control.Top = (old.Height - control.Height) / 2;
        }

    }

    class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_COMPOSITED = 0x02000000;
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }
    }
}
