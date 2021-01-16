using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CarSharing_BO;
// ReSharper disable All

namespace CarSharing_DL
{
    public static class DBHandler
    {
        const string cs = @"server=localhost;userid=ricardo;password=2862;database=CarSharingDB";
        
        /// <summary>
        /// Uploasd a new user to db
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public static void AddUserToDB(User newUser)
        {
            var sql = "INSERT INTO users(email_address, username, birth_date, password) VALUES(@email, @name, @birthday, @password)";

            using var con = new MySqlConnection(cs);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("email", newUser.EmailAddress);
            cmd.Parameters.AddWithValue("@name", newUser.Name);
            cmd.Parameters.AddWithValue("@birthday", newUser.Birthdate);
            cmd.Parameters.AddWithValue("@password", newUser.Password);

            try
            {
                con.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                con.Close();
            }
        }
        
        /// <summary>
        /// Check if the user login credentials are correct
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckLoginCredentials(string email, string password)
        {
            const string sqlQuery = "SELECT id FROM users WHERE email_address = @email AND password = @password";

            using var conn = new MySqlConnection(cs);

            using var com = new MySqlCommand(sqlQuery, conn);
            
            com.Parameters.AddWithValue("@email", email);
            com.Parameters.AddWithValue("@password", password);
            
            try
            {
                conn.Open();

                var sqlDr = com.ExecuteReader();
                bool hasRows = sqlDr.HasRows;
                
                sqlDr.Close();
                
                return hasRows;
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
    public static class adsDBHandler
    {
        const string cs = @"server=localhost;userid=ricardo;password=2862;database=CarSharingDB";
        
        /// <summary>
        /// Inserts the given ad in the DB
        /// </summary>
        /// <param name="newAdd"></param>
        /// <exception cref="MySqlException"></exception>
        public static void InsertAdToDB(Ad newAdd)
        {
            var sql = "INSERT INTO ads(owner_email, origin, destination, title, description, contact) VALUES(@email, @origin, @destination, @title, @description, @contact)";

            using var con = new MySqlConnection(cs);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("email", newAdd.OwnerEmail);
            cmd.Parameters.AddWithValue("@origin", newAdd.Origin);
            cmd.Parameters.AddWithValue("@destination", newAdd.Destination);
            cmd.Parameters.AddWithValue("@title", newAdd.Title);
            cmd.Parameters.AddWithValue("@description", newAdd.Description);
            cmd.Parameters.AddWithValue("@contact", newAdd.Contact);

            try
            {
                con.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                con.Close();
            }
        }
        
        /// <summary>
        /// Returns a dictionary with the ads of the given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetAdsByEmailDB(string email)
        {
            var ads = new Dictionary<int, string>();
            var sqlQuery = "SELECT id, title FROM ads WHERE owner_email = @email";

            using MySqlConnection conn = new MySqlConnection(cs);
            using MySqlCommand com = new MySqlCommand(sqlQuery, conn);
            
            com.Parameters.AddWithValue("@email", email);

            try
            {
                conn.Open();
                MySqlDataReader sqlDR = com.ExecuteReader();
                
                while (sqlDR.Read()) 
                    ads.Add(sqlDR.GetInt32(0), sqlDR.GetString(1));
                sqlDR.Close();
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
            
            return ads;
        }
        
        /// <summary>
        /// Update title from an ad
        /// </summary>
        /// <param name="title"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void UpdateAdsTitleDB(string title, int key)
        {
            var sqlQuery = "UPDATE ads SET title = @title WHERE id = @id";

            using MySqlConnection conn = new MySqlConnection(cs);
            using MySqlCommand com = new MySqlCommand(sqlQuery, conn);
            
            com.Parameters.AddWithValue("@title", title);
            com.Parameters.AddWithValue("@id", key);

            try
            {
                conn.Open();
                com.Prepare();
                com.ExecuteNonQuery();
            }
            catch(MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
        }
        
        /// <summary>
        /// Update description from an ad
        /// </summary>
        /// <param name="title"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void UpdateAdsDescDB(string description, int key)
        {
            var sqlQuery = "UPDATE ads SET description = @desc WHERE id = @id";

            using MySqlConnection conn = new MySqlConnection(cs);
            using MySqlCommand com = new MySqlCommand(sqlQuery, conn);
            
            com.Parameters.AddWithValue("@desc", description);
            com.Parameters.AddWithValue("@id", key);
            
            try
            {
                conn.Open();
                com.Prepare();
                com.ExecuteNonQuery();
            }
            catch(MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
        }
        
        /// <summary>
        /// Update contact from an ad
        /// </summary>
        /// <param name="title"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void UpdateAdsContactDB(string contact, int key)
        {
            var sqlQuery = "UPDATE ads SET contact = @contact WHERE id = @id";

            MySqlConnection conn = new MySqlConnection(cs);
            MySqlCommand com = new MySqlCommand(sqlQuery, conn);
            com.Parameters.AddWithValue("@contact", contact);
            com.Parameters.AddWithValue("@id", key);
            
            try
            {
                conn.Open();
                com.Prepare();
                com.ExecuteNonQuery();
            }
            catch(MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
        }
        
        /// <summary>
        /// Returns a dictionary with the id, title and description from the DB
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Dictionary<int, Tuple<string, string>> GetAdsFromDB(string origin, string destination)
        {
            var returnValues = new Dictionary<int, Tuple<string, string>>();
            using var con = new MySqlConnection(cs);
            
            const string sqlQuery = "SELECT id, title, description FROM ads WHERE origin = @origin AND destination = @destination";
            
            using var cmd = new MySqlCommand(sqlQuery, con);
            cmd.Parameters.AddWithValue("@origin", origin);
            cmd.Parameters.AddWithValue("@destination", destination);

            try
            {
                con.Open();

                using MySqlDataReader sqlDR = cmd.ExecuteReader();
                while (sqlDR.Read())
                {
                    var t = Tuple.Create(sqlDR.GetString(1), sqlDR.GetString(2));
                    returnValues.Add(sqlDR.GetInt32(0), t);
                }
                sqlDR.Close();
            }
            catch(MySqlException exc)
            {
                throw exc;
            }
            finally
            {
                con.Close();
            }
            
            return returnValues;
        }

    }
}