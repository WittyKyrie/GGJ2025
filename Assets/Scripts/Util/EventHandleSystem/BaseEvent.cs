
namespace Util.EventHandleSystem
{
    public struct ShowPlayerTurnText
    {
        public bool IsMainPlayer;

        public ShowPlayerTurnText(bool isMainPlayer)
        {
            IsMainPlayer = isMainPlayer;
        }
    }
    
    public struct ShowSelectBuff
    {
    }
    
    public struct MainPlayerTurn
    {
        
    }
    
    public struct SubPlayerTurn
    {
    }
}