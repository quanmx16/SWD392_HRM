using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Utility
{
    public class CustomIdGenerator : ValueGenerator<string>
    {
        private static int _nextNumber;
        private static readonly object _lock = new object();
        private string _prefix { get; set; }
        public CustomIdGenerator(string prefix)
        {
            _prefix = prefix;
            _nextNumber = GetLastGeneratedNumber();
        }
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            lock (_lock)
            {
                string prefix = _prefix;
                string nextId = $"{prefix}{_nextNumber:D4}";
                _nextNumber++;
                SaveLastGeneratedNumber();
                return nextId;
            }
        }

        private void SaveLastGeneratedNumber()
        {
            string filePath = $"{_prefix}_lastNumber.txt";
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write((_nextNumber + 1).ToString());
            }
        }


        private int GetLastGeneratedNumber()
        {
            // Retrieve the last generated number from the persistence storage
            string filePath = $"{_prefix}_lastNumber.txt";
            if (File.Exists(filePath))
            {
                string lastNumberStr = File.ReadAllText(filePath);
                if (int.TryParse(lastNumberStr, out int lastNumber))
                {
                    return lastNumber;
                }
            }
            return 0; // Default value if file or value parsing fails
        }
    }

}
