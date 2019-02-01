namespace CheckersLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BoardChangeEventArgs : EventArgs
    {
        private CheckersMove m_CheckersMove;
        private eSquareType m_SquareTypeOfDestination;
        private bool m_IsEatMove;
        private int m_ColOfPieceRemoved;
        private int m_RowOfPieceRemoved;

        public CheckersMove CheckersMove
        {
            get
            {
                return m_CheckersMove;
            }

            set
            {
                m_CheckersMove = value;
            }
        }

        public eSquareType DestSquareType
        {
            get
            {
                return m_SquareTypeOfDestination;
            }

            set
            {
                m_SquareTypeOfDestination = value;
            }
        }

        public bool IsEatMove
        {
            get
            {
                return m_IsEatMove;
            }

            set
            {
                m_IsEatMove = value;
            }
        }

        public int ColOfPieceRemoved
        {
            get
            {
                return m_ColOfPieceRemoved;
            }

            set
            {
                m_ColOfPieceRemoved = value;
            }
        }

        public int RowOfPieceRemoved
        {
            get
            {
                return m_RowOfPieceRemoved;
            }

            set
            {
                m_RowOfPieceRemoved = value;
            }
        }
    }
}
