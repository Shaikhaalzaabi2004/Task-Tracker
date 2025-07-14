using System.Collections.ObjectModel;
using System.Text.Json;
using TaskTracker.Models;
using TaskTracker.ViewModels;

namespace TaskTracker
{
    public partial class MainPage : ContentPage
    {
        public ToDoListViewModel viewModel = new ToDoListViewModel();
        public MainPage()
        {
            InitializeComponent();

            refreshTask();

            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.loadTaskList();
            refreshTask();
        }

        private void refreshTask()
        {
            if (viewModel.Items.Count == 0)
                noTaskDisplay.IsVisible = true;
            else
                noTaskDisplay.IsVisible = false;
        }

        private async void viewTasksBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ViewTasksPage");
        }

        private async void addTaskBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddTaskPage");
        }

        private void taskCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var obj = sender as CheckBox;

            var task = obj.BindingContext as TaskItem;

            removeTask(task);

            viewModel.loadTaskList();
        }

        public void removeTask(TaskItem task)
        {
            var taskListString = Preferences.Get("@task_list", string.Empty);

            if (!string.IsNullOrEmpty(taskListString))
            {
                var taskListOc = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(taskListString);

                var itemToRemove = taskListOc.FirstOrDefault(t => t.Id == task.Id);

                if (itemToRemove != null)
                {
                    taskListOc.Remove(itemToRemove);

                    var taskListJson = JsonSerializer.Serialize(taskListOc);
                    Preferences.Set("@task_list", taskListJson);
                }
            }
        }
    }
}
