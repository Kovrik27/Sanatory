using Sanatory.Api;
using Sanatory.Documents;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.ViewModel
{
    public class GuVM : BaseVM
    {
        private ObservableCollection<Guest> guests;
        private List<Guest> allGuestsCache;
        private ObservableCollection<Service> allServices;

        private MainWindowVM MainVM;

        public CommandVM EditGuests { get; set; }
        public CommandVM DeleteGuests { get; set; }

        private Procedure selectedProcedure;
        public CommandVM AddProcedure { get; set; }

        public Guest selectedGuest;
        public CommandVM ExportToWordCommand { get; set; }

        public ObservableCollection<Guest> Guests
        {
            get => guests;
            set
            {
                guests = value;
                Signal();
            }
        }

        public Guest SelectedGuest
        {
            get => selectedGuest;
            set
            {
                selectedGuest = value;
                Signal();
            }
        }

        public ObservableCollection<Service> AllServices
        {
            get => allServices;
            set
            {
                allServices = value;
                Signal();
            }
        }

        public Procedure SelectedProcedures
        {
            get => selectedProcedure;
            set
            {
                selectedProcedure = value;
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
                FilterGuests();
            }
        }

        public GuVM()
        {
            MainVM = MainWindowVM.Instance;
            LoadAllGuests();
            LoadAllServices();

            EditGuests = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedGuest);
            });

            AddProcedure = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrcAddGu(SelectedGuest);
            });

            ExportToWordCommand = new CommandVM(() => ExportToWord());
        }

        private async void LoadAllGuests()
        {
            var serverGuests = await DB.GetInstance().GetAllGuests();
            allGuestsCache = serverGuests.ToList();
            FilterGuests();
        }
        private async void LoadAllServices()
        {
            try
            {
                var services = await DB.GetInstance().GetAllServices();
                AllServices = new ObservableCollection<Service>(services);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($">>> Ошибка загрузки услуг: {ex.Message}");
                AllServices = new ObservableCollection<Service>();
            }
        }

        private void FilterGuests()
        {
            if (allGuestsCache == null)
                return;

            IEnumerable<Guest> filtered = allGuestsCache;

            if (!string.IsNullOrEmpty(Search))
            {
                var searchLower = Search.ToLower();
                filtered = allGuestsCache.Where(g =>
                    (g.Lastname?.ToLower().Contains(searchLower) ?? false) ||
                    (g.Name?.ToLower().Contains(searchLower) ?? false) ||
                    (g.Surname?.ToLower().Contains(searchLower) ?? false)
                );
            }

            Guests = new ObservableCollection<Guest>(filtered);
        }

        private void ExportToWord()
        {
            if (SelectedGuest == null)
            {
                MessageBox.Show("Выберите гостя из списка!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var documentCard = new DocumentCardWord();
                documentCard.ExportGuestToWord(SelectedGuest);

                MessageBox.Show(
                    "Документ успешно создан!\n\n" +
                    $"Файл сохранён: Рабочий стол\\GuestsDirectory\\Карта гостя: {SelectedGuest.Surname}.docx",
                    "Успех", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка при создании документа:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}