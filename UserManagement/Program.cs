using Logic.Abstractions;
using Logic;
using Logic.Exceptions;
using Logic.Models;

namespace UserManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string selread;
            int opc = 0;
            Console.WriteLine(" Select what options you owant to execute ");
            Console.WriteLine(" 1. Seek one user ");
            Console.WriteLine(" 2. Send messages to employees");

            selread = Console.ReadLine();
            try
            {
                opc = Convert.ToInt32(selread);
            }
            catch (FormatException)
            {
                throw new AppException("Invalid character, only numbers are allowed");
            }
            catch (InvalidCastException e)
            {

                Console.WriteLine(e.Message);
            }

            var repository = new UserRepository();
            var sender = new EmailService();
            var logger = new WriteLog();
            var lusers = new UserService(repository, sender, logger);
            try
            {
                switch (opc)
                {
                    case 1:
                        int usid = 0;
                        Console.WriteLine("Introduce the id number to search");
                        selread = Console.ReadLine();
                        try
                        {
                            usid = Convert.ToInt32(selread);
                            lusers.GetUserById(usid);
                        }
                        catch (Exception ex) //alksjdfñlkjñlk
                        {

                            Console.WriteLine(ex.Message);
                            if (ex.InnerException != null)
                                Console.WriteLine("Inner exception getting user: {0}", ex.InnerException);
                            // throw;  //ay que ver aqui como reacciona //Aqui el throw iria dependiendo del requerimiento, si se quiere saber que ocasiono el error interno, utilizo el catchinner

                        }
                        break;
                    case 2:

                        //UserService esta dependiendo de las implementaciones de repository, sender y logger
                        lusers.NotifyUser(); //si hay 2 throws en write log y pasa la segunda vez, para la app pero pone que la linea original del que causo la excepcion es esta linea, la linea 20

                        Console.WriteLine("Sending Emails process has finished successfully");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please introduce 1 or 2 only");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            Console.WriteLine("Aqui contnuaria la app funcionando");
        }
    }
}