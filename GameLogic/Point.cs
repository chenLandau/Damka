namespace GameLogic
{
    public struct Point
    {
        private int m_X;
        private int m_Y;

        public Point(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public static bool operator ==(Point i_Point1, Point i_Point2)
        {
            return i_Point1.X == i_Point2.X && i_Point1.Y == i_Point2.Y;
        }

        public static bool operator !=(Point i_Point1, Point i_Point2)
        {
            return i_Point1.X != i_Point2.X || i_Point1.Y != i_Point2.Y;
        }

        public override bool Equals(object i_Point)
        {
            bool isEqual;

            if ((i_Point == null) || !this.GetType().Equals(i_Point.GetType()))
            {
                isEqual = false;
            }
            else
            {
                Point newPoint = (Point)i_Point;
                isEqual = (X == newPoint.X) && (Y == newPoint.Y);
            }

            return isEqual;
        }
    }
}
