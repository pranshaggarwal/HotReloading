using System;
using Hotswapping.ModApi;

namespace Hotswapping.Module1
{
    internal class AMod : IMod
    {
        public void Init()
        {
            Console.WriteLine("A mod has initialized..");
        }
    }
}