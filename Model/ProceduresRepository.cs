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

        internal List<Procedure> GetAllProcedures()
        {
            List<Procedure> result = new List<Procedure>();
            var connect = DB.Instance.GetConnection();
            string sql = "SELECT * FROM Procedures";
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var procedures = new Procedure
                    {
                        ID = reader.GetInt32("ID"),
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        Duration = reader.GetInt32("Duration"),
                        Price = reader.GetDouble("Price")
                    };
                    result.Add(procedures);
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
