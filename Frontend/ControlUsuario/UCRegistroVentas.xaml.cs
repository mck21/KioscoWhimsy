using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.MVVM;
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

namespace Kiosco_Whimsy.Frontend.ControlUsuario
{
    /// <summary>
    /// Lógica de interacción para UCRegistroVentas.xaml
    /// </summary>
    public partial class UCRegistroVentas : UserControl
    {
        private KioscoContext kioscoContext;
        private MVVenta mvVenta; 
        private MainWindow mainWindow;

        // Constructor que recibe una instancia de MainWindow
        public UCRegistroVentas(KioscoContext kioscoContext, Usuario usuLogin, MainWindow mainWindow)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            mvVenta = new MVVenta(kioscoContext, usuLogin);
            this.DataContext = mvVenta;
            this.mainWindow = mainWindow;

            MVVenta.yaHanSidoCargadas = true;
        }

        private void btnAnyadirVenta_Click(object sender, RoutedEventArgs e)
        {
            // Utilizar mainWindow para acceder a PanelCentral
            if (mainWindow != null)
            {
                Grid panelCentral = mainWindow.PanelCentral;
                UCVentas uc = new UCVentas(kioscoContext, mvVenta.usuLogin);
                if (panelCentral.Children != null)
                {
                    panelCentral.Children.Clear();
                    panelCentral.Children.Add(uc);
                }
            }
        }

        private void btnEditarItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
