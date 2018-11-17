using DndHelperApiDal.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DndHelperApiDal.Repositories
{
    public interface IRepository
    {
        Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> queryDatabase);
        Task<T> UpsertAsync<T>(Func<IDbConnection, SqlTransaction, Task<T>> queryDatabase);
        int Upsert(Func<IDbConnection, SqlTransaction, int> queryDatabase);
        T Query<T>(Func<IDbConnection, T> queryDatabase);
    }

    public class Repository : IRepository
    {
        private readonly ConnectionConfiguration _connectionConfiguration;
        private const string SqlTimeoutErrorMessage = "experienced a SQL timeout";
        private const string SqlExceptionErrorMessage = "experienced a SQL exception (not a timeout)";

        public Repository(IOptions<ConnectionConfiguration> connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration.Value;
        }

        public async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionConfiguration.DndHelperConnectionString))
                {
                    await connection.OpenAsync();
                    return await queryDatabase(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }

        //TODO: is this really upsert?
        public async Task<T> UpsertAsync<T>(Func<IDbConnection, SqlTransaction, Task<T>> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionConfiguration.DndHelperConnectionString))
                {
                    await connection.OpenAsync();
                    var sqlTransaction = connection.BeginTransaction();
                    var result = await queryDatabase(connection, sqlTransaction);
                    sqlTransaction.Commit();
                    return result;
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }

        public int Upsert(Func<IDbConnection, SqlTransaction, int> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionConfiguration.DndHelperConnectionString))
                {
                    connection.Open();
                    var sqlTransaction = connection.BeginTransaction();
                    var result = queryDatabase(connection, sqlTransaction);
                    sqlTransaction.Commit();
                    return result;
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }

        public T Query<T>(Func<IDbConnection, T> queryDatabase)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionConfiguration.DndHelperConnectionString))
                {
                    connection.Open();
                    return queryDatabase(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlTimeoutErrorMessage}"), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(($"{GetType().FullName} {SqlExceptionErrorMessage}"), ex);
            }
        }
    }
}
