﻿
using System;
using System.Text;
using System.Collections;
using System.DirectoryServices;

namespace FormsAuth
{
    public class LdapAuthentication
    {
        private String _path;
        private String _filterAttribute;

        public LdapAuthentication(String path)
        {
            _path = path;
        }

        public bool IsAuthenticated( String username, String pwd)
        {
            // String domainAndUsername = domain + @"\" + username;
            String domainAndUsername = "DC" + @"\" + username;

            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {   //Bind to the native AdsObject to force authentication.			
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }
        public String getPath()
        {
            return _path;
        }

        public String GetGroups()
        {
            DirectoryEntry oRoot = new DirectoryEntry();
            oRoot.Username = "readonly@dc.intranet";
            oRoot.Password = "id.13542";
            oRoot.Path = "LDAP://magnix.dc.intranet/OU=Employees,DC=dc,DC=intranet";
            DirectorySearcher search = new DirectorySearcher(oRoot);

            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
    }
}