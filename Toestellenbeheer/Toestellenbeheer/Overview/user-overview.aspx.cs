﻿using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toestellenbeheer.Overview
{
    public partial class user_overview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryEntry rootDSE = rootDSE = new DirectoryEntry("LDAP://magnix.dc.intranet", "readonly@dc.intranet", "id.13542");

            DirectorySearcher search = new DirectorySearcher(rootDSE);

            search.PageSize = 1001;// To Pull up more than 100 records.

            search.Filter = "(&(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2))";//UserAccountControl will only Include Non-Disabled Users.
            SearchResultCollection result = search.FindAll();
            String DisplayName, EmailAddress, DomainName, Department, Title, Company, memberof, aaa;
            DisplayName = EmailAddress = DomainName = Department = Title = Company = memberof = aaa = "";
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
                if (item.Properties["SamAccountName"].Count > 0)
                {
                    DomainName = item.Properties["SamAccountName"][0].ToString();
                }
                if (item.Properties["department"].Count > 0)
                {
                    Department = item.Properties["department"][0].ToString();
                }
                if (item.Properties["title"].Count > 0)
                {
                    Title = item.Properties["title"][0].ToString();
                }
                if (item.Properties["company"].Count > 0)
                {
                    Company = item.Properties["company"][0].ToString();
                }
                if (item.Properties["DistinguishedName"].Count > 0)
                {
                    memberof = item.Properties["DistinguishedName"][0].ToString();
                }
                if (item.Properties["AccountExpirationDate"].Count > 0)
                {
                    aaa = item.Properties["AccountExpirationDate"][0].ToString();
                }
                DataTable dt = new DataTable();
                //dt.Columns("DisplayName", "EmailAddress", "DomainName", "Department", "Title", "Company", "memberof");
                dt.Columns.Add(new DataColumn("Display Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Email Address", typeof(string)));
                dt.Columns.Add(new DataColumn("Domain Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Department", typeof(string)));
                dt.Columns.Add(new DataColumn("Title", typeof(string)));
                dt.Columns.Add(new DataColumn("Company", typeof(string)));
                dt.Columns.Add(new DataColumn("Member Of", typeof(string)));

                dt.Rows.Add(DisplayName, EmailAddress, DomainName, Department, Title, Company, memberof);

                rootDSE.Dispose();
                gv.DataSource = dt;

                gv.DataBind();
            }
        }
    }
}