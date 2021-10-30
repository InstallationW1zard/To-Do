using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace TaskApplication.ViewModels
{
    public class DialogViewModel: INotifyPropertyChanged
    {
       
        public Task BoundTask { get; set; }
        public Appointment BoundAppointment { get; set; }

        private bool isTask;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsTask { 
            get {
                return isTask;
            } 

            set
            {
                isTask = value;

                if((BoundTask == null || BoundTask.Id <=0) && (BoundAppointment == null || BoundAppointment.Id <=0))
                {
                    if (isTask)
                    {
                        BoundTask = new Task();
                        BoundAppointment = null;
                    }
                    else
                    {
                        BoundAppointment = new Appointment();
                        BoundTask = null;
                    }
                }

                NotifyPropertyChanged("IsTaskVisible");
                NotifyPropertyChanged("IsAppointmentVisible");
                NotifyPropertyChanged("BoundAppointment");
                NotifyPropertyChanged("BoundTask");
            }
        }

        public Visibility IsTaskVisible
        {
            get
            {
                return IsTask ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsAppointmentVisible
        {
            get
            {
                return IsTask ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        
        public DialogViewModel(Item item)
        {
            if(item is Appointment)
            {
                BoundAppointment = item as Appointment;
                BoundTask = null;
                IsTask = false;

                NotifyPropertyChanged("BoundAppointment");
            } else if (item is Task)
            {
                BoundTask = item as Task;
                BoundAppointment = null;
                IsTask = true;
                NotifyPropertyChanged("BoundTask");
            } else
            {
                IsTask = true;
            }
        }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
