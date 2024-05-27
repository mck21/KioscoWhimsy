using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.MVVM;
using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
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

namespace Kiosco_Whimsy.Frontend.Charts
{
    /// <summary>
    /// Lógica de interacción para ChartVentasPorMes.xaml
    /// </summary>
    public partial class ChartVentasPorMes : MetroWindow
    {
        /// <summary>
        /// Colección de series de datos representados en los charts
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; }

        /// <summary>
        /// Lista de nombres de los meses del año
        /// </summary>
        public List<string> Meses { get; set; }

        /// <summary>
        /// ViewModel de Venta
        /// </summary>
        private MVVenta mvVenta;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mvVenta">ViewModel de Venta</param>
        public ChartVentasPorMes(MVVenta mvVenta)
        {
            InitializeComponent();
            DataContext = this;

            this.mvVenta = mvVenta;

            List<VentaPorMes> ventasPorMes = ObtenerVentasPorMes();

            SeriesCollection = new SeriesCollection();
            ColumnSeries series = new ColumnSeries
            {
                Title = "Ventas",
                Values = new ChartValues<double>(ventasPorMes.Select(v => v.TotalVentas)),
            };
            SeriesCollection.Add(series);

            Meses = ventasPorMes.Select(v => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(v.Mes)).ToList();


        }

        /// <summary>
        /// Método para obtener el total de ventas cada mes en una lista
        /// </summary>
        /// <returns></returns>
        private List<VentaPorMes> ObtenerVentasPorMes()
        {
            Dictionary<int, double> ventasPorMes = new Dictionary<int, double>();

            foreach (Venta venta in mvVenta.listaVentas)
            {
                int mes = venta.Fecha.Month;
                double totalVenta = venta.Total;

                if (ventasPorMes.ContainsKey(mes))
                {
                    ventasPorMes[mes] += totalVenta;
                }
                else
                {
                    ventasPorMes.Add(mes, totalVenta);
                }
            }

            List<VentaPorMes> ventasPorMesList = new List<VentaPorMes>();
            foreach (var kvp in ventasPorMes)
            {
                ventasPorMesList.Add(new VentaPorMes { Mes = kvp.Key, TotalVentas = kvp.Value });
            }

            return ventasPorMesList;
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
    }
}

/// <summary>
/// Clase que representa el total de ventas por cada mes
/// </summary>
public class VentaPorMes
{
    public int Mes { get; set; }
    public double TotalVentas { get; set; }
}