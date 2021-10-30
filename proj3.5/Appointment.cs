using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace TaskApplication
{
    public class Appointment : Item, INotifyPropertyChanged
    {
        public Appointment()
        {
            BoundStart = DateTime.Today;
            BoundEnd = DateTime.Today.AddDays(1);

            Priority = 1;
        }
        public override Visibility IsCompleteable => Visibility.Collapsed;
        public DateTime StartTime { get; set; }
        private DateTimeOffset boundStart;
        public DateTimeOffset BoundStart
        {
            get
            {
                return boundStart;
            }
            set
            {
                boundStart = value;
                StartTime = boundStart.Date;
                NotifyPropertyChanged("StartTime");
                NotifyPropertyChanged("SecondaryText");
            }
        }
        public DateTime EndTime { get; set; }
        private DateTimeOffset boundEnd;
        public DateTimeOffset BoundEnd
        {
            get
            {
                return boundEnd;
            }
            set
            {
                boundEnd = value;
                EndTime = boundEnd.Date;
                NotifyPropertyChanged("EndTime");
            }
        }
        public string NewAttendee { get; set; }
        public ObservableCollection<string> Attendees2 { get; set; }
        private string attendeeString;
        public string AttendeesString
        {
            get
            {
                return attendeeString;
            }
            set
            {
                attendeeString = value;
                NotifyPropertyChanged("Attendees");
            }
        }
        private ObservableCollection<string> attendees;
        public ObservableCollection<string> Attendees
        {
            get
            {
                attendees = new ObservableCollection<string>();
                var list = AttendeesString?.Split(new char[] { ',' })?.ToList() ?? new List<string>();
                list.Where(t => !string.IsNullOrEmpty(t)).ToList().ForEach(attendees.Add);
                return attendees;
            }
        }
        public override string PrimaryText => $"Appointment:  {Name} - {Description}";
        public override string SecondaryText => $"{Priority} {StartTime} - {EndTime} - {attendeeString}";
    }
}
