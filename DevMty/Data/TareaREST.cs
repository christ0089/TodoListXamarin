
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DevMty.Models;

namespace DevMty.Data
{
    public class TareaREST : IRestService
    {
        HttpClient client;

        public List<Tarea> Items { get; private set; }

        // Acualiza la lista del Cliente 
        public async Task<List<Tarea>> RefreshDataAsync()
        {
            Items = new List<Tarea>();

           // URL REST = localhost:5000/api/todo/
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {   
                //GET: Obitiene una lista completa de items del API
                var response = await client.GetAsync(uri);
                // Buena Respuesta del Servidor.
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Tarea>>(content);

                }
            }
            // Mala Respuesta del Servidor.
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
            }

            return Items;
        }
        // PUSH/PUT: Agrega los items a la base de datos del API
        public async Task SaveTodoItemAsync(Tarea item, bool isNewItem = false)
        {
            // URL REST = localhost:5000/api/todo/
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                // PUSH: Agrega los items a la base de datos del API
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                // PUT: Actualiza los items a la base de datos del API
                else
                {
                    response = await client.PutAsync(uri, content);
                }
                // Buena Respuesta del Servidor
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             TodoItem successfully saved.");
                }

            }
            // Mala Respuesta del Servidor.
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
            }
        }
        // DELETE: Borra los item de la base de datos del API
        public async Task DeleteTodoItemAsync(int id)
        {
            // RestUrl = http://localhost/api/todo/{0}
            var uri = new Uri(string.Format(Constants.RestUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);
                // Buena Respuesta del Servidor.
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             TowdoItem successfully deleted.");
                }

            }
            // Mala Respuesta del Servidor.
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
            }
        }
    }
}
