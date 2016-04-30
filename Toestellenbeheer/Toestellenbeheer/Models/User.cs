using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.DirectoryServices;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace Toestellenbeheer.Models
{
    public class User
    {
        public String NameAD { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with the user principal name of AD.
        /// </summary>
        /// <param name="nameAD">The user principal name of active directory</param>
        public User(string nameAD)
        {
            this.NameAD = nameAD;
        }
        /// <summary>
        /// Checks if user already exists in the database
        /// and create a user eventID in the database if nessesary
        /// and returns the users eventID.
        /// </summary>
        /// <returns>System.Int32 the users eventID.</returns>
        public int ReturnEventID()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            mysqlConnectie.Open();
            MySqlCommand checkPeopleAlreadyExist = new MySqlCommand("SELECT eventID FROM people Where nameAD = '" + NameAD + "'", mysqlConnectie);
            object checkObj = checkPeopleAlreadyExist.ExecuteScalar();
            if (checkObj == null)
            {
                MySqlCommand addPeople = new MySqlCommand("INSERT INTO people (nameAD) values (@nameAD)", mysqlConnectie);
                addPeople.Parameters.AddWithValue("@nameAd", NameAD);
                addPeople.ExecuteNonQuery();
                addPeople.Dispose();
                MySqlCommand getMaxIndex = new MySqlCommand("SELECT eventID FROM people WHERE eventID = (SELECT MAX(eventID) FROM people)", mysqlConnectie);
                int maxIndex = Convert.ToInt16(getMaxIndex.ExecuteScalar().ToString());
                return maxIndex;
            }
            else
            {
                int maxIndex = Convert.ToInt16(checkPeopleAlreadyExist.ExecuteScalar().ToString());
                return maxIndex;
            }
        }
        /// <summary>
        /// Returns all users in AD in specific path in the data table in a datatable.
        /// </summary>
        /// <returns>DataTable of all users in AD.</returns>
        public DataTable ReturnDataTable()
        {
            DirectoryEntry rootDSE = rootDSE = new DirectoryEntry(SetupFile.AD.ADConnectionPrefix, SetupFile.AD.ADUserName, SetupFile.AD.ADUserPassword); //SetupFile
            DirectorySearcher search = new DirectorySearcher(rootDSE);
            search.PageSize = 1001;// To Pull up more than 100 records.
            search.Filter = "(&(objectClass=user)(objectCategory=person)(!userAccountControl:1.2.840.113556.1.4.803:=2))";//UserAccountControl will only Include Non-Disabled Users.
            SearchResultCollection result = search.FindAll();
            String DisplayName, EmailAddress, DomainName, Department, Title, Company, memberof, aaa;
            DisplayName = EmailAddress = DomainName = Department = Title = Company = aaa = "";
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Display Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Email Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Domain Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Department", typeof(string)));
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Company", typeof(string)));
            // dt.Columns.Add(new DataColumn("Member Of", typeof(string)));
            foreach (SearchResult item in result)
            {
                if (item.Properties["cn"].Count > 0)
                {
                    DisplayName = item.Properties["cn"][0].ToString();
                }
                if (item.Properties["mail"].Count > 0)
                {
                    EmailAddress = item.Properties["mail"][0].ToString();
                }
                else if (item.Properties["email"].Count == 0)
                {
                    EmailAddress = "";
                }
                if (item.Properties["SamAccountName"].Count > 0)
                {
                    DomainName = item.Properties["SamAccountName"][0].ToString();
                }
                if (item.Properties["department"].Count > 0)
                {
                    Department = item.Properties["department"][0].ToString();
                }
                else if (item.Properties["departement"].Count == 0)
                {
                    Department = "";
                }
                if (item.Properties["title"].Count > 0)
                {
                    Title = item.Properties["title"][0].ToString();
                }
                else if (item.Properties["title"].Count == 0)
                {
                    Title = "";
                }
                if (item.Properties["company"].Count > 0)
                {
                    Company = item.Properties["company"][0].ToString();
                }
                else if (item.Properties["company"].Count == 0)
                {
                    Company = "";
                }
                /*if (item.Properties["DistinguishedName"].Count > 0)
                {
                    memberof = item.Properties["DistinguishedName"][0].ToString();
                }*/
                if (item.Properties["AccountExpirationDate"].Count > 0)
                {
                    aaa = item.Properties["AccountExpirationDate"][0].ToString();
                }
                //dt.Columns("DisplayName", "EmailAddress", "DomainName", "Department", "Title", "Company", "memberof");
                dt.Rows.Add(DisplayName, EmailAddress, DomainName, Department, Title, Company);
            }
            rootDSE.Dispose();
            return dt;
        }
    }
}