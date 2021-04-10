using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestProjectAPIBaseLayer.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class SystemEnums
    {
        /// <summary>
        /// 
        /// </summary>
        public enum ResponseCodes
        {
            /// <summary>
            /// 
            /// </summary>
            Success = 200,
            /// <summary>
            /// 
            /// </summary>
            InternalServerError = 500,
            /// <summary>
            /// 
            /// </summary>
            PageNotFound = 404,
            /// <summary>
            /// 
            /// </summary>
            UnAuthorized = 401
        }

        public enum PasswordResetType
        {
            Forgot = 0,
            Change = 1,
        }
    }
}
