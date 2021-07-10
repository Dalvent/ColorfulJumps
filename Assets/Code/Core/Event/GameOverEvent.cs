namespace Code.Core.Event
{
    public class GameOverEvent
    {
        public float CurrentScore { get; set; }
        public float BeastScore { get; set; }
        public bool IsNewBestScore { get; set; }
    }
}