using System.Collections.Generic;

namespace ProductionMan.Desktop.Controls.MainParts
{
    public class InMemoryDataStore
    {
        private readonly Dictionary<string, object> _database;

        public InMemoryDataStore()
        {
            _database = new Dictionary<string, object>();
        }

        internal void Set(string storeName, object value)
        {
            if (_database.ContainsKey(storeName))
            {
                _database[storeName] = value;
            }
            else
            {
                _database.Add(storeName, value);
            }
        }

        internal object Get(string storeName)
        {
            if (_database.ContainsKey(storeName))
            {
                return _database[storeName];
            }

            return null;
        }
    }
}
