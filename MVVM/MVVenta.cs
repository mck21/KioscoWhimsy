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

        // Capas de Servicio para Venta
        /// <summary>
        /// Servicio de venta
        /// </summary>
        private VentaServicio ventaServ;
        /// <summary>
        /// Servicio de producto
        /// </summary>
        private ProductoServicio prodServ;
        /// <summary>
        /// Servicio de categoría
        /// </summary>
        private TipoProductoServicio tipoProdServ;
        /// <summary>
        /// Servicio de usuario
        /// </summary>
        private UsuarioServicio usuServ;
        /// <summary>
        /// Servicio de cliente
        /// </summary>
        private ClienteServicio clienteServ;

        /// <summary>
        /// Venta a insertar en la base de datos
        /// </summary>
        private Venta _venta;

        /// <summary>
        /// Lista de productos seleccionados para una venta
        /// </summary>
        private List<Detalleventa>? _listaDetalleVenta;
        private ListCollectionView _listAuxDetalleVenta;

        /// <summary>
        /// Precio total de la venta
        /// </summary>
        private double _total;

        /// <summary>
        /// Fecha de la venta
        /// </summary>
        private DateTime _fechaVenta;

        /// <summary>
        /// Usuario que ha iniciado sesion (empleado vendedor de la venta)
        /// </summary>
        private Usuario _usuLogin;        

        /// <summary>
        /// Categoria seleccionada
        /// </summary>
        public Tipoproducto categoriaSeleccionada;

        /// <summary>
        /// Lista de Productos
        /// </summary>
        private List<Producto> _listaProductos;

        //Filtros listaProductos
        private ListCollectionView listAux;
        private Usuario _empleadoSeleccionado;
        private DateTime? _fechaSeleccionada;

        /// <summary>
        /// Lista de criterios
        /// </summary>
        private List<Predicate<Venta>> criterios;
        //cada uno de los criterios:
        private Predicate<Venta> criterioEmpleado;
        private Predicate<Venta> criterioFecha;


        /// <summary>
        /// Constructor
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
        /// Constructor
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
        /// Usuario que ha iniciado sesion
        /// </summary>
        public Usuario usuLogin
        {
            get { return _usuLogin; }
            set { _usuLogin = value; NotifyPropertyChanged(nameof(usuLogin)); }
        }        

        /// <summary>
        /// Lista de detalle de venta
        /// </summary>
        public List<Detalleventa> listaDetalleVenta
        {
            get { return _listaDetalleVenta; }
            set
            {
                _listaDetalleVenta = value;
                NotifyPropertyChanged(nameof(listaDetalleVenta));
            }
        }

        /// <summary>
        /// Lista de detalle de venta
        /// </summary>

        public ListCollectionView listAuxDetalleVenta
        {
            get { return _listAuxDetalleVenta; }
            set
            {
                _listAuxDetalleVenta = value;
                NotifyPropertyChanged(nameof(listAuxDetalleVenta));
            }
        }

        /// <summary>
        /// Total de la venta
        /// </summary>
        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyPropertyChanged(nameof(Total));
            }
        }

        /// <summary>
        /// Fecha de la venta
        /// </summary>
        public DateTime FechaVenta
        {
            get { return _fechaVenta; }
            set
            {
                _fechaVenta = value;
                NotifyPropertyChanged(nameof(FechaVenta));
            }
        }

        // Listas recogidas en la interfaz
        /// <summary>
        /// Lista de ventas
        /// </summary>
        public ListCollectionView listaVentas { get { return listAux; } }
        /// <summary>
        /// Lista de categorias
        /// </summary>
        public List<Tipoproducto> listaCategorias { get { return tipoProdServ.GetAll; } }
        /// <summary>
        /// Lista de usuarios
        /// </summary>
        public List<Usuario> listaUsuarios { get { return usuServ.GetAll; } }
        /// <summary>
        /// Lista de clientes
        /// </summary>
        public List<Cliente> listaClientes { get { return clienteServ.GetAll; } }
        /// <summary>
        /// Lista de todos los productos (auxiliar)
        /// </summary>
        public List<Producto> listaAllProductos
        {
            get { return prodServ.GetAll; }
            set
            {
                NotifyPropertyChanged(nameof(listaAllProductos));
            }
        }
        /// <summary>
        /// Lista de productos
        /// </summary>
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
        /// Calcula el total sumando los precios de los productos en listaProductosSeleccionados
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
        /// Criterios que siguen los filtros de registro de ventas
        /// </summary>
        private void inicializaCriterios()
        {
            criterioEmpleado = new Predicate<Venta>(v => v.Usuario != null && v.Usuario.Equals(empleadoSeleccionado));
            criterioFecha = new Predicate<Venta>(v => v.Fecha != null && v.Fecha.Equals(fechaSeleccionada));
        }

        /// <summary>
        /// Empleado seleccionado en el comboBox
        /// </summary>
        public Usuario empleadoSeleccionado
        {
            get { return _empleadoSeleccionado; }                             
            set { _empleadoSeleccionado = value; NotifyPropertyChanged(nameof(empleadoSeleccionado)); }
        }

        /// <summary>
        /// Fecha seleccionada en el datePicker
        /// </summary>
        public DateTime? fechaSeleccionada
        {
            get { return _fechaSeleccionada; }
            set { _fechaSeleccionada = value; NotifyPropertyChanged(nameof(fechaSeleccionada)); }
        }

        /// <summary>
        /// Combina todos los criterios seleccionados
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Añade los criterios a la lista de criterios
        /// </summary>
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

        /// <summary>
        /// Filtra la lista de ventas según los criterios seleccionados
        /// </summary>
        public void filtrar()
        {
            listaVentas.Filter = null;
            addCriterios();
            listaVentas.Filter = new Predicate<object>(filtroCombinadoCriterios);
        }

        /// <summary>
        /// Quita los filtros y vuelve a mostrar todas las ventas
        /// </summary>
        public void limpiar()
        {
            listaProductos = listaAllProductos;
        }
    }
}
