using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardGenIDE
{
    public partial class frmSelItem : Form
    {
        public frmSelItem()
        {
            InitializeComponent();
        }


        public DialogResult ShowDialog(ref CardObjectType selType)
        {
            if (selType == CardObjectType.label) rbLabel.Checked = true;
            else if (selType == CardObjectType.text) rbText.Checked = true;
            else if (selType == CardObjectType.image) rbImage.Checked = true;
            else if (selType == CardObjectType.shape) rbShape.Checked = true;
            DialogResult res = base.ShowDialog();
            if (rbLabel.Checked) selType = CardObjectType.label;
            else if (rbImage.Checked) selType = CardObjectType.image;
            else if (rbText.Checked) selType = CardObjectType.text;
            else if (rbShape.Checked) selType = CardObjectType.shape;
            return res;
        }

        private void frmSelItem_Load(object sender, EventArgs e)
        {

        }
    }
}
