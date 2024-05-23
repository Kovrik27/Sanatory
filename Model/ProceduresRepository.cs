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

        internal IEnumerable<Procedure> GetAllProcedure(string sql)
        {
            var result = new List<Procedure>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Procedure procedure = new Procedure();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (procedure.ID != id)

                    {
                        procedure = new Procedure();
                        result.Add(procedure);
                        procedure.ID = id;
                        procedure.Title = reader.GetString("Title");
                        procedure.Description = reader.GetString("Description");
                        procedure.Duration = reader.GetInt32("Duration");
                        procedure.Price = reader.GetDouble("Price");
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

        

    }
}
