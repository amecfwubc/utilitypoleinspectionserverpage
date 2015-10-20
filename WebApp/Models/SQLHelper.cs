using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class SQLHelper
    {


        public static object ExecuteProcedureAsScaler(string SpName, Hashtable SPParameterHashTable, SqlConnection connection, SqlTransaction transaction)
        {

            object ReturnScaler = null;
            using (var command = new SqlCommand(SpName, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry val in SPParameterHashTable)
                {
                    command.Parameters.AddWithValue((string)val.Key, val.Value);
                }

                var rval = command.ExecuteScalar();

                //ReturnScaler = command.ExecuteScalar();
            }
            return ReturnScaler;
        }

        public static DataSet ExecuteProcedureAsFromDataAdapter(string SpName, Hashtable SPParameterHashTable, SqlConnection connection, SqlTransaction transaction)
        {

            DataSet dset = new DataSet();
            using (var command = new SqlCommand(SpName, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry val in SPParameterHashTable)
                {
                    command.Parameters.AddWithValue((string)val.Key, val.Value);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dset);
                }

            }
            return dset;
        }

        public static DataTable GetExcelData(string _FilePath, string ExcelSheetName)
        {
            DataTable tbl = new DataTable();

            //string excelConnString = @"provider=Microsoft.ACE.OLEDB.12.0;data source=" + _FilePath + ";extended properties=" + "\"excel 12.0;hdr=yes;IMEX=1\"";
            string excelConnString = @"provider=Microsoft.ACE.OLEDB.12.0;data source=" + _FilePath + ";extended properties=" + "\"excel 12.0;hdr=yes;IMEX=1\"";



            //Create Connection to Excel work book 
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
            {
                ///----------------------------
                string sql = "Select * " + " from [" + ExcelSheetName + "$]";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, excelConnection);
                da.Fill(tbl);

                ///Remove Temp File
                System.IO.File.Delete(_FilePath);
            }


            return tbl;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PoleConnection"].ConnectionString;
        }

        public static string GetEntityConnectionString()
        {
            //string EntitieConstring = ConfigurationManager.ConnectionStrings["PoleInfoEntities"].ConnectionString;
            //string[] conspval = EntitieConstring.Split(new string[] { @"provider connection string=" }, StringSplitOptions.None);
            //conspval = conspval[1].Split(new string[] { "MultipleActiveResultSets=True;" }, StringSplitOptions.None);
            //string ccc=conspval[0].Substring(1);
            return ConfigurationManager.ConnectionStrings["PoleInfoEntities"].ConnectionString;
        }


    }
}