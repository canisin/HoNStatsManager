using Newtonsoft.Json.Linq;

namespace HonStatsManager
{
    internal class Player
    {
        public string AccountId { get; }
        public string Nickname { get; }

        public Player(string accountId, string nickname)
        {
            AccountId = accountId;
            Nickname = nickname;
        }

        public Player(JToken token)
        {
            AccountId = (string) token["account_id"];
            Nickname = (string) token["nickname"];
        }
    }
}