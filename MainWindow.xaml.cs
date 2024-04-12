using di.proyecto2023.MVVM;
using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Frontend.ControlUsuario;
using Kiosco_Whimsy.Frontend.Dialogos;
using Kiosco_Whimsy.MVVM;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kiosco_Whimsy
{
    /// <summary>
    /// Lógica de interacción para MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Contexto de la base de datos y viewModel de usuario
        /// </summary>
        private KioscoContext kioscoContext;
        private MVUsuario mvUsuario;

        /// <summary>
        /// Constructor que pasa el contexto de la base de datos, el usuario que ha iniciado sesión 
        /// y el viewModel de usuario que despues instancia con el contexto de la base de datos
        /// Además, modifica la visibilidad de las funciones de la ventana principal dependiendo del 
        /// rol del usuario que ha iniciado sesión
        /// </summary>
        /// <param name="kioscoContext"></param>
        /// <param name="usuLogin"></param>
        /// <param name="mvUsuario"></param>
        public MainWindow(KioscoContext kioscoContext, Usuario usuLogin)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            mvUsuario = new MVUsuario(kioscoContext, usuLogin);
            this.mvUsuario.usuLogin = usuLogin;
            this.DataContext = mvUsuario;

            UCCircuito uc = new UCCircuito();
            if (panelCentral.Children != null)
            {

                panelCentral.Children.Clear();
                panelCentral.Children.Add(uc);
            }

            // Mostrar u ocultar botones según el rol del usuario
            switch (usuLogin.Rol.NombreRol)
            {
                case "Gerente":
                    // Puede ver todas las funciones
                    break;
                case "Encargado":

                    btnUsuarios.Visibility = Visibility.Collapsed;
                    /*
                    btnGestionContraseñas.Visibility = Visibility.Collapsed;
                    btnEdicionPermisos.Visibility = Visibility.Collapsed;
                    btnEdicionRoles.Visibility = Visibility.Collapsed;
                    */
                    break;
                case "Empleado":

                    btnUsuarios.Visibility = Visibility.Collapsed;
                    btnCampanyasPublicidad.Visibility = Visibility.Collapsed;
                    /*
                    btnGestionContraseñas.Visibility = Visibility.Collapsed;
                    btnEdicionPermisos.Visibility = Visibility.Collapsed;
                    btnEdicionRoles.Visibility = Visibility.Collapsed;                    
                    btnModificarProducto.Visibility = Visibility.Collapsed;
                    btnEliminarProducto.Visibility = Visibility.Collapsed;
                    btnDevolucionVenta.Visibility = Visibility.Collapsed;
                    */
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Botón que cierra la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Botón para maximizar o minimizar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                btnMaximizarImage.Source = new BitmapImage(new Uri("/Recursos/Iconos/PajamasMaximize.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                btnMaximizarImage.Source = new BitmapImage(new Uri("/Recursos/Iconos/PajamasMinimize.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// Boton para abrir el dialogo para modificar la contraseña del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContrasenya_Click(object sender, RoutedEventArgs e)
        {
            DialogoCambioContrasenya diag = new DialogoCambioContrasenya(kioscoContext, mvUsuario.usuLogin);
            diag.ShowDialog();
        }

        /// <summary>
        /// Boton para volver a la pantalla de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInicio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            panelCentral.Children.Clear();

            Image logo = new Image();
            logo.Source = new BitmapImage(new Uri("/Recursos/Iconos/SweetLogo.png", UriKind.Relative));
            logo.Height = 200;
            logo.Width = 200;
            logo.Margin = new System.Windows.Thickness(0, 0, 0, 10);

            panelCentral.Children.Add(logo);
        }

        private void btnVentas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnStock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnUsuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UCUsuarios uc = new UCUsuarios(kioscoContext);
            if (panelCentral.Children != null)
            {

                panelCentral.Children.Clear();
                panelCentral.Children.Add(uc);
            }
        }

        private void btnCampanyasPublicidad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
