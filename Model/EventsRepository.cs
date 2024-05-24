using MySqlConnector;
using Sanatory.View;
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
                Events events;
                int id;
                while (reader.Read())
                {
                        events = new Events();
                        events.ID = reader.GetInt32("ID");
                        events.Title = reader.GetString("Title");
                        events.Times = reader.GetInt32("Times");
                        events.Place = reader.GetString("Place");
                        result.Add(events);
                }
            }

            return result;
        }

        internal void AddEvent(Events events)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Events");

            string sql = "INSERT INTO Events VALUES (0, @title, @times, @place)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", events.Title));
                mc.Parameters.Add(new MySqlParameter("times", events.Times));
                mc.Parameters.Add(new MySqlParameter("place", events.Place));
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

            string sql = "UPDATE Events SET Title = @title, Times = @times, Place = @place" + events.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", events.Title));
                mc.Parameters.Add(new MySqlParameter("times", events.Times));
                mc.Parameters.Add(new MySqlParameter("place", events.Place));
                mc.ExecuteNonQuery();
            }
        }

    }
}

