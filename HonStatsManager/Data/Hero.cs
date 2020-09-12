namespace HonStatsManager.Data
{
    internal class Hero
    {
        public string Id { get; }
        public string Name { get; }

        public Hero(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}