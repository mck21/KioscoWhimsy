using di.proyecto2023.Backend.Servicios;
using Kiosco_Whimsy.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosco_Whimsy.Backend.Servicios
{
    /// <summary>
    /// Servicio de Tipoproducto que hereda los métodos para interactuar con 
    /// la tabla Tipoproducto en la base de datos
    /// </summary>
    public class TipoProductoServicio : ServicioGenerico<Tipoproducto>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext">Contexto de la base de datos</param>
        public TipoProductoServicio(KioscoContext kioscoContext) : base(kioscoContext)
        {
            this.kioscoContext = kioscoContext;
        }

    }
}
