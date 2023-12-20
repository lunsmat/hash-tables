namespace HashMethods
{
    public class QuadraticProbing(int tableSize): IHashMethod
    {
        private int[] table = new int[tableSize];

        public string GetHashName()
        {
            return "Sondagem quadrática";
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
                var i = 1;

                while (table[hash] != 0) {
                    hash = (hash + i * i) % table.Length;
                    i++;
                }

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
                var i = 1;

                while (table[hash] != key) {
                    hash = (hash + i * i) % table.Length;
                    i++;
                }

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
                Console.WriteLine($"\"{key}\" encontrado em {hash}");
            else {
                var i = 1;

                while (table[hash] != key) {
                    hash = (hash + i * i) % table.Length;
                    i++;
                }

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

            for (int i = 0; i < oldTable.Length; i++) {
                if (oldTable[i] == 0)
                    continue;

                var hash = oldTable[i] % newTableSize;
                var j = 1;

                while (table[hash] != 0) {
                    hash = (hash + j * j) % table.Length;
                    j++;
                }

                table[hash] = oldTable[i];
            }

            Console.WriteLine($"Tabela reestruturada para {newTableSize}");
        }

        public void Clear()
        {
            table = new int[table.Length];
        }
    }
}
