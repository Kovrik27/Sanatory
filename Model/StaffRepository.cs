using MySqlConnector;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class StaffRepository
    {
        private StaffRepository()
        {

        }

        static StaffRepository instance;
        public static StaffRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new StaffRepository();
                return instance;
            }
        }

        internal IEnumerable<Staff> GetTechStaff(string sql)
        {
            var result = new List<Staff>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Staff staff = new Staff();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (staff.ID != id)

                    {
                        staff = new Staff();
                        staff.ID = id;
                        staff.Lastname = reader.GetString("Lastname");
                        staff.Name = reader.GetString("Name");
                        staff.Surname = reader.GetString("Surname");                     
                        staff.JobTitle = reader.GetString("JobTitle");
                        staff.Phone = reader.GetString("Phone");
                        staff.Mail = reader.GetString("Mail");
                        if (!reader.IsDBNull("Description"))
                        {
                            staff.Problem = new Problem()
                            {
                                Description = reader.GetString("Description")
                            };
                        }                     
                        result.Add(staff);
                    }
                    Days days = new Days
                    {
                        ID = reader.GetInt32("daysID"),
                        Day = reader.GetString("daysDay"),
                    };
                    staff.Days.Add(days);

                   

                }
            }

            return result;
        }

        internal IEnumerable<Staff> GetMedStaff(string sql)
        {
            var result = new List<Staff>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Staff staff = new Staff();
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("ID");
                    if (staff.ID != id)

                    {
                        staff = new Staff();
                        staff.ID = id;
                        staff.Lastname = reader.GetString("Lastname");
                        staff.Name = reader.GetString("Name");
                        staff.Surname = reader.GetString("Surname");
                        staff.JobTitle = reader.GetString("JobTitle");
                        staff.Phone = reader.GetString("Phone");
                        staff.Mail = reader.GetString("Mail");
                        if (!reader.IsDBNull("Number"))
                        {
                            staff.Cabinet = new Cabinet()
                            {
                                Number = reader.GetInt32("Number")
                            };
                        }
                        result.Add(staff);
                    }
                    Days days = new Days
                    {
                        ID = reader.GetInt32("daysID"),
                        Day = reader.GetString("daysDay"),
                    };
                    staff.Days.Add(days);

                    

                }
            }

            return result;
        }

        internal void AddStaff(Staff staff)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Staff");

            string sql = "INSERT INTO Staff VALUES (0, @lastname, @name, @surname, @jobtitle, @phone, @mail, @cabinetid, @problemid)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", staff.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", staff.Name));
                mc.Parameters.Add(new MySqlParameter("surname", staff.Surname));              
                mc.Parameters.Add(new MySqlParameter("jobtitle", staff.JobTitle));
                mc.Parameters.Add(new MySqlParameter("phone", staff.Phone));
                mc.Parameters.Add(new MySqlParameter("mail", staff.Mail));
                mc.Parameters.Add(new MySqlParameter("cabinetid", null));
                mc.Parameters.Add(new MySqlParameter("problemid", null));
                if (mc.ExecuteNonQuery() > 0)
                {
                    sql = "";
                    foreach (var days in staff.Days)
                        sql += "INSERT INTO CrossDaysStaff VALUES ("+ days.ID +","+id+");";
                    using (var mcCross = new MySqlCommand(sql, connect))
                        mcCross.ExecuteNonQuery();

                }
            }

        }

        internal void Remove(Staff staff)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM CrossDaysStaff WHERE StaffID  = '" + staff.ID + "';";
            sql += "DELETE FROM Staff WHERE ID = '" + staff.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }


        internal void UpdateStaff(Staff staff)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;


            string sql = "DELETE FROM CrossDaysStaff WHERE StaffID  = '" + staff.ID + "';";
            using (var mc = new MySqlCommand(sql,connect))
                mc.ExecuteNonQuery();


            sql = " ";
            foreach (var days in staff.Days)
                sql += "INSERT INTO CrossDaysStaff VALUES (" + staff.ID + "," + days.ID + ");";
            using (var mcCross = new MySqlCommand(sql, connect))
                mcCross.ExecuteNonQuery();


            sql = "UPDATE Staff SET Lastname = @lastname, Name = @name, Surname = @surname, JobTitle = @jobtitle, Phone = @phone, Mail = @mail WHERE ID = " + staff.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", staff.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", staff.Name));
                mc.Parameters.Add(new MySqlParameter("surname", staff.Surname));
                mc.Parameters.Add(new MySqlParameter("jobtitle", staff.JobTitle));
                mc.Parameters.Add(new MySqlParameter("phone", staff.Phone));
                mc.Parameters.Add(new MySqlParameter("mail", staff.Mail));
                mc.ExecuteNonQuery();
            }
        }

        internal void AddProblem(Staff staff, Problem s)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;
            string sql = "UPDATE Staff SET ProblemID = @problemID WHERE ID = " + staff.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID", staff.ID));
                mc.Parameters.Add(new MySqlParameter("problemID", s.ID));
                mc.ExecuteNonQuery();
            }
        }

        internal void AddCabinet(Staff staff, Cabinet s)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;
            string sql = "UPDATE Staff SET CabinetID = @cabinetID WHERE ID = " + staff.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID", staff.ID));
                mc.Parameters.Add(new MySqlParameter("cabinetID", s.ID));
                mc.ExecuteNonQuery();
            }
        }

        internal void DoneP(Staff staff)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;
            string sql = "UPDATE Staff SET ProblemID = NULL WHERE ID = " + staff.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID", staff.ID));
                mc.Parameters.Add(new MySqlParameter("problemid", staff.ProblemID));
                mc.ExecuteNonQuery();
            }
        }

        internal void DoneC(Staff staff)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;
            string sql = "UPDATE Staff SET CabinetID = NULL WHERE ID = " + staff.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("ID", staff.ID));
                mc.Parameters.Add(new MySqlParameter("cabinetid", staff.ProblemID));
                mc.ExecuteNonQuery();
            }
        }

    }
}

