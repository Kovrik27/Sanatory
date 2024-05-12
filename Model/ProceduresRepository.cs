using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class ProceduresRepository
    {
        private ProceduresRepository()
        {

        }

        static ProceduresRepository instance;
        public static ProceduresRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProceduresRepository();
                return instance;
            }
        }

        internal IEnumerable<Procedure> GetAllProcedures(string sql)
        {
            var result = new List<Procedure>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Procedure procedures = new Procedure();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (procedures.ID != id)
                    {
                        procedures = new Procedure();
                        result.Add(procedures);
                        procedures.ID = id;
                        procedures.Title = reader.GetString("Title");
                        procedures.Description = reader.GetString("Description");
                        procedures.Duration = reader.GetInt32("Duration");
                        procedures.Price = reader.GetDouble("Price");
                    }
                }
            }

            return result;
        }

        internal void AddProcedures(Procedure procedures)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Procedures");

            string sql = "INSERT INTO Procedures VALUES (0, @title, @description, @duration, @price)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", procedures.Title));
                mc.Parameters.Add(new MySqlParameter("description", procedures.Description));
                mc.Parameters.Add(new MySqlParameter("duration", procedures.Duration));
                mc.Parameters.Add(new MySqlParameter("price", procedures.Price));
                mc.ExecuteNonQuery();
            }

        }

        internal void Remove(Procedure procedures)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM Procedures WHERE id = '" + procedures.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }


        internal void UpdateProcedures(Procedure procedures)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Procedures SET Title = @title, Description = @description, Duration = @duration, Price = @price WHERE ID = " + procedures.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", procedures.Title));
                mc.Parameters.Add(new MySqlParameter("description", procedures.Description));
                mc.Parameters.Add(new MySqlParameter("duration", procedures.Duration));
                mc.Parameters.Add(new MySqlParameter("price", procedures.Price));
                mc.ExecuteNonQuery();
            }
        }

        internal void AddPrc(Guest guests, Procedure procedures)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;
            string sql = "UPDATE Guests SET ProcedureID = @procedureid WHERE ID = " + guests.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID", guests.ID));
                mc.Parameters.Add(new MySqlParameter("proceduresID", procedures.ID));
                mc.ExecuteNonQuery();
            }
        }

    }
}
