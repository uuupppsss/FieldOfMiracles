using System.Text;

namespace SignalRServer
{
    public class GameState
    {
        public string Answer {  get; set; }
        public HashSet<char> GuessedLetters { get; set; }
        public GameState(string answer) 
        {
            Answer=answer.ToLower();
            GuessedLetters = new HashSet<char>();
        }

        public string GetCurrentState()
        {
            var displayedWord = new StringBuilder();
            foreach (var letter in Answer)
            {
                if(GuessedLetters.Contains(letter)) displayedWord.Append(letter);
                else displayedWord.Append("_");
            }
            return displayedWord.ToString();
        }

        public bool GuessLetter(char letter)
        {
            if (!GuessedLetters.Contains(letter))
            {
                GuessedLetters.Add(letter);
                return true; //буква угадана
            }
            else return false; //буква уже угадана
        }

        public bool IfWordGuessed()
        {
            return Answer.All(c => GuessedLetters.Contains(c));
        }
    }
}
