/*
 *This file contains a class that contains all the menu in the program
 */

using System;

namespace CarSharing_FE
{
    /// <summary>
    /// This class contains all the menus in the program
    /// </summary>
    public static class Menus
    {
        /// <summary>
        /// First menu in the program
        /// </summary>
        public static void MainMenu()
        {
            Console.WriteLine("(1) LOGIN");
            Console.WriteLine("(2) REGISTER");
            Console.Write("Decision: ");
        }
        
        /// <summary>
        /// Menu after the user login
        /// </summary>
        public static void LoggedMenu()
        {
            Console.WriteLine("(1) My ads");
            Console.WriteLine("(2) Search for ads");
            Console.Write("Decision: ");
        }
        
        /// <summary>
        /// Menu after user selects "My ads" in LoggedMenu
        /// </summary>
        public static void MyAdsMenu()
        {
            Console.WriteLine("(1) Publish ad");
            Console.WriteLine("(2) Edit ad");
            Console.WriteLine("(3) List ads");
            Console.Write("Decision: ");
        }
        
        /// <summary>
        /// Menu after user selects "Edit ad" in "MyAdsMenu" 
        /// </summary>
        public static void EditMenu()
        {
            Console.WriteLine("(1) Edit title");
            Console.WriteLine("(2) Edit description");
            Console.WriteLine("(3) Edit contact");
            Console.Write("Decision: ");
        }
    }
}