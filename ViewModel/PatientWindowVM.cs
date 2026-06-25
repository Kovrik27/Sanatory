using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sanatory.ViewModel
{
    public class PatientWindowVM : BaseVM
    {
        public static PatientWindowVM Instance { get; set; }

        private Guest guest;
        private int guestId;

        public Guest Guest
        {
            get => guest;
            set { guest = value; Signal(); }
        }
        public ObservableCollection<Procedure> Procedures { get; set; } = new ObservableCollection<Procedure>();

        public CommandVM OpenHome { get; set; }
        public CommandVM OpenInformation { get; set; }
        public CommandVM OpenProblems { get; set; }
        public CommandVM OpenFeedbacks { get; set; }

        public CommandVM OpenServiceRequest { get; set; }

        public PatientWindowVM()
        {
            Instance = this;

            OpenHome = new CommandVM(() =>
            {
                var mainWindow = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(w => w is PatientsWindow);

                if (mainWindow != null)
                {
                    var frame = FindVisualChild<Frame>(mainWindow);
                    if (frame != null && Guest != null)
                    {
                        frame.Content = new PatientHome(Guest.ID);
                    }
                }
            });

            OpenInformation = new CommandVM(() =>
            {
                var mainWindow = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(w => w is PatientsWindow);

                if (mainWindow != null)
                {
                    var frame = FindVisualChild<Frame>(mainWindow);
                    if (frame != null)
                    {
                        frame.Content = new Information();
                    }
                }
            });

            OpenProblems = new CommandVM(() =>
            {
                var mainWindow = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(w => w is PatientsWindow);

                if (mainWindow != null)
                {
                    var frame = FindVisualChild<Frame>(mainWindow);
                    if (frame != null)
                    {
                        frame.Content = new ProblemGuests();
                    }
                }
            });

            OpenFeedbacks = new CommandVM(() =>
            {
                var mainWindow = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(w => w is PatientsWindow);

                if (mainWindow != null)
                {
                    var frame = FindVisualChild<Frame>(mainWindow);
                    if (frame != null && Guest != null)
                    {
                        frame.Content = new FeedbacksGuest(Guest.User);
                    }
                }
            });

           
        OpenServiceRequest = new CommandVM(() =>
{
            var mainWindow = Application.Current.Windows.Cast<Window>()
                .FirstOrDefault(w => w is PatientsWindow);

            if (mainWindow != null)
            {
                var frame = FindVisualChild<Frame>(mainWindow);
                if (frame != null && Guest != null)
                {
                    frame.Content = new ServiceRequestPage(Guest.ID);
                }
            }
        });
        }

        public async Task SetGuestId(int userId)
        {
            try
            {
                guestId = userId;
                Guest = await DB.GetInstance().GetGuestByUserId(userId);

                if (Guest == null)
                {
                    Procedures = new ObservableCollection<Procedure>();
                    Signal(nameof(Procedures));
                    return;
                }

                var proceduresList = await DB.GetInstance().GetProceduresByGuest(Guest.ID);

                Procedures = proceduresList != null
                    ? new ObservableCollection<Procedure>(proceduresList)
                    : new ObservableCollection<Procedure>();
                Signal(nameof(Procedures));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных пациента: {ex.Message}");
                Procedures = new ObservableCollection<Procedure>();
                Signal(nameof(Procedures));
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }
    }
}