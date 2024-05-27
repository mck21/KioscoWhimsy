using Kiosco_Whimsy.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace Kiosco_Whimsy.Frontend.ControlUsuario
{
    /// <summary>
    /// Clase Button para cada una de los productos de la ventana de Ventas
    /// </summary>
    public class BtnProductos : Button
    {
        public Producto producto { get; set; }
        public string imagen { get; set; }
        public string nombre { get; set; }

        private Image _imagen;
        private TextBlock _nombre;
        private StackPanel _stackPanel;

        /// <summary>
        /// Constructor
        /// </summary>
        public BtnProductos()
        {
        }

        /// <summary>
        /// Método que instancia los objetos necesarios
        /// </summary>
        public void inicializa()
        {
            _imagen = new Image
            {
                Source = new BitmapImage(new Uri(imagen, UriKind.RelativeOrAbsolute)),
                Width = 60,
                Height = 60,
                Margin = new System.Windows.Thickness(7)
        };
            _nombre = new TextBlock();
            _nombre.Text = nombre;
            _nombre.Foreground = Brushes.White;
            _nombre.HorizontalAlignment = HorizontalAlignment.Center;
            _nombre.FontWeight = FontWeights.Bold;
            _nombre.FontSize = 11;
            _nombre.Margin = new System.Windows.Thickness(0, 5, 0, 0);


            _stackPanel = new StackPanel();

            Color colorFondo = (Color)ColorConverter.ConvertFromString("#212121");
            SolidColorBrush colorFondoBrush = new SolidColorBrush(colorFondo);

            this.Background = colorFondoBrush;
            this.Margin = new System.Windows.Thickness(7);
            this.BorderBrush = Brushes.Transparent;
            this.Width = 165;
            this.Height = 165;

            this.Style = (Style)FindResource("MaterialDesignOutlinedButton");

            _stackPanel.Children.Add(_imagen);
            _stackPanel.Children.Add(_nombre);
            _stackPanel.Margin = new System.Windows.Thickness(6);
            _stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.Content = _stackPanel;
        }


    }
}
