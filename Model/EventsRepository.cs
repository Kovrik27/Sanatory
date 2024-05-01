using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class EventsRepository
    {
        private EventsRepository()
        {

        }

        static EventsRepository instance;
        public static EventsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new EventsRepository();
                return instance;
            }
        }

        internal IEnumerable<Events> GetAllEvents(string sql)
        {
            var result = new List<Events>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Events events = new Events();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (events.ID != id)
                    {
                        events = new Events();
                        result.Add(events);
                        events.ID = id;
                        events.Title = reader.GetString("Title");
                        events.Time = reader.GetTimeOnly("Time");
                        events.Place = reader.GetString("Place");
                    }
                }
            }

            return result;
        }

        internal void AddEvent(Events events)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Rooms");

            string sql = "INSERT INTO Events VALUES (0, @title, @time, @place)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("number", events.Title));
                mc.Parameters.Add(new MySqlParameter("type", events.Time));
                mc.Parameters.Add(new MySqlParameter("price", events.Place));
                mc.ExecuteNonQuery();
            }

        }

        internal void Remove(Events events)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM Events WHERE id = '" + events.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }


        internal void UpdateEvent(Events events)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Events SET Title = @title, Time = @time, Place = @place" + events.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", events.Title));
                mc.Parameters.Add(new MySqlParameter("time", events.Time));
                mc.Parameters.Add(new MySqlParameter("place", events.Place));
                mc.ExecuteNonQuery();
            }
        }

    }
}

