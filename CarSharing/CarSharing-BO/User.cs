using System;
using System.Globalization;

namespace CarSharing_BO
{
    public class User : Person
    {
        #region ATTRIBUTES

        private string password;
        private string emailAddress;
        
        #endregion

        #region PROPERTIES

        public override string Name 
        { 
            get => name; 
            set => name = value; 
        }

        public override string Address
        {
            get => address; 
            set => address = value;
        }

        public override DateTime Birthdate
        {
            get => birthDate; 
            set => birthDate = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string EmailAddress
        {
            get => emailAddress;
            set => emailAddress = value;
        }

        #endregion

        #region CONSTRUCTORS
        
        /// <summary>
        /// Creates an empty User
        /// </summary>
        public User()
        {
            this.name = "";
            this.password = "";
            this.address = "";
            this.emailAddress = "";
            this.birthDate = DateTime.ParseExact("01/01/0001", "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        
        public User(string name, string password, string address, string emailAddress, DateTime birthDate)
        {
            this.name = name;
            this.password = password;
            this.address = address;
            this.emailAddress = emailAddress;
            this.birthDate = birthDate;
        }
        

        #endregion
    }
}