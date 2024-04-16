using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class CabinetsRepository
    {
        private CabinetsRepository()
        {

        }

        static CabinetsRepository instance;
        public static CabinetsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CabinetsRepository();
                return instance;
            }
        }

        internal IEnumerable<Cabinets> GetAllCabinets(string sql)
        {
            var result = new List<Cabinets>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Cabinets cabinets = new Cabinets();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (cabinets.ID != id)
                    {
                        cabinets = new Cabinets();
                        result.Add(cabinets);
                        cabinets.ID = id;
                        cabinets.Number = reader.GetInt32("Number");
                        cabinets.Type = reader.GetString("Type");

                    }
                }
            }

            return result;
        }

        internal void AddCabinets(Cabinets cabinets)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Cabinet");

            string sql = "INSERT INTO Cabinet VALUES (0, @number, @type)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("number", cabinets.Number));
                mc.Parameters.Add(new MySqlParameter("type", cabinets.Type));

                mc.ExecuteNonQuery();
            }

        }

        internal void Remove(Cabinets cabinets)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM Cabinet WHERE id = '" + cabinets.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }


        internal void UpdateCabinets(Cabinets cabinets)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Cabinet SET Number = @number, Type = @type WHERE ID = " + cabinets.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("number", cabinets.Number));
                mc.Parameters.Add(new MySqlParameter("type", cabinets.Type));

                mc.ExecuteNonQuery();
            }
        }

    }
}
