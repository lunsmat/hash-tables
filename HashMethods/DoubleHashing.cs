﻿using System;
using System.Diagnostics;

namespace HashMethods
{
    public class DoubleHashing(int tableSize): IHashMethod
    {
        private int collisions;
        private Stopwatch stopwatch = new();

        private int[] table = new int[tableSize];

        public string GetHashName()
        {
            return "Hashing duplo";
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

        private int Hash2(int key)
        {
            return 1 + (key % (table.Length - 1));
        }

        public void Insert(int key)
        {
            collisions = 0;
            stopwatch.Start();

            var hash = Hash(key);
            var hash2 = Hash2(key);

            if (table[hash] == 0)
                table[hash] = key;
            else {
                while (table[hash] != 0) {
                    hash = (hash + hash2) % table.Length;
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
            var hash2 = Hash2(key);

            if (table[hash] == key)
                table[hash] = 0;
            else {
                while (table[hash] != key) {
                    hash = (hash + hash2) % table.Length;
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
            var hash2 = Hash2(key);

            if (table[hash] == key)
                Console.WriteLine($"\"{key}\" encontrado em {hash}");
            else {
                while (table[hash] != key) {
                    hash = (hash + hash2) % table.Length;
                    collisions += 1;
                }

                Console.WriteLine($"\"{key}\" encontrado em {hash}");
            }

            
            stopwatch.Stop();
            Console.WriteLine($"Buscado em {stopwatch.Elapsed.Milliseconds}ms com um total de {collisions} colisões");
            stopwatch.Restart();
        }

        public void Search(string key)
        {
            Console.WriteLine($"Buscando \"{key}\" com código {Math.Abs(key.GetHashCode())}");
            Search(Math.Abs(key.GetHashCode()));
        }

        public void Restructure(int newTableSize)
        {
            var restructureStopWatch = new Stopwatch();
            restructureStopWatch.Start();

            var oldTable = table;
            table = new int[newTableSize];

            foreach (var key in oldTable)
                Insert(key);

            restructureStopWatch.Stop();
            
            Console.WriteLine($"Tabela reestruturada para {newTableSize}");
            Console.WriteLine($"Reestruturado em {stopwatch.Elapsed.Milliseconds}ms");
        }

        public void Clear()
        {
            table = new int[table.Length];
        }
    }
}
