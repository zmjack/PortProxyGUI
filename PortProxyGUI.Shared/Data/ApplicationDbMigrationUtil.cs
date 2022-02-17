using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PortProxyGUI.Data
{
    public class ApplicationDbMigrationUtil
    {
        public ApplicationDbScope DbScope { get; private set; }

        public ApplicationDbMigrationUtil(ApplicationDbScope context)
        {
            DbScope = context;
            EnsureHistoryTable();
            EnsureUpdateVersion();
        }

        public void EnsureHistoryTable()
        {
            if (!DbScope.SqlQuery($"SELECT * FROM sqlite_master WHERE type = 'table' AND name = '__history';").Any())
            {
                DbScope.UnsafeSql(@"CREATE TABLE __history ( MigrationId text PRIMARY KEY, ProductVersion text);");
                DbScope.UnsafeSql($"INSERT INTO __history (MigrationId, ProductVersion) VALUES ('000000000000', '0.0');");
            }
        }

        public void EnsureUpdateVersion()
        {
            var migration = GetLastMigration();
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (new Version(migration.ProductVersion) > assemblyVersion)
                throw new InvalidOperationException("The current software version cannot use the configuration. Please download the latest version (https://github.com/zmjack/PortProxyGUI).");
        }

        public Migration GetLastMigration()
        {
            return DbScope.SqlQuery<Migration>($"SELECT * FROM __history ORDER BY MigrationId DESC LIMIT 1;").First();
        }

        public void MigrateToLast()
        {
            var migration = GetLastMigration();
            var migrationId = migration.MigrationId;
            var pendingMigrations = migrationId != "000000000000"
                ? History.SkipWhile(pair => pair.Key.MigrationId != migrationId).Skip(1)
                : History;

            foreach (var pendingMigration in pendingMigrations)
            {
                foreach (var sql in pendingMigration.Value)
                {
                    DbScope.UnsafeSql(sql);
                }
                DbScope.Sql($"INSERT INTO __history (MigrationId, ProductVersion) VALUES ({pendingMigration.Key.MigrationId}, {pendingMigration.Key.ProductVersion});");
            }
        }

        public Dictionary<MigrationKey, string[]> History = new Dictionary<MigrationKey, string[]>
        {
            [new MigrationKey { MigrationId = "202103021542", ProductVersion = "1.1.0" }] = new[]
            {
                @"CREATE TABLE rules
(
    Id text PRIMARY KEY,
    Type text,
    ListenOn text,
    ListenPort integer,
    ConnectTo text,
    ConnectPort integer
);",
                "CREATE UNIQUE INDEX IX_Rules_Type_ListenOn_ListenPort ON Rules(Type, ListenOn, ListenPort);",
            },

            [new MigrationKey { MigrationId = "202201172103", ProductVersion = "1.2.0" }] = new[]
            {
                "ALTER TABLE rules ADD Note text;",
                "ALTER TABLE rules ADD `Group` text;",
            },
        };
    }
}
