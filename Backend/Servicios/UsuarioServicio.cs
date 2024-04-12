using di.proyecto2023.Backend.Servicios;
using Kiosco_Whimsy.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosco_Whimsy.Backend.Servicios
{

    /// <summary>
    /// Clase que contiene la lógica de negocio de la tabla Usuario
    /// </summary>
    public class UsuarioServicio : ServicioGenerico<Usuario>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;

        /// <summary>
        /// Variable que guarda el usuario que ha iniciado sesión
        /// </summary>
        public Usuario usuLogin { get; set; }

        /// <summary>
        /// Constructor que pasa el contexto de la base de datos
        /// </summary>
        /// <param name="kioscoContext"></param>
        public UsuarioServicio(KioscoContext kioscoContext) : base(kioscoContext)
        {
            this.kioscoContext = kioscoContext;
        }

        /// <summary>
        /// Método que comprueba las credenciales del usuario en la base de datos, ademas instancia las
        /// claves ajenas de Rol y Persona
        /// </summary>
        /// <param name="user">Username</param>
        /// <param name="pass">Password</param>
        /// <returns>
        /// True si el nombre de usuario y la contraseña estan en la base de datos; 
        /// de lo contrario, False
        /// </returns>
        public Boolean login(String user, String pass)
        {
            Boolean correcto = false; 

            try
            {
                this.usuLogin = kioscoContext.Set<Usuario>().FirstOrDefault(u => u.Username == user && u.Password == pass);
                if (usuLogin != null)
                {  
                    correcto = true; 
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }

            return correcto;
        }


        /// <summary>
        /// Comprueba si en la base de datos existe un usuario con ese login
        /// El login de un usuario debe de ser único
        /// </summary>
        /// <param name="usu">El nombre de usuario que se desea verificar</param>
        /// <returns>
        /// True si el nombre de usuario es único; 
        /// de lo contrario, False
        /// </returns>
        public Boolean usuarioUnico(string usu)
        {
            bool unico = true;
            if (kioscoContext.Set<Usuario>().Where(u => u.Username == usu).Count() > 0)
            {
                unico = false;
            }
            return unico;
        }

    }
}
