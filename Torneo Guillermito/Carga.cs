using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIPa
{
    public partial class Carga : Form
    {
        public ProgressBar progress;
        public Label label;
        public Carga()
        {

            InitializeComponent();
            this.progress = progressBar1;
            this.label = label1;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Carga_Load(object sender, EventArgs e)
        {
        }
    }
}
