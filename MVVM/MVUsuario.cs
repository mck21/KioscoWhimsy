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
        /// Capa de Servicio de Usuario y contexto de la base de datos
        /// </summary>
        private UsuarioServicio usuServ;
        private KioscoContext kioscoContext;

        /// <summary>
        /// Variable que recoge el Usuario que ha iniciado sesion del Servicio de Usuario
        /// </summary>
        private Usuario _usuLogin;

        //private ListCollectionView _listaUsuarios;

        public MVUsuario(KioscoContext kioscoContext)
        {
            this.kioscoContext = kioscoContext;

            servicio = new UsuarioServicio(kioscoContext);
            usuServ = (UsuarioServicio)servicio;

            //_listaUsuarios = new ListCollectionView(usuServ.GetAll); //*
        }

        /// <summary>
        /// Constructor que pasa el contexto de la base de datos
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
        /// Variable de usuLogin pública para ser recogida por el Binding en la interfaz
        /// </summary>
        public Usuario usuLogin
        {
            get { return _usuLogin; }
            set { _usuLogin = value; NotifyPropertyChanged(nameof(usuLogin)); }
        }

        public List<Usuario> listaUsuarios { get { return usuServ.GetAll; } }

    }
}
