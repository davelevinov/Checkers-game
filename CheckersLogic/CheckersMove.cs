namespace CheckersLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CheckersMove
    {
       private int m_fromRow, m_fromCol;  // Position of piece to be moved.
       private int m_toRow, m_toCol;    // Square it is to move to.

        public CheckersMove(int i_r1, int i_c1, int i_r2, int i_c2)
        {
            // Constructor.  Just set the values of the instance variables.
            m_fromRow = i_r1;
            m_fromCol = i_c1;
            m_toRow = i_r2;
            m_toCol = i_c2;
        }

        public override bool Equals(object obj)
        {
            CheckersMove move = obj as CheckersMove;
            bool fieldsMatch = false;

            if (!(obj == null) && !((object)move == null))
            {
                fieldsMatch = (m_fromRow == move.FromRow) && (m_fromCol == move.FromCol) && (m_toRow == move.ToRow) && (m_toCol == move.ToCol);
            }

            return fieldsMatch;
        }

        public override int GetHashCode()
        {
            return m_fromRow * m_fromCol * m_toRow * m_toCol;
        }

        public int FromRow
        {
            get
            {
                return this.m_fromRow;
            }
        }

        public int FromCol
        {
            get
            {
                return this.m_fromCol;
            }
        }

        public int ToCol
        {
            get
            {
                return this.m_toCol;
            }
        }

        public int ToRow
        {
            get
            {
                return this.m_toRow;
            }
        }
    }
}