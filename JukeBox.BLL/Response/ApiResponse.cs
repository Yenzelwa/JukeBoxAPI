using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JukeBox.BLL.Response
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


}