using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Backend.Servicios;
using Kiosco_Whimsy.MVVM;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kiosco_Whimsy.Frontend.Login
{
    /// <summary>
    /// Conexión con la base de datos y
    /// lógica de usuarios de la base de datos para hacer login en la aplicación  
    /// </summary>
    public partial class Login : MetroWindow
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;

        /// <summary>
        /// Constructor, si hay conexion con la base de datos lanza la aplicacion
        /// si no, lanza un mensaje de error
        /// </summary>
        public Login()
        {
            if (ConectaBD())
            {
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("ERROR!!! No hay comunicacion con la base de datos\n" +
                    "Ponte en contacto con tu administrador de sistema",
                    "ACCESO A LA BASE DE DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Abre la conexion con la base de datos
        /// </summary>
        /// <returns>
        /// True si ha podido hacer la conexion, en caso contrario False
        /// </returns>
        private bool ConectaBD()
        {
            bool conecta = true;
            kioscoContext = new KioscoContext();
            try
            {
                kioscoContext.Database.OpenConnection();
            }
            catch (Exception ex)
            {
                conecta = false;
            }
            return conecta;
        }

        /// <summary>
        /// Boton que abre la ventana principal si las credenciales de usuario coinciden con las de la base de datos
        /// En caso de ser incorrectas o de que el usuario deje campos vacios, salta un mensaje de error (MahApps)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UsuarioServicio usuServ = new UsuarioServicio(kioscoContext);
            if (usuServ.login(txtNombreUsuario.Text, passContrasenya.Password))
            {
                MainWindow ventanaPrincipal = new MainWindow(kioscoContext, usuServ.usuLogin);
                ventanaPrincipal.Show();
                this.Close();
            }
            else if (txtNombreUsuario.Text == "" || passContrasenya.Password == "")
            {
                await this.ShowMessageAsync("LOGIN DE USUARIO",
                    "El campo de nombre de usuario o el de la contraseña está vacío\n" +
                    "Por favor complete ambos campos");
            }
            else
            {
                await this.ShowMessageAsync("LOGIN DE USUARIO",
                    "Usuario o contraseña incorrectos");
            }
        }

        /// <summary>
        /// Boton que cierra la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
