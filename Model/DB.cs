using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.Model
{
    public class DB
    {


        //MySqlConnection mySqlConnection;

        //private DB()
        //{
        //    MySqlConnectionStringBuilder stringBuilder = new();
        //    stringBuilder.UserID = "student";
        //    stringBuilder.Password = "student";
        //    stringBuilder.Database = "sanatory";
        //    stringBuilder.Server = "192.168.200.13";
        //    stringBuilder.CharacterSet = "utf8mb4";
           
        //    mySqlConnection = new MySqlConnection(stringBuilder.ToString());
        //    OpenConnection();
        //}

        //private bool OpenConnection()
        //{
        //    try
        //    {
        //        mySqlConnection.Open();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //public void CloseConnection()
        //{
        //    try
        //    {
        //        mySqlConnection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //internal MySqlConnection GetConnection()
        //{
        //    if (mySqlConnection.State != System.Data.ConnectionState.Open)
        //        if (!OpenConnection())
        //            return null;

        //    return mySqlConnection;
        //}

        //static DB instance;
        //public static DB Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new DB();
        //        return instance;
        //    }
        //}

        //public int GetAutoID(string table)
        //{
        //    try
        //    {
        //        string sql = "SHOW TABLE STATUS WHERE `Name` = '" + table + "'";
        //        using (var mc = new MySqlCommand(sql, mySqlConnection))
        //        using (var reader = mc.ExecuteReader())
        //        {
        //            if (reader.Read())
        //                return reader.GetInt32("Auto_increment");
        //        }
        //        return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return -1;
        //    }
        //}
    }   
}
