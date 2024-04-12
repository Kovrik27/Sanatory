using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    internal class DaysRepository
    {
        private DaysRepository()
        {

        }

        static DaysRepository instance;
        public static DaysRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new DaysRepository();
                return instance;
            }
        }

        internal List<Days> GetDays()
        {
            List<Days> result = new List<Days>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;

            string sql = "SELECT * FROM Days";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var days = new Days
                    {
                        ID = reader.GetInt32("ID"),
                        Day = reader.GetString("Day")
                    };
                    result.Add(days);
                }
            }
            return result;
        }


    }
}
