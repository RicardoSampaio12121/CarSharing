using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using CarSharing_RN;
using CarSharing_BO;
using MySql.Data.MySqlClient;

namespace CarSharing_FE
{
    internal static class Program
    {
        private static void Main()
        {
            const string dateFormat = "d/M/yyyy";

            while (true)
            {
                //Print the main menu
                Console.Clear();
                Menus.MainMenu();
                //Check if the user input is an int
                if (!int.TryParse(Console.ReadLine(), out int decision))
                {
                    Console.WriteLine("Wrong format!\nPress any key to continue...");
                    Console.ReadKey();
                }
                //Check if the user input is an existing option
                else if (decision < 1 || decision > 2)
                {
                    Console.WriteLine("Invalid option!\nPress any key to continue...");
                    Console.ReadKey();
                }

                switch (decision)
                {
                    case 1: //Login

                        //Gets the user email and password
                        Console.WriteLine("E-mail address: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Password: ");
                        string password = Console.ReadLine();

                        //Tries to log in
                        try
                        {
                            if (!RN_DB.Login(email, password))
                            {
                                Console.WriteLine("Incorrect email or password\nPress any key to continue...");
                                Console.ReadKey();
                                break;
                            }
                        }
                        catch (MySqlException exc)
                        {
                            Console.WriteLine(exc.Message + "\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                        
                        //Once logged in, prints the next menu
                        Console.Clear();
                        Menus.LoggedMenu();
                        
                        if (!int.TryParse(Console.ReadLine(), out decision))
                        {
                            Console.WriteLine("Wrong format!\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        if (decision < 0 || decision > 2)
                        {
                            Console.WriteLine("Invalid option!\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        switch (decision)
                        {
                            case 1: //My ads menu
                                
                                //Prints the 
                                Console.Clear();
                                Menus.MyAdsMenu();
                                if (!int.TryParse(Console.ReadLine(), out decision))
                                {
                                    Console.WriteLine("Wrong format!\nPress any key to continue...");
                                    Console.ReadKey();
                                    break;
                                }

                                if (decision < 0 || decision > 3)
                                {
                                    Console.WriteLine("Invalid option!\nPress any key to continue...");
                                    Console.ReadKey();
                                    break;
                                }

                                switch (decision)
                                {
                                    case 1: //Creates an ad
                                    {
                                        Console.WriteLine("Origin: ");
                                        string origin = Console.ReadLine();

                                        Console.WriteLine("Destination: ");
                                        string destination = Console.ReadLine();

                                        Console.WriteLine("Ad title: ");
                                        string title = Console.ReadLine();

                                        Console.WriteLine("Description: ");
                                        string description = Console.ReadLine();

                                        Console.WriteLine("How to contact you: ");
                                        string contact = Console.ReadLine();

                                        var newAd = new Ad(email, origin, destination, title, description, contact);
                                        
                                        //Tries to upload the ad to the db
                                        try
                                        {
                                            RN_DB_ADS.SubmitAd(newAd);
                                        }
                                        catch (MySqlException exc)
                                        {
                                            Console.WriteLine(exc.Message + "\nPress any key to continue...");
                                            Console.ReadKey();
                                        }
                                        break;
                                    }
                                    case 2: //Edit info from ad
                                    {
                                        var ads = new Dictionary<int, string>();
                                        
                                        //Gets all the ads of the current user
                                        try
                                        {
                                            ads = RN_DB_ADS.GetAdsByEmail(email);
                                        }
                                        catch(MySqlException exc)
                                        {
                                            Console.WriteLine(exc.Message + "\nPress any key to continue...");
                                            Console.ReadKey();
                                        }
                                        
                                        //Prints the ads
                                        foreach (var ad in ads)
                                            Console.WriteLine("({0}) {1}", ad.Key.ToString(), ad.Value);
                                        
                                        Console.Write("Decision: ");
                                        if (!int.TryParse(Console.ReadLine(), out int key))
                                        {
                                            Console.WriteLine("Invalid format!\nPress any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }

                                        if (!ads.ContainsKey(key))
                                        {
                                            Console.WriteLine("Invalid option\nPress any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }
                                        
                                        //Prints the next menu
                                        Console.Clear();
                                        Menus.EditMenu();
                                        if (!int.TryParse(Console.ReadLine(), out decision))
                                        {
                                            Console.WriteLine("Invalid format!\nPress any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }

                                        if (decision < 1 || decision > 3)
                                        {
                                            Console.WriteLine("Invalid option|\nPress any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }

                                        string title = "", description = "", contact = "";

                                        switch (decision)
                                        {
                                            case 1:
                                                Console.WriteLine("New title: ");
                                                title = Console.ReadLine();
                                                break;
                                            case 2:
                                                Console.WriteLine("New description: ");
                                                description = Console.ReadLine();
                                                break;
                                            default:
                                                Console.WriteLine("New contact: ");
                                                contact = Console.ReadLine();
                                                break;
                                        }

                                        if (title.Length < 2 && description.Length < 2 && contact.Length < 2)
                                        {
                                            Console.WriteLine("Edition can't be empty!\nPress any key to continue...");
                                            break;
                                        }
                                        
                                        //Tries to update the ad
                                        try
                                        {
                                            RN_DB_ADS.UpdateAdInfo(title, description, contact, key);
                                        }
                                        catch (MySqlException exc)
                                        {
                                            Console.WriteLine(exc.Message + "\nPress any key to continue...");
                                            Console.ReadKey();
                                        }

                                        break;
                                    }
                                    default: //List your ads
                                    {
                                        var ads = new Dictionary<int, string>();
                                        //Gets the ads
                                        try
                                        {
                                            ads = RN_DB_ADS.GetAdsByEmail(email);
                                        }
                                        catch (MySqlException exc)
                                        {
                                            Console.WriteLine(exc.Message + "\nPress any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }
                                        
                                        //Prints the ads
                                        foreach (var ad in ads)
                                            Console.WriteLine("({0}) {1}\n", ad.Key.ToString(), ad.Value);
                                        break;
                                    }
                                }
                                break;
                            case 2: //Search for ad
                                Console.Write("Origin: ");
                                string sOrigin = Console.ReadLine();

                                Console.WriteLine("Destination: ");
                                string sDestination = Console.ReadLine();

                                var d = new Dictionary<int, Tuple<string, string>>();
                                
                                //Search ads
                                try
                                {
                                    d = RN_DB_ADS.SearchAds(sOrigin, sDestination);
                                }
                                catch (MySqlException exc)
                                {
                                    Console.WriteLine(exc.Message + "\nPress any key to continue...");
                                    Console.ReadKey();
                                    break;
                                }

                                foreach (var v in d)
                                {
                                    Console.WriteLine("({0}) {1}", v.Key.ToString(), v.Value.Item1);
                                    Console.WriteLine(v.Value.Item2);
                                }

                                break;
                        }

                        break;
                    case 2: //Register

                        Console.WriteLine("First and last name: ");
                        string username = Console.ReadLine();

                        Console.WriteLine("Password: ");
                        password = Console.ReadLine();

                        Console.WriteLine("Address: ");
                        string address = Console.ReadLine();

                        Console.WriteLine("Email address: ");
                        email = Console.ReadLine();

                        Console.WriteLine("Date of birth: ");
                        if (!DateTime.TryParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out var birthDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }

                        var newuser = new User(username, password, address, email, birthDate);

                        //fazer a verificação se foi sucedido ou não
                        try
                        {
                            RN_DB.CreateUser(newuser);
                        }
                        catch(MySqlException exc)
                        {
                            Console.WriteLine(exc.Message + "\nPress any key to continue...");
                            Console.ReadKey();
                        }

                        break;
                }
            }
        }
    }
}