
namespace Util.EventHandleSystem
{
    public struct ShowPlayerTurnText
    {
        public readonly bool IsMainPlayer;

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
    
    public struct SettlementEvent
    {
    }
}