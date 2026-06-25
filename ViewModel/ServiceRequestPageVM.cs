using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.ViewModel
{
    public class ServiceRequestPageVM : BaseVM
    {
        private int guestId;
        private string title;
        private string description;

        public string Title
        {
            get => title;
            set { title = value; Signal(); }
        }

        public string Description
        {
            get => description;
            set { description = value; Signal(); }
        }

        public CommandVM SubmitRequestCommand { get; set; }

        public ServiceRequestPageVM(int guestId)
        {
            this.guestId = guestId;
            SubmitRequestCommand = new CommandVM(async () => await SubmitRequest());
        }

        private async Task SubmitRequest()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Введите название материала!");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Введите описание!");
                return;
            }

            try
            {
                var service = new Service
                {
                    Title = Title,
                    Description = Description,
                    GuestId = guestId
                };

                var result = await DB.GetInstance().CreateServiceRequest(service);

                if (result)
                {
                    MessageBox.Show("✅ Заявка успешно отправлена!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    Title = "";
                    Description = "";
                }
                else
                {
                    MessageBox.Show("❌ Ошибка при отправке заявки!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}