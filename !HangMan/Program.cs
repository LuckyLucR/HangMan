using System;

namespace _HangMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string readText = File.ReadAllText("Words.txt");
            string[] words = readText.Split(Environment.NewLine);
            Random random = new Random();

            List<char> randomWord_chars = new List<char>();
            List<char> guessed_chars = new List<char>();
            List<char> wrongGuesses_chars = new List<char>();

            bool startGame = true;
            bool loopingGame = true;
            bool loopingGuess = true;
            bool alreadyGuessed = false;
            bool win = true;
            bool livesLost = false;
            bool fail = true;
            int lives = 6;
            
            void Man()
            {
                if (lives == 6) { Console.WriteLine("\n +---+\n |   |\n |\n |\n |\n |\n========\n"); }
                else if (lives == 5) { Console.WriteLine("\n +---+\n |   |\n |   o\n |\n |\n |\n========\n"); }
                else if (lives == 4) { Console.WriteLine("\n +---+\n |   |\n |   o\n |   |\n |\n |\n========\n"); }
                else if (lives == 3) { Console.WriteLine("\n +---+\n |   |\n |   o\n |  /|\n |\n |\n========\n"); }
                else if (lives == 2) { Console.WriteLine("\n +---+\n |   |\n |   o\n |  /|\\ \n |\n |\n========\n"); }
                else if (lives == 1) { Console.WriteLine("\n +---+\n |   |\n |   o\n |  /|\\ \n |  /\n |\n========\n"); }
            }
            void Board()
            {
                Man();
                foreach (char c in randomWord_chars)
                {
                    foreach (char c2 in guessed_chars)
                    {
                        if (c == c2)
                        {
                            Console.Write($"{c} ");
                            fail = false;
                        }
                    }
                    if (fail)
                    {
                        Console.Write("_ ");
                        win = false;
                    }
                    fail = true;
                }
                Console.WriteLine("");
                foreach (char c in wrongGuesses_chars)
                {
                    Console.Write($"{c} ");
                }
                Console.WriteLine("");
            }


            while (startGame)
            {
                Console.Clear();
                randomWord_chars.Clear();
                guessed_chars.Clear();
                wrongGuesses_chars.Clear();
                lives = 6;
                int rdm = random.Next(0, words.Length);
                string randomWord = words[rdm];
                for (int i = 0; i < randomWord.Length; i++)
                {
                    randomWord_chars.Add(randomWord[i]);
                }



                while (loopingGame)
                {
                    // CHECKING GUESSED CHARS
                    Board();


                    if (win)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Man();
                        Console.WriteLine($"Gewonnen! Het woord was {randomWord}!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Klik op een knop om opnieuw te spelen...");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        break;
                    }
                    else if (lives == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n +---+\n |   |\n |   o\n |  /|\\ \n |  / \\ \n |\n========\n");
                        Console.WriteLine($"Verloren! Het woord was {randomWord}!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Klik op een knop om opnieuw te spelen...");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();
                        break;
                    }

                    loopingGuess = true;
                    while (loopingGuess)
                    {
                        win = true;
                        Console.Write("> ");
                        string userInputString = Console.ReadLine().ToLower();
                        bool tryParseSucces = char.TryParse(userInputString, out char userInput);
                        if (tryParseSucces)
                        {
                            alreadyGuessed = false;
                            foreach (char c in guessed_chars)
                            {
                                if (userInput == c)
                                {
                                    alreadyGuessed = true;
                                }
                            }
                            if (!alreadyGuessed)
                            {
                                guessed_chars.Add(userInput);
                                loopingGuess = false;

                                livesLost = true;
                                foreach (char c in randomWord_chars)
                                {
                                    if (c == userInput)
                                    {
                                        livesLost = false;
                                    }
                                }
                                if (livesLost)
                                {
                                    wrongGuesses_chars.Add(userInput);
                                    lives--;
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Board();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Je hebt {userInput} al geprobeerd!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Board();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Je moet één letter invoeren!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    Console.Clear();
                }
            }
        }
    }
}