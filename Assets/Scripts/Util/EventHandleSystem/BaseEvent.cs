
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
    
    public struct ShowUseBuff
    {
        public readonly string BuffKey;

        public ShowUseBuff(string buffKey)
        {
            BuffKey = buffKey;
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
    
    public struct BeerIsFullEvent
    {
    }
    
    public struct DrinkingEvent
    {
    }
}