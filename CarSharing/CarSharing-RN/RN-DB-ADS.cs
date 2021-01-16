using System;
using System.Collections.Generic;
using CarSharing_DL;
using CarSharing_BO;
using MySql.Data.MySqlClient;

namespace CarSharing_RN
{
    /// <summary>
    /// Makes a bridge between front and back end
    /// </summary>
    public static class RN_DB_ADS
    {
        /// <summary>
        /// Calls a function to upload the ad to the database
        /// </summary>
        /// <param name="newAd"></param>
        /// <returns></returns>
        public static void SubmitAd(Ad newAd)
        {
            try
            {
                adsDBHandler.InsertAdToDB(newAd);
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
        }
        
        /// <summary>
        /// Calls a function to return the ads of the given email in a dictionary from the DB
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="MySqlException"></exception>
        public static Dictionary<int, string> GetAdsByEmail(string email)
        {
            try
            {
                return adsDBHandler.GetAdsByEmailDB(email);
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
        }

       /// <summary>
       /// Calls a function to update either the title, the description or the contact
       /// </summary>
       /// <param name="title"></param>
       /// <param name="description"></param>
       /// <param name="contact"></param>
       /// <param name="key"></param>
       /// <exception cref="MySqlException"></exception>
        public static void UpdateAdInfo(string title, string description, string contact, int key)
        {
            try
            {
                if (title != "")
                    adsDBHandler.UpdateAdsTitleDB(title, key);
                else if (description != "")
                    adsDBHandler.UpdateAdsDescDB(description, key);
                else
                    adsDBHandler.UpdateAdsContactDB(contact, key);
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
        }
        
        /// <summary>
        /// Calls a function to get the dictionary with the db info
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="MySqlException"></exception>
        public static Dictionary<int, Tuple<string, string>> SearchAds(string origin, string destination)
        {
            try
            {
                return adsDBHandler.GetAdsFromDB(origin, destination);
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
        }
    }
}