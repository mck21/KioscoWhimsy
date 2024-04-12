using di.proyecto2023.Backend.Servicios;
using di.proyecto2023.MVVM;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto2023.MVVM
{
    public class MVBaseCRUD<T> : MVBase
        where T : class
    {
        public ServicioGenerico<T> servicio { get; set; }
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Realiza una inserción en la base de datos y captura la excepción
        /// </summary>
        /// <param name="entity">Objeto a guardar</param>
        /// <returns></returns>
        public bool add(T entity)
        {
            bool correcto = true;
            try
            {
                servicio.Add(entity);
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                // Guardamos en el Log el error
                log.Error("\n" + "Insertando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" + dbex.StackTrace);
                System.Console.WriteLine("\n" + "Insertando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n"+
                   dbex.InnerException + dbex.StackTrace);
            }
            return correcto;
        }

        /// <summary>
        /// Realiza una actualización de una tupla de la base de datos
        /// </summary>
        /// <param name="entity">Objeto que se actualiza</param>
        /// <returns></returns>
        public bool update(T entity)
        {
            bool correcto = true;
            try
            {
                servicio.AddOrUpdate(entity);
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                // Guardamos en el Log el error
                log.Error("\n" + "Actualizando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" + dbex.StackTrace);
                System.Console.WriteLine("\n" + "Actualizando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" +
                   dbex.InnerException + dbex.StackTrace);
            }
            return correcto;
        }
        /// <summary>
        /// Borra una fila de la tabla correspondiente
        /// </summary>
        /// <param name="entity">Objeto que se borra</param>
        /// <returns></returns>
        public bool delete(T entity)
        {
            bool correcto = true;
            try
            {
                servicio.Delete(entity);
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                // Guardamos en el Log el error
                log.Error("\n" + "Borrando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" + dbex.StackTrace);
                System.Console.WriteLine("\n" + "Borrando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n"+
                   dbex.InnerException + dbex.StackTrace);
            }
            return correcto;
        }
    }
}
