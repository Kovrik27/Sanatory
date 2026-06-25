using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class FdVM : BaseVM
    {
        private ObservableCollection<Feedback> allFeedbacks;
        private ObservableCollection<Feedback> feedbacks;
        private double averageRating;
        private int totalReviews;
        private int star1Count;
        private int star2Count;
        private int star3Count;
        private int star4Count;
        private int star5Count;
        private string search;

        public ObservableCollection<Feedback> Feedbacks
        {
            get => feedbacks;
            set
            {
                feedbacks = value;
                Signal();
            }
        }

        public double AverageRating
        {
            get => averageRating;
            set
            {
                averageRating = value;
                Signal();
            }
        }

        public int TotalReviews
        {
            get => totalReviews;
            set
            {
                totalReviews = value;
                Signal();
            }
        }

        public int Star1Count { get => star1Count; set { star1Count = value; Signal(); } }
        public int Star2Count { get => star2Count; set { star2Count = value; Signal(); } }
        public int Star3Count { get => star3Count; set { star3Count = value; Signal(); } }
        public int Star4Count { get => star4Count; set { star4Count = value; Signal(); } }
        public int Star5Count { get => star5Count; set { star5Count = value; Signal(); } }

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAll();
            }
        }

        public CommandVM ShowAllCommand { get; set; }
        public CommandVM ShowHappyCommand { get; set; }
        public CommandVM ShowBadCommand { get; set; }

        private bool _filterHappy;
        private bool _filterBad;

        public FdVM()
        {
            ShowAllCommand = new CommandVM(() =>
            {
                _filterHappy = false;
                _filterBad = false;
                GetAll();
            });

            ShowHappyCommand = new CommandVM(() =>
            {
                _filterHappy = true;
                _filterBad = false;
                GetAll();
            });

            ShowBadCommand = new CommandVM(() =>
            {
                _filterHappy = false;
                _filterBad = true;
                GetAll();
            });

            LoadFeedbacks();
        }

        private async void LoadFeedbacks()
        {
            try
            {
                var feedbacksFromServer = await DB.GetInstance().GetFeedbacks();
                allFeedbacks = feedbacksFromServer != null
                    ? new ObservableCollection<Feedback>(feedbacksFromServer)
                    : new ObservableCollection<Feedback>();
                GetAll();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки: {ex.Message}");
                allFeedbacks = new ObservableCollection<Feedback>();
            }
        }

        public void GetAll()
        {
            if (allFeedbacks == null)
                return;

            IEnumerable<Feedback> filtered = allFeedbacks;

            if (!string.IsNullOrEmpty(Search))
            {
                var searchLower = Search.ToLower();
                filtered = filtered.Where(s =>
                    (s.Description?.ToLower().Contains(searchLower) ?? false) ||
                    (s.UserLogin?.ToLower().Contains(searchLower) ?? false));
            }

            if (_filterHappy)
            {
                filtered = filtered.Where(s => s.Mark > 4);
            }
            else if (_filterBad)
            {
                filtered = filtered.Where(s => s.Mark < 4);
            }

            Feedbacks = new ObservableCollection<Feedback>(filtered);
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            if (allFeedbacks == null || allFeedbacks.Count == 0)
            {
                AverageRating = 0;
                TotalReviews = 0;
                Star1Count = 0;
                Star2Count = 0;
                Star3Count = 0;
                Star4Count = 0;
                Star5Count = 0;
                return;
            }

            TotalReviews = allFeedbacks.Count;
            AverageRating = allFeedbacks.Average(f => f.Mark);

            Star1Count = allFeedbacks.Count(f => f.Mark == 1);
            Star2Count = allFeedbacks.Count(f => f.Mark == 2);
            Star3Count = allFeedbacks.Count(f => f.Mark == 3);
            Star4Count = allFeedbacks.Count(f => f.Mark == 4);
            Star5Count = allFeedbacks.Count(f => f.Mark == 5);
        }
    }
}