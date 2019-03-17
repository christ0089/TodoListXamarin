using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DevMty
{
    public partial class TareaListPage : ContentPage
    {
        private bool alertShown = false;
        public TareaListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetItemsNotDoneAsync();

            // Checar coneccion con el Backend
            if (Constants.RestUrl.Contains("localhost"))
            {
                if (!alertShown)
                {
                    await DisplayAlert(
                        "Hosted Back-End",
                        "App is Connected",
                        "OK");
                    alertShown = true;
                }
            }

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
                await App.Database.SaveItemAsync(data);
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
