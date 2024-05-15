using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class ProblemRepository
    {
        private ProblemRepository()
        {

        }

        static ProblemRepository instance;
        public static ProblemRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProblemRepository();
                return instance;
            }
        }

        internal IEnumerable<Problem> GetAllProblem(string sql)
        {
            var result = new List<Problem>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Problem problem = new Problem();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (problem.ID != id)

                    {
                        problem = new Problem();
                        result.Add(problem);
                        problem.ID = id;
                        problem.Description = reader.GetString("Description");
                        problem.StaffID = reader.GetInt32("StaffID");
                        //problem.Place = reader.GetString("Place");
                    }
                }
            }

            return result;
        }

        internal void AddProblem(Problem problem)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Problem");

            string sql = "INSERT INTO Problem VALUES (0, @description, @place, @staffid)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("description", problem.Description));
                mc.Parameters.Add(new MySqlParameter("place", problem.Place));
                mc.Parameters.Add(new MySqlParameter("staffid", problem.Place));
                mc.ExecuteNonQuery();
              
            }

        }

        internal void Remove(Problem problem)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM Problem WHERE ID = '" + problem.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }


        internal void UpdateProblem(Problem problem)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;


            string sql = "DELETE FROM Problem WHERE ID  = '" + problem.ID + "';";
            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();


            sql = "UPDATE Problem SET Description = @description, Place = @place, WHERE ID = " + problem.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("description", problem.Description));
                mc.Parameters.Add(new MySqlParameter("place", problem.Place));
                mc.ExecuteNonQuery();
            }
        }

        //internal void RemoveFromStaff(Problem problem, Staff staff)
        //{
        //    var connect = DB.Instance.GetConnection();
        //    if (connect == null) 
        //        return;

        //    string sql = "UPDATE Staff SET ProblemID = NULL WHERE StaffID ='" + staff.ID + "';";
        //    using (var mc = new MySqlCommand(sql, connect))
        //        mc.ExecuteNonQuery();
        //}

    }
}
