using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.DTO
{
    public class ReportPeriodDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class AccommodationReportDTO
    {
        public ReportPeriodDTO Period { get; set; }
        public AccommodationSummaryDTO Summary { get; set; }
        public List<AccommodationDetailDTO> Details { get; set; }
        public List<RoomOccupancyDTO> RoomStats { get; set; }
        public List<DailyIncomeDTO> DailyIncome { get; set; }
    }

    public class AccommodationSummaryDTO
    {
        public int TotalGuests { get; set; }
        public int TotalNights { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal AverageStayLength { get; set; }
        public decimal AverageDailyRate { get; set; }
        public double OccupancyRate { get; set; }
    }

    public class AccommodationDetailDTO
    {
        public int StayId { get; set; }
        public string GuestName { get; set; }
        public string Passport { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Nights { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? PaymentDate { get; set; }
    }

    public class RoomOccupancyDTO
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int TotalDays { get; set; }
        public int OccupiedDays { get; set; }
        public double OccupancyPercentage { get; set; }
        public decimal TotalIncome { get; set; }
    }

    public class DailyIncomeDTO
    {
        public DateTime Date { get; set; }
        public decimal AccommodationIncome { get; set; }
        public int GuestsCheckedIn { get; set; }
        public int GuestsCheckedOut { get; set; }
    }
    public class ProcedureReportDTO
    {
        public ReportPeriodDTO Period { get; set; }
        public ProcedureSummaryDTO Summary { get; set; }
        public List<ProcedurePopularityDTO> Popularity { get; set; }
        public List<ProcedureDetailDTO> Details { get; set; }
        public List<DoctorStatsDTO> DoctorStats { get; set; }
        public List<CategoryStatsDTO> CategoryStats { get; set; }
    }

    public class ProcedureSummaryDTO
    {
        public int TotalProcedures { get; set; }
        public int UniqueGuests { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal AveragePerDay { get; set; }
        public int ProceduresPerGuest { get; set; }
        public decimal AverageProcedurePrice { get; set; }
    }

    public class ProcedurePopularityDTO
    {
        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public string Category { get; set; }
        public int TimesPerformed { get; set; }
        public int UniqueGuests { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal AveragePrice { get; set; }
        public double PopularityPercent { get; set; }
    }

    public class ProcedureDetailDTO
    {
        public int GuestProcedureId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string GuestName { get; set; }
        public int RoomNumber { get; set; }
        public string ProcedureName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalCost { get; set; }
        public string DoctorName { get; set; }
        public bool IsPaid { get; set; }
    }

    public class DoctorStatsDTO
    {
        public string DoctorName { get; set; }
        public int ProceduresCount { get; set; }
        public int UniqueGuests { get; set; }
        public decimal TotalIncome { get; set; }
        public List<ProcedurePopularityDTO> TopProcedures { get; set; }
    }

    public class CategoryStatsDTO
    {
        public string Category { get; set; }
        public int ProceduresCount { get; set; }
        public decimal TotalIncome { get; set; }
        public double Percentage { get; set; }
    }

    public class FinancialReportDTO
    {
        public ReportPeriodDTO Period { get; set; }
        public FinancialSummaryDTO Summary { get; set; }
        public IncomeBreakdownDTO IncomeBreakdown { get; set; }
        public List<MonthlyComparisonDTO> MonthlyComparison { get; set; }
        public List<TopSpenderDTO> TopSpenders { get; set; }
    }

    public class FinancialSummaryDTO
    {
        public decimal TotalIncome { get; set; }
        public decimal AccommodationIncome { get; set; }
        public decimal ProceduresIncome { get; set; }
        public double AccommodationPercent { get; set; }
        public double ProceduresPercent { get; set; }
        public int TotalGuests { get; set; }
        public decimal AverageGuestSpending { get; set; }
    }

    public class IncomeBreakdownDTO
    {
        public List<CategoryIncomeDTO> ByCategory { get; set; }
        public List<RoomIncomeDTO> ByRoom { get; set; }
    }

    public class CategoryIncomeDTO
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
    }

    public class RoomIncomeDTO
    {
        public int RoomNumber { get; set; }
        public decimal AccommodationIncome { get; set; }
        public decimal ProceduresIncome { get; set; }
        public decimal TotalIncome { get; set; }
    }

    public class MonthlyComparisonDTO
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal AccommodationIncome { get; set; }
        public decimal ProceduresIncome { get; set; }
        public decimal TotalIncome { get; set; }
    }

    public class TopSpenderDTO
    {
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public decimal AccommodationCost { get; set; }
        public decimal ProceduresCost { get; set; }
        public decimal TotalSpent { get; set; }
        public int ProceduresCount { get; set; }
    }
}
