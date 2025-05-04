using ControlEscolar.View;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ControlEscolar.Utilities;
using System.IO;
using OfficeOpenXml;

namespace ControlEscolar
{


    static class Program
    {
        // Logger para el programa principal
        private static Logger? _logger;

        /// <summary>
        /// STA (Single-Threaded Apartment) es un modelo de subprocesos (threads) 
        /// usado en COM (Component Object Model)
        /// Clipboard.SetText() (portapapeles)
        ///OpenFileDialog / SaveFileDialog
        /// </summary>
        [STAThread]
        static void Main()
        {

            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");

            // Inicializar el sistema de logging
            //_logger = LoggingManager.GetLogger("ControlEscolar.Program");
            //_logger.Info("Aplicación iniciada");

            //Activa los estilos visuales modernos de Windows
            // Sin esta línea, los controles como botones,
            // cuadros de texto y pestañas se verán con el estilo antiguo de Windows 95/98.
            Application.EnableVisualStyles();
            //Se usa para evitar que los controles usen el motor de renderizado de texto
            //antiguo de .NET 1.0, que era menos eficiente.
            Application.SetCompatibleTextRenderingDefault(false);

            //Inicia la aplicación con la ventana de inicio
            //Application.Run(new View.frmLogin());

            frmLogin login_form = new frmLogin();
            if (login_form.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MDI_Control_escolar());
            }
        }
    }
}
