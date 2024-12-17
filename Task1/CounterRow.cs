namespace Task1
{
    /// <summary>
    /// Строка объекта и счётчика
    /// </summary>
    internal class CounterRow
    {
        public CounterRow(string obj, int count) {
            this.Object = obj;
            this.Count = count;
        }

        public string Object { get; private set; }
        public int Count { get; private set; }
    }
}
