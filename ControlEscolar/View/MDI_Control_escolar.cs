using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlEscolar.View
{
    public partial class MDI_Control_escolar : Form
    {
        public MDI_Control_escolar()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void estudiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbreVentanaHija("frmestudiantes");
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbreVentanaHija("frmroles");
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AbreVentanaHija("frmusuarios");
        }


        private void reporte111ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbreVentanaHija("frmreporte111");
        }

        private void reporte12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbreVentanaHija("frmreporte12");
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mosaicoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void ventanasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reporte2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AbreVentanaHija(string nombre_forma)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name.ToLower() == nombre_forma)
                {
                    // Si la ventana ya está abierta, traerla al frente y restaurarla si estaba minimizada
                    form.WindowState = FormWindowState.Normal;
                    form.BringToFront();
                    return;
                }
            }

            // Si no está abierta, crear y mostrar una nueva instancia
            Form childForm;
            switch (nombre_forma.ToLower())
            {
                case "frmestudiantes":
                    childForm = new frmEstudiantes(this);
                    break;
                case "frmreporte111":
                    childForm = new frmReporte111(this);
                    break;
                case "frmreporte12":
                    childForm = new frmReporte12(this);
                    break;
                case "frmroles":
                    childForm = new frmRoles(this);
                    break;
                case "frmusuarios":
                    childForm = new frmUsuarios(this);
                    break;
                default:
                    return;
            }
            childForm.Show();
        }
    }
}
