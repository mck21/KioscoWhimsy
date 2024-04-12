using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.MVVM;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace Kiosco_Whimsy.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogoCambioContrasenya.xaml
    /// </summary>
    public partial class DialogoCambioContrasenya : MetroWindow
    {
        /// <summary>
        /// ViewModel de Usuario y contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;
        private MVUsuario mvUsuario;

        /// <summary>
        /// Variable que recibirá el usuario que ha iniciado sesión
        /// </summary>
        private Usuario usuLogin;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos y el usuario que ha iniciado sesión
        /// y asigna al mvUsuario como DataContext
        /// </summary>
        /// <param name="kioscoContext"></param>
        /// <param name="usuLogin"></param>
        public DialogoCambioContrasenya(KioscoContext kioscoContext, Usuario usuLogin)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            this.usuLogin = usuLogin;
            mvUsuario = new MVUsuario(kioscoContext);
            this.DataContext = mvUsuario; 
        }

        /// <summary>
        /// Botón para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Botón para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Botón que controla la lógica para cambiar la contraseña del usuario
        /// que ha iniciado sesión y lo actualiza en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (passAntigua.Password != "" && passNueva.Password != "")
            {
                if (usuLogin.Password == passAntigua.Password)
                {
                    usuLogin.Password = passNueva.Password;

                    if (mvUsuario.update(usuLogin))
                    {
                        popCorrecto.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        DialogResult = true;
                        Close();
                        /*mvUsuario.listaUsuarios.EditItem(mvUsuario);
                        mvUsuario.listaUsuarios.CommitEdit();
                        mvUsuario.listaUsuarios.Refresh();*/
                    }
                    else
                    {
                        await this.ShowMessageAsync("MODIFICAR CONTRASEÑA", 
                            "Error al guardar en la base de datos");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("MODIFICAR CONTRASEÑA",
                        "Contraseña incorrecta, vuelva a intentarlo otra vez");
                }                
            }
            else
            {
                await this.ShowMessageAsync("MODIFICAR CONTRASEÑA",
                    "Rellene ambos campos por favor");
            }

        }

        
    }
}
