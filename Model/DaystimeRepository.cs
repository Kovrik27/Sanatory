﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class DaystimeRepository
    {
        private DaystimeRepository()
        {

        }

        static DaystimeRepository instance;
        public static DaystimeRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new DaystimeRepository();
                return instance;
            }
        }

        internal IEnumerable<Daytime> GetAllDaytime(string sql)
        {
            var result = new List<Daytime>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Daytime daytime = new Daytime();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (daytime.ID != id)

                    {
                        daytime = new Daytime();
                        result.Add(daytime);
                        daytime.ID = id;
                        daytime.DayTim = reader.GetDateOnly("Data");
                    }
                }
            }

            return result;
        }

        internal void AddDaytime(Daytime daytime)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Daytime");

            string sql = "INSERT INTO Daytime VALUES (0, @data)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("daytim", daytime.DayTim));
                mc.ExecuteNonQuery();

            }

        }
    }
}