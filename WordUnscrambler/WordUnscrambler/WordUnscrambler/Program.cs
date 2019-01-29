using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordUnscrambler.Data;
using WordUnscrambler.Workers;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private const string wordListFileName = "wordlist.txt";

        static void Main(string[] args)
        {
            bool continueWordUnscramble = true;
            do
            {
                Console.WriteLine("Please enter the option - F for File and M for Manual");
                var option = Console.ReadLine() ?? string.Empty;

                switch(option.ToUpper())
                {
                    case "F":
                        Console.WriteLine("Enter scrambled words file name: ");
                        ExecuteScrambledWordsInFileScenario();
                        break;
                    case "M":
                        Console.WriteLine("Enter scrabled words Manually: ");
                        ExecuteScrambledWordsManualEntryScenario();
                        break;
                    default:
                        Console.WriteLine("Option was not recognized: ");
                        break;
                }
                var continueDecision = string.Empty;
                do
                {

                    Console.WriteLine("Do you want to continue? Y/N");
                    continueDecision = (Console.ReadLine() ?? string.Empty);

                } while (
                 !continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase) && 
                 !continueDecision.Equals("N", StringComparison.OrdinalIgnoreCase));

                 continueWordUnscramble = continueDecision.Equals("Y", StringComparison.OrdinalIgnoreCase);


            } while (continueWordUnscramble);
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var filename = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(filename);
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(wordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match();

            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine("Match found for {0}: {1}", matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine("No matches have been found.");
            }
        }
    }
}
