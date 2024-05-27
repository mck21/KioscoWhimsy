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
    /// Lógica de interacción para DialogoAnyadirProducto.xaml
    /// </summary>
    public partial class DialogoAnyadirProducto : MetroWindow
    {
        /// <summary>
        /// ViewModel de Producto y contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;
        private MVProducto mvProducto;

        public DialogoAnyadirProducto(KioscoContext kioscoContext)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            mvProducto = new MVProducto(kioscoContext);
            this.DataContext = mvProducto;

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvProducto.OnErrorEvent));
            mvProducto.btnGuardar = btnGuardar; 

        }

        public DialogoAnyadirProducto(KioscoContext kioscoContext, Producto productoSeleccionado)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            mvProducto = new MVProducto(kioscoContext);
            this.DataContext = mvProducto;

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvProducto.OnErrorEvent));
            mvProducto.btnGuardar = btnGuardar;


            mvProducto.producto = productoSeleccionado;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (mvProducto.IsValid(this))
            {
                mvProducto.producto.Iva = 0.16;
                mvProducto.producto.Precio = Math.Round(mvProducto.producto.Precio, 2);

                if (mvProducto.update(mvProducto.producto))
                {
                    popCorrecto.IsOpen = true;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    DialogResult = true;
                    Close();
                    //editar
                    mvProducto.listaProductos2.EditItem(mvProducto);
                    mvProducto.listaProductos2.CommitEdit();
                    mvProducto.listaProductos2.Refresh();

                }
                else
                {
                    await this.ShowMessageAsync("GESTION STOCK", "ERROR!! No se puede insertar en la base de datos");
                }
            }
            else
            {
                await this.ShowMessageAsync("GESTION STOCK", "CUIDADO!! Rellena todos los campos obligatorios");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
