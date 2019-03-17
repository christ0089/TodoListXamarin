
using System;
using System.IO;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevMty.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DevMty
{
    public partial class App : Application
    {

        static TareaBaseDatos database;
        public static TodoItemManager TodoManager { get; private set; }

        public App()
        {
            InitializeComponent();
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new TareaListPage());
            TodoManager = new TodoItemManager(new TareaREST());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        public static TareaBaseDatos Database
        {
            get
            {
                if (database == null)
                {
                    database = new TareaBaseDatos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
                }
                return database;
            }
        }


        public int ResumeAtTodoId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
