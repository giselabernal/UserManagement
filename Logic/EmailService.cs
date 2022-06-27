using Logic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class EmailService : ISender
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Email to: {to} with following message {message}");
        }
    }
}
