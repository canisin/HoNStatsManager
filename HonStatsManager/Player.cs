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
    }
}