
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
}