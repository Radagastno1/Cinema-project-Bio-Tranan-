// using Microsoft.Data.Sqlite;
// using Hangfire.Storage.SQLite;
// using Hangfire;
// using Hangfire.Storage;

// namespace Core.Data
// {
//     public class HangfireConfig : JobStorage
//     {
//         public override IStorageConnection GetConnection()
//         {
//             var connection = new SqliteConnection("Data Source=hangfire.db");
//             connection.Open();
//             return (IStorageConnection)new SQLiteStorage(connection.ConnectionString);
//         }

//         public override IMonitoringApi GetMonitoringApi()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }
