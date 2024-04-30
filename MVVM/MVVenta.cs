using di.proyecto2023.MVVM;
using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosco_Whimsy.MVVM
{
    /// <summary>
    /// ViewModel para la gestión de ventas en la interfaz
    /// </summary>
    public class MVVenta : MVBaseCRUD<Venta>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;


        /// <summary>
        /// Capa de Servicio de Venta y de Producto
        /// </summary>
        private VentaServicio ventaServ;
        private ProductoServicio prodServ;
        private TipoProductoServicio tipoProdServ;


        /// <summary>
        /// Variable que recoge el Usuario que ha iniciado sesion (empleado vendedor de la venta)
        /// </summary>
        private Usuario _usuLogin;

        /// <summary>
        /// Contador para que las imagenes solo carguen la ruta relativa una vez
        /// Accesible desde cualquier clase, para que se pueda controlar el numero de veces
        /// Cada vez que se instancia esta clase 
        /// </summary>
        public static bool yaHanSidoCargadas = false;

        /// <summary>
        /// Constructor que pasa el contexto de la base de datos y el usuario que ha iniciado 
        /// sesión e instancia los servicios, carga las rutas relativas de las imágenes (solo una vez)
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVVenta(KioscoContext kioscoContext, Usuario usuLogin)
        {
            this.kioscoContext = kioscoContext;

            prodServ = new ProductoServicio(kioscoContext);
            tipoProdServ = new TipoProductoServicio(kioscoContext);

            _usuLogin = usuLogin;

            servicio = new VentaServicio(kioscoContext);
            ventaServ = (VentaServicio)servicio;

            if (!yaHanSidoCargadas)
            {
                cargarRutaRelativaDeImagenes();
            }
            

        }

        /// <summary>
        /// Constructor que pasa el contexto de la base de datos e instancia los servicios
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVVenta(KioscoContext kioscoContext)
        {
            this.kioscoContext = kioscoContext;

            prodServ = new ProductoServicio(kioscoContext);
            tipoProdServ = new TipoProductoServicio(kioscoContext);

            servicio = new VentaServicio(kioscoContext);
            ventaServ = (VentaServicio)servicio;
        }


        /// <summary>
        /// Carga las rutas relativas donde se ubican las imagenes de los productos y categorías
        /// </summary>
        private void cargarRutaRelativaDeImagenes()
        {
            if (listaProductos != null)
            {
                foreach (var producto in listaProductos)
                {
                    if (producto.Imagen != null)
                    {
                        producto.Imagen = @"/Recursos/Imagenes/" + producto.Imagen;
                    }
                }
            }

            if (listaCategorias != null)
            {
                foreach (var categoria in listaCategorias)
                {
                    if (categoria.Imagen != null)
                    {
                        categoria.Imagen = @"/Recursos/Imagenes/" + categoria.Imagen;
                    }
                }
            }
        }

        /// <summary>
        /// Variable de usuLogin pública para ser recogida por el Binding en la interfaz
        /// </summary>
        public Usuario usuLogin
        {
            get { return _usuLogin; }
            set { _usuLogin = value; NotifyPropertyChanged(nameof(usuLogin)); }
        }

        /// <summary>
        /// Listas públicas de todas las ventas, los productos y las categorías
        /// </summary>
        public List<Venta> listaVentas { get { return ventaServ.GetAll; } }
        public List<Producto> listaProductos { get { return prodServ.GetAll; } }
        public List<Tipoproducto> listaCategorias { get { return tipoProdServ.GetAll; } }
    }
}
