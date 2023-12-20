using System;
using HashMethods;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo ao Hash Methods");

            while (true) {
                var hashChoice = ChooseHashMethod();

                if (hashChoice == null)
                    break;

                int tableSize = ChooseTableSize();

                IHashMethod hashMethod;

                try {
                    hashMethod = GetHashMethod((HashMethodChoice)hashChoice, tableSize);
                } catch (Exception) {
                    Console.WriteLine("Método não implementado");
                    Console.ReadKey();
                    continue;
                }

                while (true) {
                    var programChoice = ChooseProgram(hashMethod);

                    if (programChoice == null)
                        break;

                    try {
                        ExecuteProgram((ProgramChoice)programChoice, hashMethod);
                    } catch (Exception) {
                        Console.WriteLine("Método não implementado");
                        Console.ReadKey();
                        continue;
                    }
                }
            }
        }

        enum HashMethodChoice
        {
            ChainHashing = 1,
            DoubleHashing = 2,
            LinearProbing = 3,
            QuadraticProbing = 4,
            Exit = 5,
        }

        enum ProgramChoice
        {
            Print = 1,
            Insert = 2,
            Remove = 3,
            Search = 4,
            Restructure = 5,
            Exit = 6,
        }

        static HashMethodChoice? ChooseHashMethod()
        {
            int choice;
            bool chosen = false;

            do {
                Console.Clear();

                if (chosen)
                    Console.WriteLine("Escolha uma opção válida");

                Console.WriteLine("Escolha o método de hash");
                Console.WriteLine("1. Hashing com encadeamento");
                Console.WriteLine("2. Hashing duplo");
                Console.WriteLine("3. Sondagem linear");
                Console.WriteLine("4. Sondagem quadrática"); 
                Console.WriteLine("5. Sair");

                choice = Convert.ToInt32(Console.ReadLine());
                chosen = true;
            } while (choice < 1 || choice > 5);

            if (HashMethodChoice.Exit == (HashMethodChoice)choice)
                return null;

            return (HashMethodChoice)choice;
        }

        static IHashMethod GetHashMethod(HashMethodChoice choice, int tableSize)
        {
            return choice switch
            {
                HashMethodChoice.ChainHashing => new ChainHashing(tableSize),
                HashMethodChoice.DoubleHashing => new DoubleHashing(tableSize),
                HashMethodChoice.LinearProbing => new LinearProbing(tableSize),
                HashMethodChoice.QuadraticProbing => new QuadraticProbing(tableSize),
                _ => throw new Exception(),
            };
        }

        static ProgramChoice? ChooseProgram(IHashMethod hashMethod)
        {
            int choice;
            bool chosen = false;

            do {
                Console.Clear();

                if (chosen)
                    Console.WriteLine("Escolha uma opção válida");

                Console.WriteLine(hashMethod.GetHashName());

                Console.WriteLine("Escolha uma opção");
                Console.WriteLine("1. Imprimir tabela");
                Console.WriteLine("2. Inserir");
                Console.WriteLine("3. Remover");
                Console.WriteLine("4. Buscar");
                Console.WriteLine("5. Reestruturar");
                Console.WriteLine("6. Sair");

                choice = Convert.ToInt32(Console.ReadLine());
                chosen = true;
            } while (choice < 1 || choice > 6);

            if (ProgramChoice.Exit == (ProgramChoice)choice)
                return null;

            return (ProgramChoice)choice;
        }

        public static int ChooseTableSize()
        {
            int choice;
            bool chosen = false;

            do {
                Console.Clear();

                if (chosen)
                    Console.WriteLine("Escolha uma opção válida");

                Console.Write("Insira o tamanho da tabela: ");
                choice = Convert.ToInt32(Console.ReadLine());
                chosen = true;
            } while (choice <= 0);

            return choice;
        }

        static void ExecuteProgram(ProgramChoice choice, IHashMethod hashMethod)
        {
            Console.Clear();
            string? value;

            switch (choice) {

                case ProgramChoice.Print:
                    hashMethod.Print();
                    break;

                case ProgramChoice.Insert:
                    bool chosen = false;
                    do {
                        Console.Clear();
                        if (chosen)
                            Console.WriteLine("Escolha uma opção válida");
                        Console.Write("Insira o valor a ser inserido: ");
                        value = Console.ReadLine();
                    } while (value == null);

                    if (int.TryParse(value, out int intValue))
                        hashMethod.Insert(intValue);
                    else
                        hashMethod.Insert(value);

                    break;

                case ProgramChoice.Remove:
                    chosen = false;
                    do {
                        Console.Clear();
                        if (chosen)
                            Console.WriteLine("Escolha uma opção válida");
                        Console.Write("Insira o valor a ser removido: ");
                        value = Console.ReadLine();
                    } while (value == null);

                    if (int.TryParse(value, out intValue))
                        hashMethod.Remove(intValue);
                    else
                        hashMethod.Remove(value);

                    break;

                case ProgramChoice.Search:
                    chosen = false;
                    do {
                        Console.Clear();
                        if (chosen)
                            Console.WriteLine("Escolha uma opção válida");
                        Console.Write("Insira o valor a ser buscado: ");
                        value = Console.ReadLine();
                    } while (value == null);

                    if (int.TryParse(value, out intValue))
                        hashMethod.Search(intValue);
                    else
                        hashMethod.Search(value);

                    break;

                case ProgramChoice.Restructure:
                    int newTableSize;
                    chosen = false;

                    do {
                        Console.Clear();
                        if (chosen)
                            Console.WriteLine("Escolha uma opção válida");
                        Console.Write("Insira o novo tamanho da tabela: ");
                        newTableSize = Convert.ToInt32(Console.ReadLine());
                        chosen = true;
                    } while (newTableSize <= 0);

                    hashMethod.Restructure(newTableSize);
                    break;

                default:
                    throw new Exception();
            }

            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
        }
    }
}
