using System;

namespace GameLogic
{
    public delegate void MoveEnteredEventHandler(MovePointsEventArgs e);

    public class MovePointsEventArgs : EventArgs
    {
        private Point m_SourcePoint;
        private Point m_DestinationPoint;

        public MovePointsEventArgs()
        {
        }

        public MovePointsEventArgs(Point i_SourcePoint, Point i_DestinationPoint)
        {
            m_SourcePoint = i_SourcePoint;
            m_DestinationPoint = i_DestinationPoint;
        }

        public Point SourcePoint
        {
            get { return m_SourcePoint; }
            set { m_SourcePoint = value; }
        }

        public Point DestinationPoint
        {
            get { return m_DestinationPoint; }
            set { m_DestinationPoint = value; }
        }
    }
}
