using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sanatory.ViewModel
{
    public class StaffWindowVM : BaseVM
    {
        public static StaffWindowVM Instance { get; set; }

        private Problem selectedProblem;
        private ObservableCollection<Problem> problems;
        private Staff staff;
        private int staffId;

        public Staff Staff
        {
            get => staff;
            set { staff = value; Signal(); }
        }

        public ObservableCollection<Problem> Problems
        {
            get => problems;
            set { problems = value; Signal(); }
        }

        public Problem SelectedProblem
        {
            get => selectedProblem;
            set { selectedProblem = value; Signal(); }
        }

        public CommandVM CreateProblem { get; set; }
        public CommandVM EditProblem { get; set; }
        public CommandVM EditStatusProblem { get; set; }
        public CommandVM OpenMaterials { get; set; }
        public CommandVM OpenTasks { get; set; }

        public StaffWindowVM()
        {
            Instance = this;

            OpenMaterials = new CommandVM(() =>
            {
                if (Staff == null)
                {
                    MessageBox.Show("Сотрудник не загружен!");
                    return;
                }

                var mainWindow = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(w => w is StaffWindow);

                if (mainWindow != null)
                {
                    var frame = FindVisualChild<Frame>(mainWindow);
                    if (frame != null)
                    {
                        frame.Content = new ResourcePage(Staff.ID);
                    }
                }
            });

            OpenTasks = new CommandVM(() =>
            {
                var mainWindow = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(w => w is StaffWindow);

                if (mainWindow != null)
                {
                    var frame = FindVisualChild<Frame>(mainWindow);
                    if (frame != null)
                    {
                        if (Staff != null)
                        {
                            frame.Content = new StaffTasksPage(Staff.ID);
                        }
                    }
                }
            });
        

            EditStatusProblem = new CommandVM(async () =>
            {
                if (SelectedProblem == null)
                {
                    MessageBox.Show("Выберите задачу!");
                    return;
                }

                await DB.GetInstance().DoneProblem(SelectedProblem.ID);
                MessageBox.Show("Задача выполнена!");

                await SetStaffId(staffId);
            });
        }
        public async Task SetStaffId(int userId)
        {
            try
            {
                staffId = userId;

                Staff = await DB.GetInstance().GetStaffByUserId(userId);

                if (Staff == null)
                {
                    Problems = new ObservableCollection<Problem>();
                    Signal(nameof(Problems));
                    return;
                }

                var problemsList = await DB.GetInstance().GetProblemsByStaff(Staff.ID);

                Problems = problemsList ?? new ObservableCollection<Problem>();
                Signal(nameof(Problems));
            }
            catch (Exception ex)
            {
                Problems = new ObservableCollection<Problem>();
                Signal(nameof(Problems));
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