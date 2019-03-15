
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DevMty
{
    // Clase TareeBaseDatos maneja la insercion, borrado y lectura de base de datos SQLite
    public class TareaBaseDatos
    {
        readonly SQLiteAsyncConnection database;
        // Inicializa la base de datos.
        public TareaBaseDatos(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Models.Tarea>().Wait();
        }
        // Regresa los items que estan guardados en la base de datos
        public Task<List<Models.Tarea>> GetItemsAsync()
        {
            return database.Table<Models.Tarea>().ToListAsync();
        }
        // Regresa los items que estan guardados en la base con un query de Done igual a cero o terminada.
        public Task<List<Models.Tarea>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Models.Tarea>("SELECT * FROM [Tarea] WHERE [Done] = 0");
        }
        // Regresa un item en especifico basada en el id.
        public Task<Models.Tarea> GetItemAsync(int id)
        {
            return database.Table<Models.Tarea>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        // Regresa la tabla actualizada he inserta un item si la tabla esta vacia o actualiza la tabla de la base de datos con el nuevo item.
        public Task<int> SaveItemAsync(Models.Tarea item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        // Regresa la base de datos sin el item que fue borrado.
        public Task<int> DeleteItemAsync(Models.Tarea item)
        {
            return database.DeleteAsync(item);
        }

    }
}
