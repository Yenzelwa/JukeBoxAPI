﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static JukeBoxApi.Models.ApiAccount;

namespace JukeBoxApi.Models
{

    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the type of the response.
        /// </summary>
        /// <value>
        /// The type of the response.
        /// </value>
        public int ResponseType { get; set; }
    /// <summary>
    /// Gets or sets the response message.
    /// </summary>
    /// <value>
    /// The response message.
    /// </value>
    public string ResponseMessage { get; set; }

}
    /// <summary>
    /// ApiLoginResponse
    /// </summary>
    public class ApiLoginUserResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the response object.
        /// </summary>
        /// <value>
        /// The response object.
        /// </value>
        public ApiUser ResponseObject { get; set; }
        /// <summary>
        /// Gets or sets the session token.
        /// </summary>
        /// <value>
        /// The session token.
        /// </value>
        public string SessionToken { get; set; }
    }
    public class ApiLoginClientResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the response object.
        /// </summary>
        /// <value>
        /// The response object.
        /// </value>
        public ApiClient ResponseObject { get; set; }
        /// <summary>
        /// Gets or sets the session token.
        /// </summary>
        /// <value>
        /// The session token.
        /// </value>
        public string SessionToken { get; set; }
    }
    public class ApiLibraryResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the response object.
        /// </summary>
        /// <value>
        /// The response object.
        /// </value>
        public List<ApiLibrary> ResponseObject { get; set; }
        
    }
    public class ApiLibraryDetailResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the response object.
        /// </summary>
        /// <value>
        /// The response object.
        /// </value>
        public List<ApiLibraryDetail> ResponseObject { get; set; }

    }
}