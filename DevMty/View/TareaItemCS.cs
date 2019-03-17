using System;

using Xamarin.Forms;

namespace DevMty
{
    public class TareaItemCS : ContentPage
    {
        public TareaItemCS()
        {
            Title = "Tarea a Agregar";
            // Initialize the all components
            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var notesEntry = new Entry();
            notesEntry.SetBinding(Entry.TextProperty, "Notes");

            // Initialize and set minimum date as the current date 
            var dateStart = new DatePicker
            {
                MinimumDate = DateTime.Now,
            };

            dateStart.SetBinding(DatePicker.DateProperty, "StartDate");

            // Initialize and set minimum date as one day after the start date 
            var dateEnd = new DatePicker
            {
                MinimumDate = dateStart.Date.AddDays(1)
            };

            // Update endData minimum every time we change the start date so that startDate is never greater than
            // end date
            dateStart.DateSelected += (sender, e) =>
            {
                dateEnd.MinimumDate = e.NewDate.AddDays(1);
            };
            dateEnd.SetBinding(DatePicker.DateProperty, "EndDate");

            // Initialize the switch that will determine if a task is done or not.
            var doneSwitch = new Switch();
            doneSwitch.SetBinding(Switch.IsToggledProperty, "Done");

            // Initialize Save Button as well as event handlers
            var saveButton = new Button { Text = "Guardar" };
            saveButton.Clicked += async (sender, e) =>
            {
                var todoItem = (Models.Tarea)BindingContext;
                todoItem.StartDate = dateStart.Date;
                todoItem.EndDate = dateEnd.Date;
                await App.Database.SaveItemAsync(todoItem);
                await App.TodoManager.SaveTaskAsync(todoItem);
                await Navigation.PopAsync();
            };
            // Initialize Delelete Button as well as event handlers
            var deleteButton = new Button { Text = "Borrar" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var todoItem = (Models.Tarea)BindingContext;
                await App.Database.DeleteItemAsync(todoItem);
                await App.TodoManager.DeleteTaskAsync(todoItem);
                await Navigation.PopAsync();
            };
            // Initialize Cancel Button as well as event handlers
            var cancelButton = new Button { Text = "Cancelar" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };

            // Create Content View with all the components
            var content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Nombre" },
                    nameEntry,
                    new Label { Text = "Descripcion" },
                    notesEntry,
                    new Label { Text = "Dia de Inicio" },
                    dateStart,
                    new Label { Text = "Dia de Fin" },
                    dateEnd,
                    new Label { Text = "Terminar" },
                    doneSwitch,
                    saveButton,
                    deleteButton,
                    cancelButton
                }
            };

            // Set main view as a scroll view with the content inside.
            Content = new ScrollView
            {
                Content = content
            };

        }

    }

}