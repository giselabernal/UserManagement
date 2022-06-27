using Logic.Abstractions;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class WriteLog : ILogger
    {
        
        void ILogger.WriteLog(string message)
        {
            //var CurrentDirectory = Directory.GetCurrentDirectory().ToString() + "\\Logs";
            string CurrentDirectory = "c:\\UsersAppLogs\\Logs";
            try
            {
                if (!Directory.Exists(CurrentDirectory))
                {
                    Directory.CreateDirectory(CurrentDirectory);
                }

                string filename = GetFile(CurrentDirectory);
                //throw new FileNotFoundException(filename); //si esta linea la dejo 2 veces en ejecucion, para la ejecucion

                using (StreamWriter sw = File.AppendText(filename))
                {
                    wLog(message, sw);
                }
            }
            catch (DirectoryNotFoundException e) //este catch lo tenia en userservice y encontre su comportamiento que, si no lo tengo en esta clase, se propaga a la user service
            //y termina por completo el loop lo cual  no es conveniente, por eso lo pase para aca y solo marca app not found, se va al exception del user service y continuaria el loop
            //este catch lo agregue debido a que probe si comento el que no se cree la carpeta de logs pero descomentadas las lineas no debe marcar error
            {
                throw new AppException("Directory not found ", e);
            }
            catch (FileNotFoundException e)
            {
                throw; //si dejo throw solamente aqui y no hay un catch en el metodo papa de UserService, es decir linea 58,   la app se cierra
                //throw new AppException("test", e);   //esto lo hago para que genere mi custom exception y me marque en su metodo papa el error original y su catchinner
            }
            catch (AppException e)
            {
               
                throw new AppException("Error in CatchInner caused by calling the ThrowInner method.", e);
                
            }

        }

         string GetFile(string path)
        {
            string filename = "Log_" + DateTime.Now.ToShortDateString().Replace("/", "") + ".log";
            string cpath = path + "\\"+ filename;
            try
            {
                if (!File.Exists(cpath))
                {
                    using (StreamWriter sw = new StreamWriter(cpath))
                    {
                        sw.WriteLine("new file created {0}", DateTime.Now.ToString());
                        path = path + "\\" + filename;
                    }
                }
                //using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
                
            }
            catch(AppException e)
            {
                Console.WriteLine( e.Message.ToString());
            }
            return cpath;
        }

        void wLog(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString()); ;
            }
        }
    }
}
