using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DevMty.Data;
using DevMty.Models;
namespace DevMty.Data
{
    public class TodoItemManager
    {
        IRestService restService;

        public TodoItemManager(IRestService service)
        {
            restService = service;
        }
        // GET: Obtiene los items de la base de datos del API
        public Task<List<Tarea>> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }
        // PUSH/PUT: Agrega los items a la base de datos del API
        public Task SaveTaskAsync(Tarea item, bool isNewItem = false)
        {
            return restService.SaveTodoItemAsync(item, isNewItem);
        }
        // DELETE: Borra un items a la base de datos del API
        public Task DeleteTaskAsync(Tarea item)
        {
            return restService.DeleteTodoItemAsync(item.ID);
        }
    }

}
