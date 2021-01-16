using System;

namespace CarSharing_BO
{
    public abstract class Person
    {
        #region ATTRIBUTES

        protected string name;
        protected DateTime birthDate;
        protected string address;
        
        #endregion
        
        #region PROPERTIES

        public abstract string Name
        {
            get;
            set;
        }
        public abstract string Address
        {
            get;
            set;
        }
        public abstract DateTime Birthdate
        {
            get;
            set;
        }
        
        #endregion
    }
}