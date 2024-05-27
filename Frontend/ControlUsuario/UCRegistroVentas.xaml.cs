using Castle.Components.DictionaryAdapter;
using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Frontend.Charts;
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
    /// Lógica de interacción para UCRegistroVentas.xaml
    /// </summary>
    public partial class UCRegistroVentas : UserControl
    {
        /// <summary>
        /// Contexto de la base de  datos
        /// </summary>
        private KioscoContext kioscoContext;
        /// <summary>
        /// ViewModel de Venta
        /// </summary>
        private MVVenta mvVenta;
        /// <summary>
        /// Contexto de la ventana principal para poder abrir un UserControl en el panel principal
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext">Contexto de la base de datos</param>
        /// <param name="mvUsuario">ViewModel de Usuario</param>
        /// <param name="mainWindow">Contexto de la ventana principal</param>
        public UCRegistroVentas(KioscoContext kioscoContext, MVUsuario mvUsuario, MainWindow mainWindow)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            mvVenta = new MVVenta(kioscoContext, mvUsuario.usuLogin);
            this.DataContext = mvVenta;
            this.mainWindow = mainWindow;

            dpFecha.DisplayDate = DateTime.Today;
        }

        /// <summary>
        /// Boton que abre la ventana para añadir una nueva Venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnyadirVenta_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow != null)
            {
                Grid panelCentral = mainWindow.PanelCentral;
                UCVentas uc = new UCVentas(kioscoContext, mvVenta.usuLogin, mainWindow);
                if (panelCentral.Children != null)
                {
                    panelCentral.Children.Clear();
                    panelCentral.Children.Add(uc);
                }
            }

            mvVenta.listaDetalleVenta.Clear();
        }

        /// <summary>
        /// Botón para eliminar una Venta del datagrid de ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgVentas.SelectedItem != null)
            {
                Venta ventaAEliminar = dgVentas.SelectedItem as Venta;

                if (ventaAEliminar != null)
                {
                    foreach (var detalle in ventaAEliminar.Detalleventa.ToList())
                    {
                        ventaAEliminar.Detalleventa.Remove(detalle);
                    }

                    if (mvVenta.delete(ventaAEliminar))
                    {
                        popEliminado.IsOpen = true;
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        popEliminado.IsOpen = false;

                        mvVenta.listaVentas.Refresh();
                        dgVentas.Items.Refresh();

                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar de la base de datos\"", "GESTION VENTAS", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }

        }

        /// <summary>
        /// Método que detecta cuándo el usuario ha cambiado la fecha seleccionada en el datepicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            mvVenta.fechaSeleccionada = dpFecha.SelectedDate;
        }

        /// <summary>
        /// Botón para filtrar las ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            mvVenta.filtrar();
        }

        /// <summary>
        /// Botón que limpia los filtros y muestra la lista de Ventas completa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFiltros_Click(object sender, RoutedEventArgs e)
        {
            cbEmpleados.Text = "";
            dpFecha.Text = "";
            mvVenta.filtrar();
        }

        /// <summary>
        /// Botón que abre el chart de ventas por mes en un diálogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVentasPorMes_Click(object sender, RoutedEventArgs e)
        {
            ChartVentasPorMes diag = new ChartVentasPorMes(mvVenta);
            diag.ShowDialog();
        }
    }
}
