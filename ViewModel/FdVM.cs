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
        private bool showAll;
        private bool showHappy;
        private bool showBad;
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

        public int Star1Count
        {
            get => star1Count;
            set
            {
                star1Count = value;
                Signal();
            }
        }

        public int Star2Count
        {
            get => star2Count;
            set
            {
                star2Count = value;
                Signal();
            }
        }

        public int Star3Count
        {
            get => star3Count;
            set
            {
                star3Count = value;
                Signal();
            }
        }

        public int Star4Count
        {
            get => star4Count;
            set
            {
                star4Count = value;
                Signal();
            }
        }

        public int Star5Count
        {
            get => star5Count;
            set
            {
                star5Count = value;
                Signal();
            }
        }

        public bool ShowAll
        {
            get => showAll;
            set
            {
                showAll = value;
                Signal();
                GetAll();
            }
        }

        public bool ShowHappy
        {
            get => showHappy;
            set
            {
                showHappy = value;
                Signal();
                GetAll();
            }
        }

        public bool ShowBad
        {
            get => showBad;
            set
            {
                showBad = value;
                Signal();
                GetAll();
            }
        }

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

        public CommandVM GetHappy { get; set; }
        public CommandVM GetBad { get; set; }
        public CommandVM CreateFeedback { get; set; }

        public FdVM()
        {
            GetHappy = new CommandVM(() => ShowHappy = true);
            GetBad = new CommandVM(() => ShowBad = true);
            CreateFeedback = new CommandVM(() => { });

            LoadFeedbacks();
        }

        private async void LoadFeedbacks()
        {
            allFeedbacks = new ObservableCollection<Feedback>(await DB.GetInstance().GetFeedbacks());
            GetAll();
        }

        public async void GetAll()
        {
            if (allFeedbacks == null)
            {
                allFeedbacks = new ObservableCollection<Feedback>(await DB.GetInstance().GetFeedbacks());
            }

            var filteredFeedbacks = allFeedbacks;

            if (!string.IsNullOrEmpty(Search))
            {
                filteredFeedbacks = new ObservableCollection<Feedback>(filteredFeedbacks.Where(s => s.Description != null && s.Description.Contains(Search)));
            }

            if (ShowAll)
            {
                filteredFeedbacks = new ObservableCollection<Feedback>();
            }

            if (ShowHappy)
            {
                filteredFeedbacks = new ObservableCollection<Feedback>(filteredFeedbacks.Where(s => s.Mark > 4));
            }

            if (ShowBad)
            {
                filteredFeedbacks = new ObservableCollection<Feedback>(filteredFeedbacks.Where(s => s.Mark < 4));
            }

            Feedbacks = new ObservableCollection<Feedback>(filteredFeedbacks);
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