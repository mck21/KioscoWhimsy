using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.MVVM;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Kiosco_Whimsy.Frontend.ControlUsuario
{
    /// <summary>
    /// Lógica de interacción para UCVentas.xaml
    /// </summary>
    public partial class UCVentas : UserControl
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;
        /// <summary>
        /// ViewModel de Venta
        /// </summary>
        private MVVenta mvVenta;
        MVProducto mvProducto;
        /// <summary>
        /// Contexto de la ventana principal
        /// </summary>
        private MainWindow mainWindow;
        /// <summary>
        /// Detalle de venta buscado en base de datos
        /// </summary>
        private Detalleventa detalleVentaExistente;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext"></param>
        /// <param name="usuLogin"></param>
        /// <param name="mainWindow"></param>
        public UCVentas(KioscoContext kioscoContext, Usuario usuLogin, MainWindow mainWindow)
        {
            InitializeComponent();
            this.kioscoContext = kioscoContext;
            mvVenta = new MVVenta(kioscoContext, usuLogin);
            this.DataContext = mvVenta;

            mvProducto = new MVProducto(kioscoContext);
            this.mainWindow = mainWindow;

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvVenta.OnErrorEvent));

            mvVenta.btnGuardar = btnGuardar;

            mvVenta.cargarRutaRelativaDeImagenes();
            cargaCategorias();
            cargaProductos();
        }

        /// <summary>
        /// Recorre la lista de categorias y le asigna las propiedades a cada uno de los botones de categoria
        /// </summary>
        private void cargaCategorias()
        {
            foreach (Tipoproducto categoria in mvVenta.listaCategorias)
            {
                BtnCategorias btn = new BtnCategorias();
                if(categoria.Imagen != null && categoria.Categoria != null)
                {
                    btn.imagen = categoria.Imagen;
                    btn.nombre = categoria.Categoria;
                }
                btn.tipo = categoria;
                btn.inicializa();
                wpCategorias.Children.Add(btn);
                btn.Click += new RoutedEventHandler(btnCategoria_Click);
            }
        }

        /// <summary>
        /// Recorre la lista de productos y le asigna las propiedades a cada uno de los botones de producto
        /// </summary>
        private void cargaProductos()
        {
            foreach (Producto producto in mvVenta.listaProductos)
            {
                BtnProductos btn = new BtnProductos();
                btn.imagen = producto.Imagen;
                btn.nombre = producto.Descripcion;
                btn.producto = producto;
                btn.inicializa();
                wpProductos.Children.Add(btn);
                btn.Click += new RoutedEventHandler(btnProducto_Click);
            }
        }

        /// <summary>
        /// Botón que filtra la lista de productos por categoria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCategoria_Click(object sender, RoutedEventArgs e)
        {
            BtnCategorias btn = (BtnCategorias)sender;
            Tipoproducto? categoriaSeleccionada = btn.tipo;

            mvVenta.listaProductos.Clear();
            foreach (Producto producto in mvVenta.listaAllProductos)
            {
                if (categoriaSeleccionada == producto.Tipoproducto)
                {
                    mvVenta.listaProductos.Add(producto);
                }
            }

            wpProductos.Children.Clear();
            cargaProductos();
        }

        /// <summary>
        /// Botón que añade el prodcuto seleccionado a la lista de detalle de venta
        /// Maneja la cantidad si el producto ya ha sido seleccionado previamente
        /// Muestra un mensaje en el caso de no haber stock suficiente de ese producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnProducto_Click(object sender, RoutedEventArgs e)
        {
            BtnProductos btn = (BtnProductos)sender;
            Producto? productoSeleccionado = btn.producto;

            if (productoSeleccionado != null)
            {
                if (productoSeleccionado.Stock > 0)
                {
                    detalleVentaExistente = mvVenta.listaDetalleVenta
                        .FirstOrDefault(dv => dv.ProductoId == productoSeleccionado.Idproducto);

                    if (detalleVentaExistente != null)
                    {
                        detalleVentaExistente.Cantidad++;
                    }
                    else
                    {
                        Detalleventa detalleVenta = new Detalleventa
                        {
                            Cantidad = 1,
                            PrecioUnitario = productoSeleccionado.Precio,
                            ProductoId = productoSeleccionado.Idproducto,
                            Producto = productoSeleccionado
                        };

                        mvVenta.listaDetalleVenta.Add(detalleVenta);
                    }

                    productoSeleccionado.Stock--;

                    mvVenta.listAuxDetalleVenta.Refresh();

                    mvVenta.calcularTotal();
                }
                else
                {
                    productoSeleccionado.Stock = 0;

                    popStockInsuficiente.IsOpen = true;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    popStockInsuficiente.IsOpen = false;
                }
            }

        }

        /// <summary>
        /// Botón que limpia los filtros y muestra la lista con todos los productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiarFiltros_Click(object sender, RoutedEventArgs e)
        {
            mvVenta.limpiar();

            wpProductos.Children.Clear();
            cargaProductos();
        }

        /// <summary>
        /// Botón que guarda la venta en la base de datos 
        /// Manda al usuario a la gestión de stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (mvVenta.IsValid(this))
            {
                mvVenta.venta.Detalleventa.Clear();

                mvVenta.venta.Total = mvVenta.Total;
                mvVenta.venta.Usuario = mvVenta.usuLogin;
                mvVenta.venta.Fecha = mvVenta.FechaVenta;
                mvVenta.venta.Detalleventa = mvVenta.listaDetalleVenta;


                if (mvVenta.update(mvVenta.venta))
                {
                    popCorrecto.IsOpen = true;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    popCorrecto.IsOpen = false;

                    Grid panelCentral = mainWindow.PanelCentral;
                    UCVentas uc = new UCVentas(kioscoContext, mvVenta.usuLogin, mainWindow);
                    if (panelCentral.Children != null)
                    {
                        panelCentral.Children.Clear();
                        panelCentral.Children.Add(uc);
                    }

                    mvVenta.listaVentas.Refresh();
                }
                else
                {
                    MessageBox.Show("No se puede insertar en la base de datos\"", "GESTION VENTAS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Rellena todos los campos obligatorios", "GESTION VENTAS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Botón que elimina el último producto añadido a la lista de detalle de venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeshacer_Click(object sender, RoutedEventArgs e)
        {
            if (mvVenta.listaDetalleVenta.Count > 0)
            {
                var ultimoDetalle = mvVenta.listaDetalleVenta.Last();

                if (ultimoDetalle.Producto != null && ultimoDetalle.Cantidad.HasValue)
                {
                    ultimoDetalle.Producto.Stock += ultimoDetalle.Cantidad.Value;
                }

                mvVenta.listaDetalleVenta.RemoveAt(mvVenta.listaDetalleVenta.Count - 1);

                mvVenta.calcularTotal();

                mvVenta.listAuxDetalleVenta.Refresh();
            }
        }

        /// <summary>
        /// Detecta si el valor del NumericUpDown ha cambiado para recalcular el total y controlar
        /// el stock de productos disponible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (sender is MahApps.Metro.Controls.NumericUpDown numericUpDown)
            {
                var detalleVenta = numericUpDown.DataContext as Detalleventa;

                if (detalleVenta != null)
                {
                    if (detalleVenta.Producto.Stock > 0 && (int)numericUpDown.Value <= detalleVenta.Producto.Stock)
                    {
                        if (detalleVenta.Producto.Stock < 5)
                        {
                            popStockAlerta.IsOpen = true;
                            await Task.Delay(TimeSpan.FromSeconds(3));
                            popStockAlerta.IsOpen = false;
                        }

                        detalleVenta.Cantidad = (int)numericUpDown.Value;

                        mvVenta.calcularTotal();

                    }
                    else
                    {
                        numericUpDown.IsEnabled = false;

                        detalleVenta.Producto.Stock = 0;
                    }

                }

            }

        }

        /// <summary>
        /// Botón que genera el pdf del ticket de la venta en Documentos\Tickets_Whimsy
        /// Guarda el titulo del pdf con la fecha de la venta y un UUID/GUID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTicket_Click(object sender, RoutedEventArgs e)
        {
            if (mvVenta.listaDetalleVenta.Count > 0)
            {
                string uuid = Guid.NewGuid().ToString();

                string fechaActual = mvVenta.FechaVenta.ToString("yyyyMMdd");

                string nombreArchivo = $"TicketVenta_{fechaActual}_{uuid}.pdf";

                string rutaCarpeta = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tickets_Whimsy");

                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                string rutaArchivo = System.IO.Path.Combine(rutaCarpeta, nombreArchivo);

                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create))
                {
                    Document doc = new Document(PageSize.PENGUIN_SMALL_PAPERBACK, 5, 5, 7, 7);
                    PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                    doc.Open();

                    Font titleFont = new Font(Font.FontFamily.HELVETICA, 26, Font.BOLD);
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("WHIMSY", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    doc.Add(Chunk.NEWLINE);

                    doc.AddTitle("Ticket de Venta");
                    doc.Add(new iTextSharp.text.Paragraph("Fecha: "));
                    doc.Add(new iTextSharp.text.Paragraph(mvVenta.FechaVenta.ToString("yyyy-MM-dd")));
                    doc.Add(Chunk.NEWLINE);

                    PdfPTable table = new PdfPTable(3);
                    table.WidthPercentage = 100;

                    PdfPCell cellProducto = new PdfPCell(new Phrase("Producto"));
                    cellProducto.BorderWidth = 0;
                    cellProducto.BorderWidthBottom = 0.75f;

                    PdfPCell cellPrecio = new PdfPCell(new Phrase("Precio"));
                    cellPrecio.BorderWidth = 0;
                    cellPrecio.BorderWidthBottom = 0.75f;

                    PdfPCell cellCantidad = new PdfPCell(new Phrase("Cantidad"));
                    cellCantidad.BorderWidth = 0;
                    cellCantidad.BorderWidthBottom = 0.75f;

                    table.AddCell(cellProducto);
                    table.AddCell(cellPrecio);
                    table.AddCell(cellCantidad);

                    foreach (var detalleVenta in mvVenta.listaDetalleVenta)
                    {
                        cellProducto = new PdfPCell(new Phrase(detalleVenta.Producto.Descripcion));
                        cellPrecio = new PdfPCell(new Phrase(detalleVenta.PrecioUnitario.ToString())); // Formatear con dos decimales
                        cellCantidad = new PdfPCell(new Phrase(detalleVenta.Cantidad.ToString()));

                        table.AddCell(cellProducto);
                        table.AddCell(cellPrecio);
                        table.AddCell(cellCantidad);
                    }

                    doc.Add(new iTextSharp.text.Paragraph("TOTAL: "));
                    doc.Add(new iTextSharp.text.Paragraph(mvVenta.Total.ToString("F2"))); // Formatear con dos decimales
                    doc.Add(Chunk.NEWLINE);

                    doc.Add(new iTextSharp.text.Paragraph("Resumen de venta: "));
                    doc.Add(Chunk.NEWLINE);

                    doc.Add(table);

                    PdfPTable empleadoTable = new PdfPTable(1);
                    empleadoTable.WidthPercentage = 100;
                    PdfPCell cellHola = new PdfPCell(new Phrase("Le ha atendido " + mvVenta.usuLogin.Persona.Nombre));
                    cellHola.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHola.Border = 0;
                    empleadoTable.AddCell(cellHola);

                    doc.Add(Chunk.NEWLINE);
                    doc.Add(empleadoTable);

                    PdfPTable thanksTable = new PdfPTable(1);
                    thanksTable.WidthPercentage = 100;
                    PdfPCell cellThanks = new PdfPCell(new Phrase("¡GRACIAS POR COMPRAR EN WHIMSY!"));
                    cellThanks.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellThanks.Border = 0;
                    thanksTable.AddCell(cellThanks);

                    doc.Add(Chunk.NEWLINE);
                    doc.Add(thanksTable);

                    doc.Close();
                    pw.Close();

                    doc.Close();
                    pw.Close();
                }

                MessageBox.Show("Ticket guardado en la carpeta Tickets_Whimsy en Documentos", "Ticket de venta", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ningun producto ha sido añadido a la venta", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
