using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class RoomsRepository
    {
        private RoomsRepository()
        {

        }

        static RoomsRepository instance;
        public static RoomsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new RoomsRepository();
                return instance;
            }
        }

        internal IEnumerable<Room> GetAllRooms(string sql)
        {
            var result = new List<Room>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Room room = new Room();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (room.ID != id)
                    {
                        room = new Room();
                        result.Add(room);
                        room.ID = id;
                        room.Number = reader.GetInt32("Number");
                        room.Type = reader.GetString("Type");
                        room.Price = reader.GetDouble("Price");
                        room.Status = reader.GetString("Status");
                    }
                }
            }

            return result;
        }

        internal void AddRoom(Room room)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Rooms");

            string sql = "INSERT INTO Rooms VALUES (0, @number, @type, @price, @status)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("number", room.Number));
                mc.Parameters.Add(new MySqlParameter("type", room.Type));
                mc.Parameters.Add(new MySqlParameter("price", room.Price));
                mc.Parameters.Add(new MySqlParameter("status", room.Status));
                mc.ExecuteNonQuery();
            }
           
        }

        internal void Remove(Room room)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM Rooms WHERE id = '" + room.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }


        internal void UpdateRoom(Room room)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Rooms SET Number = @number, Type = @type, Price = @price, Status = @status WHERE ID = " + room.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("number", room.Number));
                mc.Parameters.Add(new MySqlParameter("type", room.Type));
                mc.Parameters.Add(new MySqlParameter("price", room.Price));
                mc.Parameters.Add(new MySqlParameter("status", room.Status));
                mc.ExecuteNonQuery();
            }
        }

    }
}
