﻿using MySqlConnector;
using System;
using System.Collections.Generic;
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

        internal IEnumerable<Staff> GetAllStaff(string sql)
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
                        result.Add(staff);
                        staff.ID = id;
                        staff.Name = reader.GetString("Name");
                        staff.Surname = reader.GetString("Surname");
                        staff.Lastname = reader.GetString("Lastname");
                        staff.JobTitle = reader.GetString("JobTitle");
                        staff.Phone = reader.GetString("Phone");
                        staff.Mail = reader.GetString("Mail");
                    }
                    Days days = new Days
                    {
                        ID = reader.GetInt32("IID"),
                        Day = reader.GetString("DayD"),
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

            string sql = "INSERT INTO Staff VALUES (0, @name, @surname, @lastname, @jobtitle, @phone, @mail)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("name", staff.Name));
                mc.Parameters.Add(new MySqlParameter("surname", staff.Surname));
                mc.Parameters.Add(new MySqlParameter("lastname", staff.Lastname));
                mc.Parameters.Add(new MySqlParameter("jobtitle", staff.JobTitle));
                mc.Parameters.Add(new MySqlParameter("phone", staff.Phone));
                mc.Parameters.Add(new MySqlParameter("mail", staff.Mail));
                if (mc.ExecuteNonQuery() > 0)
                {
                    sql = " ";
                    foreach (var days in staff.Days)
                        sql += "INSERT INTO CrossDaysStaff VALUES (" + id + "," + days.ID + ");";
                    using (var mcCross = new MySqlCommand(sql, connect))
                        mcCross.ExecuteNonQueryAsync();

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
                mcCross.ExecuteNonQueryAsync();


            sql = "UPDATE Staff SET Name = @name, Surname = @surname, Lastname = @lastname, JobTitle = @jobtitle, Phone = @phone, Mail = @mail WHERE ID = " + staff.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("name", staff.Name));
                mc.Parameters.Add(new MySqlParameter("surname", staff.Surname));
                mc.Parameters.Add(new MySqlParameter("lastname", staff.Lastname));
                mc.Parameters.Add(new MySqlParameter("jobtitle", staff.JobTitle));
                mc.Parameters.Add(new MySqlParameter("phone", staff.Phone));
                mc.Parameters.Add(new MySqlParameter("mail", staff.Mail));
                mc.ExecuteNonQuery();
            }
        }

    }
}
