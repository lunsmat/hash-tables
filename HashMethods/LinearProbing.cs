using System.Diagnostics;

namespace HashMethods
{
    public class LinearProbing(int tableSize): IHashMethod
    {
        private int collisions;
        private Stopwatch stopwatch = new();

        private int[] table = new int[tableSize];

        public string GetHashName()
        {
            return "Sondagem linear";
        }

        public void Print()
        {
            stopwatch.Start();

            for (int i = 0; i < table.Length; i++)
                Console.WriteLine($"{i}: {table[i]}");

            stopwatch.Stop();
            Console.WriteLine($"Print Executou em ${stopwatch.Elapsed.Milliseconds}ms");
            stopwatch.Restart();
        }

        private int Hash(int key)
        {
            return key % table.Length;
        }

        public void Insert(int key)
        {
            collisions = 0;
            stopwatch.Start();

            var hash = Hash(key);

            if (table[hash] == 0)
                table[hash] = key;
            else {
                while (table[hash] != 0) {
                    hash = (hash + 1) % table.Length;
                    collisions += 1;
                }

                table[hash] = key;
            }

            stopwatch.Stop();

            Console.WriteLine($"Inserido \"{key}\" em {hash}");
            Console.WriteLine($"Inserido em {stopwatch.Elapsed.Milliseconds}ms com um total de {collisions} colisões");

            stopwatch.Restart();
        }

        public void Insert(string key)
        {
            Console.WriteLine($"Inserindo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Insert(Math.Abs(key.GetHashCode()));
        }

        public void Remove(int key)
        {
            stopwatch.Start();
            collisions = 0;

            var hash = Hash(key);

            if (table[hash] == key)
                table[hash] = 0;
            else {
                while (table[hash] != key) {
                    hash = (hash + 1) % table.Length;
                    collisions += 1;
                }

                table[hash] = 0;
            }

            stopwatch.Stop();
            Console.WriteLine($"Removido \"{key}\" de {hash}");
            Console.WriteLine($"Removido em {stopwatch.Elapsed.Milliseconds}ms com um total de {collisions} colisões");
            stopwatch.Restart();
        }

        public void Remove(string key)
        {
            Console.WriteLine($"Removendo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Remove(Math.Abs(key.GetHashCode()));
        }

        public void Search(int key)
        {
            collisions = 0;
            stopwatch.Start();
            var hash = Hash(key);

            if (table[hash] == key)
                Console.WriteLine($"Encontrado \"{key}\" em {hash}");
            else {
                while (table[hash] != key) {
                    hash = (hash + 1) % table.Length;
                    collisions += 1;
                }

                Console.WriteLine($"Encontrado \"{key}\" em {hash}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Buscado em {stopwatch.Elapsed.Milliseconds}ms com um total de {collisions} colisões");
            stopwatch.Restart();
        }

        public void Search(string key)
        {
            Console.WriteLine($"Procurando \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Search(Math.Abs(key.GetHashCode()));
        }

        public void Restructure(int newTableSize)
        {
            collisions = 0;
            var restructureStopWatch = new Stopwatch();
            restructureStopWatch.Start();

            var newTable = new int[newTableSize];

            for (int i = 0; i < table.Length; i++) {
                if (table[i] == 0)
                    continue;

                var hash = table[i] % newTableSize;

                if (newTable[hash] == 0)
                    newTable[hash] = table[i];
                else {
                    while (newTable[hash] != 0) {
                        hash = (hash + 1) % newTableSize;
                        collisions += 1;
                    }

                    newTable[hash] = table[i];
                }
            }

            table = newTable;

            restructureStopWatch.Stop();
            
            Console.WriteLine($"Tabela reestruturada para {newTableSize}");
            Console.WriteLine($"Reestruturado em {stopwatch.Elapsed.Milliseconds}ms com {collisions} colisões");
        }

        public void Clear()
        {
            table = new int[table.Length];
        }
    }
}
