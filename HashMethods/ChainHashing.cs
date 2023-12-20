using System;
using System.Diagnostics;

namespace HashMethods
{
    public class ChainHashing(int tableSize) : IHashMethod
    {
        private int collisions;
        private Stopwatch stopwatch = new();

        internal class Node 
        {
            public int Key { get; set; }
            public Node? Next { get; set; }
        }

        private Node?[] table = new Node[tableSize];

        public string GetHashName()
        {
            return "Hashing com encadeamento";
        }

        public void Print()
        {
            stopwatch.Start();

            for (int i = 0; i < table.Length; i++) {
                var node = table[i];
                Console.Write($"{i}: ");

                while (node != null) {
                    Console.Write($"{node.Key} ");
                    node = node.Next;
                }

                Console.WriteLine();
            }

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
            var node = table[hash];

            if (node == null)
                table[hash] = new Node { Key = key };
            else {
                while (node.Next != null)
                {
                    node = node.Next;
                    collisions += 1;
                }
                
                node.Next = new Node { Key = key };
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

        public void Search(int key)
        {
            collisions = 0;
            stopwatch.Start();

            var hash = Hash(key);
            var node = table[hash];

            while (node != null && node.Key != key) {
                node = node.Next;
                collisions += 1;
            }
            
            stopwatch.Stop();

            if (node == null)
                Console.WriteLine($"\"{key}\" não encontrado");
            else
                Console.WriteLine($"\"{key}\" encontrado em {hash}");

            Console.WriteLine($"Buscado em {stopwatch.Elapsed.Milliseconds}ms com um total de {collisions} colisões");
            stopwatch.Restart();
        }

        public void Search(string key)
        {
            Console.WriteLine($"Buscando \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Search(Math.Abs(key.GetHashCode()));
        }

        public void Remove(int key)
        {
            stopwatch.Start();
            collisions = 0;
            bool found;

            var hash = Hash(key);
            var node = table[hash];
            var previous = table[hash];

            while (node != null && node.Key != Math.Abs(key.GetHashCode())) {
                previous = node;
                node = node.Next;
                collisions += 1;
            }

            
            if (node == null || previous == null) {
                found = false;
            } else {
                found = true;
                if (node == previous)
                    table[hash] = node.Next ?? null;
                else
                    previous.Next = node.Next;
            }

            stopwatch.Stop();
            if (!found)
                Console.WriteLine($"\"{key}\" não encontrado");
            else
                Console.WriteLine($"\"{key}\" removido de {hash}");

            stopwatch.Restart();
            Console.WriteLine($"Removido em {stopwatch.Elapsed.Milliseconds}ms com um total de {collisions} colisões");
        }

        public void Remove(string key)
        {
            Console.WriteLine($"Removendo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Remove(Math.Abs(key.GetHashCode()));
        }

        public void Restructure(int newTableSize)
        {
            var restructureStopWatch = new Stopwatch();

            restructureStopWatch.Start();

            var oldTable = table;
            table = new Node[newTableSize];

            foreach (var register in oldTable) {
                var node = register;

                while (node != null) {
                    var next = node.Next;
                    Insert(node.Key);
                    node = next;
                }
            }

            restructureStopWatch.Stop();
            
            Console.WriteLine($"Tabela reestruturada para {newTableSize}");
            Console.WriteLine($"Reestruturado em {stopwatch.Elapsed.Milliseconds}ms");
        }

        public void Clear()
        {
            table = new Node[table.Length];
        }
    }
}
