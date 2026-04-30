using Lab4Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Services
{
    public class ConsoleMenuService : IMenuService
    {
        public void Show() => Console.WriteLine("Console menu");
    }

}
