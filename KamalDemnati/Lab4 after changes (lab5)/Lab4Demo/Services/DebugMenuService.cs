using Lab4Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Services
{
    public class DebugMenuService : IMenuService
    {
        public void Show() => Console.WriteLine("Debug menu");
    }
}
