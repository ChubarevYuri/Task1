using Devart.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Task1
{
    internal class DataSource
    {
        private static async Task<ObservableCollection<CounterRow>> Request(string request)
        {
            using (var db = new SQLiteConnection(string.Format(
                "Data Source={0};Encryption = SQLCipher;Password={1};", Properties.Resources.dbPath, Properties.Resources.dbPassword
                )))
            {
                try
                {
                    //Открыть БД
                    if (db.State != System.Data.ConnectionState.Open)
                    {
                        await db.OpenAsync();
                    }
                    //Запрос на выборку
                    SQLiteCommand command = new SQLiteCommand(request, db);
                    ObservableCollection<CounterRow> result = new ObservableCollection<CounterRow>();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //Сохранить все записи
                                result.Add(new CounterRow(reader.GetString(0), reader.GetInt32(1)));
                            }
                        }
                    }
                    return result;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static async Task<ObservableCollection<CounterRow>> GroupByCountries()
        {
            return await Request(
            "SELECT country, COUNT(*) FROM user WHERE is_owner = 1 AND confirmed = 1 GROUP BY country");
        }

        public static async Task<ObservableCollection<CounterRow>> GroupByRegions(string country)
        {
            return await Request(string.Format(
                            "SELECT region, COUNT(*) FROM user WHERE is_owner = 1 AND confirmed = 1 AND country LIKE '{0}' GROUP BY region",
                            country));
        }

        public static async Task<ObservableCollection<CounterRow>> GroupByMonths(int year)
        {
            return await Request(string.Format(
                            "SELECT SUBSTR(purchase_date, 4, 2), COUNT(*) FROM user " +
                            "WHERE is_owner = 1 AND confirmed = 1 AND SUBSTR(purchase_date, 7) LIKE '{0}' " +
                            "GROUP BY SUBSTR(purchase_date, 4, 2)",
                            year.ToString()));
        }
    }
}
