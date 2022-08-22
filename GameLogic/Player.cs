using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class Player
    {
        private static readonly Random sr_Random = new Random();
        private readonly string r_Name;
        private readonly ePlayerType r_Type;
        private readonly ePlayerObjectsColor r_Color;
        private List<GameObject> m_GameObjectsArray = null;
        private int m_Score;

        public event MoveEnteredEventHandler ObjectMoved;

        public event ChangedCellEventHandler ObjectEaten;

        public event ChangedCellEventHandler BecameKing;

        public Player(ePlayerType i_Type, string i_Name, ePlayerObjectsColor i_Color, int i_Score = 0)
        {
            r_Type = i_Type;
            r_Name = i_Name;
            r_Color = i_Color;
            m_Score = i_Score;
        }

        public string Name
        {
            get { return r_Name; }
        }

        public ePlayerType Type
        {
            get { return r_Type; }
        }

        public ePlayerObjectsColor Color
        {
            get { return r_Color; }
        }

        public List<GameObject> GameObjectsArray
        {
            get { return m_GameObjectsArray; }
        }

        public int Score
        {
            get { return m_Score; }
        }

        public static bool CheckIfTie(Player i_player1, Player i_player2, BoardGame i_Board)
        {
            return i_player1.CheckIfLoose(i_Board) && i_player2.CheckIfLoose(i_Board);
        }

        public void ResetGameObjectArray(BoardGame i_Board)
        {
            int arraySize = ((i_Board.BoardSize / 2) - 1) * 4;
            m_GameObjectsArray = new List<GameObject>(arraySize);
            eObjectType objectsColor;

            if (r_Color == ePlayerObjectsColor.Black)
            {
                objectsColor = eObjectType.Black;
            }
            else
            {
                objectsColor = eObjectType.White;
            }

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Board.Board[i, j] == objectsColor)
                    {
                        m_GameObjectsArray.Add(new GameObject(objectsColor, i, j));
                    }
                }
            }
        }

        public void FindAllObjectsNextMoves(BoardGame i_Board, bool i_IsEatingMove = false)
        {
            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                m_GameObjectsArray[i].FindAllPossibleMoves(i_Board, i_IsEatingMove);
            }
        }

        public bool CheckIfAnyEatingMoveExist()
        {
            bool anyEatingMoveExist = false;

            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].NextPossibleMoves.Count > 0)
                {
                    anyEatingMoveExist = true;
                    break;
                }
            }

            return anyEatingMoveExist;
        }

        public bool CheckValidityNextMove(Point i_SourcePosition, Point i_DestinationPosition)
        {
            bool isValidNextMove = false;

            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].Position == i_SourcePosition)
                {
                    for (int j = 0; j < m_GameObjectsArray[i].NextPossibleMoves.Count; j++)
                    {
                        if (m_GameObjectsArray[i].NextPossibleMoves[j] == i_DestinationPosition)
                        {
                            isValidNextMove = true;
                        }
                    }

                    break;
                }
            }

            return isValidNextMove;
        }

        public bool CheckPossibleMovesAfterEatingMove(Point i_CurrentPosition)
        {
            bool isValidNextMove = false;

            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].Position == i_CurrentPosition && m_GameObjectsArray[i].NextPossibleMoves.Count > 0)
                {
                    isValidNextMove = true;
                    break;
                }
            }

            return isValidNextMove;
        }

        private void changeObjectToKing(BoardGame i_Board, Point i_SourcePosition, Point i_DestinationPosition, eObjectType i_KingColor)
        {
            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].Position == i_SourcePosition)
                {
                    m_GameObjectsArray[i].Type = i_KingColor;
                    m_GameObjectsArray[i].Position = i_DestinationPosition;
                }
            }

            i_Board.Board[i_DestinationPosition.X, i_DestinationPosition.Y] = i_KingColor;
            OnBecameKing(new CellChangedEventArgs(i_SourcePosition));
        }
        
        protected virtual void OnBecameKing(CellChangedEventArgs e)
        {
            if (BecameKing != null)
            {
                BecameKing.Invoke(e);
            }
        }

        private void removeGameObjectFromGameObjectsArray(Point i_SkippingPoint)
        {
            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].Position == i_SkippingPoint)
                {
                    m_GameObjectsArray.RemoveAt(i);
                    return;
                }
            }
        }

        public void MoveComputer(BoardGame i_Board, Player i_Opponent)
        {
            Point sourcePosition, destinationPosition;
            int randomGameObjectArrayIndex, randomNextMoveIndex;
            bool isEatingMove;

            FindAllObjectsNextMoves(i_Board, true);

            if (!CheckIfAnyEatingMoveExist())
            {
                FindAllObjectsNextMoves(i_Board, false);
            }

            do
            {
                randomGameObjectArrayIndex = sr_Random.Next(m_GameObjectsArray.Count);
            }
            while (m_GameObjectsArray[randomGameObjectArrayIndex].NextPossibleMoves.Count == 0);

            randomNextMoveIndex = sr_Random.Next(m_GameObjectsArray[randomGameObjectArrayIndex].NextPossibleMoves.Count);

            sourcePosition = m_GameObjectsArray[randomGameObjectArrayIndex].Position;
            destinationPosition = m_GameObjectsArray[randomGameObjectArrayIndex].NextPossibleMoves[randomNextMoveIndex];

            MoveObject(sourcePosition, destinationPosition, i_Opponent, i_Board, out isEatingMove);            

            while (isEatingMove)
            {
                sourcePosition = destinationPosition;
                FindAllObjectsNextMoves(i_Board, true);

                if (CheckPossibleMovesAfterEatingMove(sourcePosition))
                {
                    for (int i = 0; i < m_GameObjectsArray.Count; i++)
                    {
                        if (sourcePosition == m_GameObjectsArray[i].Position)
                        {
                            randomNextMoveIndex = sr_Random.Next(m_GameObjectsArray[i].NextPossibleMoves.Count - 1);
                            destinationPosition = m_GameObjectsArray[i].NextPossibleMoves[randomNextMoveIndex];
                        }
                    }

                    MoveObject(sourcePosition, destinationPosition, i_Opponent, i_Board, out isEatingMove);
                }
                else
                {
                    isEatingMove = false;
                }
            }
        }

        public void MoveObject(Point i_SourcePosition, Point i_DestinationPosition, Player i_Opponent, BoardGame i_Board, out bool o_IsEatingMove)
        {
            Point skippingPoint;
            o_IsEatingMove = false;

            if (Math.Abs(i_SourcePosition.X - i_DestinationPosition.X) == 2)
            {
                skippingPoint = new Point(Math.Max(i_DestinationPosition.X, i_SourcePosition.X) - 1, Math.Max(i_DestinationPosition.Y, i_SourcePosition.Y) - 1);
                i_Opponent.removeGameObjectFromGameObjectsArray(skippingPoint);
                i_Board.Board[skippingPoint.X, skippingPoint.Y] = eObjectType.Empty;
                o_IsEatingMove = true;
                OnObjectEaten(new CellChangedEventArgs(skippingPoint));
            }

            if (i_DestinationPosition.X == i_Board.BoardSize - 1 && r_Color == ePlayerObjectsColor.White)
            {
                changeObjectToKing(i_Board, i_SourcePosition, i_DestinationPosition, eObjectType.WhiteKing);
            }
            else if (i_DestinationPosition.X == 0 && r_Color == ePlayerObjectsColor.Black)
            {
                changeObjectToKing(i_Board, i_SourcePosition, i_DestinationPosition, eObjectType.BlackKing);
            }
            else
            {
                i_Board.Board[i_DestinationPosition.X, i_DestinationPosition.Y] = i_Board.Board[i_SourcePosition.X, i_SourcePosition.Y];
                for (int i = 0; i < m_GameObjectsArray.Count; i++)
                {
                    if (i_SourcePosition == m_GameObjectsArray[i].Position)
                    {
                        m_GameObjectsArray[i].Position = i_DestinationPosition;
                    }
                }
            }

            i_Board.Board[i_SourcePosition.X, i_SourcePosition.Y] = eObjectType.Empty;
            OnObjectMoved(new MovePointsEventArgs(i_SourcePosition, i_DestinationPosition));
        }

        protected virtual void OnObjectMoved(MovePointsEventArgs e)
        {
            if (ObjectMoved != null)
            {
                ObjectMoved.Invoke(e);
            }
        }

        protected virtual void OnObjectEaten(CellChangedEventArgs e)
        {
            if (ObjectEaten != null)
            {
                ObjectEaten.Invoke(e);
            }
        }

        private bool checkIfAnyValidMoveExist()
        {
            bool existingValidMove = false;

            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].NextPossibleMoves.Count > 0)
                {
                    existingValidMove = true;
                    break;
                }
            }

            return existingValidMove;
        }

        public bool CheckIfLoose(BoardGame i_Board)
        {
            FindAllObjectsNextMoves(i_Board, false);

            return m_GameObjectsArray.Count == 0 || !checkIfAnyValidMoveExist();
        }

        public int CalculateScore()
        {
            int sumOfPoints = 0;

            for (int i = 0; i < m_GameObjectsArray.Count; i++)
            {
                if (m_GameObjectsArray[i].Type == eObjectType.BlackKing || m_GameObjectsArray[i].Type == eObjectType.WhiteKing)
                {
                    sumOfPoints += 4;
                }
                else
                {
                    sumOfPoints++;
                }
            }

            return sumOfPoints;
        }

        public void CalculateWinnerPoints(Player i_Opponent)
        {
            m_Score += Math.Abs(CalculateScore() - i_Opponent.CalculateScore());
        }
    }
}