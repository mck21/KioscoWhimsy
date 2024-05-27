using Kiosco_Whimsy.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kiosco_Whimsy.Frontend.ControlUsuario
{
    /// <summary>
    /// Clase Button para cada una de las categorias de la ventana de Ventas
    /// </summary>
    public class BtnCategorias : Button
    {
        public Tipoproducto tipo { get; set; }
        public string imagen { get; set; }
        public string nombre { get; set; }


        private Image _imagen;
        private TextBlock _nombre;
        private StackPanel _stackPanel;

        /// <summary>
        /// Constructor
        /// </summary>
        public BtnCategorias() {  }

        /// <summary>
        /// Método que instancia los objetos necesarios
        /// </summary>
        public void inicializa()
        {
            _imagen = new Image
            {
                Source = new BitmapImage(new Uri(imagen, UriKind.RelativeOrAbsolute)),
                Width = 36,
                Height = 36
            };
            _nombre = new TextBlock();
            _nombre.Text = nombre;
            _nombre.Foreground = Brushes.White;
            _nombre.FontSize = 11;
            _stackPanel = new StackPanel();

            this.Background = Brushes.Transparent;
            this.BorderBrush = Brushes.Transparent;
            this.Style = (Style)FindResource("MaterialDesignIconForegroundButton");
            this.Width = 95;
            this.Height = 95;

            _stackPanel.Children.Add(_imagen);
            _stackPanel.Children.Add(_nombre);
            //_stackPanel.Background = Brushes.DarkGray;
            _stackPanel.Margin = new System.Windows.Thickness(7);
            this.Content = _stackPanel;
        }


    }
}
