using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class RoomsRepository
    {
    //    private RoomsRepository()
    //    {

    //    }

    //    static RoomsRepository instance;
    //    public static RoomsRepository Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //                instance = new RoomsRepository();
    //            return instance;
    //        }
    //    }

    //    internal IEnumerable<Room> GetAllRooms(string sql)
    //    {
    //        var result = new List<Room>();
    //        var connect = MySqlDB.Instance.GetConnection();
    //        if (connect == null)
    //            return result;
    //        using (var mc = new MySqlCommand(sql, connect))
    //        using (var reader = mc.ExecuteReader())
    //        {
    //            Room room = new Room();
    //            int id;
    //            while (reader.Read())
    //            {
    //                id = reader.GetInt32("id");
    //                if (room.ID != id)
    //                {
    //                    room = new Room();
    //                    result.Add(room);
    //                    room.Id = id;
    //                    room.Number = reader.GetInt32("Number");
    //                    room.Price = reader.GetDouble("Price");
    //                    room.Type = reader.GetString("Type");
    //                    room.Status = reader.GetString("Status");
    //                }
    //            }
    //        }

    //        return result;
    //    }

    //    internal void AddRoom(Room room)
    //    {
    //        var connect = MySqlDB.Instance.GetConnection();
    //        if (connect == null)
    //            return;

    //        int id = MySqlDB.Instance.GetAutoID("Room");

    //        string sql = "INSERT INTO Room VALUES (0, @number, @price, @type, @status)";
    //        using (var mc = new MySqlCommand(sql, connect))
    //        {
    //            mc.Parameters.Add(new MySqlParameter("number", room.Number));
    //            mc.Parameters.Add(new MySqlParameter("price", room.Price));
    //            mc.Parameters.Add(new MySqlParameter("type", room.Type));
    //            mc.Parameters.Add(new MySqlParameter("status", room.Status));
                
    //        }
    //    }

    //    internal void Remove(Room room)
    //    {
    //        var connect = MySqlDB.Instance.GetConnection();
    //        if (connect == null)
    //            return;

    //        sql += "DELETE FROM Room WHERE id = '" + room.Id + "';";

    //        using (var mc = new MySqlCommand(sql, connect))
    //            mc.ExecuteNonQuery();
    //    }


    //    internal void UpdateRoom(Room room)
    //    {
    //        var connect = MySqlDB.Instance.GetConnection();
    //        if (connect == null)
    //            return;                     

    //        sql = "UPDATE Room SET Number = @number, Price = @price, Type = @type, Status = @status WHERE Id = " + room.Id;
    //        using (var mc = new MySqlCommand(sql, connect))
    //        {
    //            mc.Parameters.Add(new MySqlParameter("number", room.Number));
    //            mc.Parameters.Add(new MySqlParameter("price", room.Price));
    //            mc.Parameters.Add(new MySqlParameter("type", room.Type));
    //            mc.Parameters.Add(new MySqlParameter("status", room.Status));
    //            mc.ExecuteNonQuery();
    //        }
    //    }

    }
}
