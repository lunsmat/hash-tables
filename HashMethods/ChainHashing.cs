using System;

namespace HashMethods
{
    public class ChainHashing(int tableSize) : IHashMethod
    {
        
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
            for (int i = 0; i < table.Length; i++) {
                var node = table[i];
                Console.Write($"{i}: ");

                while (node != null) {
                    Console.Write($"{node.Key} ");
                    node = node.Next;
                }

                Console.WriteLine();
            }
        }

        private int Hash(int key)
        {
            return key % table.Length;
        }

        public void Insert(int key)
        {
            var hash = Hash(key);
            var node = table[hash];

            if (node == null)
                table[hash] = new Node { Key = key };
            else {
                while (node.Next != null)
                    node = node.Next;

                node.Next = new Node { Key = key };
            }

            Console.WriteLine($"Inserido \"{key}\" em {hash}");
        }

        public void Insert(string key)
        {
            Console.WriteLine($"Inserindo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Insert(Math.Abs(key.GetHashCode()));
        }

        public void Search(int key)
        {
            var hash = Hash(key);
            var node = table[hash];

            while (node != null && node.Key != key)
                node = node.Next;

            if (node == null)
                Console.WriteLine($"\"{key}\" não encontrado");
            else
                Console.WriteLine($"\"{key}\" encontrado em {hash}");
        }

        public void Search(string key)
        {
            Console.WriteLine($"Buscando \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Search(Math.Abs(key.GetHashCode()));
        }

        public void Remove(int key)
        {
            var hash = Hash(key);
            var node = table[hash];
            var previous = table[hash];

            while (node != null && node.Key != Math.Abs(key.GetHashCode())) {
                previous = node;
                node = node.Next;
            }

            if (node == null || previous == null) {
                Console.WriteLine($"\"{key}\" não encontrado");
                return;
            }

            if (node == previous)
                table[hash] = node.Next ?? null;
            else
                previous.Next = node.Next;

            Console.WriteLine($"\"{key}\" removido de {hash}");
        }

        public void Remove(string key)
        {
            Console.WriteLine($"Removendo \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Remove(Math.Abs(key.GetHashCode()));
        }

        public void Restructure(int newTableSize)
        {
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

            Console.WriteLine($"Tabela reestruturada para {newTableSize}");
        }

        public void Clear()
        {
            table = new Node[table.Length];
        }
    }
}
