using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using Sanatory.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Spire.Doc;

namespace Sanatory.ViewModel
{
    public class GuAddVM : BaseVM
    {
        public CommandVM Save { get; set; }
        public CommandVM SelectAllCommand { get; set; }
        public CommandVM DeselectAllCommand { get; set; }

        private Guest guest = new();

        private ObservableCollection<ProcedureCheckbox> proceduresWithSelection;
        public ObservableCollection<ProcedureCheckbox> ProceduresWithSelection
        {
            get => proceduresWithSelection;
            set
            {
                proceduresWithSelection = value;
                Signal();
            }
        }

        private ObservableCollection<User> users;
        private ObservableCollection<Procedure> procedures;

        public Guest Guest
        {
            get => guest;
            set
            {
                guest = value;
                Signal();
            }
        }

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                Signal();
            }
        }

        public ObservableCollection<Procedure> Procedures
        {
            get => procedures;
            set
            {
                procedures = value;
                Signal();
            }
        }

        private string search;
        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                FilterProcedures();
            }
        }

        private int selectedProceduresCount;
        public int SelectedProceduresCount
        {
            get => selectedProceduresCount;
            set
            {
                selectedProceduresCount = value;
                Signal();
            }
        }

        private decimal totalProceduresPrice;
        public decimal TotalProceduresPrice
        {
            get => totalProceduresPrice;
            set
            {
                totalProceduresPrice = value;
                Signal();
            }
        }

        public GuAddVM()
        {
            LoadProcedures();

            Save = new CommandVM(async () =>
            {
                await SaveGuest();
            });

            SelectAllCommand = new CommandVM(() =>
            {
                foreach (var proc in ProceduresWithSelection)
                {
                    proc.IsSelected = true;
                }
                UpdateSelectedInfo();
            });

            DeselectAllCommand = new CommandVM(() =>
            {
                foreach (var proc in ProceduresWithSelection)
                {
                    proc.IsSelected = false;
                }
                UpdateSelectedInfo();
            });
        }

        private async void LoadProcedures()
        {
            var allProcedures = await DB.GetInstance().GetAllProcedure();

            ProceduresWithSelection = new ObservableCollection<ProcedureCheckbox>(
                allProcedures.Where(p => p.Id != 1).Select(p => new ProcedureCheckbox
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Duration = p.Duration,
                    Price = p.Price,
                    IsSelected = false
                })
            );
        }

        private void FilterProcedures()
        {
            if (ProceduresWithSelection == null) return;

            if (string.IsNullOrEmpty(Search))
            {
                foreach (var proc in ProceduresWithSelection)
                {
                    proc.IsVisible = true;
                }
            }
            else
            {
                foreach (var proc in ProceduresWithSelection)
                {
                    proc.IsVisible = proc.Title.Contains(Search) || proc.Description.Contains(Search);
                }
            }
        }

        private void UpdateSelectedInfo()
        {
            var selected = ProceduresWithSelection.Where(p => p.IsSelected).ToList();
            SelectedProceduresCount = selected.Count;
            TotalProceduresPrice = selected.Sum(p => p.Price);
        }

        public void OnProcedureSelectionChanged()
        {
            UpdateSelectedInfo();
        }

        private async Task SaveGuest()
        {
            try
            {
                var selectedProcedures = ProceduresWithSelection
                    .Where(p => p.IsSelected)
                    .Select(p => new Procedure
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Duration = p.Duration,
                        Price = p.Price
                    })
                    .ToList();

                if (Guest.ID == 0)
                {
                    var guestWithProcedures = new GuestWithProceduresDTO
                    {
                        Guest = Guest,
                        ProcedureIds = selectedProcedures.Select(p => p.Id).ToList()
                    };

                    var result = await DB.GetInstance().AddNewGuestWithProcedures(guestWithProcedures);

                    if (result)
                    {
                        if (Guest.Room != null)
                        {
                            await DB.GetInstance().EditStatusRoom(Guest.Room);
                        }

                        MessageBox.Show("Гость успешно добавлен с выбранными процедурами!", "Успех",
                                      MessageBoxButton.OK);

                        MainWindowVM.Instance.CurrentPage = new Guests();
                    }
                }
                else
                {
                    await DB.GetInstance().EditGuest(Guest);
                    MainWindowVM.Instance.CurrentPage = new Guests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void SetEditGuest(Guest selectedGuest)
        {
            Guest = selectedGuest;
        }

        internal void SetRoom(Room? selectedRoom)
        {
            Guest.RoomID = selectedRoom.ID;
            Guest.Room = selectedRoom;
            Signal(nameof(Guest));
        }

       
    }

    
    public class ProcedureCheckbox : BaseVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                Signal();
            }
        }

        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                Signal();
            }
        }

        public object CurrentPageViewModel { get; set; }
    }
}