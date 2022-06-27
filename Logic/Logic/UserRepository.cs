using Logic.Abstractions;
using Logic.Data;
using Logic.Exceptions;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserRepository : IRepository<User>
    {
        public List<User> GetAll()
        {
            var context = new EFCoreDemoContext();
            return context.Users.ToList();
        }

        public User GetUserByID(int id)
        {
            var context = new EFCoreDemoContext();
            return context.Users.FirstOrDefault(x => x.Id == id);
            
        }
    }
}
