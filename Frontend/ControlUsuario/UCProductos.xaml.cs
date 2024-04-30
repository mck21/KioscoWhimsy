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
    /// Lógica de interacción para UCProductos.xaml
    /// </summary>
    public partial class UCProductos : UserControl
    {
        private KioscoContext kioscoContext;
        private MVVenta mvVenta;

        public UCProductos(KioscoContext kioscoContext)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            inicializa();
        }

        private void inicializa()
        {
            mvVenta = new MVVenta(kioscoContext);
            this.DataContext = mvVenta;
        }

        private void btnAnyadirProducto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditarItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
