using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiRequestFilter
    {

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int userID { get; set; }
    }

    /// <summary>
    /// ApiLoginRequest
    /// </summary>
    public class ApiLoginRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string password { get; set; }
    }

    #region APi Login Client
    /// <summary>
    /// ApiClientLoginRequest
    /// </summary>
    public class ApiClientLoginRequest
    {
        /// <summary>
        /// Gets or sets the client account number.
        /// </summary>
        /// <value>
        /// The client account number.
        /// </value>
        public string username { get; set; }
        /// <summary>
        /// Gets or sets the client pin.
        /// </summary>
        /// <value>
        /// The client pin.
        /// </value>
        public string password { get; set; }
    }
    #endregion
}
