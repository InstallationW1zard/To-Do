using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TaskApplication.Dialogs
{
    public sealed partial class TaskDialog : ContentDialog
    {
        private IList<Item> _TaskList;
        public TaskDialog(IList<Item> TaskList)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(null);
            _TaskList = TaskList;
        }

        public TaskDialog(Item selectedTask, IList<Item> TaskList)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(selectedTask);
            _TaskList = TaskList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var context = DataContext as DialogViewModel;
            if(context.BoundTask != null)
            {
                var Task = context.BoundTask;
                if(Task.Id <= 0)
                {
                    FakeDatabase.LastTaskId++;
                    Task.Id = FakeDatabase.LastTaskId;
                    _TaskList.Add(Task);
                }
            } else if (context.BoundAppointment != null)
            {
                var appointment = context.BoundAppointment;
                if(appointment.Id <= 0)
                {
                    FakeDatabase.LastTaskId++;
                    appointment.Id = FakeDatabase.LastTaskId;
                    _TaskList.Add(appointment);
                }
            }
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
