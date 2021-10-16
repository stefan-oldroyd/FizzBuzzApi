using System;
using Microsoft.Data.Sqlite;
using System.Data;


namespace Rules
{
    public class SqLiteBaseRepository
    {

        public string DbPath { get; private set; }

        public SqLiteBaseRepository()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Rules.db";
        }

        public IDbConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbPath);
        }

    }
}