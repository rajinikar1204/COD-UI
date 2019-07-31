using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ADP.TaaS.COD
{
    /// <summary>
    /// Refactored reading headers from context
    /// </summary>
    public interface IGetHeaders
    {
        string ReadHeaders(string key);
    }
    [ExcludeFromCodeCoverage]
    public class GetHeaders : IGetHeaders
    {
        public string ReadHeaders(string key)
        {
          
            var request = HttpContext.Current.Request;
            string  output=null;
            switch (key)
            {
                case "ORGOID":
                    output = request.Headers["ORGOID"];
                    break;

                case "AOID":
                    output = request.Headers["AOID"];
                    break;
            }
            return output;
        }
    }
}