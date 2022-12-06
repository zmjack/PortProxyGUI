using SQLib.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PortProxyGUI.Data
{
    public class ApplicationDbScope : SqliteScope<ApplicationDbScope>
    {
        public static readonly string DbDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PortProxyGUI");
        public static readonly string DbFile = Path.Combine(DbDirectory, "config.db");
        private static readonly string ConnectionString = $"Data Source={DbFile}";

        public static ApplicationDbScope UseDefault() => new ApplicationDbScope(ConnectionString);

        public ApplicationDbScope(string connectionString) : base(connectionString)
        {
        }

        public override void Initialize()
        {
            if (!Directory.Exists(DbDirectory)) Directory.CreateDirectory(DbDirectory);
            if (!File.Exists(DbFile))
            {
#if NET35 || NET45
                System.Data.SQLite.SQLiteConnection.CreateFile(DbFile);
#endif
            }
        }

        public void Migrate() => new ApplicationDbMigrationUtil(this).MigrateToLast();

        public IEnumerable<Rule> Rules => SqlQuery<Rule>($"SELECT * FROM Rules;");

        public Rule GetRule(string type, string listenOn, int listenPort)
        {
            return SqlQuery<Rule>($"SELECT * FROM Rules WHERE Type={type} AND ListenOn={listenOn} AND ListenPort={listenPort} LIMIT 1;").FirstOrDefault();
        }

        public void Add<T>(T obj) where T : class
        {
            var newid = Guid.NewGuid().ToString();
            if (obj is Rule rule)
            {
                Sql($"INSERT INTO Rules (Id, Type, ListenOn, ListenPort, ConnectTo, ConnectPort, Comment, `Group`) VALUES ({newid}, {rule.Type}, {rule.ListenOn}, {rule.ListenPort}, {rule.ConnectTo}, {rule.ConnectPort}, {rule.Comment ?? ""}, {rule.Group ?? ""});");
                rule.Id = newid;
            }
            else throw new NotSupportedException($"Adding {obj.GetType().FullName} is not supported.");
        }
        public void AddRange<T>(IEnumerable<T> objs) where T : class
        {
            foreach (var obj in objs) Add(obj);
        }

        public void Update<T>(T obj) where T : class
        {
            if (obj is Rule rule)
            {
                Sql($"UPDATE Rules SET Type={rule.Type}, ListenOn={rule.ListenOn}, ListenPort={rule.ListenPort}, ConnectTo={rule.ConnectTo}, ConnectPort={rule.ConnectPort} WHERE Id={rule.Id};");
            }
            else throw new NotSupportedException($"Updating {obj.GetType().FullName} is not supported.");
        }
        public void UpdateRange<T>(IEnumerable<T> objs) where T : class
        {
            foreach (var obj in objs) Update(obj);
        }

        public void Remove<T>(T obj) where T : class
        {
            if (obj is Rule rule)
            {
                Sql($"DELETE FROM Rules WHERE Id={rule.Id};");
            }
            else throw new NotSupportedException($"Removing {obj.GetType().FullName} is not supported.");
        }
        public void RemoveRange<T>(IEnumerable<T> objs) where T : class
        {
            foreach (var obj in objs) Remove(obj);
        }

    }
}
