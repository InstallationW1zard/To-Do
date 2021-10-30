using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace TaskApplication.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        internal static string PersistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData2.json";
        internal static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public ObservableCollection<Item> TaskList { get; set; }
        private bool isSortedAsc;

        public ObservableCollection<Item> FilteredItemList
        {
            get
            {
                if(string.IsNullOrEmpty(Query))
                {
                    return isSortedAsc 
                        ? new ObservableCollection<Item>(TaskList.OrderBy(t => t.Priority)) 
                        : new ObservableCollection<Item>(TaskList.OrderByDescending(t => t.Priority));
                }

                return isSortedAsc
                    ? new ObservableCollection<Item>(
                    TaskList.Where(t => t.Name.ToUpper().Contains(Query.ToUpper()) 
                    || t.Description.ToUpper().Contains(Query.ToUpper())
                    || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                    ).OrderBy(t => t.Priority))
                    : new ObservableCollection<Item>(
                    TaskList.Where(t => t.Name.ToUpper().Contains(Query.ToUpper())
                    || t.Description.ToUpper().Contains(Query.ToUpper())
                    || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                    ).OrderByDescending(t => t.Priority));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Item SelectedItem { get; set; }
        public MainViewModel()
        {
            TaskList = new ObservableCollection<Item>();
        }

        public void RemoveItem()
        {
            
            if(SelectedItem == null)
            {
                
                return;
            }
            else
            {
                
                TaskList.Remove(SelectedItem);
                RefreshList();
            }
        }

        public string Query
        {
            get; set;
        }

        public void ToggleCompleteness()
        {
            if(SelectedItem == null || (SelectedItem as Task) == null)
            {
                return;
            }
            (SelectedItem as Task).IsCompleted = !(SelectedItem as Task).IsCompleted;
        }

        public void SaveState()
        {
            File.WriteAllText(PersistencePath, JsonConvert.SerializeObject(this, Settings));
        }

        public void RefreshList()
        {
            NotifyPropertyChanged("FilteredItemList");
        }

        public void Sort()
        {
            isSortedAsc = !isSortedAsc;
            RefreshList();
        }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
