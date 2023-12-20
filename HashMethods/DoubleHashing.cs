namespace HashMethods
{
    public class DoubleHashing(int tableSize): IHashMethod
    {
        private int[] table = new int[tableSize];

        public string GetHashName()
        {
            return "Hashing duplo";
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

        private int Hash2(int key)
        {
            return 1 + (key % (table.Length - 1));
        }

        public void Insert(int key)
        {
            var hash = Hash(key);
            var hash2 = Hash2(key);

            if (table[hash] == 0)
                table[hash] = key;
            else {
                while (table[hash] != 0)
                    hash = (hash + hash2) % table.Length;

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
            var hash2 = Hash2(key);

            if (table[hash] == key)
                table[hash] = 0;
            else {
                while (table[hash] != key)
                    hash = (hash + hash2) % table.Length;

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
            var hash2 = Hash2(key);

            if (table[hash] == key)
                Console.WriteLine($"\"{key}\" encontrado em {hash}");
            else {
                while (table[hash] != key)
                    hash = (hash + hash2) % table.Length;

                Console.WriteLine($"\"{key}\" encontrado em {hash}");
            }
        }

        public void Search(string key)
        {
            Console.WriteLine($"Buscando \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Search(Math.Abs(key.GetHashCode()));
        }

        public void Restructure(int newTableSize)
        {
            var oldTable = table;
            table = new int[newTableSize];

            foreach (var key in oldTable)
                Insert(key);

            Console.WriteLine($"Tabela reestruturada para {newTableSize}");
        }

        public void Clear()
        {
            table = new int[table.Length];
        }
    }
}
