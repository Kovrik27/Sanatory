using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Sanatory.ViewModel
{
    public class FdGuVM : BaseVM
    {
        private Feedback feedback;
        private User selectedUser;
        private bool star1;
        private bool star2;
        private bool star3;
        private bool star4;
        private bool star5;

        public Feedback Feedback
        {
            get => feedback;
            set
            {
                feedback = value;
                Signal();
            }
        }

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                Signal();
            }
        }

        public bool Star1 { get => star1; set { star1 = value; Signal(); } }
        public bool Star2 { get => star2; set { star2 = value; Signal(); } }
        public bool Star3 { get => star3; set { star3 = value; Signal(); } }
        public bool Star4 { get => star4; set { star4 = value; Signal(); } }
        public bool Star5 { get => star5; set { star5 = value; Signal(); } }

        public CommandVM<string> SetRating { get; set; }
        public CommandVM Save { get; set; }

        public FdGuVM()
        {
            Feedback = new Feedback();

            SetRating = new CommandVM<string>(parameter =>
            {
                if (int.TryParse(parameter, out int rating))
                {
                    Feedback.Mark = rating;
                    UpdateStars(rating);
                }
            });

            Save = new CommandVM(async () =>
            {
                if (Feedback.Mark == 0)
                {
                    MessageBox.Show("Пожалуйста, выставьте оценку");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Feedback.Description))
                {
                    MessageBox.Show("Пожалуйста, введите текст отзыва");
                    return;
                }

                if (SelectedUser != null)
                {
                    if (Feedback.Users == null)
                        Feedback.Users = new List<User>();

                    Feedback.Users.Add(SelectedUser);
                }

                await DB.GetInstance().AddNewFeedback(Feedback);

                PatientsWindow patientsWindow = new PatientsWindow(SelectedUser.Id);
                patientsWindow.Show();

                var currentWindow = Window.GetWindow(Application.Current.Windows[0]);
                currentWindow?.Close();
            });
        }

        private void UpdateStars(int rating)
        {
            Star1 = rating >= 1;
            Star2 = rating >= 2;
            Star3 = rating >= 3;
            Star4 = rating >= 4;
            Star5 = rating >= 5;
        }

        internal void SetEditFeedback(Feedback selectedFeedback)
        {
            Feedback = selectedFeedback;
            UpdateStars(selectedFeedback.Mark);
        }

        internal void SetUser(User selectedUser)
        {
            SelectedUser = selectedUser;
        }
    }
}