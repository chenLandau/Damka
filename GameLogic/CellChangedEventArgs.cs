using System;

namespace GameLogic
{
    public delegate void ChangedCellEventHandler(CellChangedEventArgs e);

    public class CellChangedEventArgs : EventArgs
    {
        private Point m_ChangedPoint;

        public CellChangedEventArgs(Point i_ChangedPoint)
        {
            m_ChangedPoint = i_ChangedPoint;
        }

        public Point ChangedPoint
        {
            get { return m_ChangedPoint; }
            set { m_ChangedPoint = value; }
        }
    }
}
