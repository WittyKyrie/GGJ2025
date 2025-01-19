using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class InputReader : Singleton<InputReader>
{
    public event Action<KeyState> OnP1PourKeyInput;
    public event Action<KeyState> OnP1Skill1KeyInput;
    public event Action<KeyState> OnP1Skill2KeyInput;
    public event Action<KeyState> OnP1Skill3KeyInput;
    
    public event Action<KeyState> OnP2PourKeyInput;
    public event Action<KeyState> OnP2Skill1KeyInput;
    public event Action<KeyState> OnP2Skill2KeyInput;
    public event Action<KeyState> OnP2Skill3KeyInput;
    public enum KeyState
    {
        Down,
        Hold,
        Up
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnP1PourKeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.Q)) OnP1Skill1KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.W)) OnP1Skill2KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.E)) OnP1Skill3KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.DownArrow)) OnP2PourKeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.J)) OnP2Skill1KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.K)) OnP2Skill2KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.L)) OnP2Skill3KeyInput?.Invoke(KeyState.Down);    
        
        if (Input.GetKey(KeyCode.Space)) OnP1PourKeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.Q)) OnP1Skill1KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.W)) OnP1Skill2KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.E)) OnP1Skill3KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.DownArrow)) OnP2PourKeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.J)) OnP2Skill1KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.K)) OnP2Skill2KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.L)) OnP2Skill3KeyInput?.Invoke(KeyState.Hold);  
        
        if (Input.GetKeyUp(KeyCode.Space)) OnP1PourKeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.Q)) OnP1Skill1KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.W)) OnP1Skill2KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.E)) OnP1Skill3KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.DownArrow)) OnP2PourKeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.J)) OnP2Skill1KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.K)) OnP2Skill2KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.L)) OnP2Skill3KeyInput?.Invoke(KeyState.Up);  
    }

    public void ClearItemKeys()
    {
        InputReader.Instance.OnP1Skill2KeyInput = null;
        InputReader.Instance.OnP1Skill3KeyInput = null;
        InputReader.Instance.OnP2Skill1KeyInput = null;
        InputReader.Instance.OnP2Skill2KeyInput = null;
        InputReader.Instance.OnP2Skill3KeyInput = null;
    }
}
