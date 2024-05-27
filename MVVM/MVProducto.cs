using di.proyecto2023.MVVM;
using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Kiosco_Whimsy.MVVM
{
    public class MVProducto : MVBaseCRUD<Producto>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;

        /// <summary>
        /// Capa de servicio de Producto
        /// </summary>
        private ProductoServicio prodServ;

        /// <summary>
        /// new Producto a guardar en la base de datos
        /// </summary>
        private Producto _producto;

        // Listas y servicios de los combobox        
        private List<string> _listaUbicaciones = new List<string> { "Estante A", "Estante B", "Estante C", "Nevera", "Almacén" };
        private List<string> _listaImagenes;
        private TipoProductoServicio tipoProdServ;
        private OfertaServicio ofertaServ;

        //Filtros listaProductos
        private ListCollectionView listAux;
        public Tipoproducto categoriaSeleccionada;

        private List<Predicate<Producto>> criterios;
        //cada uno de los criterios:
        private Predicate<Producto> criterioCategoria;

        private List<Producto> _productosFiltrados;

        public MVProducto(KioscoContext kioscoContext)
        {
            this.kioscoContext = kioscoContext;
            inicializa();
        }

        private void inicializa()
        {
            servicio = new ProductoServicio(kioscoContext);
            prodServ = (ProductoServicio)servicio;

            _producto = new Producto();

            tipoProdServ = new TipoProductoServicio(kioscoContext);
            ofertaServ = new OfertaServicio(kioscoContext);

            criterios = new List<Predicate<Producto>>();
            inicializaCriterios();

            listAux = new ListCollectionView(prodServ.GetAll.ToList());
            _productosFiltrados = new List<Producto>(prodServ.GetAll.ToList());

            _listaImagenes = new List<string>();
            cargarListaImagenes();
        }

        /// <summary>
        /// Producto a guardar en la base de datos
        /// </summary>
        public Producto producto
        {
            get { return _producto; }                           
            set { _producto = value; NotifyPropertyChanged(nameof(producto)); }
        }

        //Listas para los combos
        public List<string> listaUbicaciones { get { return _listaUbicaciones; } }
        public List<string> listaImagenes
        {
            get { return _listaImagenes; }
            set
            {
                _listaImagenes = value;
                NotifyPropertyChanged(nameof(listaImagenes));
            }
        }
        public List<Oferta> listaOfertas { get { return ofertaServ.GetAll; } }
        public List<Tipoproducto> listaCategorias { get { return tipoProdServ.GetAll; } }
        public List<Producto> listaAllProductos
        {
            get { return prodServ.GetAll; }
            set
            {
                NotifyPropertyChanged(nameof(listaAllProductos));
            }
        }
        public List<Producto> listaProductosFiltrados
        {
            get { return _productosFiltrados; }
            set
            {
                _productosFiltrados = value;
                NotifyPropertyChanged(nameof(listaProductosFiltrados));
            }
        }
        public List<Producto> listaProductos
        {
            get { return categoriaSeleccionada != null ? listaProductosFiltrados : listaAllProductos; }
        }
        public ListCollectionView listaProductos2
        {
            get { return listAux; }
        }

        private void inicializaCriterios()
        {
            criterioCategoria = new Predicate<Producto>(p => p.Tipoproducto != null && p.Tipoproducto.Equals(categoriaSeleccionada));
        }

        private bool filtroCombinadoCriterios(object item)
        {
            bool correcto = true;
            Producto producto = (Producto)item;
            if (criterios.Count() != 0)
            {
                correcto = criterios.TrueForAll(x => x(producto));
            }
            return correcto;
        }

        private void addCriterios()
        {
            criterios.Clear();

            if (categoriaSeleccionada != null)
            {
                criterios.Add(criterioCategoria);
            }

        }

        public void filtrar()
        {
            addCriterios();
            listaProductosFiltrados = listaAllProductos.Where(p => filtroCombinadoCriterios(p)).ToList();
            NotifyPropertyChanged(nameof(listaProductosFiltrados));
        }

        public void cargarListaImagenes()
        {
            listaImagenes.Clear();

            foreach (var producto in listaAllProductos)
            {
                if (!string.IsNullOrEmpty(producto.Imagen))
                {
                    //Si se habian cargado las rutas relativas (el usuario ha entrado primero en Ventas), se quita el prefijo de la ruta
                    if (producto.Imagen.StartsWith(@"/Recursos/Imagenes/"))
                    {
                        producto.Imagen = producto.Imagen.Replace(@"/Recursos/Imagenes/", "");
                    }
                    
                    listaImagenes.Add(producto.Imagen);
                }
            }

            NotifyPropertyChanged(nameof(listaImagenes));

            foreach (var categoria in listaCategorias)
            {
                if (!string.IsNullOrEmpty(categoria.Imagen))
                {
                    //Si se habian cargado las rutas relativas (el usuario ha entrado primero en Ventas), se quita el prefijo de la ruta
                    if (categoria.Imagen.StartsWith(@"/Recursos/Imagenes/"))
                    {
                        categoria.Imagen = categoria.Imagen.Replace(@"/Recursos/Imagenes/", "");
                    }
                }
            }

            
        }

       
    }
}
