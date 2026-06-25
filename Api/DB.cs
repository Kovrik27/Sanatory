using Microsoft.Extensions.Logging;
using Sanatory.Api;
using Sanatory.DTO;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.Api
{
    public class DB
    {
        private static DB instance;

        public static DB GetInstance()
        {
            if (instance == null)
            {
                instance = new DB();
            }
            return instance;
        }

        HttpClient client = new HttpClient();

        public DB()
        {
            client.BaseAddress = new Uri("http://localhost:5179/api/");
        }

        public async Task<ObservableCollection<User>> GetAllUsers()
        {
            var responce = await client.GetAsync($"Users/GetAllUsers");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в выводе списка!");
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
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task<User> CheckUser(User user)
        {
            var arg = JsonSerializer.Serialize(user);
            var responce = await client.PostAsync($"Users/CheckUser", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка! Пользователь не найден!");
                return null;
            }
            else
            {
                var result = await responce.Content.ReadFromJsonAsync<User>();
                return result;
            }
        }

        public async Task AddUserOnStaff(Staff staff, User user)
        {
            UserOnDTO useronDTO;
            int doctorId = 0;
            if (staff != null && staff.JobTitle.Title.Contains("Врач"))
            {
                doctorId = staff.ID;

                useronDTO = new UserOnDTO
                {
                    UserId = user.Id,
                    DoctorId = doctorId,
                };
            }
            else
            {
                useronDTO = new UserOnDTO
                {
                    UserId = user.Id,
                    StaffId = staff.ID,
                };
            }

            var arg = JsonSerializer.Serialize(useronDTO);
            var responce = await client.PostAsync($"Users/AddUserOnStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task AddUserOnGuest(Guest guets, User user)
        {
            var userOnDTO = new UserOnDTO
            {
                GuestId = guets.ID,
                UserId = user.Id,
            };

            var arg = JsonSerializer.Serialize(userOnDTO);
            var responce = await client.PostAsync($"Users/AddUserOnGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }




        ////////////////////////////

        public async Task<ObservableCollection<Cabinet>> GetAllCabinets()
        {
            var responce = await client.GetAsync($"Cabinets/GetAllCabinets");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
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
                MessageBox.Show("Ошибка данных!");
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
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteCabinet(int id)
        {
            var responce = await client.DeleteAsync($"Cabinets/DeleteCabinet/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DoneCabinet(int id)
        {
            var arg = JsonSerializer.Serialize(id);
            var responce = await client.PutAsync($"Staffs/DoneCabinet/{id}", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        ///////////////////////////

        public async Task<ObservableCollection<Daytime>> GetAllDaytime()
        {
            var responce = await client.GetAsync($"Eventss/GetAllDaysWithEvents");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
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
            var daytimeToSend = new DaytimeDTO
            {
                Id = daytime.Id,
                Time = daytime.Time
            };

            var arg = JsonSerializer.Serialize(daytimeToSend);
            var response = await client.PostAsync($"Daytims/AddNewDaytime",
                new StringContent(arg, Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Ошибка: {result}");
            }
        }

        public async Task EditDaytime(Daytime daytime)
        {
            var daytimeToSend = new DaytimeDTO
            {
                Id = daytime.Id,
                Time = daytime.Time
            };

            var arg = JsonSerializer.Serialize(daytimeToSend);

            var response = await client.PutAsync($"Daytims/EditDaytime",
                new StringContent(arg, Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Ошибка данных!\n{result}");
            }
        }

        public async Task DeleteDaytime(int id)
        {
            var responce = await client.DeleteAsync($"Daytims/DeleteDaytime");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task<ObservableCollection<Events>> GetAllEvents()
        {
            var responce = await client.GetAsync($"Eventss/GetAllEvents");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var events = await responce.Content.ReadFromJsonAsync<ObservableCollection<Events>>();
                return events;
            }
        }

        public async Task AddNewEventOnDay(Daytime daytime, Events eventt)
        {
            var eventOnDayDTO = new EventOnDayDTO
            {
                DaytimeId = daytime.Id,
                EventId = eventt.Id,
            };

            var arg = JsonSerializer.Serialize(eventOnDayDTO);
            var responce = await client.PostAsync($"Eventss/AddEventOnDaytime", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task<ObservableCollection<Events>> GetEventsByDate(DateTime? date)
        {
            var response = await client.GetAsync($"Daytims/GetEventByDate/{date.Value.ToString("yyyy-MM-dd")}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var eventByDate = await response.Content.ReadFromJsonAsync<ObservableCollection<Events>>();
                return eventByDate;
            }
        }

        ///////////////////////////


        //////////////////////////////

        public async Task<ObservableCollection<Guest>> GetAllGuests()
        {
            var responce = await client.GetAsync($"Guests/GetAllGuests");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var guests = await responce.Content.ReadFromJsonAsync<ObservableCollection<Guest>>();
                return guests;
            }
        }
        public async Task<Guest> GetGuestId(int id)
        {
            var responce = await client.GetAsync($"Guests/GetGuestId/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
                return null;
            }
            else
            {
                var guestid = await responce.Content.ReadFromJsonAsync<Guest>();
                return guestid;
            }
        }


        public async Task<bool> AddNewGuestWithProcedures(GuestWithProceduresDTO guestWithProcedures)
        {
                var arg = JsonSerializer.Serialize(guestWithProcedures);
                var responce = await client.PostAsync($"Guests/AddNewGuestWithProcedures", new StringContent(arg, Encoding.UTF8, "application/json"));
                if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка данных!");
                    return false;
            }
                else
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    return true; 
                }

        }

        //public async Task AddNewGuest(Guest guest)
        //{
        //    var arg = JsonSerializer.Serialize(guest);
        //    var responce = await client.PostAsync($"Guests/AddNewGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
        //    if (responce.StatusCode != System.Net.HttpStatusCode.OK)
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //        MessageBox.Show("Ошибка данных!");
        //    }
        //    else
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //    }
        //}

        public async Task EditGuest(Guest guest)
        {
            var arg = JsonSerializer.Serialize(guest);
            var responce = await client.PutAsync($"Guests/EditGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteGuest(int id)
        {
            var responce = await client.DeleteAsync($"Guests/GoOutGuest/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        //public async Task AddProcedureOnGuest(Guest guest, Procedure procedure)
        //{
        //    var procedureOnGuest = new ProcedureOnGuestDTO
        //    {
        //        GuestId = guest.ID,
        //        ProcedureId = procedure.Id
        //    };

        //    var arg = JsonSerializer.Serialize(procedureOnGuest);
        //    var responce = await client.PostAsync($"Guests/AddProcedureOnGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
        //    if (responce.StatusCode != System.Net.HttpStatusCode.OK)
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //        MessageBox.Show("Ошибка данных!");
        //    }
        //    else
        //    {
        //        var result = await responce.Content.ReadAsStringAsync();
        //    }
        //}

        internal async Task<ObservableCollection<Procedure>> GetProceduresByGuest(int id)
        {
            var responce = await client.GetAsync($"Guests/GetProceduresByGuest/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var procedures = await responce.Content.ReadFromJsonAsync<ObservableCollection<Procedure>>();
                return procedures;
            }
        }

        ///////////////////////

        public async Task<ObservableCollection<Problem>> GetAllProblems()
        {
            var responce = await client.GetAsync($"Problems/GetAllProblems");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
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
            var responce = await client.PostAsync("Problems/AddNewProblem"
                , new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteProblem(int id)
        {
            var responce = await client.DeleteAsync($"Problems/DeleteProblem/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DoneProblem(int id)
        {
            var arg = JsonSerializer.Serialize(id);
            var responce = await client.PutAsync($"Staffs/DoneProblem/{id}", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditStatusProblem(Problem problem)
        {
            var arg = JsonSerializer.Serialize(problem);
            var responce = await client.PutAsync($"Problems/EditStatusProblem", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка в получении списка!");
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
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteProcedure(int id)
        {
            var responce = await client.DeleteAsync($"Procedures/DeleteProcedure/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }


        ///////////////////////


        public async Task<ObservableCollection<Room>> GetRoomWithStatus()
        {
            var responce = await client.GetAsync("Rooms/GetRoomWithStatus");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var roomst = await responce.Content.ReadFromJsonAsync<ObservableCollection<Room>>();
                return roomst;
            }
        }

        public async Task AddNewRoom(Room room)
        {
            var arg = JsonSerializer.Serialize(room);
            var responce = await client.PostAsync($"Rooms/AddNewRoom", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteRoom(int id)
        {
            var responce = await client.DeleteAsync($"Rooms/DeleteRoom/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }
        ////////////////////////////////

        public async Task<ObservableCollection<Staff>> GetAllStaff()
        {
            var responce = await client.GetAsync($"Staffs/GetAllStaff");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var staff = await responce.Content.ReadFromJsonAsync<ObservableCollection<Staff>>();
                return staff;
            }
        }

        public async Task<ObservableCollection<Staff>> GetStaffWithProblem()
        {
            var responce = await client.GetAsync("Staffs/GetStaffWithProblem");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var staffpro = await responce.Content.ReadFromJsonAsync<ObservableCollection<Staff>>();
                return staffpro;
            }
        }

        public async Task<ObservableCollection<Staff>> GetStaffWithCabinet()
        {
            var responce = await client.GetAsync("Staffs/GetStaffWithCabinet");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var staffpro = await responce.Content.ReadFromJsonAsync<ObservableCollection<Staff>>();
                return staffpro;
            }
        }

        public async Task<Staff> GetStaffId(int id)
        {
            var responce = await client.GetAsync($"Staffs/GetStaffId/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var staffid = await responce.Content.ReadFromJsonAsync<Staff>();
                return staffid;
            }
        }

        public async Task AddNewStaff(Staff staff)
        {
            var arg = JsonSerializer.Serialize(staff);
            var responce = await client.PostAsync($"Staffs/AddNewStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditStaff(Staff staff)
        {
            var arg = JsonSerializer.Serialize(staff);
            var responce = await client.PutAsync($"Staffs/EditStaff", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteStaff(int id)
        {
            var responce = await client.DeleteAsync($"Staffs/GoOutStaff/{id}");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка данных!");
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
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task<Staff> GetStaffByUserId(int userId)
{
    try
    {
        Console.WriteLine($"=== GetStaffByUserId({userId}) ===");
        
        var response = await client.GetAsync($"Staffs/GetByUserId/{userId}");
        
        Console.WriteLine($"Status: {response.StatusCode}");
        
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ошибка API: {errorText}");
            MessageBox.Show($"Ошибка получения сотрудника: {errorText}");
            return null;
        }
        
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"JSON получен (первые 200 символов):\n{json.Substring(0, Math.Min(200, json.Length))}...");
        
        var staff = await response.Content.ReadFromJsonAsync<Staff>(
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Console.WriteLine($"Десериализовано: ID={staff?.ID}, UserId={staff?.UserId}, Name={staff?.Name}");
        
        return staff;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Исключение: {ex.Message}\n{ex.StackTrace}");
        MessageBox.Show($"Исключение: {ex.Message}");
        return null;
    }
}

        ///////////////////////////////

        public async Task<ObservableCollection<Day>> GetAllDays()
        {
            var responce = await client.GetAsync($"Days/GetAllDays");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var days = await responce.Content.ReadFromJsonAsync<ObservableCollection<Day>>();
                return days;
            }
        }

        public async Task<ObservableCollection<Status>> GetAllStatusesForRoom()
        {
            var responce = await client.GetAsync("Rooms/GetAllStatusesForRoom");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var statuses = await responce.Content.ReadFromJsonAsync<ObservableCollection<Status>>();
                return statuses;
            }

        }

        public async Task<ObservableCollection<StatusProblem>> GetAllStatusesProblem()
        {
            var responce = await client.GetAsync("Staffs/GetAllStatusesProblem");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var statusesproblem = await responce.Content.ReadFromJsonAsync<ObservableCollection<StatusProblem>>();
                return statusesproblem;
            }
        }

        public async Task<ObservableCollection<JobTitle>> GetAllJobTitle()
        {
            var responce = await client.GetAsync("Staffs/GetAllJobTitle");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var jobtitles = await responce.Content.ReadFromJsonAsync<ObservableCollection<JobTitle>>();
                return jobtitles;
            }
        }

        public async Task<ObservableCollection<Problem>> GetProblemsByStaff(int id)
        {
            try
            {
                var response = await client.GetAsync($"Problems/GetProblemsByStaff/{id}");

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка в получении списка: {result}");
                    return null;
                }


                var problems = await response.Content.ReadFromJsonAsync<ObservableCollection<Problem>>(
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return problems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Исключение: {ex.Message}");
                return null;
            }
        }

        public async Task<ObservableCollection<Role>> GetAllRoleUser()
        {
            var responce = await client.GetAsync($"Users/GetAllRoleUser");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var roles = await responce.Content.ReadFromJsonAsync<ObservableCollection<Role>>();
                return roles;
            }
        }
        public async Task EditStatusRoom(Room room)
        {
            var arg = JsonSerializer.Serialize(room);
            var responce = await client.PutAsync($"Rooms/EditStatusRoom", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }




        ///////////

        public async Task<List<Feedback>> GetFeedbacks()
        {
            try
            {
                var response = await client.GetAsync($"FeedbackMobile/GetAllFeedbacks");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var feedbacks = JsonSerializer.Deserialize<List<Feedback>>(result, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return feedbacks ?? new List<Feedback>();
                }

                return new List<Feedback>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отзывов: {ex.Message}");
                return new List<Feedback>();
            }
        }
        public async Task AddNewFeedback(Feedback feedback)
        {
            var feedbackToSend = new Feedback
            {
                Id = feedback.Id,
                Mark = feedback.Mark,
                Description = feedback.Description,
                Users = feedback.Users?.Select(u => new User
                {
                    Id = u.Id,
                    Login = u.Login,
                }).ToList()
            };

            var arg = JsonSerializer.Serialize(feedbackToSend);
            var response = await client.PostAsync($"Feedbacks/AddNewFeedback",
                new StringContent(arg, Encoding.UTF8, "application/json"));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Ошибка: {result}");
            }
        }
        public async Task EditFeedback(Feedback feedback)
        {
            var arg = JsonSerializer.Serialize(feedback);
            var responce = await client.PutAsync($"FeedbackMobile/EditFeedback", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }


        ///////////////////////////////////
        public async Task<AccommodationReportDTO> GetAccommodationReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var response = await client.GetAsync($"Reports/accommodation?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка в получении отчёта по проживанию!");
                    return null;
                }
                else
                {
                    var report = await response.Content.ReadFromJsonAsync<AccommodationReportDTO>();
                    return report;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return null;
            }
        }

        public async Task<ProcedureReportDTO> GetProcedureReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var response = await client.GetAsync($"Reports/Procedures?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка в получении отчёта по процедурам!");
                    return null;
                }
                else
                {
                    var report = await response.Content.ReadFromJsonAsync<ProcedureReportDTO>();
                    return report;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return null;
            }
        }

        public async Task<FinancialReportDTO> GetFinancialReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var response = await client.GetAsync($"Reports/financial?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка в получении финансового отчёта!");
                    return null;
                }
                else
                {
                    var report = await response.Content.ReadFromJsonAsync<FinancialReportDTO>();
                    return report;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return null;
            }
        }

        public async Task<byte[]> ExportReport(string reportType, DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await client.GetAsync(
                    $"Reports/export?reportType={reportType}" +
                    $"&startDate={startDate:yyyy-MM-dd}" +
                    $"&endDate={endDate:yyyy-MM-dd}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}");
                return null;
            }
        }

        ////////////////////////////////////////////////

        public async Task<ObservableCollection<Resource>> GetResourcesWithStaff()
        {
            var responce = await client.GetAsync("Resources/GetResourcesWithStaff");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var resources = await responce.Content.ReadFromJsonAsync<ObservableCollection<Resource>>();
                return resources;
            }
        }

        public async Task AddNewResource(Resource resource)
        {
            var arg = JsonSerializer.Serialize(resource);
            var responce = await client.PostAsync($"Resource/AddNewResource", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditResource(Resource resource)
        {
            var arg = JsonSerializer.Serialize(resource);
            var responce = await client.PutAsync($"Resource/EditResource", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        ////////////////////////////////////////////

        public async Task<ObservableCollection<Applications>> GetApplicationWithStaff()
        {
            var responce = await client.GetAsync("Applications/GetApplicationsWithStaff");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var applications = await responce.Content.ReadFromJsonAsync<ObservableCollection<Applications>>();
                return applications;
            }
        }

        public async Task AddNewApplication(Applications applications)
        {
            var arg = JsonSerializer.Serialize(applications);
            var responce = await client.PostAsync($"Applications/AddNewApplication", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        public async Task EditApplication(Applications applications)
        {
            var arg = JsonSerializer.Serialize(applications);
            var responce = await client.PutAsync($"Applications/EditApplication", new StringContent(arg, Encoding.UTF8, "application/json"));
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка данных!");
            }
            else
            {
                var result = await responce.Content.ReadAsStringAsync();
            }
        }

        //////////////////////////////////

        public async Task<List<Resource>> GetResourcesByStaff(int staffId)
        {
            try
            {
                var response = await client.GetAsync($"Resources/GetByStaff/{staffId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resources = JsonSerializer.Deserialize<List<Resource>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return resources ?? new List<Resource>();
                }

                return new List<Resource>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return new List<Resource>();
            }
        }

        public async Task<bool> UpdateResourceAmount(int resourceId, decimal newAmount)
        {
            try
            {

                var json = JsonSerializer.Serialize(newAmount);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"Resources/UpdateAmount/{resourceId}", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка API: {responseContent}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Исключение в UpdateResourceAmount: {ex.Message}");
                return false;
            }
        }

        public async Task<Guest> GetGuestByUserId(int userId)
        {
            try
            {
                var response = await client.GetAsync($"Guests/GetByUserId/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var guest = await response.Content.ReadFromJsonAsync<Guest>(
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return guest;
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateServiceRequest(Service service)
        {
            try
            {
                var response = await client.PostAsJsonAsync("Services/CreateServiceRequest", service);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Ошибка создания заявки: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Исключение: {ex.Message}");
                return false;
            }
        }

        //////////////////////////

        public async Task<List<Service>> GetAllServices()
        {
            var response = await client.GetAsync("Services/GetAllServices");
            if (response.IsSuccessStatusCode)
            {
                var services = await response.Content.ReadFromJsonAsync<List<Service>>();
                return services;
            }
            return new List<Service>();
        }

        public async Task<List<Service>> GetServicesByGuest(int guestId)
        {
            var response = await client.GetAsync($"Services/GetServicesByGuest/{guestId}");
            if (response.IsSuccessStatusCode)
            {
                var services = await response.Content.ReadFromJsonAsync<List<Service>>();
                return services;
            }
            return new List<Service>();
        }

        /////////////////////TestingCheatsEnabled true
        /////bb.moveobjects 
        /////cas.fulleditmode






    }
}
