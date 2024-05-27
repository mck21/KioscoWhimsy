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
    /// Servicio de Usuario que hereda los métodos para interactuar con 
    /// la tabla Usuario en la base de datos
    /// </summary>
    public class UsuarioServicio : ServicioGenerico<Usuario>
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private KioscoContext kioscoContext;

        /// <summary>
        /// Usuario que ha iniciado sesion
        /// </summary>
        public Usuario usuLogin { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kioscoContext">Contexto de la base de datos</param>
        public UsuarioServicio(KioscoContext kioscoContext) : base(kioscoContext)
        {
            this.kioscoContext = kioscoContext;
        }

        /// <summary>
        /// Método que comprueba las credenciales del usuario en la base de datos
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
    }
}
