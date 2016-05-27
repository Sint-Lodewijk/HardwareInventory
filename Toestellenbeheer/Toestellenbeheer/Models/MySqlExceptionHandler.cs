using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Toestellenbeheer.Models
{
    public class MySqlExceptionHandler
    {
        public MySqlExceptionHandler()
        {
        }
        public MySqlExceptionHandler(MySqlException ex, string strExceptionType)
        {
            ExceptionType = strExceptionType;
            MySqlExceptionName = ex;
        }
        public string ExceptionType { get; set; }
        public MySqlException MySqlExceptionName { get; set; }
        public string ReturnMessage()
        {
            switch (MySqlExceptionName.Number)
            {
                case 1042:
                    return "Can not connect with the specified MySql Connection, please checks the connection string or the status of MySqlServer";
                case 1062:
                    return ExceptionType + " already exists in database";
                case 1149:
                    return "\', ; and \" are not allowed in the search word";
                default:
                    return "Unexpected error encountered! " + MySqlExceptionName.Message;
            }
        }
    }
}