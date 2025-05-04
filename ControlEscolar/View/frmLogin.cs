using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlEscolarCore.Bussines;
using ControlEscolar.Utilities;
using NLog;

namespace ControlEscolar.View
{
    public partial class frmLogin : Form
    {
        //private static readonly Logger _logger = LoggingManager.GetLogger("ControlEscolar.View.frmLogin");


        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            //{
            //    MessageBox.Show("El campo de usuario no puede estar vacío.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            //{
            //    MessageBox.Show("El campo de contraseña no puede estar vacío.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            //if (!UsuariosNegocio.EsFormatoValido(txtUsuario.Text))
            //{
            //    MessageBox.Show("El nombre de usuario no tiene un formato correcto.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            //MessageBox.Show("Listo para iniciar sesión.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //Estamos listos para iniciar sesión en proximas clases, por lo pronto simplemente ocultamos la ventana de login y mostramos el MDI
            //this.Close();
            //MDI_Control_escolar mdi = new MDI_Control_escolar();
            //mdi.Show();

            #region Comentarios adicionales
            //OJO, ¿QUE PASA CON EL PROYECTO, POR NO SE TERMINO DE EJECUTAR AL SALIR?
            #endregion

            #region Solución a los comentarios
            this.DialogResult = DialogResult.OK;
            this.Close();
            #endregion


        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                // Aquí provocamos una primera excepción
                try
                {
                    int divisor = 0;
                    int resultado = 10 / divisor; // Esto generará una DivideByZeroException
                }
                catch (DivideByZeroException ex)
                {
                    // Capturamos la primera excepción y la envolvemos en otra
                    throw new ApplicationException("Error al realizar el cálculo en la aplicación", ex);
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción que contiene la inner exception

                // O registrar específicamente usando la inner exception
            }
        }
    }
}
