using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Frontend.Dialogos;
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
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;
        /// <summary>
        /// ViewModel de Producto
        /// </summary>
        private MVProducto mvProducto;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext">Contexto de la base de datos</param>
        public UCProductos(KioscoContext kioscoContext)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            inicializa();
        }

        /// <summary>
        /// Método que instancia los objetos necesarios
        /// </summary>
        private void inicializa()
        {
            mvProducto = new MVProducto(kioscoContext);
            this.DataContext = mvProducto;
        }

        /// <summary>
        /// Botón que abre el dialogo para añadir un producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnyadirProducto_Click(object sender, RoutedEventArgs e)
        {
            DialogoAnyadirProducto diag = new DialogoAnyadirProducto(kioscoContext);
            diag.ShowDialog();
        }

        /// <summary>
        /// Boton que elimina un producto del datagrid de productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem != null)
            {
                Producto productoAEliminar = dgProductos.SelectedItem as Producto;

                if (productoAEliminar != null)
                {

                    if (mvProducto.delete(productoAEliminar))
                    {
                        dgProductos.Items.Refresh();

                        popEliminado.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        popEliminado.IsOpen = false;                       

                        mvProducto.listaProductos2.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar de la base de datos\"", "GESTION PRODUCTOS", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }

        }

        /// <summary>
        /// Boton que abre el dialogo para modificar el producto seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditarItem_Click(object sender, RoutedEventArgs e)
        {
            DialogoAnyadirProducto diag = new DialogoAnyadirProducto(kioscoContext, (Producto)dgProductos.SelectedItem);
            diag.ShowDialog();
            dgProductos.Items.Refresh();
        }
    }
}
