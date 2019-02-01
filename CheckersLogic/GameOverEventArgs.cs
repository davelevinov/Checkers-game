namespace CheckersLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GameOverEventArgs : EventArgs
    {
        private eGameOverStatusCode m_GameOver;
        private CheckersMove m_LastMove;

       public eGameOverStatusCode GameOverStatusCode
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

       public CheckersMove LastMove
       {
           get 
           { 
               return m_LastMove;
           }

           set 
           { 
               m_LastMove = value;
           }
       }
    }
}
