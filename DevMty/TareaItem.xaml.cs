using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DevMty
{
    public partial class TareaItemPage : ContentPage
    {


        public TareaItemPage()
        {
           // InitializeComponent();
        }
        // 
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (Models.Tarea)BindingContext;
            await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (Models.Tarea)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
