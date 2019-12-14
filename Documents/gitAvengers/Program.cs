using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ConsoleApp7
{
    class Program
    {
        static int key;

        static void Main(string[] args)
        {
            menu();
        }
        static void menu()
        {
            while (true)
            {
                printMenu();
                key = Int32.Parse(Console.ReadLine());
                actionSwitch();
                Console.WriteLine(key);
            }
        }


        public static void actionSwitch()
        {
            if (key == 1)
            {
                // TO DO pobierz plik
                Console.WriteLine("pobierz plik");

            }

            else if (key == 2)
            {
                // TO DO Zlicz liczbe liter
            }
            else if (key == 3)
            {
                // TO DO Zlicz wyrazy

            }

            else if (key == 4)
            {
                // TO DO Zlicz znaki interpunkcyjne

            }
            else if (key == 5)
            {
                //TO DO Zlicz liczbe zdań

            }
            else if (key == 6)
            {
                // TO DO wygeneruj raport
            }
            else if (key == 7)
            {
                //TO DO

            }
            else if (key == 8)
            {
                // TO DO ZAMKNIJ APKE
            }

        }
        static void printMenu()
        {
            Console.WriteLine("1. Pobierz plik z internetu");
            Console.WriteLine("2. Zlicz liczbę liter w pobranym pliku");
            Console.WriteLine("3. Zlicz liczbę wyrazów w pliku");
            Console.WriteLine("4. Zlicz liczbę znaków interpunkcyjnych w pliku.");
            Console.WriteLine("5. Zlicz liczbę zdań w pliku");
            Console.WriteLine("6. Wygeneruj raport o użyciu liter (A-Z)");
            Console.WriteLine("7. Zapisz statystyki z punktów 2-5 do pliku statystyki.txt");
            Console.WriteLine("8. Wyjście z programu");
        }
    }
}
