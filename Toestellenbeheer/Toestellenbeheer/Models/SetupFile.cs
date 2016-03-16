using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toestellenbeheer.Models
{
    public class SetupFile
    {
        /// <summary>
        /// Contains global variables for project.
        /// </summary>
        public static class GlobalVar
        {
            /// <summary>
            /// Global variable that is constant.
            /// </summary>
            public const string ADConnectionPrefix= "LDAP://dc.6ib.eu/OU=Employees,DC=6ib,DC=eu";
            public const string ADUserName = "jli@6ib.eu";
            public const string ADUserPassword = "1234QWEr";
            public const string ScripturaPath = "http://scriptix:13542/web/ontvangstbewijs/generate-pdf";

        }
    }
}