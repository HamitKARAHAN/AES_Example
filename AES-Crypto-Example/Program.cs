using AES_Crypto_Example.Concrete;
using System;

namespace AES_Crypto_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            FileService _fileService = new FileService(new EncryptionService());
            UserService _userService = new UserService(_fileService);
            UserOperations user = new UserOperations();
            //MENU
            bool continuee = true;
            while (continuee)
            {
                #region Scene
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * *");
                Console.WriteLine("%      1 - Add User                           %\n");
                Console.WriteLine("%      2 - Search User                        %\n");
                Console.WriteLine("%      3 - Search User by id                  %\n");
                Console.WriteLine("%      4 - List Users                         %\n");
                Console.WriteLine("%      5 - Update Users                       %\n");
                Console.WriteLine("%      6 - Delete User                        %\n");
                Console.WriteLine("%      7 - Exit                               %\n");
                Console.WriteLine("%      Choice:                                %");
                Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * *");               
                Console.SetCursorPosition(19, 15);
                int menu = 0;
                try
                {
                    menu = int.Parse(Console.ReadLine());                  
                    if (menu<=0 || menu>7)
                    { 
                        Console.Write("\n                                              %");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n     Please enter a number between 1-7!   ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("    %\n");
                        Console.Write("                                              %");
                        Console.WriteLine("\nx x x x x x x x x x x x x x x x x x x x x x x x");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        Main(args);
                    }
                }
                catch (Exception)
                {
                    Console.Write("\n                                              %");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n       Please enter a valid number!     ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("      %\n");
                    Console.Write("                                              %");
                    Console.WriteLine("\nx x x x x x x x x x x x x x x x x x x x x x x x");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    Main(args);
                }
                Console.ForegroundColor = ConsoleColor.White;
                #endregion
                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        user.AddUser(_userService);
                        Console.Clear();
                        break;

                    case 2:
                        Console.Clear();
                        user.FindUser(_userService);
                        Console.Clear();
                        break;

                    case 3:
                        Console.Clear();
                        user.FindUserWithId(_userService);
                        Console.Clear();
                        break;

                    case 4:
                        Console.Clear();
                        user.GetUsers(_userService);
                        Console.Clear();
                        break;
                    
                    case 5:
                        Console.Clear();
                        user.UpdateUser(_userService);
                        Console.Clear();
                        break;

                    case 6:
                        Console.Clear();
                        user.DeleteUser(_userService);
                        Console.Clear();
                        break;

                    case 7:
                    default:
                        Console.WriteLine("\n\nEXIT\n");
                        continuee = false;
                        break;
                }
            }
        }
    }
}
