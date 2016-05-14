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
        public static class AD
        {
            /// <summary>
            /// Configurationfile of Active Directory
            /// </summary>
            public const String ADConnectionPrefix = "LDAP://dc.6ib.eu/OU=Employees,DC=6ib,DC=eu";
            public const String ADUserName = "jli@6ib.eu";
            public const String ADUserPassword = "1234QWEr";
            public const String ADPath = "OU=Employees,DC=6ib,DC=eu";
            public const String ADSAMAccountAt = "@6ib.eu";
            public const String ADDomainControllerName = "dc";
            public const String ADDomainName = "6IB";
            public const String ADRootPath = "LDAP://dc.6ib.eu";
        }
        public static class XML
        {
            public const String ScripturaPath = "http://scriptix:13542/web/ontvangstbewijs/generate-pdf";
        }
        public static class Email
        {
            public const String EmailFrom = "hardwareinventory@outlook.com";
            public const String EmailPassword = "JIANINg520";
            public const String MailServer = "smtp-mail.outlook.com";
            public const int SMTPPort = 587;
            public const String EmailTo = "mr.jianing@hotmail.com";
        }
        public static class Requests
        {
            public const int hardwareRequestChangeColorAfter = 3;
            public const String hardwareRequestChangeColorHex = "red";
        }
        public static class Web
        {
            public const String WebLocation = "jianing.xyz";
        }
        public static class Style
        {
            public const String SelectedRowColorHex = "red";
        }
        public static class MySql
        {
            public const string SHA2EncryptionKey = "HardwareInventory";
        }
    }
}