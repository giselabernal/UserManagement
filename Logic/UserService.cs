using Logic.Abstractions;
using Logic.Exceptions;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

    public class UserService 
    {
        //esta clase  de la lista de miembros
        //1. EnvioCorreos
        //2. Lista de Miembros

        IRepository<User> _repository;
        ISender _sender;
        ILogger _logger;
       
        public UserService(IRepository<User> repository, ISender sender, ILogger logger)
        {
            _repository = repository;
            _sender = sender;
            _logger = logger;
        }

        public void NotifyUser()
         {
            
            try
            {
                List<User> AllUsers = _repository.GetAll();
                foreach (var user in AllUsers)
                {
                    try
                    {
                        _sender.Send(user.Email, $"Message to {user.Name} with id: {user.Id}");
                         if (user.Id == 1)
                            throw new AppException("Id exception"); //excepciones custom como crearlas o trabajarlas
                        _logger.WriteLog($"Log: Message sent to {user.Name} with id: {user.Id}");
                    }
                    
                    catch (AppException ex)
                    {
                        _logger.WriteLog(ex.ToString());
                        Console.WriteLine("Error{0} due to Id exception 1");
                        //en WriteLog si marcara error y le genero el AppException en Write log line 35, se viene para aca y me marca su innerexcepcion de ese metodo
                        //pero si no le tiro AppException se va a ir hasta Program
                        if (ex.InnerException != null)
                            Console.WriteLine("Inner exception reading file: {0}", ex.InnerException);
                    }
                
                }
                
            }      
            
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.Message.Contains("Directory not found"))
                {
                    Console.WriteLine(ex.Message + " " + ex.InnerException);
                }
                else
                {
                _logger.WriteLog(ex.Message);
                }
               
               
            }
            finally 
            {
                //aqui prenderia un flag si hubo errores arriba que diga aqui si termino con errores o sin errores, quitando el hardcoded del id 1
                _logger.WriteLog("Loop terminated"); //aqui ya vi que esta mal este mensaje porque si hay error y termina el loop, no siempre sera successfully
            }

        }

        public User GetUserById(int id)
        {
            var us = new User();
            us = _repository.GetUserByID(id);
            //ay que ver sin el try catch que hace
            if (us == null)
                throw new UserNotFoundException($"User id {id} not found on Database"); 
            else
                return us;
        }
      
    }
}
