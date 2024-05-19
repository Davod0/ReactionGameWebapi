namespace ReactionGame
{
    class Highscore
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public long Score { get; set; }
        public string GameName { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return PlayerName + "  -  " + Score;
        }
    }
}