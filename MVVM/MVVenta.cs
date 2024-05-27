using di.proyecto2023.MVVM;
using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

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
        /// Capas de Servicio para Venta
        /// </summary>
        private VentaServicio ventaServ;
        private ProductoServicio prodServ;
        private TipoProductoServicio tipoProdServ;
        private UsuarioServicio usuServ;
        private ClienteServicio clienteServ;


        /// <summary>
        /// Venta a insertar en la base de datos
        /// </summary>
        private Venta _venta;
        private DateTime _fechaVenta;

        /// <summary>
        /// Productos seleccionados para una venta
        /// </summary>
        private List<Detalleventa>? _listaDetalleVenta;
        private ListCollectionView _listAuxDetalleVenta;

        /// <summary>
        /// Precio total de la venta
        /// </summary>
        private double _total;

        /// <summary>
        /// Variable que recoge el Usuario que ha iniciado sesion (empleado vendedor de la venta)
        /// </summary>
        private Usuario _usuLogin;

        

        public Tipoproducto categoriaSeleccionada;

        //Filtros listaProductos
        private List<Producto> _listaProductos;
        private ListCollectionView listAux;
        private Usuario _empleadoSeleccionado;
        private DateTime? _fechaSeleccionada;

        private List<Predicate<Venta>> criterios;
        //cada uno de los criterios:
        private Predicate<Venta> criterioEmpleado;
        private Predicate<Venta> criterioFecha;


        /// <summary>
        /// Constructor que pasa el contexto de la base de datos y el usuario que ha iniciado 
        /// sesión e instancia los servicios, carga las rutas relativas de las imágenes (solo una vez)
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVVenta(KioscoContext kioscoContext, Usuario usuLogin)
        {
            this.kioscoContext = kioscoContext;
            this._usuLogin = usuLogin;

            inicializa();                   
        }

        /// <summary>
        /// Instancia los servicios y variables necesarias
        /// </summary>
        private void inicializa()
        {
            prodServ = new ProductoServicio(kioscoContext);
            tipoProdServ = new TipoProductoServicio(kioscoContext);
            usuServ = new UsuarioServicio(kioscoContext);
            clienteServ = new ClienteServicio(kioscoContext);

            servicio = new VentaServicio(kioscoContext);
            ventaServ = (VentaServicio)servicio;

            _venta = new Venta();

            listAux = new ListCollectionView(ventaServ.GetAll);

            _listaDetalleVenta = new List<Detalleventa>();
            _listAuxDetalleVenta = new ListCollectionView(listaDetalleVenta);

            _total = 0.00;

            criterios = new List<Predicate<Venta>>();
            inicializaCriterios();

            _listaProductos = new List<Producto>(prodServ.GetAll.ToList());
            FechaVenta = DateTime.Today;

        }

        /// <summary>
        /// Venta a insertar en la base de datos
        /// </summary>
        public Venta venta
        {
            get { return _venta; }
            set { _venta = value; NotifyPropertyChanged(nameof(venta)); }
        }

        /// <summary>
        /// Constructor que pasa el contexto de la base de datos e instancia los servicios
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVVenta(KioscoContext kioscoContext)
        {
            this.kioscoContext = kioscoContext;

            inicializa2();
        }

        /// <summary>
        /// Instancia los servicios y variables necesarias
        /// </summary>
        private void inicializa2()
        {
            prodServ = new ProductoServicio(kioscoContext);
            tipoProdServ = new TipoProductoServicio(kioscoContext);

            servicio = new VentaServicio(kioscoContext);
            ventaServ = (VentaServicio)servicio;
        }

        /// <summary>
        /// Variable de usuLogin pública para ser recogida por el Binding en la interfaz
        /// </summary>
        public Usuario usuLogin
        {
            get { return _usuLogin; }
            set { _usuLogin = value; NotifyPropertyChanged(nameof(usuLogin)); }
        }        

        public List<Detalleventa> listaDetalleVenta
        {
            get { return _listaDetalleVenta; }
            set
            {
                _listaDetalleVenta = value;
                NotifyPropertyChanged(nameof(listaDetalleVenta));
            }
        }

        public ListCollectionView listAuxDetalleVenta
        {
            get { return _listAuxDetalleVenta; }
            set
            {
                _listAuxDetalleVenta = value;
                NotifyPropertyChanged(nameof(listAuxDetalleVenta));
            }
        }

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyPropertyChanged(nameof(Total));
            }
        }


        public DateTime FechaVenta
        {
            get { return _fechaVenta; }
            set
            {
                _fechaVenta = value;
                NotifyPropertyChanged(nameof(FechaVenta));
            }
        }

        /// <summary>
        /// Listas públicas de todas las ventas, los usuarios y los productos
        /// </summary>
        public ListCollectionView listaVentas { get { return listAux; } }
        public List<Tipoproducto> listaCategorias { get { return tipoProdServ.GetAll; } }
        public List<Usuario> listaUsuarios { get { return usuServ.GetAll; } }
        public List<Cliente> listaClientes { get { return clienteServ.GetAll; } }
        public List<Producto> listaAllProductos
        {
            get { return prodServ.GetAll; }
            set
            {
                NotifyPropertyChanged(nameof(listaAllProductos));
            }
        }
        public List<Producto> listaProductos
        {
            get { return _listaProductos; }
            set
            {
                _listaProductos = value;
                NotifyPropertyChanged(nameof(listaProductos));
            }
        }


        /// <summary>
        /// Carga las rutas relativas donde se ubican las imagenes de los productos y categorías
        /// </summary>
        public void cargarRutaRelativaDeImagenes()
        {
            if (listaAllProductos != null)
            {
                foreach (var producto in listaAllProductos)
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
        /// Método para calcular el total sumando los precios de los productos en la listaProductosSeleccionados
        /// </summary>
        public void calcularTotal()
        {
            double total = 0;

            if (_listaDetalleVenta != null && _listaDetalleVenta.Any())
            {
                foreach (var detalleventa in _listaDetalleVenta)
                {
                    int cantidad = detalleventa.Cantidad ?? 1;
                    total += detalleventa.Producto.Precio * cantidad;
                }
            }

            Total = total;
        }

        /// <summary>
        /// Logica de los criterios que sigue el filtro de categorias
        /// </summary>
        private void inicializaCriterios()
        {
            criterioEmpleado = new Predicate<Venta>(v => v.Usuario != null && v.Usuario.Equals(empleadoSeleccionado));
            criterioFecha = new Predicate<Venta>(v => v.Fecha != null && v.Fecha.Equals(fechaSeleccionada));
        }

        public Usuario empleadoSeleccionado
        {
            get { return _empleadoSeleccionado; }                             
            set { _empleadoSeleccionado = value; NotifyPropertyChanged(nameof(empleadoSeleccionado)); }
        }

        public DateTime? fechaSeleccionada
        {
            get { return _fechaSeleccionada; }
            set { _fechaSeleccionada = value; NotifyPropertyChanged(nameof(fechaSeleccionada)); }
        }

        private bool filtroCombinadoCriterios(object item)
        {
            bool correcto = true;
            Venta venta = (Venta)item;
            if (criterios.Count() != 0)
            {
                correcto = criterios.TrueForAll(x => x(venta));
            }
            return correcto;
        }

        private void addCriterios()
        {
            criterios.Clear();

            if (empleadoSeleccionado != null)
            {
                criterios.Add(criterioEmpleado);
            }

            if (fechaSeleccionada != null)
            {
                criterios.Add(criterioFecha);
            }

        }

        public void filtrar()
        {
            listaVentas.Filter = null;
            addCriterios();
            listaVentas.Filter = new Predicate<object>(filtroCombinadoCriterios);
        }

        public void limpiar()
        {
            listaProductos = listaAllProductos;
        }
    }
}
