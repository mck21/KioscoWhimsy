using di.proyecto2023.Backend.Servicios;
using di.proyecto2023.MVVM;
using Kiosco_Whimsy.Backend.Modelos;
using Kiosco_Whimsy.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Kiosco_Whimsy.MVVM
{
    /// <summary>
    /// ViewModel para la gestión de usuarios en la interfaz
    /// </summary>
    public class MVUsuario : MVBaseCRUD<Usuario>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;
        /// <summary>
        /// Capa de Servicio de Usuario
        /// </summary>
        private UsuarioServicio usuServ;

        /// <summary>
        /// Usuario que ha iniciado sesion
        /// </summary>
        private Usuario _usuLogin;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVUsuario(KioscoContext kioscoContext)
        {
            this.kioscoContext = kioscoContext;

            servicio = new UsuarioServicio(kioscoContext);
            usuServ = (UsuarioServicio)servicio;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext"></param>
        public MVUsuario(KioscoContext kioscoContext, Usuario usuLogin) 
        { 
            this.kioscoContext = kioscoContext;

            _usuLogin = usuLogin;

            servicio = new UsuarioServicio(kioscoContext);
            usuServ = (UsuarioServicio)servicio;
            
        }        

        /// <summary>
        /// Usuario que ha iniciado sesión
        /// </summary>
        public Usuario usuLogin
        {
            get { return _usuLogin; }
            set { _usuLogin = value; NotifyPropertyChanged(nameof(usuLogin)); }
        }

        /// <summary>
        /// Lista de usuarios recibida en la interfaz
        /// </summary>
        public List<Usuario> listaUsuarios { get { return usuServ.GetAll; } }

    }
}
