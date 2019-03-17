using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevMty.Models;
namespace DevMty.Data
{
        // Interface del API a Implementar
        public interface IRestService
        {
            Task<List<Tarea>> RefreshDataAsync();

            Task SaveTodoItemAsync(Tarea item, bool isNewItem);

            Task DeleteTodoItemAsync(int id);
        }


}
