using Sanatory.DTO;
using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.Api
{
    public class DB
    {
        private static DB instance;

        public static DB GetInstance()
        {
            if(instance == null)
            {
                instance = new DB();
            }
            return instance;
        }

        HttpClient client = new HttpClient();
        
        public DB()
        {
            client.BaseAddress = new Uri("http://localhost:5179");
        }

        public async Task<ObservableCollection<User>> GetAllUsers()
        {
            var responce = await client.GetAsync($"Users/GetAllUsers");
            if(responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var users = await responce.Content.ReadFromJsonAsync<ObservableCollection<User>>();
                return users;
            }
        }

        public async Task AddNewUser(User user)
        {
            var arg = JsonSerializer.Serialize(user);
            var responce = await client.PostAsync($"Users/AddNewUser", new StringContent(arg, Encoding.UTF8, "application/json"));
            if(responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteUser(int id)
        {
            var responce = await client.DeleteAsync($"Users/DeleteUser");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task<User> CheckUser(User user)
        {
            var arg = JsonSerializer.Serialize(user);
            var responce = await client.PostAsync($"User/CheckUser", new StringContent(arg, Encoding.UTF8, "application/json"));
            if(responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var result = await responce.Content.ReadFromJsonAsync<User>();
                return result;
            }
        }
        ////////////////////////////

        public async Task<ObservableCollection<Cabinet>> GetAllCabinets()
        {
            var responce = await client.GetAsync($"Cabinets/GetAllCabinets");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var cabinets = await responce.Content.ReadFromJsonAsync<ObservableCollection<Cabinet>>();
                return cabinets;
            }
        }

        public async Task AddNewCabinet(Cabinet cabinet)
        {
            var arg = JsonSerializer.Serialize(cabinet);
            var responce = await client.PostAsync($"Cabinets/AddNewCabinet", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditCabinet(Cabinet cabinet)
        {
            var arg = JsonSerializer.Serialize(cabinet);
            var responce = await client.PutAsync($"Cabinets/EditCabinet", new StringContent(arg, Encoding.UTF8, "application/json"));
            if(responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteCabinet(int id)
        {
            var responce = await client.DeleteAsync($"Cabinet/DeleteCabinet");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }
        ///////////////////////////

        public async Task<ObservableCollection<Daytime>> GetAllDaytime()
        {
            var responce = await client.GetAsync($"Daytims/GetAllDaytime");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var daytime = await responce.Content.ReadFromJsonAsync<ObservableCollection<Daytime>>();
                return daytime;
            }
        }

        public async Task AddNewDaytime(Daytime daytime)
        {
            var arg = JsonSerializer.Serialize(daytime);
            var responce = await client.PostAsync($"Daytims/AddNewDaytime", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditDaytime(Daytime daytime)
        {
            var arg = JsonSerializer.Serialize(daytime);
            var responce = await client.PutAsync($"Daytims/EditDaytime", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteDaytime(int id)
        {
            var responce = await client.DeleteAsync($"Daytims/DeleteDaytime");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task AddNewEventOnDay(Daytime daytime, List<Events> eventt)
        {
            var eventOnDayDTO = new EventOnDayDTO
            {
                Events = eventt,
                Day = daytime.Time
            };

            var arg = JsonSerializer.Serialize(eventOnDayDTO);
            var responce = await client.PostAsync($"Events/AddNewEventOnDay", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        ///////////////////////////
     

        //public async Task EditEvent(Events eventt)
        //{
        //    var arg = JsonSerializer.Serialize(eventt);
        //    var responce = await client.PutAsync($"Events/EditEvent", new StringContent(arg, Encoding.UTF8, "application/json"));
        //    if (responce.StatusCode != System.Net.HttpStatusCode.OK)
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //    }
        //    else
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //    }
        //}

        //public async Task DeleteEvent(int id)
        //{
        //    var responce = await client.DeleteAsync($"Events/DeleteEvent");
        //    if (responce.StatusCode != System.Net.HttpStatusCode.OK)
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //    }
        //    else
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //    }
        //}

        //////////////////////////////

        public async Task<ObservableCollection<Guest>> GetAllGuests()
        {
            var responce = await client.GetAsync($"Guests/GetAllGuests");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var guests = await responce.Content.ReadFromJsonAsync<ObservableCollection<Guest>>();
                return guests;
            }
        }

        public async Task AddNewGuest(Guest guest)
        {
            var arg = JsonSerializer.Serialize(guest);
            var responce = await client.PostAsync($"Guests/AddNewGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditGuest(Guest guest)
        {
            var arg = JsonSerializer.Serialize(guest);
            var responce = await client.PutAsync($"Guests/EditGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteGuest(int id)
        {
            var responce = await client.DeleteAsync($"Guests/GoOutGuest");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        ///////////////////////

        public async Task<ObservableCollection<Problem>> GetAllProblems()
        {
            var responce = await client.GetAsync($"Problems/GetAllProblems");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var problems = await responce.Content.ReadFromJsonAsync<ObservableCollection<Problem>>();
                return problems;
            }
        }

        public async Task AddNewProblem(Problem problem)
        {
            var arg = JsonSerializer.Serialize(problem);
            var responce = await client.PostAsync($"Problems/AddNewProblem", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditProblem(Problem problem)
        {
            var arg = JsonSerializer.Serialize(problem);
            var responce = await client.PutAsync($"Problems/EditProblem", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteProblem(int id)
        {
            var responce = await client.DeleteAsync($"Problems/DeleteProblem");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DoneProblem(int id)
        {
            var responce = await client.DeleteAsync($"Staffs/DoneProblem");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }


        ///////////////////////

        public async Task<ObservableCollection<Procedure>> GetAllProcedure()
        {
            var responce = await client.GetAsync($"Procedures/GetAllProcedures");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var procedures = await responce.Content.ReadFromJsonAsync<ObservableCollection<Procedure>>();
                return procedures;
            }
        }

        public async Task AddNewProcedure(Procedure procedure)
        {
            var arg = JsonSerializer.Serialize(procedure);
            var responce = await client.PostAsync($"Procedures/AddNewProcedure", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditProcedure(Procedure procedure)
        {
            var arg = JsonSerializer.Serialize(procedure);
            var responce = await client.PutAsync($"Procedures/EditProcedure", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteProcedure(int id)
        {
            var responce = await client.DeleteAsync($"Procedures/DeleteProcedure");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

       
        ///////////////////////

        public async Task<ObservableCollection<Room>> GetAllRooms()
        {
            var responce = await client.GetAsync($"Rooms/GetAllRooms");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var rooms = await responce.Content.ReadFromJsonAsync<ObservableCollection<Room>>();
                return rooms;
            }
        }

        public async Task AddNewRoom(Room room)
        {
            var arg = JsonSerializer.Serialize(room);
            var responce = await client.PostAsync($"Rooms/AddNewRoom", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditRoom(Room room)
        {
            var arg = JsonSerializer.Serialize(room);
            var responce = await client.PutAsync($"Rooms/EditRoom", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteRoom(int id)
        {
            var responce = await client.DeleteAsync($"Rooms/DeleteRoom");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }
        ////////////////////////////////

        public async Task<ObservableCollection<Staff>> GetAllStaff()
        {
            var responce = await client.GetAsync($"Staff/GetAllStaff");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var staff = await responce.Content.ReadFromJsonAsync<ObservableCollection<Staff>>();
                return staff;
            }
        }

        public async Task AddNewStaff(Staff staff)
        {
            var arg = JsonSerializer.Serialize(staff);
            var responce = await client.PostAsync($"Staffs/AddNewStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditStaff(Staff staff)
        {
            var arg = JsonSerializer.Serialize(staff);
            var responce = await client.PutAsync($"Staff/EditStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteStaff(int id)
        {
            var responce = await client.DeleteAsync($"Staffs/GoOutStaff");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task AddCabinetOnStaff(Staff staff, Cabinet cabinet)
        {
            var cabinetOnStaffDTO = new CabinetOnStaffDTO
            {
                StaffId = staff.ID,
                CabinetId = cabinet.ID
            };
           
            var arg = JsonSerializer.Serialize(cabinetOnStaffDTO);
            var responce = await client.PostAsync($"Staffs/AddCabinetOnStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task AddProblemOnStaff(Staff staff, Problem problem)
        {
            var problemOnStaffDTO = new ProblemOnStaffDTO
            {
                StaffId = staff.ID,
                ProblemId = problem.ID
            };

            var arg = JsonSerializer.Serialize(problemOnStaffDTO);
            var responce = await client.PostAsync($"Staffs/AddProblemOnStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        ///////////////////////////////

        public async Task<ObservableCollection<Days>> GetAllDays()
        {
            var responce = await client.GetAsync($"Days/GetAllDays");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return null;
            }
            else
            {
                var days = await responce.Content.ReadFromJsonAsync<ObservableCollection<Days>>();
                return days;
            }
        }
    }
    /////////////////////TestingCheatsEnabled true
    /////bb.moveobjects 
    /////cas.fulleditmode
    


    

    
}
