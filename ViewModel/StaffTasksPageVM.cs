using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sanatory.ViewModel
{
    public class StaffTasksPageVM : BaseVM
    {
        private ObservableCollection<Problem> problems;
        private Problem selectedProblem;
        private int staffId;

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

        public CommandVM EditProblemCommand { get; set; }
        public CommandVM CompleteProblemCommand { get; set; }

        public StaffTasksPageVM(int staffId)
        {
            this.staffId = staffId;

            EditProblemCommand = new CommandVM(() => EditProblem());
            CompleteProblemCommand = new CommandVM(async () => await CompleteProblem());

            LoadProblems();
        }

        private async void LoadProblems()
        {
            try
            {
                var list = await DB.GetInstance().GetProblemsByStaff(staffId);

                Problems = list ?? new ObservableCollection<Problem>();   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}\n{ex.StackTrace}");
                Problems = new ObservableCollection<Problem>();
            }
        }

        private async Task LoadProblemsAsync()
        {
            var list = await DB.GetInstance().GetProblemsByStaff(staffId);

            Problems = list ?? new ObservableCollection<Problem>();
        }

        private void EditProblem()
        {
            if (SelectedProblem == null)
            {
                MessageBox.Show("Выберите задачу для редактирования!");
                return;
            }

            var mainWindow = Application.Current.Windows.Cast<Window>()
                .FirstOrDefault(w => w is StaffWindow);

            if (mainWindow != null)
            {
                var frame = FindVisualChild<Frame>(mainWindow);
                if (frame != null)
                {
                    frame.Content = new PrAdd(SelectedProblem);
                }
            }
        }

        private async Task CompleteProblem()
        {
            if (SelectedProblem == null)
            {
                MessageBox.Show("Выберите задачу для выполнения!");
                return;
            }

            var result = MessageBox.Show(
                $"Отметить задачу \"{SelectedProblem.Description}\" как выполненную?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await DB.GetInstance().DoneProblem(SelectedProblem.ID);
                    MessageBox.Show("✅ Задача выполнена!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    await LoadProblemsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
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