using System.Collections.Generic;

namespace GameLogic
{
    public class GameObject
    {
        private Point m_Position;
        private eObjectType m_Type;
        private List<Point> m_NextPossibleMoves;

        public GameObject(eObjectType i_Type, int i_X = 0, int i_Y = 0)
        {
            m_Type = i_Type;
            m_Position = new Point(i_X, i_Y);
        }

        public Point Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public eObjectType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public List<Point> NextPossibleMoves
        {
            get { return m_NextPossibleMoves; }
        }

        private void findPossibleMovesDownTheBoard(BoardGame i_Board, ePlayerObjectsColor i_Color, bool i_IsEatingMove)
        {
            eObjectType opponentColor;
            eObjectType opponentKing;

            if (i_Color == ePlayerObjectsColor.Black)
            {
                opponentColor = eObjectType.White;
                opponentKing = eObjectType.WhiteKing;
            }
            else
            {
                opponentColor = eObjectType.Black;
                opponentKing = eObjectType.BlackKing;
            }

            if (m_Position.Y > 0 && m_Position.X + 1 < i_Board.BoardSize)
            {
                // $G$ DSN-004 (-3) Code duplication!
                if (i_Board.Board[m_Position.X + 1, m_Position.Y - 1] == eObjectType.Empty && !i_IsEatingMove)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X + 1, m_Position.Y - 1));
                }
                else if ((i_Board.Board[m_Position.X + 1, m_Position.Y - 1] == opponentColor ||
                      i_Board.Board[m_Position.X + 1, m_Position.Y - 1] == opponentKing) &&
                      m_Position.Y - 1 > 0 && m_Position.X + 2 < i_Board.BoardSize &&
                      i_Board.Board[m_Position.X + 2, m_Position.Y - 2] == eObjectType.Empty)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X + 2, m_Position.Y - 2));
                }
            }

            if (m_Position.Y + 1 < i_Board.BoardSize && m_Position.X + 1 < i_Board.BoardSize)
            {
                if (i_Board.Board[m_Position.X + 1, m_Position.Y + 1] == eObjectType.Empty && !i_IsEatingMove)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X + 1, m_Position.Y + 1));
                }
                else if ((i_Board.Board[m_Position.X + 1, m_Position.Y + 1] == opponentColor ||
                      i_Board.Board[m_Position.X + 1, m_Position.Y + 1] == opponentKing) &&
                      m_Position.Y + 2 < i_Board.BoardSize && m_Position.X + 2 < i_Board.BoardSize &&
                      i_Board.Board[m_Position.X + 2, m_Position.Y + 2] == eObjectType.Empty)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X + 2, m_Position.Y + 2));
                }
            }
        }

        private void findPossibleMovesUpTheBoard(BoardGame i_Board, ePlayerObjectsColor i_Color, bool i_IsEatingMove)
        {
            eObjectType opponentColor;
            eObjectType opponentKing;

            if (i_Color == ePlayerObjectsColor.Black)
            {
                opponentColor = eObjectType.White;
                opponentKing = eObjectType.WhiteKing;
            }
            else
            {
                opponentColor = eObjectType.Black;
                opponentKing = eObjectType.BlackKing;
            }

            if (m_Position.Y > 0 && m_Position.X > 0)
            {
                // $G$ DSN-004 (-3) Code duplication!
                if (i_Board.Board[m_Position.X - 1, m_Position.Y - 1] == eObjectType.Empty && !i_IsEatingMove)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X - 1, m_Position.Y - 1));
                }
                else if ((i_Board.Board[m_Position.X - 1, m_Position.Y - 1] == opponentColor ||
                      i_Board.Board[m_Position.X - 1, m_Position.Y - 1] == opponentKing) &&
                      m_Position.Y - 1 > 0 && m_Position.X - 1 > 0 &&
                      i_Board.Board[m_Position.X - 2, m_Position.Y - 2] == eObjectType.Empty)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X - 2, m_Position.Y - 2));
                }
            }

            if (m_Position.Y + 1 < i_Board.BoardSize && m_Position.X > 0)
            {
                if (i_Board.Board[m_Position.X - 1, m_Position.Y + 1] == eObjectType.Empty && !i_IsEatingMove)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X - 1, m_Position.Y + 1));
                }
                else if ((i_Board.Board[m_Position.X - 1, m_Position.Y + 1] == opponentColor ||
                      i_Board.Board[m_Position.X - 1, m_Position.Y + 1] == opponentKing) &&
                      m_Position.Y + 2 < i_Board.BoardSize && m_Position.X - 1 > 0 &&
                      i_Board.Board[m_Position.X - 2, m_Position.Y + 2] == eObjectType.Empty)
                {
                    m_NextPossibleMoves.Add(new Point(m_Position.X - 2, m_Position.Y + 2));
                }
            }
        }

        public void FindAllPossibleMoves(BoardGame i_Board, bool i_IsEatingMove)
        {
            switch (m_Type)
            {
                case eObjectType.White:
                    {
                        m_NextPossibleMoves = new List<Point>(2);
                        findPossibleMovesDownTheBoard(i_Board, ePlayerObjectsColor.White, i_IsEatingMove);
                        break;
                    }

                case eObjectType.Black:
                    {
                        m_NextPossibleMoves = new List<Point>(2);
                        findPossibleMovesUpTheBoard(i_Board, ePlayerObjectsColor.Black, i_IsEatingMove);
                        break;
                    }

                case eObjectType.WhiteKing:
                    {
                        m_NextPossibleMoves = new List<Point>(4);
                        findPossibleMovesDownTheBoard(i_Board, ePlayerObjectsColor.White, i_IsEatingMove);
                        findPossibleMovesUpTheBoard(i_Board, ePlayerObjectsColor.White, i_IsEatingMove);
                        break;
                    }

                case eObjectType.BlackKing:
                    {
                        m_NextPossibleMoves = new List<Point>(4);
                        findPossibleMovesDownTheBoard(i_Board, ePlayerObjectsColor.Black, i_IsEatingMove);
                        findPossibleMovesUpTheBoard(i_Board, ePlayerObjectsColor.Black, i_IsEatingMove);
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
