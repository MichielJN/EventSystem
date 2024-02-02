using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Abstractions;
using Microsoft.Extensions.Options;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace EventSystem.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : TableData, new()
    {
        SQLiteConnection connection;
        public string? StatusMessage { get; set; }

        public BaseRepository()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            connection.CreateTable<T>();
        }

        public void SaveEntity(T entity)
        {
            int result = 0;
            if (entity != null)
            {
                try
                {
                    if(entity.Id != 0)
                    {
                        result = connection.Update(entity);
                        StatusMessage = $"{result} row(s) updated";
                    }
                    else
                    {
                        result = connection.Insert(entity);
                        StatusMessage = $"{result} row(s) added";
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error {ex.Message}";
                }
            }
        }

        public void DeleteEntity(T entity) 
        {
            try
            {
                connection.Delete(entity);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error {ex.Message}";
            }
        }

        public T? GetEntity(int id)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error {ex.Message}";
            }
            return null;
        }

        public List<T>? GetEntities()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch(Exception ex)
            {
                StatusMessage = $"Error {ex.Message}";
            }
            return null;
        }

        public void SaveEntityWithChildren(T? entity, bool recursive = false)
        {
            int result = 0;
            if (entity != null)
            {
                try
                {
                    if (entity.Id != 0)
                    {
                        connection.UpdateWithChildren(entity);
                        StatusMessage = $"{result} row(s) updated";
                    }
                    else
                    {
                        connection.InsertWithChildren(entity, recursive);
                        StatusMessage = $"{result} row(s) added";
                   }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error: {ex.Message}";
                }
            }
            //connection.InsertWithChildren(entity, recursive);
        }

        public T? GetEntityWithChildren(int id)
        {
            try
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public List<T>? GetEntitiesWithChildren()
        {
            try
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            catch(Exception ex) 
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public void DeleteEntityWithChildren(T? entity)
        {
            try
            {
                connection.Delete(entity, true);
            }
            catch(Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Dispose();   
        }

    }
}
