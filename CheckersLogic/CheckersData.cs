namespace CheckersLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Threading;

    public enum eSquareType
    {
        [Description(" ")]
        Empty,
        [Description("O")]
        Player1RegularPiece,
        [Description("U")]
        Player1King,
        [Description("X")]
        Player2RegularPiece,
        [Description("K")]
        Player2King,
    }

    public enum eMoveStatusCode
    {
        Successful,
        MustEat,
        InvalidPosition
    }

    public enum eGameOverStatusCode
    {
        Player1Won,
        Player2Won,
        Draw
    }

    public delegate void BoardChangeEventHandler(object sender, BoardChangeEventArgs e);

    public delegate void GameOverEventHandler(object sender, GameOverEventArgs e);

    public class CheckersData
    {
        private readonly eSquareType[,] r_Board;
        private readonly int r_BoardSize;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private bool m_Player1Turn;

        public event GameOverEventHandler m_GameOver;

        public event BoardChangeEventHandler m_BoardChangeOccured;

        public CheckersData(int i_BoardSize, Player i_Player1, Player i_Player2)
        {
            r_BoardSize = i_BoardSize;
            r_Board = new eSquareType[r_BoardSize, r_BoardSize];
            r_Player1 = i_Player1;
            r_Player2 = i_Player2;
            m_Player1Turn = true;
            initializeBoard();
        }

        public GameOverEventHandler GameOverDelegate
        {
            get
            {
                return m_GameOver;
            }

            set
            {
                m_GameOver = value;
            }
        }

        public bool Player1Turn
        {
            get
            {
                return m_Player1Turn;
            }
        }

        public eSquareType[,] Board
        {
            get
            {
                return this.r_Board;
            }
        }

        public Player Player1
        {
            get
            {
                return r_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return r_Player2;
            }
        }

        public BoardChangeEventHandler BoardChangeOccured
        {
            get
            {
                return m_BoardChangeOccured;
            }

            set
            {
                m_BoardChangeOccured = value;
            }
        }

        public GameOverEventHandler GameOverOccured
        {
            get
            {
                return m_GameOver;
            }

            set
            {
                m_GameOver = value;
            }
        }

        private void initializeBoard()
        {
            int numberOfRowsOfPiecesForEachPlayer = (r_BoardSize / 2) - 1;
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (!(((row + col) % 2) == 0))
                    {
                        if (row < numberOfRowsOfPiecesForEachPlayer)
                        {
                            r_Board[row, col] = eSquareType.Player1RegularPiece;
                        }
                        else if (row > r_BoardSize - numberOfRowsOfPiecesForEachPlayer - 1)
                        {
                            r_Board[row, col] = eSquareType.Player2RegularPiece;
                        }
                        else
                        {
                            r_Board[row, col] = eSquareType.Empty;
                        }
                    }
                    else
                    {
                        r_Board[row, col] = eSquareType.Empty;
                    }
                }
            }
        }

        public List<CheckersMove> GetLegalMoves(Player i_Player)
        {
            eSquareType playerPieceSymbol = i_Player.PieceSymbol;
            eSquareType playerKingSymbol = i_Player.KingSymbol;
            List<CheckersMove> listOfMoves = new List<CheckersMove>();

            listOfMoves = getLegalEatMoves(i_Player);
            bool playerCanMakeAJump = listOfMoves.Count > 0;

            if (!playerCanMakeAJump)
            {
                listOfMoves = getValidMovesThatAreNotEatMoves(i_Player);
            }

            if (listOfMoves.Count() == 0)
            {
                listOfMoves = null;
            }

            return listOfMoves;
        }

        private List<CheckersMove> getLegalEatMoves(Player i_Player)
        {
            eSquareType playerPieceSymbol = i_Player.PieceSymbol;
            eSquareType playerKingSymbol = i_Player.KingSymbol;
            List<CheckersMove> listOfEatMoves = new List<CheckersMove>();

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (r_Board[row, col] == i_Player.PieceSymbol || r_Board[row, col] == i_Player.KingSymbol)
                    {
                        if (canEat(i_Player, row, col, row + 1, col + 1, row + 2, col + 2))
                        {
                            listOfEatMoves.Add(new CheckersMove(row, col, row + 2, col + 2));
                        }

                        if (canEat(i_Player, row, col, row + 1, col - 1, row + 2, col - 2))
                        {
                            listOfEatMoves.Add(new CheckersMove(row, col, row + 2, col - 2));
                        }

                        if (canEat(i_Player, row, col, row - 1, col - 1, row - 2, col - 2))
                        {
                            listOfEatMoves.Add(new CheckersMove(row, col, row - 2, col - 2));
                        }

                        if (canEat(i_Player, row, col, row - 1, col + 1, row - 2, col + 2))
                        {
                            listOfEatMoves.Add(new CheckersMove(row, col, row - 2, col + 2));
                        }
                    }
                }
            }

            return listOfEatMoves;
        }

        private List<CheckersMove> getValidMovesThatAreNotEatMoves(Player i_Player)
        {
            eSquareType playerPieceSymbol = i_Player.PieceSymbol;
            eSquareType playerKingSymbol = i_Player.KingSymbol;
            List<CheckersMove> listOfMoves = new List<CheckersMove>();

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (r_Board[row, col] == playerPieceSymbol || r_Board[row, col] == playerKingSymbol)
                    {
                        if (checkRegularMove(i_Player, row, col, row + 1, col - 1))
                        {
                            listOfMoves.Add(new CheckersMove(row, col, row + 1, col - 1));
                        }

                        if (checkRegularMove(i_Player, row, col, row - 1, col - 1))
                        {
                            listOfMoves.Add(new CheckersMove(row, col, row - 1, col - 1));
                        }

                        if (checkRegularMove(i_Player, row, col, row + 1, col + 1))
                        {
                            listOfMoves.Add(new CheckersMove(row, col, row + 1, col + 1));
                        }

                        if (checkRegularMove(i_Player, row, col, row - 1, col + 1))
                        {
                            listOfMoves.Add(new CheckersMove(row, col, row - 1, col + 1));
                        }
                    }
                }
            }

            return listOfMoves;
        }

        private bool checkRegularMove(Player i_Player, int i_R1, int i_C1, int i_R2, int i_C2)
        {
            bool isValidMove = true;

            if (i_R2 < 0 || i_R2 >= r_BoardSize || i_C2 < 0 || i_C2 >= r_BoardSize)
            {
                isValidMove = false;
            }
            else if (Math.Abs(i_R1 - i_R2) != 1 || Math.Abs(i_C1 - i_C2) != 1)
            {
                isValidMove = false;
            }
            else if (r_Board[i_R2, i_C2] != eSquareType.Empty)
            {
                isValidMove = false;
            }
            else if (r_Board[i_R1, i_C1] == eSquareType.Player1RegularPiece && i_R1 >= i_R2)
            {
                isValidMove = false;
            }
            else if (r_Board[i_R1, i_C1] == eSquareType.Player2RegularPiece && i_R1 <= i_R2)
            {
                isValidMove = false;
            }

            return isValidMove;
        }

        private bool isEatMove(CheckersMove i_Move)
        {
            return isEatMove(i_Move.FromRow, i_Move.FromCol, i_Move.ToRow, i_Move.ToCol);
        }

        private bool isEatMove(int i_FromRow, int i_FromCol, int i_ToRow, int i_ToCol)
        {
            bool isEatMove = (Math.Abs(i_FromRow - i_ToRow) == 2) && (Math.Abs(i_FromCol - i_ToCol) == 2);

            return isEatMove;
        }

        private bool canEat(Player i_Player, int i_FromRow, int i_FromCol, int i_MiddleRow, int i_MiddleCol, int i_ToRow, int i_ToCol)
        {
            bool canEat = true;

            if (i_ToRow < 0 || i_ToRow >= r_BoardSize || i_ToCol < 0 || i_ToCol >= r_BoardSize)
            {
                canEat = false;
            }
            else if (r_Board[i_ToRow, i_ToCol] != eSquareType.Empty)
            {
                canEat = false;
            }
            else if (r_Board[i_MiddleRow, i_MiddleCol] == eSquareType.Empty)
            {
                canEat = false;
            }
            else if ((r_Board[i_FromRow, i_FromCol] == eSquareType.Player1RegularPiece || r_Board[i_FromRow, i_FromCol] == eSquareType.Player1King)
                && (r_Board[i_MiddleRow, i_MiddleCol] == eSquareType.Player1RegularPiece || r_Board[i_MiddleRow, i_MiddleCol] == eSquareType.Player1King))
            {
                canEat = false;
            }
            else if ((r_Board[i_FromRow, i_FromCol] == eSquareType.Player2RegularPiece || r_Board[i_FromRow, i_FromCol] == eSquareType.Player2King)
                && (r_Board[i_MiddleRow, i_MiddleCol] == eSquareType.Player2RegularPiece || r_Board[i_MiddleRow, i_MiddleCol] == eSquareType.Player2King))
            {
                canEat = false;
            }
            else if ((r_Board[i_FromRow, i_FromCol] == eSquareType.Player1RegularPiece) && (i_ToRow < i_FromRow))
            {
                canEat = false;
            }
            else if ((r_Board[i_FromRow, i_FromCol] == eSquareType.Player1RegularPiece) && (i_ToRow < i_FromRow))
            {
                canEat = false;
            }
            else if ((r_Board[i_FromRow, i_FromCol] == eSquareType.Player2RegularPiece) && (i_ToRow > i_FromRow))
            {
                canEat = false;
            }

            return canEat;
        }

        public eMoveStatusCode CheckIfMoveIsValid(CheckersMove i_Move)
        {
            Player playerThatPlaysNow = getPlayerThatPlaysNow();
            List<CheckersMove> listOfMoves = GetLegalMoves(playerThatPlaysNow);
            eMoveStatusCode moveStatusCode = eMoveStatusCode.Successful;

            if (i_Move.FromRow < 0 || i_Move.FromRow >= r_BoardSize || i_Move.FromCol < 0 || i_Move.FromCol >= r_BoardSize)
            {
                moveStatusCode = eMoveStatusCode.InvalidPosition;
            }
            else if (r_Board[i_Move.FromRow, i_Move.FromCol] != playerThatPlaysNow.PieceSymbol && r_Board[i_Move.FromRow, i_Move.FromCol] != playerThatPlaysNow.KingSymbol)
            {
                moveStatusCode = eMoveStatusCode.InvalidPosition;
            }
            else if (isEatMove(listOfMoves[0]) && !isEatMove(i_Move.FromRow, i_Move.FromCol, i_Move.ToRow, i_Move.ToCol))
            {
                moveStatusCode = eMoveStatusCode.MustEat;
            }
            else if (isEatMove(i_Move.FromRow, i_Move.FromCol, i_Move.ToRow, i_Move.ToCol))
            {
                int r2 = (i_Move.FromRow + i_Move.ToRow) / 2;
                int c2 = (i_Move.FromCol + i_Move.ToCol) / 2;

                if (!canEat(playerThatPlaysNow, i_Move.FromRow, i_Move.FromCol, r2, c2, i_Move.ToRow, i_Move.ToCol))
                {
                    moveStatusCode = eMoveStatusCode.InvalidPosition;
                }
            }
            else
            {
                if (!checkRegularMove(playerThatPlaysNow, i_Move.FromRow, i_Move.FromCol, i_Move.ToRow, i_Move.ToCol))
                {
                    moveStatusCode = eMoveStatusCode.InvalidPosition;
                }
            }

            return moveStatusCode;
        }

        public bool DoMakeMove(CheckersMove i_Move)
        {
            Player playerThatPlaysNow = getPlayerThatPlaysNow();
            bool hasMoreMoves = false;
            List<CheckersMove> moves;

            makeMove(playerThatPlaysNow, i_Move);

            if (isEatMove(i_Move) && (moves = getLegalMovesFrom(playerThatPlaysNow, i_Move.ToRow, i_Move.ToCol)) != null)
            {
                hasMoreMoves = true;
            }

            if (!hasMoreMoves)
            {
                m_Player1Turn = !m_Player1Turn;
            }

            BoardChanged(i_Move);

            checkIfGameEnded(playerThatPlaysNow, i_Move);

            return hasMoreMoves;
        }

        private void BoardChanged(CheckersMove i_Move)
        {
            BoardChangeEventArgs e = new BoardChangeEventArgs();
            e.CheckersMove = i_Move;
            e.DestSquareType = r_Board[i_Move.ToRow, i_Move.ToCol];
            e.IsEatMove = isEatMove(i_Move);
            if (e.IsEatMove)
            {
                e.RowOfPieceRemoved = (i_Move.FromRow + i_Move.ToRow) / 2;
                e.ColOfPieceRemoved = (i_Move.FromCol + i_Move.ToCol) / 2;
            }

            OnBoardChange(e);
        }

        private void checkIfGameEnded(Player i_PlayerThatActedNow, CheckersMove i_LastMove)
        {
            Player theOtherPlayer = i_PlayerThatActedNow == r_Player1 ? r_Player2 : r_Player1;

            if (GetLegalMoves(theOtherPlayer) == null)
            {
                if (GetLegalMoves(i_PlayerThatActedNow) == null)
                {
                    GameOver(eGameOverStatusCode.Draw, i_LastMove);
                }
                else
                {
                    GameOver(
                        i_PlayerThatActedNow.Equals(r_Player1) ?
                        eGameOverStatusCode.Player1Won :
                        eGameOverStatusCode.Player2Won,
                        i_LastMove);
                }
            }
        }

        private void GameOver(eGameOverStatusCode i_eGameEndedStatusCode, CheckersMove i_LastMove)
        {
            GameOverEventArgs e = new GameOverEventArgs();
            e.LastMove = i_LastMove;
            e.GameOverStatusCode = i_eGameEndedStatusCode;

            switch (i_eGameEndedStatusCode)
            {
                case eGameOverStatusCode.Draw:
                    break;
                case eGameOverStatusCode.Player1Won:
                    r_Player1.Score++;
                    break;
                case eGameOverStatusCode.Player2Won:
                    r_Player2.Score++;
                    break;
                default: break;
            }

            m_Player1Turn = true;
            initializeBoard();
            OnGameOver(e);
        }

        protected virtual void OnBoardChange(BoardChangeEventArgs e)
        {
            if (BoardChangeOccured != null)
            {
                BoardChangeOccured.Invoke(this, e);
            }
        }

        protected virtual void OnGameOver(GameOverEventArgs e)
        {
            if (GameOverOccured != null)
            {
                GameOverOccured.Invoke(this, e);
            }
        }

        private List<CheckersMove> getLegalMovesFrom(Player i_Player, int i_Row, int i_Col)
        {
            List<CheckersMove> listOfMoves = new List<CheckersMove>();

            if (r_Board[i_Row, i_Col] == i_Player.PieceSymbol || r_Board[i_Row, i_Col] == i_Player.KingSymbol)
            {
                if (canEat(i_Player, i_Row, i_Col, i_Row + 1, i_Col + 1, i_Row + 2, i_Col + 2))
                {
                    listOfMoves.Add(new CheckersMove(i_Row, i_Col, i_Row + 2, i_Col + 2));
                }

                if (canEat(i_Player, i_Row, i_Col, i_Row - 1, i_Col + 1, i_Row - 2, i_Col + 2))
                {
                    listOfMoves.Add(new CheckersMove(i_Row, i_Col, i_Row - 2, i_Col + 2));
                }

                if (canEat(i_Player, i_Row, i_Col, i_Row + 1, i_Col - 1, i_Row + 2, i_Col - 2))
                {
                    listOfMoves.Add(new CheckersMove(i_Row, i_Col, i_Row + 2, i_Col - 2));
                }

                if (canEat(i_Player, i_Row, i_Col, i_Row - 1, i_Col - 1, i_Row - 2, i_Col - 2))
                {
                    listOfMoves.Add(new CheckersMove(i_Row, i_Col, i_Row - 2, i_Col - 2));
                }
            }

            if (listOfMoves.Count == 0)
            {
                listOfMoves = null;
            }

            return listOfMoves;
        }

        private void makeMove(Player i_Player, CheckersMove i_Move)
        {
            makeMove(i_Player, i_Move.FromRow, i_Move.FromCol, i_Move.ToRow, i_Move.ToCol);
        }

        private void makeMove(Player i_Player, int i_FromRow, int i_FromCol, int i_ToRow, int i_ToCol)
        {
            r_Board[i_ToRow, i_ToCol] = r_Board[i_FromRow, i_FromCol];
            r_Board[i_FromRow, i_FromCol] = eSquareType.Empty;
            if (Math.Abs(i_FromRow - i_ToRow) == 2)
            {
                // The move is a jump.  Remove the piece we jumped above from the board;
                int jumpedPieceRow = (i_FromRow + i_ToRow) / 2;
                int jumpPieceCol = (i_FromCol + i_ToCol) / 2;
                r_Board[jumpedPieceRow, jumpPieceCol] = eSquareType.Empty;
            }

            if (i_ToRow == r_BoardSize - 1 && i_Player.MovesDown && !(r_Board[i_ToRow, i_ToCol] == eSquareType.Player2King))
            {
                r_Board[i_ToRow, i_ToCol] = eSquareType.Player1King;
            }

            if (i_ToRow == 0 && !i_Player.MovesDown && !(r_Board[i_ToRow, i_ToCol] == eSquareType.Player2King))
            {
                r_Board[i_ToRow, i_ToCol] = eSquareType.Player2King;
            }
        }

        public void DoComputerTurn()
        {
            List<CheckersMove> computerMoves = GetLegalMoves(r_Player2);
            int startingIndexOfList = 0;
            int endIndexOfList = computerMoves.Count - 1;
            CheckersMove computerMove;
            Random random = new Random();
            int randomNumber = random.Next(startingIndexOfList, endIndexOfList);
            computerMove = computerMoves.ElementAt(randomNumber);
            if (DoMakeMove(computerMove))
            {
                DoComputerTurn();
            }
        }

        private Player getPlayerThatPlaysNow()
        {
            return m_Player1Turn ? r_Player1 : r_Player2;
        }
    }
}
