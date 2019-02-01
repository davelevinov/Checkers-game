namespace CheckersLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum ePlayerType
    {
        Computer,
        Human
    }

    public class Player
    {
        private int m_Score;
        private string m_Name;
        private ePlayerType m_TypeOfPlayer;
        private eSquareType m_PieceSymbol;
        private eSquareType m_KingSymbol;
        private bool m_MovesDown;

        public Player(string i_name, ePlayerType i_TypeOfPlayer, int i_Score, eSquareType i_PieceSymbol, eSquareType i_KingSymbol, bool i_MovesDown)
        {
            this.m_Name = i_name;
            this.m_Score = i_Score;
            this.m_TypeOfPlayer = i_TypeOfPlayer;
            this.m_PieceSymbol = i_PieceSymbol;
            this.m_KingSymbol = i_KingSymbol;
            this.m_MovesDown = i_MovesDown;
        }

        public bool MovesDown
        {
            get
            {
                return this.m_MovesDown;
            }

            set
            {
                this.m_MovesDown = value;
            }
        }

        public eSquareType KingSymbol
        {
            get
            {
                return this.m_KingSymbol;
            }

            set
            {
                this.m_KingSymbol = value;
            }
        }

        public eSquareType PieceSymbol
        {
            get
            {
                return this.m_PieceSymbol;
            }

            set
            {
                this.m_PieceSymbol = value;
            }
        }

        public int Score
        {
            get
            {
                return this.m_Score;
            }

            set
            {
                this.m_Score = value;
            }
        }

        public ePlayerType TypeOfPlayer
        {
            get
            {
                return this.m_TypeOfPlayer;
            }

            set
            {
                this.m_TypeOfPlayer = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }

            set
            {
                this.m_Name = value;
            }
        }
    }
}