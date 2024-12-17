using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Task1
{
    internal class MainWindowVM : INotifyPropertyChanged
    {

        public MainWindowVM()
        {
            //При смене SelectYear обновить группировку по месяцам
            PropertyChanged += (obj, e) =>
            {
                if (e.PropertyName == "SelectYear")
                {
                    RefreshMonthsTable(SelectYear);
                }
                else if (e.PropertyName == "SelectCountry")
                {
                    RefreshRegionTable(SelectCountry);
                }
            };
            RefreshMonthsTable(SelectYear);
        }


        private ObservableCollection<CounterRow> countriesTable = DataSource.GroupByCountries().Result;
        /// <summary>
        /// Группировка по странам
        /// </summary>
        public ObservableCollection<CounterRow> CountriesTable
        {
            get
            {
                return countriesTable;
            }
        }

        private string selectCountry = string.Empty;
        public string SelectCountry
        {
            get
            {
                return selectCountry;
            }
            set
            {
                selectCountry = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectCountry"));
            }
        }

        private ObservableCollection<CounterRow> regionsTable = null;
        /// <summary>
        /// Группировка по регионам выбранной страны
        /// </summary>
        public ObservableCollection<CounterRow> RegionsTable
        {
            get
            {
                return regionsTable;
            }
            set
            {
                regionsTable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RegionsTable"));
            }
        }
        /// <summary>
        /// Обновить группировку
        /// </summary>
        /// <param name="country">фильтр по стране</param>
        public async void RefreshRegionTable(string country)
        {
            RegionsTable = await DataSource.GroupByRegions(country);
        }


        private int selectYear = 2018;
        public int SelectYear
        {
            get
            {
                return selectYear;
            }
            set
            {
                selectYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectYear"));
            }
        }

        private ObservableCollection<CounterRow> monthsTable = null;
        /// <summary>
        /// Группировка по месяцам выбранного года
        /// </summary>
        public ObservableCollection<CounterRow> MonthsTable
        {
            get
            {
                return monthsTable;
            }
            set
            {
                monthsTable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MonthsTable"));
            }
        }
        /// <summary>
        /// Обновить группировку
        /// </summary>
        /// <param name="year">фильтр по году</param>
        public async void RefreshMonthsTable(int year)
        {
            MonthsTable = await DataSource.GroupByMonths(year);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
