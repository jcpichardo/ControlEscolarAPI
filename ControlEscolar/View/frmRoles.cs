﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlEscolar.Utilities;
namespace ControlEscolar.View
{
    public partial class frmRoles : Form
    {
        public frmRoles(Form parent)
        {
            InitializeComponent();
            Formas.InicializaForma(this, parent);
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
          
        }
    }
}
