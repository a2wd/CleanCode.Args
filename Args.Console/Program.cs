using System;

namespace Args.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Args arg = new Args("l,p#,d*", args);
                bool logging = args.getBoolean("l");
                int port = arg.getInt("p");
                string directory = arg.getString("d");

                executeApplication(logging, port, directory);
            }
            catch(ArgsException e)
            {
                System.Console.WriteLine($"Argument error: {e.errorMessage}");
            }
        }
    }
}