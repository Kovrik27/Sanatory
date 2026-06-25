using Sanatory.Api;
using Sanatory.DTO;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sanatory.ViewModel
{
    public class RpVM : BaseVM
    {
        private DB db;
        private string selectedReportType = "accommodation";
        private DateTime startDate;
        private DateTime endDate;
        private bool isLoading;
        private AccommodationReportDTO accommodationReport;
        private ProcedureReportDTO procedureReport;
        private FinancialReportDTO financialReport;


        public RpVM()
        {
            db = DB.GetInstance();
            startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);

            GenerateReportCommand = new RelayCommand(async () => await GenerateReport());
            ExportToExcelCommand = new RelayCommand(async () => await ExportToExcel());
        }

        public string SelectedReportType
        {
            get => selectedReportType;
            set
            {
                selectedReportType = value;
                Signal();
                Signal(nameof(PeriodDisplay));
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                Signal();
                Signal(nameof(PeriodDisplay));
            }
        }

        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                Signal();
                Signal(nameof(PeriodDisplay));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                Signal();
            }
        }

        public string PeriodDisplay
        {
            get
            {
                if (selectedReportType == "accommodation")
                    return $"Отчет по проживанию за {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}";
                if (selectedReportType == "procedures")
                    return $"Отчет по процедурам за {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}";
                return $"Финансовый отчет за {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}";
            }
        }

        public AccommodationReportDTO AccommodationReport
        {
            get => accommodationReport;
            set
            {
                accommodationReport = value;
                Signal();
            }
        }

        public ProcedureReportDTO ProcedureReport
        {
            get => procedureReport;
            set
            {
                procedureReport = value;
                Signal();
            }
        }

        public FinancialReportDTO FinancialReport
        {
            get => financialReport;
            set
            {
                financialReport = value;
                Signal();
            }
        }

        public ICommand GenerateReportCommand { get; }
        public ICommand ExportToExcelCommand { get; }

        private async Task GenerateReport()
        {
            try
            {
                isLoading = true;
                Signal(nameof(IsLoading));

                switch (selectedReportType)
                {
                    case "accommodation":
                        AccommodationReport = await db.GetAccommodationReport(startDate, endDate);
                        if (AccommodationReport == null)
                            MessageBox.Show("Нет данных по проживанию за выбранный период", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "procedures":
                        ProcedureReport = await db.GetProcedureReport(startDate, endDate);
                        if (ProcedureReport == null)
                            MessageBox.Show("Нет данных по процедурам за выбранный период", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "financial":
                        FinancialReport = await db.GetFinancialReport(startDate, endDate);
                        if (FinancialReport == null)
                            MessageBox.Show("Нет финансовых данных за выбранный период", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                isLoading = false;
                Signal(nameof(IsLoading));
            }
        }

        private async Task ExportToExcel()
        {
            try
            {
                var excelBytes = await db.ExportReport(selectedReportType, startDate, endDate);
                if (excelBytes != null && excelBytes.Length > 0)
                {
                    var saveDialog = new Microsoft.Win32.SaveFileDialog
                    {
                        Filter = "Excel файлы (*.xlsx)|*.xlsx",
                        FileName = $"report_{selectedReportType}_{DateTime.Now:yyyyMMdd}.xlsx"
                    };

                    if (saveDialog.ShowDialog() == true)
                    {
                        System.IO.File.WriteAllBytes(saveDialog.FileName, excelBytes);
                        MessageBox.Show($"Отчет сохранен: {saveDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для экспорта", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}