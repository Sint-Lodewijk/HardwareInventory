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
            /// 
            //The AD path like LDAP://dc.6ib.eu/OU=Employees,DC=6ib,DC=eu
            public const String ADConnectionPrefix = SetupFile.AD.ADRootPath + "/" + SetupFile.AD.ADPath;
            //The username who have read and write access to AD, in order to create AD user
            public const String ADUserName = "jli@6ib.eu";
            //The AD password
            public const String ADUserPassword = "1234QWEr";
            //The AD Path like "OU=Employees,DC=6ib,DC=eu"
            public const String ADPath = "OU=Employees,DC=6ib,DC=eu";
            //@Organization.com
            public const String ADSAMAccountAt = "@6ib.eu";
            //Name of domain controller
            public const String ADDomainControllerName = "dc";
            //Name of domain
            public const String ADDomainName = "6IB";
            //LDAP path like LDAP://dc.6ib.eu
            public const String ADRootPath = "LDAP://dc.6ib.eu";
        }
        public static class XML
        {
            //This feature is disabled
            public const String ScripturaPath = "http://scriptix:13542/web/ontvangstbewijs/generate-pdf";
        }
        public static class Email
        {
            //The emailaddress of the mailsender to send mail notification.
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
            //Weblocation where the application is hosted.
            public const String WebLocation = "inventory.jianing.xyz";
        }
        public static class Style
        {
            public const String SelectedRowColorHex = "red";
        }
        public static class MySql
        {
            //SHA2 Encryption key
            public const string SHA2EncryptionKey = "HardwareInventory";
        }
    }
}