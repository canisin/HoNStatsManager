namespace HonStatsManager
{
    internal class Player
    {
        public readonly string AccountId;
        public readonly string Nickname;
        public readonly string Name;

        public Player(string accountId, string nickname, string name)
        {
            AccountId = accountId;
            Nickname = nickname;
            Name = name;
        }
    }
}