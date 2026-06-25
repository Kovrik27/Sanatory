using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Resource : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int? StaffId { get; set; }

        private decimal useAmount;
        public decimal UseAmount
        {
            get => useAmount;
            set
            {
                useAmount = value;
                OnPropertyChanged();
            }
        }

        public Staff Staff { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
