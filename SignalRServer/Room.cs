using System.Collections.Generic;

namespace SignalRServer
{
    public class Room
    {
        string last = string.Empty;

        public void AddNewClient(string nickname)
        {
            if (string.IsNullOrEmpty(last))
                last = nickname;
            else
            {
               // StartNewGame(last, nickname);
                last = string.Empty;
            }
        }

        public void StartNewGame()
        {

        }

        public void GetNextPlayer()
        {

        }


    }
}
