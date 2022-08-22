namespace GameLogic
{
    public class Manager
    {
        private Player m_Player1;
        private Player m_Player2;
        private BoardGame m_Board;

        public Player Player1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        public BoardGame Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }
    }
}
