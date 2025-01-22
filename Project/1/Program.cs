using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            int userWins = 0;
            int computerWins = 0;

            // Ask the user how many games will be played (1, 3, or 5)
            int totalGames = GetNumberOfGames();
            int currentGame = 1;

            Random random = new Random();

            while (currentGame <= totalGames)
            {
                Console.WriteLine($"\nGame {currentGame} of {totalGames}");

                // Get user's choice
                int userChoice = GetUserChoice();

                // Get computer's choice
                int computerChoice = random.Next(1, 4); // Random number between 1 and 3

                // Determine the winner of the round
                string result = DetermineWinner(userChoice, computerChoice);

                if (result == "User")
                {
                    userWins++;
                    Console.WriteLine("You win this round!");
                }
                else if (result == "Computer")
                {
                    computerWins++;
                    Console.WriteLine("Computer wins this round!");
                }
                else
                {
                    Console.WriteLine("It's a tie!");
                }

                // If the match winner is determined early (best of 3 or 5)
                if ((totalGames == 3 && (userWins == 2 || computerWins == 2)) ||
                    (totalGames == 5 && (userWins == 3 || computerWins == 3)))
                {
                    break;
                }

                currentGame++;
            }

            // Announce the final result
            AnnounceFinalWinner(userWins, computerWins, totalGames);
        }

        // Method to get the number of games to play
        static int GetNumberOfGames()
        {
            int games;
            do
            {
                Console.Write("Enter the number of games for this match (1, 3, or 5): ");
            } while (!int.TryParse(Console.ReadLine(), out games) || (games != 1 && games != 3 && games != 5));
            return games;
        }

        // Method to get the user's choice (1 for Rock, 2 for Paper, 3 for Scissors)
        static int GetUserChoice()
        {
            int choice;
            do
            {
                Console.WriteLine("Choose your move: (1) Rock, (2) Paper, (3) Scissors");
            } while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3));

            return choice;
        }

        // Method to determine the winner of a round
        static string DetermineWinner(int userChoice, int computerChoice)
        {
            Console.WriteLine($"You chose {MoveToString(userChoice)}, Computer chose {MoveToString(computerChoice)}");

            if (userChoice == computerChoice)
            {
                return "Tie";
            }

            if ((userChoice == 1 && computerChoice == 3) || // Rock beats Scissors
                (userChoice == 2 && computerChoice == 1) || // Paper beats Rock
                (userChoice == 3 && computerChoice == 2))   // Scissors beat Paper
            {
                return "User";
            }
            else
            {
                return "Computer";
            }
        }

        // Method to convert the move number to a string
        static string MoveToString(int move)
        {
            return move switch
            {
                1 => "Rock",
                2 => "Paper",
                3 => "Scissors",
                _ => "Unknown"
            };
        }

        // Method to announce the final winner after all games
        static void AnnounceFinalWinner(int userWins, int computerWins, int totalGames)
        {
            Console.WriteLine($"\nOut of {totalGames} games, you won {userWins} and the computer won {computerWins}.");

            if (userWins > computerWins)
            {
                Console.WriteLine("Congratulations! You are the overall winner!");
            }
            else if (computerWins > userWins)
            {
                Console.WriteLine("The computer is the overall winner! Better luck next time.");
            }
            else
            {
                Console.WriteLine("It's a tie overall!");
            }
        }
    }
}
