using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIBS2018
{
    public partial class WhatNext : Form
    {
        public int data = 0;
        public WhatNext()
        {
            InitializeComponent();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            data = 1;
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            data = 2;
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            data = 3;
            this.Close();
        }
    }
}
