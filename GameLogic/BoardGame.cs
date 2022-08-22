namespace GameLogic
{
    public class BoardGame
    {
        private readonly int r_BoardSize;
        private eObjectType[,] m_Board;

        public BoardGame(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            m_Board = new eObjectType[r_BoardSize, r_BoardSize];
        }

        public int BoardSize
        {
            get { return r_BoardSize; }
        }

        public eObjectType[,] Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public void ResetBoard()
        {
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0))
                    {
                        if (i < (r_BoardSize / 2) - 1)
                        {
                            m_Board[i, j] = eObjectType.White;
                        }
                        else if (i > r_BoardSize / 2)
                        {
                            m_Board[i, j] = eObjectType.Black;
                        }
                        else
                        {
                            m_Board[i, j] = eObjectType.Empty;
                        }
                    }
                    else
                    {
                        m_Board[i, j] = eObjectType.BlockedCell;
                    }
                }
            }
        }
    }
}
