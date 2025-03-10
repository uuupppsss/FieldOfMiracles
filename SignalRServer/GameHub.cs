using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class GameHub:Hub
    {
        public List<Question> Questions { get; set; }
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
        }

        static Dictionary<string, ISingleClientProxy> clientsByNickname=new();



    }
}
