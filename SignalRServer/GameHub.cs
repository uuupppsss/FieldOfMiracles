using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class GameHub:Hub
    {
        private Random random;
        public List<Player> PlayersList = new();
        public List<Question> Questions { get; set; }
        private static Question CurrentQuestion { get; set; }
        public string GuessedWord  { get; set; }
        public GameHub()
        {
            Questions = new List<Question>()
            {
                new Question()
                {
                    Text="2 девушки",
                    Answer="кружка"
                }
            };
            random = new Random();
            CurrentQuestion = Questions[random.Next(Questions.Count - 1)];
            GuessedWord = "";
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Hello", "Введите ник");
            return base.OnConnectedAsync(); 
        }

        public async Task JoinGame(string nick)
        {
            var player = new Player() { ConnectionId=Context.ConnectionId, Name=nick};
            PlayersList.Add(player);
            
            await Clients.All.SendAsync("UpdatePlayers", PlayersList);
        }

        public void SetNick(string name)
        {
            var check = PlayersList.FirstOrDefault(s => s.Name == name);
            if (check != null)
            {
                Clients.Caller.SendAsync("Hello", "Такой ник уже есть. Придумай другой ");
                return;
            }
            else
            {
                PlayersList.Add(new Player() { Name = name, Proxy = Clients.Caller });
            }
        }

        public async Task SendGuess(string gues, string player_ConnectionId)
        {


            var isCorrect=CheckCorrectAnswer(gues);
            if (isCorrect)
            {
                await Clients.All.SendAsync("AnswerGuessed", gues);
            }
            if (CheckIfWinner(player_ConnectionId))
            {
                await Clients.All.SendAsync("GameOver", $"Игрок {PlayersList.FirstOrDefault(p=>p.ConnectionId == player_ConnectionId).Name} победил");
            }
            else
            {
                await Clients.All.SendAsync("NextTurn");
            }
        }

        private bool CheckCorrectAnswer(string guess)
        {
            if (guess.Length > 1)
            {
                if (CurrentQuestion.Answer == guess)
                {
                    
                    return true;
                }
                else return false;
            }
            else
            {
                if (CurrentQuestion.Answer.Contains(guess))
                {
                    GuessedWord = guess;
                    return true;
                }
                else return false;
            }
        }

        private bool CheckIfWinner(string player_ConnectionId)
        {
            if (!GuessedWord.Contains("_")) return true;
            else return false;
        }
    }
}
