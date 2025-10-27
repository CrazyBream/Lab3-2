namespace Core.Entities
{
    public class ChessGameResult
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Winner { get; set; }
        public int Moves { get; set; }

        public ChessGameResult() { }

        public ChessGameResult(string player1, string player2, string winner, int moves)
        {
            Player1 = player1;
            Player2 = player2;
            Winner = winner;
            Moves = moves;
        }
    }
}