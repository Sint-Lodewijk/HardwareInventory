using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toestellenbeheer.Models
{
    public class Request
    {
        public string InternalNr { get; set; }
        public string SerialNr { get; set; }
        public string RequestDate { get; set; }
        public int EventID { get; set; }
        public int RequestID { get; set; }
        public Request()
        {

        }
        public Request(int intRequestID)
        {
            this.RequestID = intRequestID;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class with 4 parameters.
        /// </summary>
        /// <param name="strInternalNr">The string internal nr.</param>
        /// <param name="strSerialNr">The string serial nr.</param>
        /// <param name="intEventID">The int user event id.</param>
        /// <param name="strRequestDate">The string request date.</param>
        public Request(string strInternalNr, string strSerialNr, int intEventID, string strRequestDate)
        {
            InternalNr = strInternalNr;
            SerialNr = strSerialNr;
            EventID = intEventID;
            RequestDate = strRequestDate;
        }
        public void RequestHardware()
        {

            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            mysqlConnectie.Open();
            MySqlCommand sendRequest = new MySqlCommand("INSERT INTO request (serialNr, internalNr,eventID, requestDate) Values (@serialNr, @internalNr, @eventID, @requestDate)", mysqlConnectie);
            sendRequest.Parameters.AddWithValue("@serialNr", SerialNr);
            sendRequest.Parameters.AddWithValue("@internalNr", InternalNr);
            sendRequest.Parameters.AddWithValue("@eventID", EventID);
            sendRequest.Parameters.AddWithValue("@requestDate", RequestDate);
            sendRequest.ExecuteNonQuery();
            sendRequest.Dispose();
            mysqlConnectie.Close();
        }
        public void AcceptRequest()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                mysqlConnectie.Open();
                MySqlCommand setRequestAccepted = new MySqlCommand("UPDATE request SET requestAccepted = true WHERE requestID = " + RequestID, mysqlConnectie);
                setRequestAccepted.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                mysqlConnectie.Close();
            }
        }
        public void DenyRequest()
        {
            MySqlConnection mysqlConnectie = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            mysqlConnectie.Open();
            MySqlCommand deleteSelectedRequest = new MySqlCommand("DELETE FROM request WHERE requestID = " + RequestID, mysqlConnectie);
            deleteSelectedRequest.ExecuteNonQuery();
            mysqlConnectie.Close();

        }
    }
}