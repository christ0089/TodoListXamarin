using System;

using Xamarin.Forms;

namespace DevMty
{
    public class TareaItemCS : ContentPage
    {
        public TareaItemCS()
        {
            Title = "Tarea a Agregar";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var notesEntry = new Entry();
            notesEntry.SetBinding(Entry.TextProperty, "Notes");

            var dateStart = new DatePicker
            {
                MinimumDate = DateTime.Now,
            };

            dateStart.SetBinding(DatePicker.DateProperty, "StartDate");

            var dateEnd = new DatePicker
            {
                MinimumDate = dateStart.Date.AddDays(1)
            };


            dateStart.DateSelected += (sender, e) => {
                dateEnd.MinimumDate = e.NewDate.AddDays(1);
            };
  

            dateEnd.SetBinding(DatePicker.DateProperty, "EndDate");

            var doneSwitch = new Switch();
            doneSwitch.SetBinding(Switch.IsToggledProperty, "Done");

            var saveButton = new Button { Text = "Guardar" };
            saveButton.Clicked += async (sender, e) =>
            {
                var todoItem = (Models.Tarea)BindingContext;
                todoItem.StartDate = dateStart.Date;
                todoItem.EndDate = dateEnd.Date;
                await App.Database.SaveItemAsync(todoItem);
                await Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Borrar" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var todoItem = (Models.Tarea)BindingContext;
        
                await App.Database.DeleteItemAsync(todoItem);
                await Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Cancelar" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };
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
            Content = new ScrollView
            {
                Content = content
            };

        }

    }
}

