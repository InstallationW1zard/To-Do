using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TaskApplication.Dialogs;
using TaskApplication.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TaskApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        public MainPage()
        {
            this.InitializeComponent();
            if(File.Exists(MainViewModel.PersistencePath))
            {
                DataContext = JsonConvert
                    .DeserializeObject<MainViewModel>(File.ReadAllText(MainViewModel.PersistencePath), MainViewModel.Settings);
            } else
            {
                DataContext = new MainViewModel();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var TaskDialog = new TaskDialog((DataContext as MainViewModel).TaskList);
            await TaskDialog.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as MainViewModel;
            var TaskDialog = new TaskDialog(dataContext.SelectedItem, dataContext.TaskList);
            await TaskDialog.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveItem();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).ToggleCompleteness();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveState();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RefreshList();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Sort();
        }
    }
}
