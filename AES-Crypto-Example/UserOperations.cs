using AES_Crypto_Example.Concrete;
using AES_Crypto_Example.Models.User;
using System;

namespace AES_Crypto_Example
{
    class UserOperations
    {
        internal void AddUser(UserService _userService)
        {
            #region Scene
            Console.Write("How much user you will add? Enter a number : ");
            int m_index = 0;
            try
            {
                m_index = Convert.ToInt32(Console.ReadLine());
                
                if (m_index <= 0 || m_index >= 100)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlease enter a number between 1-99!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(500);
                    AddUser(_userService);
                    return;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Value!");
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(500);
                return;
            }
            #endregion
            int sayac = 1;
            Console.WriteLine();
            Console.Clear();
            int index = _userService.FindNumberOfUsers();
            for (int i = index + 1; i <= index + m_index; i++)
            {
                #region Scene
                Console.Write("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *");
                Console.Write("    " + sayac + ". Username : ");
                Console.Write("                                 * * *\n* * * ");
                Console.Write("   " + sayac + ". Password : ");
                Console.Write("                                 * * *\n* * * ");
                Console.Write("   " + sayac + ". Servername : ");
                Console.Write("                               * * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                Console.SetCursorPosition(32, 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string KullaniciAdi = Console.ReadLine();
                Console.SetCursorPosition(32, 2);
                string Sifre = Console.ReadLine();
                Console.SetCursorPosition(32, 3);
                string ServerAdi = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                #endregion
                var response = _userService.Register(new UserModel()
                {
                    ID = i,
                    Username = KullaniciAdi,
                    Password = Sifre,
                    ServerName = ServerAdi,
                });
                Console.Clear();
                if (response.Status == true)
                {
                    Console.Write("* * * * * * * * * * * * * * * * * * * * * *\n* * *");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("    User added successfully!     ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("* * *\n* * * * * * * * * * * * * * * * * * * * * *");
                    System.Threading.Thread.Sleep(1500);
                }
                else if (response.Status == false)
                {
                    Console.Write("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("     An error occured! " + response.Message + "     ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("* * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                    System.Threading.Thread.Sleep(1500);
                }
                sayac++;
                Console.Clear();
            }
        }

        internal bool FindUser(UserService _userService)
        {
            #region Scene
            Console.WriteLine("Enter User informations!\n ");
            Console.Write("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *");
            Console.Write("    İsim : ");
            Console.Write("                                        * * *\n* * * ");
            Console.Write("   Şifre : ");
            Console.Write("                                       * * *\n* * * ");
            Console.Write("   Server : ");
            Console.Write("                                      * * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
            Console.SetCursorPosition(19, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string KullaniciAdi = Console.ReadLine();
            Console.SetCursorPosition(19, 4);
            string Sifre = Console.ReadLine();
            Console.SetCursorPosition(19, 5);
            string ServerAdi = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            #endregion
            if (_userService.IsUserExist(KullaniciAdi, Sifre, ServerAdi))
            {
                Console.Write("\n* * * * * * * * * * * * * * * * * * * * * * *\n* * *");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("    This user already registered!  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("* * *\n* * * * * * * * * * * * * * * * * * * * * * *");
                System.Threading.Thread.Sleep(1000);
                return true;
            }
            else
            {
                Console.Write("\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\nx x x");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("    This user is not exist!                        ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("x x x\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                System.Threading.Thread.Sleep(1000);
                return false;
            }
        }

        internal void FindUserWithId(UserService _userService)
        {
            #region Scene
            Console.Write("Which user are you looking for? Enter an id greater than 0: ");
            int index = 0;
            try
            {
                index = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (index <= 0 || index >= 1000)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlease enter an id between 1-999!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    FindUserWithId(_userService);
                    return;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Value!\n");
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return;
            }
            #endregion
            var response = _userService.FindUserWithId(index);
            if (response.Status)
            {
                #region Scene
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *     ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("User Infromations                           ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("* * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *     ");
                Console.Write("Username:    ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(response.Data.Username);
                Console.ForegroundColor = ConsoleColor.White;
                int x = response.Data.Username.Length;
                for (int j = 0; j < 24 - x; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("       * * *\n* * *     ");
                Console.Write("Passwrod:    ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(response.Data.Password);
                Console.ForegroundColor = ConsoleColor.White;
                int y = response.Data.Password.Length;
                for (int t = 0; t < 31 - y; t++)
                {
                    Console.Write(" ");
                }
                Console.Write("* * *\n* * *     ");
                Console.Write("Servername:  ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(response.Data.ServerName);
                Console.ForegroundColor = ConsoleColor.White;
                int z = response.Data.ServerName.Length;
                for (int k = 0; k < 25 - z; k++)
                {
                    Console.Write(" ");
                }
                Console.Write("      * * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                Console.Write("\n\nPress a key for returning main menu!");
                Console.ReadKey();
                #endregion
            }
            else
            {
                Console.Write("\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\nx x x");
                Console.ForegroundColor = ConsoleColor.Red;
                int length = index.ToString().Length;
                for (int k = 0; k < 9 - length; k++)
                {
                    Console.Write(" ");
                }
                Console.Write("This user " + index + " is not registered!                 ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("x x x\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                System.Threading.Thread.Sleep(1000);
            }
        }
        
        internal void GetUsers(UserService _userService)
        {
            var userData = _userService.GetUsers();
            
            if (userData.Status == true)
            {
               
                int kullaniciSayisi = userData.Data.Length;
                Console.Write(kullaniciSayisi + " users found!");
                for (int i = 0; i < kullaniciSayisi; i++)
                {
                    #region Scene
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\n\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *     ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(i + 1 + ".Users Infos                               ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("* * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *     ");
                    Console.Write("ID:           ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(userData.Data[i].ID);
                    Console.ForegroundColor = ConsoleColor.White;
                    int p = userData.Data[i].ID.ToString().Length;
                    for (int j = 0; j < 17 - p; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("             * * *\n* * *     ");
                    Console.Write("Username:     ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(userData.Data[i].Username);
                    Console.ForegroundColor = ConsoleColor.White;
                    int x = userData.Data[i].Username.Length;
                    for (int j = 0; j < 17 - x; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("             * * *\n* * *     ");
                    Console.Write("Password:     ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(userData.Data[i].Password);
                    Console.ForegroundColor = ConsoleColor.White;
                    int y = userData.Data[i].Password.Length;
                    for (int t = 0; t < 25 - y; t++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("     * * *\n* * *     ");
                    Console.Write("Servername:   ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(userData.Data[i].ServerName);
                    Console.ForegroundColor = ConsoleColor.White;
                    int z = userData.Data[i].ServerName.Length;
                    for (int k = 0; k < 25 - z; k++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("     * * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                    #endregion
                }
                Console.Write("\n\nPress a key for returnig main menu");
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nx x x x x x x x x x x x x x x x x x x x\nx x x");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("   There is no user!         ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("x x x\nx x x x x x x x x x x x x x x x x x x x\n");
                System.Threading.Thread.Sleep(1000);
            }
        }

        internal void UpdateUser(UserService _userService)
        {
            #region Scene
            Console.Write("Which user do you want to update? Enter an id greater than 0: ");
            int index = 0;
            try
            {
                index = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (index <= 0 || index >= 1000)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlease enter an id between 1-999!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    DeleteUser(_userService);
                    return;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Value!\n");
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return;
            }
            #endregion
            var response = _userService.FindUserWithId(index);

            if (response.Status)
            {
                #region Scene
                Console.Write("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n* * *");
                Console.Write("    Username :          ");
                Console.Write("                             * * *\n* * * ");
                Console.Write("   Password :              ");
                Console.Write("                         * * *\n* * * ");
                Console.Write("   Servername :       ");
                Console.Write("                              * * *\n* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                Console.SetCursorPosition(34, 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string KullaniciAdi = Console.ReadLine();
                Console.SetCursorPosition(34, 4);
                string Sifre = Console.ReadLine();
                Console.SetCursorPosition(34, 5);
                string ServerAdi = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                #endregion
                var responseUpdate = _userService.UpdateUser(new UserModel()
                {
                    ID = index,
                    Username = KullaniciAdi,
                    Password = Sifre,
                    ServerName = ServerAdi,
                });
                Console.Clear();
                if (responseUpdate.Status)
                {
                    Console.Write("* * * * * * * * * * * * * * * * * * * * * * * *\n* * *");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("     " + "User " + index + " is updated!              ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("* * *\n* * * * * * * * * * * * * * * * * * * * * * * *");
                    System.Threading.Thread.Sleep(1500);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\nx x x");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("     " + responseUpdate.Message + "     ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("x x x\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            else
            {
                Console.Write("\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\nx x x");
                Console.ForegroundColor = ConsoleColor.Red;
                int length = index.ToString().Length;
                for (int k = 0; k < 9 - length; k++)
                {
                    Console.Write(" ");
                }
                Console.Write("The user " + index + " is not registered!                  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("x x x\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                System.Threading.Thread.Sleep(1000);
            }
        }

        internal void DeleteUser(UserService _userService)
        {
            #region Scene
            Console.Write("Which user do you want to delete? Enter an id greater than 0: ");
            int index = 0;
            try
            {
                index = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (index <= 0 || index >= 1000)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlease enter an id between 1-999!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    DeleteUser(_userService);
                    return;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid id. Please enter an id between 1-999!\n");
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return;
            }
            #endregion
            var response = _userService.DeleteUser(index);
            if (response.Status)
            {
                Console.Write("* * * * * * * * * * * * * * * * * * * * * *\n* * *");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("     " + "User " + index + " is deleted!        ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  * * *\n* * * * * * * * * * * * * * * * * * * * * *");
                System.Threading.Thread.Sleep(1500);
            }       
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x \nx x x");
                Console.ForegroundColor = ConsoleColor.Red;
                int x = response.Message.Length;
                for (int i = 0; i < 36 - x; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(response.Message+"     ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("        x x x\nx x x x x x x x x x x x x x x x x x x x x x x x x x x x x x\n");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}