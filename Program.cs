using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TheAvengers
{
    class Program
    {
        static int key;
        static string downloadLocation = "https://s3.zylowski.net/public/input/4.txt";
        static string filename;
        static bool fileFromDisk;
        static string text;
        static string filePath;

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
        public static string getFilePath()
        {
            if (fileFromDisk)
            {

                return filePath;
            }
            else
            {
                return "C:\\Users\\" + Environment.UserName + "\\Desktop\\words.txt";
            }
        }
        public static string getStatisticsPath()
        {
            return "C:\\Users\\" + Environment.UserName + "\\Desktop\\statystyki.txt";
        }

        public static void actionSwitch()
        {
            if (key == 1)
            {

                chooseFile();
            }

            else if (key == 2)
            {
                Console.WriteLine(countVowelsText());
                Console.WriteLine(countConsonantsText());

            }
            else if (key == 3)
            {

                Console.WriteLine(countWordsText());
            }

            else if (key == 4)
            {
                // TO DO
                Console.WriteLine("Zlicz znaki interpunkcyjne '?' oraz '.'");

                Console.WriteLine(countPunctionSignsOccurances());
            }
            else if (key == 5)
            {
                Console.WriteLine("Zlicz liczbe zdań");

                Console.WriteLine(countSentencesText());
            }
            else if (key == 6)
            {
                // TO DO
                Console.WriteLine("wygeneruj raport");
                countLettersOccurances();
            }
            else if (key == 7)
            {
                Console.WriteLine("Zapisz do pliku");

                String[] stats = new String[] {

                    countVowelsText(),
                    countConsonantsText(),
                    countWordsText(),
                    countPunctionSignsOccurances(),
                    countSentencesText()
                };
                File.WriteAllLines(getStatisticsPath(), stats);
                Console.WriteLine("Zapisano do pliku " + getStatisticsPath());
            }
            else if (key == 8)
            {
                deleteFileWords();
                deleteFileStatistics();
                closeAppAction();
            }

        }
        static void printMenu()
        {
            Console.WriteLine("1. Wybierz plik wejściowy");
            Console.WriteLine("2. Zlicz liczbę liter w pobranym pliku");
            Console.WriteLine("3. Zlicz liczbę wyrazów w pliku");
            Console.WriteLine("4. Zlicz liczbę znaków interpunkcyjnych w pliku.");
            Console.WriteLine("5. Zlicz liczbę zdań w pliku");
            Console.WriteLine("6. Wygeneruj raport o użyciu liter (A-Z)");
            Console.WriteLine("7. Zapisz statystyki z punktów 2-5 do pliku statystyki.txt");
            Console.WriteLine("8. Wyjście z programu");
        }
        static void chooseFile()
        {
            Console.WriteLine("Czy pobrać plik z internetu [T/N]?");
            string choosen = Console.ReadLine();
            if (choosen.Equals("t", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("podaj adres pliku");
                downloadLocation = Console.ReadLine();
                downloadFile();
                fileFromDisk = false;
            }
            if (choosen.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                fileFromDisk = true;
                Console.WriteLine("podaj nazwe pliku txt");
                filename = Console.ReadLine();
                filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filename);
                filePath = filePath.Replace(@"bin\Debug\netcoreapp3.1\", "");

            }

        }

        static void downloadFile()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    Console.WriteLine(getFilePath());
                    client.DownloadFile(downloadLocation,
                                        getFilePath());
                }
                Console.WriteLine("The file has been downloaded");
            }
            catch (Exception e)
            {
                Console.WriteLine("Download failed");
            }
        }


        static int countAnyLetterOccurances(string letter, string text)
        {
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text.Substring(i, 1) == letter)
                    counter++;
            }
            return counter;
        }

        static String countWordsText()
        {
            try
            {
                String line;
                int count = 0;

                StreamReader file = new StreamReader(getFilePath());

                while ((line = file.ReadLine()) != null)
                {

                    String[] words = line.Split(' ');

                    foreach (string word in words)
                    {
                        if (word.Length > 1)
                        {
                            count = count + 1;
                        }
                    }
                }

                file.Close();
                return "Ilość słów w pliku: " + count;

            }
            catch (Exception e)
            {
                return "Error Can't find file";
            }

        }

        static string countSentencesText()
        {
            try
            {
                String line;
                int count = 0;

                StreamReader file = new StreamReader(getFilePath());
                while ((line = file.ReadLine()) != null)
                {

                    String[] sentences = Regex.Split(line.Trim(), "\\.|\\?");

                    foreach (String sentence in sentences)
                    {
                        if (!String.IsNullOrEmpty(sentence))
                        {
                            count++;
                        }
                    }
                }
                file.Close();
                return "Ilość zdań w pliku: " + count;
            }
            catch (Exception e)
            {
                return "Error Can't find file";
            }

        }

        static void countLetterOccurrences(string letter, string line)
        {

            int countLetter = 0;


            for (int i = 0; i < line.Length; i++)
            {
                if (line.Substring(i, 1) == letter)
                    countLetter++;
            }

            Console.WriteLine(letter + ": " + countLetter);
        }

        static void countLettersOccurances()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string fileTextInUperCase = File.ReadAllText(getFilePath());
            fileTextInUperCase = fileTextInUperCase.ToUpper(new CultureInfo("en-US", false));
            Console.WriteLine("string is: " + fileTextInUperCase);

            foreach (char c in alphabet)
            {
                countLetterOccurrences(c.ToString(new CultureInfo("en-US", false)), fileTextInUperCase);
            }

        }
        static string countPunctionSignsOccurances()
        {
            const string signs = @"?.";
            string fileTextInUperCase = File.ReadAllText(getFilePath());
            int counter = 0;
            foreach (char c in signs)
            {
                string sign = c.ToString(new CultureInfo("en-US", false));

                counter += countPunctionSignOccurances(sign, fileTextInUperCase);
            }

            return "Ilość znaków interpunkcyjnych '?' oraz '.': " + counter;
        }
        static int countPunctionSignOccurances(string sign, string textToSearchIn)
        {
            int countSign = 0;
            for (int i = 0; i < textToSearchIn.Length; i++)
            {
                if (textToSearchIn.Substring(i, 1) == sign)
                    countSign++;
            }
            return countSign;
        }

        static String countVowelsText()
        {
            try
            {
                const string vowels = "AEIOUY";
                Console.WriteLine(getFilePath());
                filePath = getFilePath();
                string fileTextInUperCase = File.ReadAllText(filePath).ToUpper();
                int finalCounter = 0;
                foreach (char c in vowels)
                {
                    finalCounter += countAnyLetterOccurances(c.ToString(new CultureInfo("en-US", false)), fileTextInUperCase);
                }
                return "Ilość samogłosek w tekście: " + finalCounter;
            }
            catch (Exception e)
            {
                return "Error Can't find file";
            }
        }

        static String countConsonantsText()
        {
            try
            {
                const string consonants = "BCDFGHJKLMNPRSTWXZ";

                Console.WriteLine(getFilePath());
                filePath = getFilePath();
                string fileTextInUperCase = File.ReadAllText(filePath).ToUpper();
                int finalCounter = 0;

                foreach (char c in consonants)
                {
                    finalCounter += countAnyLetterOccurances(c.ToString(new CultureInfo("en-US", false)), fileTextInUperCase);
                }
                return "Ilość spółgłosek w tekście: " + finalCounter;

            }
            catch (Exception e)
            {
                return "Error Can't find file";
            }
        }


        static void deleteFileWords()
        {
            if (File.Exists(getFilePath()))
            {
                try
                {
                    File.Delete(getFilePath());
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
        static void deleteFileStatistics()
        {
            if (File.Exists(getStatisticsPath()))
            {
                try
                {
                    File.Delete(getStatisticsPath());
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
        static void closeAppAction()
        {
            Environment.Exit(0);
        }
    }
}

