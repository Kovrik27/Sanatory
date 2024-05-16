﻿using MySqlConnector;
using Sanatory.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class GuestsRepository
    {
        private GuestsRepository()
        {

        }

        static GuestsRepository instance;
        public static GuestsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new GuestsRepository();
                return instance;
            }
        }

        internal IEnumerable<Guest> GetAllGuests(string sql)
        {
            var result = new List<Guest>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Guest guests;
                while (reader.Read())
                {
                    guests = new Guest();
                    guests.ID = reader.GetInt32("ID");
                    guests.Lastname = reader.GetString("Lastname");
                    guests.Name = reader.GetString("Name");
                    guests.Surname = reader.GetString("Surname");
                    //guests.Pasport = reader.GetString("Pasport");
                    //guests.Policy = reader.GetString("Policy");
                    guests.DataArrival = reader.GetString("DataArrival");
                    guests.DataOfDeparture = reader.GetString("DataOfDeparture");
                    guests.Room = new Room()
                    {
                        Number = reader.GetInt32("Number")
                    };
                    result.Add(guests);

                    Procedure procedure = new Procedure
                    {
                        ID = reader.GetInt32("prcID"),
                        Title = reader.GetString("prcPrc"),
                    };
                    guests.Procedures.Add(procedure);
                }
            }

            return result;
        }

        internal void AddGuest(Guest guests)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Guests");

            string sql = "INSERT INTO Guests VALUES (0, @surname, @name, @lastname, @pasport, @policy, @dataarrival, @dataofdeparture, @roomid)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("surname", guests.Surname));
                mc.Parameters.Add(new MySqlParameter("name", guests.Name));
                mc.Parameters.Add(new MySqlParameter("lastname", guests.Lastname));
                mc.Parameters.Add(new MySqlParameter("pasport", guests.Pasport));
                mc.Parameters.Add(new MySqlParameter("policy", guests.Policy));
                mc.Parameters.Add(new MySqlParameter("dataarrival", guests.DataArrival));
                mc.Parameters.Add(new MySqlParameter("dataofdeparture", guests.DataOfDeparture));
                mc.Parameters.Add(new MySqlParameter("roomid", guests.RoomID));
                if (mc.ExecuteNonQuery() > 0)
                {
                    sql = "";
                    foreach (var procedure in guests.Procedures)
                        sql += "INSERT INTO CrossRegistration VALUES (" + procedure.ID + "," + id + ");";
                    using (var mcCross = new MySqlCommand(sql, connect))
                    mcCross.ExecuteNonQuery();

                }
            }


        }

        internal void Remove(Guest guests)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Guests SET RoomID = NULL WHERE ID = " + guests.ID;
            sql += "DELETE FROM CrossRegistration WHERE IDGuests  = '" + guests.ID + "';";
            sql += "DELETE FROM Guests WHERE ID = '" + guests.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }



        internal void UpdateGuests(Guest guests)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;


            string sql = "DELETE FROM CrossRegistration WHERE IDGuests  = '" + guests.ID + "';";
            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();


            sql = " ";
            foreach (var procedures in guests.Procedures)
                sql += "INSERT INTO CrossRegistration VALUES (" + guests.ID + "," + procedures.ID + ");";
            using (var mcCross = new MySqlCommand(sql, connect))


            sql = "UPDATE Guests SET Lastname = @lastname, Name = @name, Surname = @surname, Pasport = @pasport, Phone = @phone, Policy = @policy, DataArrival = @dataarrival, DataOfDeparture = @dataofdeparture WHERE ID = " + guests.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("surname", guests.Surname));
                mc.Parameters.Add(new MySqlParameter("name", guests.Name));
                mc.Parameters.Add(new MySqlParameter("lastname", guests.Lastname));
                mc.Parameters.Add(new MySqlParameter("pasport", guests.Pasport));
                mc.Parameters.Add(new MySqlParameter("policy", guests.Policy));
                mc.Parameters.Add(new MySqlParameter("dataarrival", guests.DataArrival));
                mc.Parameters.Add(new MySqlParameter("dataofdeparture", guests.DataOfDeparture));
                mc.Parameters.Add(new MySqlParameter("roomid", guests.RoomID));
                mc.ExecuteNonQuery();
            }
        }

        internal void AddPrc(Guest guests)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;


            string sql = "DELETE FROM CrossRegistration WHERE IDGuests  = '" + guests.ID + "';";
            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();


            sql = " ";
            foreach (var procedures in guests.Procedures)
                sql += "INSERT INTO CrossRegistration VALUES (" + guests.ID + "," + procedures.ID + ");";
            using (var mcCross = new MySqlCommand(sql, connect))
                mcCross.ExecuteNonQuery();
        }

    }
}
