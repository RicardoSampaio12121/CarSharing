/*
 * This file contains the AD class
 */

namespace CarSharing_BO
{
    /// <summary>
    /// Class that contains the info of an ad
    /// </summary>
    public class Ad
    {
        #region PROPERTIES

        public string OwnerEmail { get; }

        public string Origin { get; }

        public string Destination { get; }

        public string Title { get; }

        public string Description { get; }

        public string Contact { get; }

        #endregion
        
        #region CONSTRUCTORS

        public Ad(string owner_email, string origin, string destination, string title, string description, string contact)
        {
            this.OwnerEmail = owner_email;
            this.Origin = origin;
            this.Destination = destination;
            this.Title = title;
            this.Description = description;
            this.Contact = contact;
        }

        #endregion
    }
}