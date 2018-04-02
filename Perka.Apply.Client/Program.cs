using System;
using Perka.Apply.Client.Actions;

namespace Perka.Apply.Client
{
    internal static class Program
    {
        private static readonly IApplicationActions ApplicationActions = new ApplicationActions();

        private static void Main(string[] args = null)
        {
            Console.WriteLine(ApplicationActions.Apply() ? "Success!" : "Failure!");
        }
    }
}