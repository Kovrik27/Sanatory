using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sanatory.ViewModel
{
    public class ResourcePageVM : BaseVM
    {
        private ObservableCollection<Resource> resources;
        private Resource selectedResource;
        private int staffId;

        public ObservableCollection<Resource> Resources
        {
            get => resources;
            set { resources = value; Signal(); }
        }

        public Resource SelectedResource
        {
            get => selectedResource;
            set { selectedResource = value; Signal(); }
        }

        public CommandVM UseResourceCommand { get; set; }

        public ResourcePageVM(int staffId)
        {
            this.staffId = staffId;
            UseResourceCommand = new CommandVM(async () => await UseResource());

            LoadResources();
        }

        private async void LoadResources()
        {
            try
            {
                var list = await DB.GetInstance().GetResourcesByStaff(staffId);
                Resources = new ObservableCollection<Resource>(list ?? new List<Resource>());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                Resources = new ObservableCollection<Resource>();
            }
        }

        private async Task UseResource()
        {
            try
            {
                if (SelectedResource == null)
                {
                    MessageBox.Show("Выберите материал!");
                    return;
                }

                if (SelectedResource.UseAmount <= 0)
                {
                    MessageBox.Show("Введите количество больше 0!");
                    return;
                }

                if (SelectedResource.UseAmount > SelectedResource.Amount)
                {
                    MessageBox.Show($"Недостаточно материала!\nДоступно: {SelectedResource.Amount:N2}");
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Использовать {SelectedResource.UseAmount:N2} ед. \"{SelectedResource.Title}\"?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirm != MessageBoxResult.Yes)
                    return;

                var newAmount = SelectedResource.Amount - SelectedResource.UseAmount;
                var success = await DB.GetInstance().UpdateResourceAmount(SelectedResource.Id, newAmount);

                if (success)
                {
                    SelectedResource.Amount = newAmount;
                    SelectedResource.UseAmount = 0;

                    var index = Resources.IndexOf(SelectedResource);
                    if (index >= 0)
                    {
                        Resources[index] = SelectedResource;
                    }

                    MessageBox.Show("✅ Материал использован!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}