using GameLogic;

namespace UserInterface
{
    internal class GameManager
    {
        private readonly Manager r_GameLogic = new Manager();
        private readonly FormDamka r_Damka = new FormDamka();

        public void Run()
        {
            resetGame();
            r_Damka.MoveEntered += MoveEntered_EventHandler;
            assignLogicEvents();
            r_Damka.ShowDialog();
        }

        private void resetGame()
        {
            resetGameLogicManager();
            resetLogicBoards();
            r_Damka.ResetButtons(r_GameLogic.Board);
        }

        private void resetGameLogicManager()
        {
            ePlayerType player2Type;
            
            r_GameLogic.Board = new BoardGame(r_Damka.GameSettingForm.BoardGameSize);
            r_GameLogic.Player1 = new Player(ePlayerType.Person, r_Damka.GameSettingForm.TextBoxPlayer1, ePlayerObjectsColor.White);
            player2Type = r_Damka.GameSettingForm.CheckPlayer2Type ? ePlayerType.Person : ePlayerType.Computer;
            r_GameLogic.Player2 = new Player(player2Type, r_Damka.GameSettingForm.TextBoxPlayer2, ePlayerObjectsColor.Black);
        }

        private void resetLogicBoards()
        {
            r_GameLogic.Board.ResetBoard();
            r_GameLogic.Player1.ResetGameObjectArray(r_GameLogic.Board);
            r_GameLogic.Player2.ResetGameObjectArray(r_GameLogic.Board);
        }

        private void assignLogicEvents()
        {           
            r_GameLogic.Player1.ObjectMoved += MoveObject_EventHandler;
            r_GameLogic.Player2.ObjectMoved += MoveObject_EventHandler;
            r_GameLogic.Player1.ObjectEaten += EatenObject_EventHandler;
            r_GameLogic.Player2.ObjectEaten += EatenObject_EventHandler;
            r_GameLogic.Player1.BecameKing += BecameKing_EventHandler;
            r_GameLogic.Player2.BecameKing += BecameKing_EventHandler;
        }
        
        private void MoveEntered_EventHandler(MovePointsEventArgs e)
        {
            playSingleTurn(e);
        }

        private void MoveObject_EventHandler(MovePointsEventArgs e)
        {
            r_Damka.SetButtonsAfterMove(e.SourcePoint, e.DestinationPoint);
        }

        private void EatenObject_EventHandler(CellChangedEventArgs e)
        {
            r_Damka.SetButtonAfterEating(e.ChangedPoint);            
        }

        private void BecameKing_EventHandler(CellChangedEventArgs e)
        {
            r_Damka.SetKing(
                e.ChangedPoint,
                r_Damka.IsPlayer1Turn ? ePlayerObjectsColor.White : ePlayerObjectsColor.Black);
        }

        private void playSingleTurn(MovePointsEventArgs e)
        {
            bool isEatingMove;
            Player player = r_Damka.IsPlayer1Turn ? r_GameLogic.Player1 : r_GameLogic.Player2;
            Player opponent = r_Damka.IsPlayer1Turn ? r_GameLogic.Player2 : r_GameLogic.Player1;

            player.FindAllObjectsNextMoves(r_GameLogic.Board, true);
            if (!player.CheckIfAnyEatingMoveExist())
            {
                player.FindAllObjectsNextMoves(r_GameLogic.Board, false);
            }

            if (player.CheckValidityNextMove(e.SourcePoint, e.DestinationPoint))
            {
                player.MoveObject(e.SourcePoint, e.DestinationPoint, opponent, r_GameLogic.Board, out isEatingMove);                
                player.FindAllObjectsNextMoves(r_GameLogic.Board, true);
                r_Damka.IsNextMoveIsEating = isEatingMove && player.CheckPossibleMovesAfterEatingMove(e.DestinationPoint);
                r_Damka.IsPlayer1Turn = r_Damka.IsNextMoveIsEating ? r_Damka.IsPlayer1Turn : !r_Damka.IsPlayer1Turn;

                if (!r_Damka.IsPlayer1Turn && r_GameLogic.Player2.Type == ePlayerType.Computer &&
                    !r_GameLogic.Player2.CheckIfLoose(r_GameLogic.Board))
                {
                    r_GameLogic.Player2.MoveComputer(r_GameLogic.Board, r_GameLogic.Player1);
                    r_Damka.IsPlayer1Turn = !r_Damka.IsPlayer1Turn;
                }
            }
            else
            {
                r_Damka.ShowInvalidMoveMessage();
            }

            if ((r_GameLogic.Player1.CheckIfLoose(r_GameLogic.Board) && r_Damka.IsPlayer1Turn) ||
                (r_GameLogic.Player2.CheckIfLoose(r_GameLogic.Board) && !r_Damka.IsPlayer1Turn))
            {
                checkGameResults(); 
            }
        }

        private void checkGameResults()
        {
            bool anotherGame;

            if (Player.CheckIfTie(r_GameLogic.Player1, r_GameLogic.Player2, r_GameLogic.Board))
            {
                anotherGame = r_Damka.HandleTieResult();               
            }
            else if (r_GameLogic.Player1.CheckIfLoose(r_GameLogic.Board))
            {
                r_GameLogic.Player2.CalculateWinnerPoints(r_GameLogic.Player1);
                anotherGame = r_Damka.HandleWinResult(r_GameLogic.Player2);
            }
            else
            {
                r_GameLogic.Player1.CalculateWinnerPoints(r_GameLogic.Player2);
                anotherGame = r_Damka.HandleWinResult(r_GameLogic.Player1);
            }

            if(anotherGame)
            {
                resetLogicBoards();
                r_Damka.ResetFormForAnotherRound(r_GameLogic.Board);
            }
            else
            {
                r_Damka.Close();
            }
        }
    }
}
