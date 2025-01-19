using System;
using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using TimToolBox.Extensions;
using UnityEngine;

public class BlockingHand : MonoBehaviour
{
    public static BlockingHand Instance;
    
    public SpriteRenderer hand;
    public CountdownTimer activeTimer = new CountdownTimer(5);

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Init(bool faceLeft, BeerGlass targetGlass)
    {
        gameObject.SetActive(true);
        hand.flipX = !faceLeft;
        transform.position = targetGlass.transform.position.Set(z: transform.position.z).Offset(y:4);
        activeTimer.Start();
    }

    public void EndEffect()
    {
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (gameObject.activeSelf && activeTimer.IsFinished) EndEffect();
    }
}
