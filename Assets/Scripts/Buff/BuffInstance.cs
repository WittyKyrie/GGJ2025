using System.Collections;
using System.Collections.Generic;
using Buff;
using UnityEngine;

public class BuffInstance
{
    public BuffData buffData;
    public Player.Player fromPlayer;
    public bool used = false;
    
    public void Init(Player.Player player)
    {
        fromPlayer = player;
    }

    public virtual void OnUseItem()
    {
    }
    public virtual void OnPourEnd(){}
    public virtual void OnRoundEnd(){}
}

public class PropHand : BuffInstance
{
    public override void OnUseItem()
    {
        if (GameManager.Instance.GetCurrentState() == GameState.MainPlayerTurn)
        {
            BlockingHand.Instance.Init(fromPlayer == GameManager.Instance.mainPlayer , GameManager.Instance.subPlayer.beerGlass);
        }
        else if (GameManager.Instance.GetCurrentState() == GameState.SubPlayerTurn)
        {
            BlockingHand.Instance.Init(fromPlayer == GameManager.Instance.mainPlayer , GameManager.Instance.mainPlayer.beerGlass);
        }
    }
    public override void OnPourEnd()
    {
        BlockingHand.Instance.EndEffect();
        fromPlayer.RemoveItem(this);
    }
    public override void OnRoundEnd()
    {
        BlockingHand.Instance.EndEffect();
        fromPlayer.RemoveItem(this);
    }
}
public class PropSnacks : BuffInstance
{
}
public class PropPower : BuffInstance
{
}
public class PropSnakes : BuffInstance
{
}
public class PropChopsticks : BuffInstance
{
}
public class PropMTS : BuffInstance
{
}
public class PropLiftTable : BuffInstance
{
}
public class PropTransform : BuffInstance
{
}
public class PropCall : BuffInstance
{
}