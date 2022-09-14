using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleLifeSimulation
{
    public partial class PanelSetting : UserControl
    {
        public PanelSetting()
        {
            InitializeComponent();
        }

        private void PanelSetting_Load(object sender, EventArgs e)
        {
            ReSizeContent();
        }

        private void ReSizeContent()
        {
            
        }

        public void Add(Control ctrl)
        {
            int top = 0;
            foreach (Control control in this.Controls) top += control.Height;
            ctrl.Top = top;
            this.Controls.Add(ctrl);
        }
    }
}
