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
    /// <summary>
    /// ViewModel para la gestión de productos en la interfaz
    /// </summary>
    public class MVProducto : MVBaseCRUD<Producto>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;

        /// <summary>
        /// Servicio de producto
        /// </summary>
        private ProductoServicio prodServ;

        /// <summary>
        /// Producto a guardar en la base de datos
        /// </summary>
        private Producto _producto;

        // Listas y servicios de los combobox
        /// <summary>
        /// Lista de ubicaciones
        /// </summary>
        private List<string> _listaUbicaciones = new List<string> { "Estante A", "Estante B", "Estante C", "Nevera", "Almacén" };
        /// <summary>
        /// Lista de imágenes
        /// </summary>
        private List<string> _listaImagenes;

        /// <summary>
        /// Servicio de categoría
        /// </summary>
        private TipoProductoServicio tipoProdServ;
        /// <summary>
        /// Servicio de Oferta
        /// </summary>
        private OfertaServicio ofertaServ;

        private ListCollectionView listAux;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVProducto(KioscoContext kioscoContext)
        {
            this.kioscoContext = kioscoContext;
            inicializa();
        }

        /// <summary>
        /// Instancia los servicios y variables necesarias
        /// </summary>
        private void inicializa()
        {
            servicio = new ProductoServicio(kioscoContext);
            prodServ = (ProductoServicio)servicio;

            _producto = new Producto();

            tipoProdServ = new TipoProductoServicio(kioscoContext);
            ofertaServ = new OfertaServicio(kioscoContext);

            listAux = new ListCollectionView(prodServ.GetAll.ToList());

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

        /// <summary>
        /// Lista de ubicaciones
        /// </summary>
        public List<string> listaUbicaciones { get { return _listaUbicaciones; } }
        /// <summary>
        /// Lista de imágenes
        /// </summary>
        public List<string> listaImagenes
        {
            get { return _listaImagenes; }
            set
            {
                _listaImagenes = value;
                NotifyPropertyChanged(nameof(listaImagenes));
            }
        }
        /// <summary>
        /// Lista de ofertas
        /// </summary>
        public List<Oferta> listaOfertas { get { return ofertaServ.GetAll; } }
        /// <summary>
        /// Lista de categorías
        /// </summary>
        public List<Tipoproducto> listaCategorias { get { return tipoProdServ.GetAll; } }
        /// <summary>
        /// Lista de todos los productos (aux)
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
        /// Lista de productos auxiliar
        /// </summary>
        public ListCollectionView listaProductos2
        {
            get { return listAux; }
        }

        /// <summary>
        /// Carga la lista de imágenes 
        /// Si contiene ruta relativa, se la quita para hacer la inserción del producto
        /// en la base de datos
        /// </summary>
        public void cargarListaImagenes()
        {
            listaImagenes.Clear();

            foreach (var producto in listaAllProductos)
            {
                if (!string.IsNullOrEmpty(producto.Imagen))
                {
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
                    if (categoria.Imagen.StartsWith(@"/Recursos/Imagenes/"))
                    {
                        categoria.Imagen = categoria.Imagen.Replace(@"/Recursos/Imagenes/", "");
                    }
                }
            }

        }

    }

}
