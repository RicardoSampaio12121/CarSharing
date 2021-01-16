using CarSharing_DL;
using CarSharing_BO;
using MySql.Data.MySqlClient;

namespace CarSharing_RN
{
    public class RN_DB
    {
        /// <summary>
        /// Calls a function to upload a new user to db
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public static void CreateUser(User newUser)
        {
            try
            {
                DBHandler.AddUserToDB(newUser);
            }
            catch (MySqlException exc)
            {
                throw exc;
            }
        }
        
        /// <summary>
        /// Cals the function to check the login credentials in a database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string email, string password)
        {
            try
            {
                return DBHandler.CheckLoginCredentials(email, password);
            }
            catch(MySqlException exc)
            {
                throw exc;
            }
        }
    }
}