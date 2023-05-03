using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CitamaniaPOC.Data.Repositories.Core
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
    {
        protected readonly TelemetryClient TelemetryClient;
        protected readonly string TableName;
        protected readonly string Schema;
        private readonly string _connectionString;

        public GenericRepository(IConfiguration config, TelemetryConfiguration telemetryConfig, string schema = "dbo", string? tableName = null)
        {
            this.TelemetryClient = new TelemetryClient(telemetryConfig);
            Schema = schema;
            TableName = tableName ?? typeof(TEntity).Name + "s";
            _connectionString = config.GetConnectionString("CitamaniaPOC");
        }

        internal IDbConnection Connection => new SqlConnection(_connectionString);

        public virtual async Task<IEnumerable<TEntity>?> GetAll(bool? active)
        {
            string activeWhere = "";
            if (active != null)
            {
                string activeString = active.Value ? "1" : "0";
                //activeWhere = typeof(IActive).IsAssignableFrom(typeof(TEntity)) ? $"where P.Active = {activeString}" : "";
            }

            try
            {
                using var dbConnection = Connection;
                dbConnection.Open();
                return await dbConnection.QueryAsync<TEntity>($"SELECT * FROM {Schema}.{TableName} as P {activeWhere}");
            }
            catch (Exception e)
            {
                TelemetryClient.TrackException(e);
                throw;
            }
        }

        public virtual async Task<TEntity?> GetByPk(params long[] keyValues)
        {
            try
            {
                using var dbConnection = new SqlConnection(_connectionString);
                var (where, parameters) = GetWhereAndParametersForPrimaryKey(keyValues: keyValues);
                var query = $"SELECT * FROM {Schema}.{TableName} {where}";
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<TEntity?>(query, parameters);
            }
            catch (Exception e)
            {
                TelemetryClient.TrackException(e);
                throw;
            }
        }

        protected (string Where, DynamicParameters parameters) GetWhereAndParametersForPrimaryKey(string tableAlias = "", params long[] keyValues)
        {
            var parameters = new DynamicParameters();
            var queryWhere = "";
            var count = 0;
            var entityType = typeof(TEntity);
            foreach (var field in from PropertyInfo? field in entityType.GetProperties()
                                  let keyAttribute = field.GetCustomAttributes(typeof(KeyAttribute), false)?.Cast<KeyAttribute>()?.FirstOrDefault()
                                  where keyAttribute is not null
                                  select field)
            {
                parameters.Add(field.Name, keyValues[count]);
                queryWhere += queryWhere == "" ? "where " : "";
                var queryAnd = count > 0 ? " and " : "";
                queryWhere += $"{queryAnd} {tableAlias}{(tableAlias == "" ? "" : ".")}{field.Name}=@{field.Name} ";
                count++;
            }

            return (queryWhere, parameters);
        }

        public virtual async Task Create(TEntity entity, string user)
        {
            using var dbConnection = new SqlConnection(_connectionString);
            var result = GetInsertQuery(entity, user);
            dbConnection.Open();
            await dbConnection.ExecuteAsync(result.Query, result.Parameters);
        }

        public virtual async Task Update(TEntity entity, string user, params long[] keyValues)
        {
            using var dbConnection = new SqlConnection(_connectionString);
            var result = GetUpdateQuery(entity, user, keyValues);
            dbConnection.Open();
            await dbConnection.ExecuteAsync(result.Query, result.Parameters);
        }

        public Task Delete(string user, params long[] keyValues)
        {
            throw new NotImplementedException();
        }

        private (string Query, DynamicParameters Parameters) GetInsertQuery(TEntity entity, string user)
        {
            var parameters = new DynamicParameters();
            var entityType = typeof(TEntity);
            string fieldList = "";
            string parameterList = "";
            var columns = (from PropertyInfo? field in entityType.GetProperties()
                           let identityAttribute = field.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false)
                               ?.Cast<DatabaseGeneratedAttribute>()?.FirstOrDefault()
                           where identityAttribute is null
                           select field);
            var filteredColumns = columns.Where(x => x.PropertyType.FullName != null && !x.PropertyType.FullName.StartsWith("MockInterview.Domain.Models"));
            int totalColumns = filteredColumns.Count();
            int count = 0;
            foreach (var field in filteredColumns)
            {
                count++;
                Object? value = field.GetValue(entity, null);
                string column = field.Name;
                switch (field.Name)
                {
                    case "CreatedOn":
                        value = DateTime.Now;
                        break;

                    case "CreatedBy":
                        value = user;
                        break;

                    case "ModifiedOn":
                    case "ModifiedBy":
                        value = null;
                        break;
                }

                parameters.Add(column, value);
                var endLine = count == totalColumns ? string.Empty : ",\n";
                fieldList += $"    {column}{endLine}";
                parameterList += $"    @{column}{endLine}";
            }
            var insert = $"INSERT INTO {Schema}.{TableName}\n({fieldList})\n    VALUES\n({parameterList})";
            return (insert, parameters);
        }

        private (string Query, DynamicParameters Parameters) GetUpdateQuery(TEntity entity, string user, params long[] keyValues)
        {
            var parameters = new DynamicParameters();
            var entityType = typeof(TEntity);
            string fieldList = "";
            var columns = (from PropertyInfo? field in entityType.GetProperties()
                           let identityAttribute = field.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false)
                               ?.Cast<DatabaseGeneratedAttribute>()?.FirstOrDefault()
                           where identityAttribute is null
                           let keyAttribute = field.GetCustomAttributes(typeof(KeyAttribute), false)
                               ?.Cast<KeyAttribute>()?.FirstOrDefault()
                           where keyAttribute is null
                           select field);
            var filteredColumns = columns.Where(x => x.PropertyType.FullName != null && !x.PropertyType.FullName.StartsWith("MockInterview.Domain.Models"));
            int totalColumns = filteredColumns.Count();
            int count = 0;
            foreach (var field in filteredColumns)
            {
                count++;
                Object? value = field.GetValue(entity, null);
                string column = field.Name;
                switch (field.Name)
                {
                    case "CreatedOn":
                        value = DateTime.Now;
                        break;

                    case "CreatedBy":
                        value = user;
                        break;

                    case "ModifiedOn":
                        value = DateTime.Now;
                        break;

                    case "ModifiedBy":
                        value = user;
                        break;
                }

                if (column != "CreatedOn" && column != "CreatedBy")
                {
                    parameters.Add(column, value);
                    var endLine = count == totalColumns ? string.Empty : ",\n";
                    fieldList += $"    {column}=@{column}{endLine}";
                }
            }

            var resultWhere = GetWhereAndParametersForPrimaryKey(keyValues: keyValues);
            //Add where parameters to current parametners
            parameters.AddDynamicParams(resultWhere.parameters);
            var update = $"UPDATE {Schema}.{TableName}\nSET\n{fieldList}\n{resultWhere.Where}";
            return (update, parameters);
        }
    }
}