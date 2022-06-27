using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstractions
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(); //esto se llama restriccion where T : new();

        //los nombres de los metodos en las interfaces deben ser genericos(generales)
        T GetUserByID(int id);
    }
}
