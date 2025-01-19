using System.Collections;
using System.Collections.Generic;
using Buff;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
    public override void OnUseItem()
    {
        fromPlayer.GetSnacksMultiplier += GetSnacksMultiplier;
        fromPlayer.SwitchSnackLabel(true);
    }
    public override void OnRoundEnd()
    {
        fromPlayer.SwitchSnackLabel(false);
        fromPlayer.GetSnacksMultiplier -= GetSnacksMultiplier;
        fromPlayer.RemoveItem(this);
    }

    private float GetSnacksMultiplier()
    {
        return 0.5f;
    }
}
public class PropPower : BuffInstance
{
    public override void OnUseItem()
    {
        fromPlayer.GetPropPowerMultiplier += GetMultiplier;
    }
    public override void OnRoundEnd()
    {
        fromPlayer.GetPropPowerMultiplier -= GetMultiplier;
        fromPlayer.RemoveItem(this);
    }
    private float GetMultiplier()
    {
        return 0.5f;
    }
}
public class PropSnakes : BuffInstance
{
    private GameObject GO;
    public override void OnUseItem()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/SnakeParticle.prefab");
        // Block until the operation is done
        handle.WaitForCompletion();
        Vector3 position = Vector2.zero;
        if (GameManager.Instance.GetCurrentState() == GameState.MainPlayerTurn)
        {
            position = GameManager.Instance.subPlayer.beerGlass.transform.position.Offset(y:7);
        }
        else if (GameManager.Instance.GetCurrentState() == GameState.SubPlayerTurn)
        {
            position = GameManager.Instance.mainPlayer.beerGlass.transform.position.Offset(y:7);
        }
        var snakePrefab = handle.Result;
        GO = Object.Instantiate(snakePrefab, position,  Quaternion.Euler(0,0,Random.Range(0, 360)));
    }
    public override void OnPourEnd()
    {
        Object.Destroy(GO);
        fromPlayer.RemoveItem(this);
    }
}
public class PropChopsticks : BuffInstance
{
    private GameObject GO;
    public override void OnUseItem()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/ChopStickParticle.prefab");
        // Block until the operation is done
        handle.WaitForCompletion();
        Vector3 position = Vector2.zero;
        if (GameManager.Instance.GetCurrentState() == GameState.MainPlayerTurn)
        {
            position = GameManager.Instance.subPlayer.beerGlass.transform.position.Offset(x:Random.Range(-2f,2f),y:10);
        }
        else if (GameManager.Instance.GetCurrentState() == GameState.SubPlayerTurn)
        {
            position = GameManager.Instance.mainPlayer.beerGlass.transform.position.Offset(x:Random.Range(-2f,2f),y:10);
        }
        var prefab = handle.Result;
        GO = Object.Instantiate(prefab, position,  Quaternion.Euler(0,0,Random.Range(0, 360)));
    }
    public override void OnPourEnd()
    {
        Object.Destroy(GO);
        fromPlayer.RemoveItem(this);
    }
}
public class PropMTS : BuffInstance
{
    private GameObject GO;
    public override void OnUseItem()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/MentosParticle.prefab");
        // Block until the operation is done
        handle.WaitForCompletion();
        Vector3 position = Vector2.zero;
        if (GameManager.Instance.GetCurrentState() == GameState.MainPlayerTurn)
        {
            position = GameManager.Instance.subPlayer.beerGlass.transform.position.Offset(x:Random.Range(-0.5f,0.5f),y:9);
        }
        else if (GameManager.Instance.GetCurrentState() == GameState.SubPlayerTurn)
        {
            position = GameManager.Instance.mainPlayer.beerGlass.transform.position.Offset(x:Random.Range(-0.5f,0.5f),y:9);
        }
        var prefab = handle.Result;
        GO = Object.Instantiate(prefab, position, Quaternion.identity);
    }
    public override void OnPourEnd()
    {
        Object.Destroy(GO);
        fromPlayer.RemoveItem(this);
    }
}
public class PropLiftTable : BuffInstance
{
    public override void OnUseItem()
    {
        if (GameManager.Instance.GetCurrentState() == GameState.MainPlayerTurn)
        {
            GameManager.Instance.subPlayer.beerGlass.TriggerLiftTable(GameManager.Instance.subPlayer);
        }
        else if (GameManager.Instance.GetCurrentState() == GameState.SubPlayerTurn)
        {
            GameManager.Instance.subPlayer.beerGlass.TriggerLiftTable(GameManager.Instance.subPlayer);
        }
    }
    public override void OnPourEnd()
    {
        fromPlayer.RemoveItem(this);
    }
}
public class PropTransform : BuffInstance
{
    public override void OnUseItem()
    {
        if (GameManager.Instance.GetCurrentState() == GameState.MainPlayerTurn)
        {
            GameManager.Instance.subPlayer.beerGlass.TriggerSwitchBeerAndFoam();
        }
        else if (GameManager.Instance.GetCurrentState() == GameState.SubPlayerTurn)
        {
            GameManager.Instance.subPlayer.beerGlass.TriggerSwitchBeerAndFoam();
        }
    }
    public override void OnPourEnd()
    {
        fromPlayer.RemoveItem(this);
    }
}
public class PropCall : BuffInstance
{
}