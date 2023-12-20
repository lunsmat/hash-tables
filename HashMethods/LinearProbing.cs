namespace HashMethods
{
    public class LinearProbing(int tableSize): IHashMethod
    {
        private int[] table = new int[tableSize];

        public string GetHashName()
        {
            return "Sondagem linear";
        }

        public void Print()
        {
            for (int i = 0; i < table.Length; i++)
                Console.WriteLine($"{i}: {table[i]}");
        }

        private int Hash(int key)
        {
            return key % table.Length;
        }

        public void Insert(int key)
        {
            var hash = Hash(key);

            if (table[hash] == 0)
                table[hash] = key;
            else {
                while (table[hash] != 0)
                    hash = (hash + 1) % table.Length;

                table[hash] = key;
            }

            Console.WriteLine($"Inserido \"{key}\" em {hash}");
        }

        public void Insert(string key)
        {
            Console.WriteLine($"Inserindo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Insert(Math.Abs(key.GetHashCode()));
        }

        public void Remove(int key)
        {
            var hash = Hash(key);

            if (table[hash] == key)
                table[hash] = 0;
            else {
                while (table[hash] != key)
                    hash = (hash + 1) % table.Length;

                table[hash] = 0;
            }

            Console.WriteLine($"Removido \"{key}\" de {hash}");
        }

        public void Remove(string key)
        {
            Console.WriteLine($"Removendo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Remove(Math.Abs(key.GetHashCode()));
        }

        public void Search(int key)
        {
            var hash = Hash(key);

            if (table[hash] == key)
                Console.WriteLine($"Encontrado \"{key}\" em {hash}");
            else {
                while (table[hash] != key)
                    hash = (hash + 1) % table.Length;

                Console.WriteLine($"Encontrado \"{key}\" em {hash}");
            }
        }

        public void Search(string key)
        {
            Console.WriteLine($"Procurando \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Search(Math.Abs(key.GetHashCode()));
        }

        public void Restructure(int newTableSize)
        {
            var newTable = new int[newTableSize];

            for (int i = 0; i < table.Length; i++) {
                if (table[i] == 0)
                    continue;

                var hash = table[i] % newTableSize;

                if (newTable[hash] == 0)
                    newTable[hash] = table[i];
                else {
                    while (newTable[hash] != 0)
                        hash = (hash + 1) % newTableSize;

                    newTable[hash] = table[i];
                }
            }

            table = newTable;
        }

        public void Clear()
        {
            table = new int[table.Length];
        }
    }
}
