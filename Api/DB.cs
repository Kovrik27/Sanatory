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
using Microsoft.Extensions.Logging;

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
            var arg = JsonSerializer.Serialize(daytime);
            var responce = await client.PostAsync($"Daytims/AddNewDaytime", new StringContent(arg, Encoding.UTF8, "application/json"));
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

        public async Task EditDaytime(Daytime daytime)
        {
            var arg = JsonSerializer.Serialize(daytime);
            var responce = await client.PutAsync($"Daytims/EditDaytime", new StringContent(arg, Encoding.UTF8, "application/json"));
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
                DaytimeId = daytime.ID,
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



        public async Task AddNewGuest(Guest guest)
        {
            var arg = JsonSerializer.Serialize(guest);
            var responce = await client.PostAsync($"Guests/AddNewGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
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

        public async Task AddProcedureOnGuest(Guest guest, Procedure procedure)
        {
            var procedureOnGuest = new ProcedureOnGuestDTO
            {
                GuestId = guest.ID,
                ProcedureId = procedure.Id
            };

            var arg = JsonSerializer.Serialize(procedureOnGuest);
            var responce = await client.PostAsync($"Guests/AddProcedureOnGuest", new StringContent(arg, Encoding.UTF8, "application/json"));
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

        internal async Task<ObservableCollection<Problem>> GetProblemsByStaff(int id)
        {
            var responce = await client.GetAsync($"Problems/GetProblemsByStaff/{id}");
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

        public async Task<ObservableCollection<Feedback>> GetFeedbacks()
        {
            var responce = await client.GetAsync("FeedbackMobile/GetAllFeedbacks");
            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка в получении списка!");
                return null;
            }
            else
            {
                var feedbacks = await responce.Content.ReadFromJsonAsync<ObservableCollection<Feedback>>();
                return feedbacks;
            }
        }
        public async Task AddNewFeedback(Feedback feedback)
        {
            var arg = JsonSerializer.Serialize(feedback);
            var responce = await client.PostAsync($"FeedbackMobile/AddNewFeedback", new StringContent(arg, Encoding.UTF8, "application/json"));
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
        public async Task EditFeedback(Feedback feedback)
        {
            var arg = JsonSerializer.Serialize(feedback);
            var responce = await client.PostAsync($"FeedbackMobile/EditFeedback", new StringContent(arg, Encoding.UTF8, "application/json"));
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





        /////////////////////TestingCheatsEnabled true
        /////bb.moveobjects 
        /////cas.fulleditmode






    }
}
