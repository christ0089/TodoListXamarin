using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DevMty
{
    public partial class TareaListPage : ContentPage
    {
        public TareaListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetItemsNotDoneAsync();
        }
        // Actualiza lista cuando se agrega una tarea
        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TareaItemCS
            {
                BindingContext = new Models.Tarea()
            });
        }
        // Poder realizar una tarea desde la pagina principal
        async void OnImg_TapGestureRecognizerTapped(object sender, EventArgs args)
        {
                await (sender as Image).RotateTo(360, 720);
                var data = ((sender as Image).BindingContext as Models.Tarea);
                data.Done = true;
                await App.Database.SaveItemAsync(data);
                listView.ItemsSource = await App.Database.GetItemsNotDoneAsync();
        }


        // Navegacion a Pagina Tarea de la tarea seleccionada
        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TareaItemCS
                {
                    BindingContext = e.SelectedItem as Models.Tarea
                });
            }
        }
    }
}
