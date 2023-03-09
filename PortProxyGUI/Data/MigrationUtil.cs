using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PortProxyGUI.Data
{
    public class MigrationUtil
    {
        public ApplicationDbScope DbScope { get; private set; }

        public MigrationUtil(ApplicationDbScope context)
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
            var migration = DbScope.GetLastMigration();
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (new Version(migration.ProductVersion) > assemblyVersion)
            {
                if (MessageBox.Show(@"The current software version cannot use the configuration.

You need to use a newer version of PortProxyGUI.

Would you like to download it now?", "Upgrade", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", "https://github.com/zmjack/PortProxyGUI/releases");
                }

                Environment.Exit(0);
            }
        }

        public void MigrateToLast()
        {
            var migration = DbScope.GetLastMigration();
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

            [new MigrationKey { MigrationId = "202202221635", ProductVersion = "1.3.0" }] = new[]
            {
                "ALTER TABLE rules RENAME TO rulesOld;",
                "DROP INDEX IX_Rules_Type_ListenOn_ListenPort;",

                @"CREATE TABLE rules (
	Id text PRIMARY KEY,
	Type text,
	ListenOn text,
	ListenPort integer,
	ConnectTo text,
	ConnectPort integer,
	Comment text,
	`Group` text 
);",
                "CREATE UNIQUE INDEX IX_Rules_Type_ListenOn_ListenPort ON Rules ( Type, ListenOn, ListenPort );",

                "INSERT INTO rules SELECT Id, Type, ListenOn, ListenPort, ConnectTo, ConnectPort, Note, `Group` FROM rulesOld;",
                "DROP TABLE rulesOld;",
            },

            [new MigrationKey { MigrationId = "202303092024", ProductVersion = "1.4.0" }] = new[]
            {
                @"CREATE TABLE configs (
	Item text,
	`Key` text,
	Value text
);",

"CREATE UNIQUE INDEX IX_Configs_Key ON configs ( Item, `Key` );",

"INSERT INTO configs ( Item, `Key`, Value ) VALUES ( 'MainWindow', 'Width', '720' );",
"INSERT INTO configs ( Item, `Key`, Value ) VALUES ( 'MainWindow', 'Height', '500' );",
"INSERT INTO configs ( Item, `Key`, Value ) VALUES ( 'PortProxy', 'ColumnWidths', '[24, 64, 140, 100, 140, 100, 100]' );",
            },
        };
    }
}
