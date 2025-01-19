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
        if (Input.GetKeyDown(KeyCode.Keypad1)) OnP2Skill1KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.Keypad2)) OnP2Skill2KeyInput?.Invoke(KeyState.Down);
        if (Input.GetKeyDown(KeyCode.Keypad3)) OnP2Skill3KeyInput?.Invoke(KeyState.Down);    
        
        if (Input.GetKey(KeyCode.Space)) OnP1PourKeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.Q)) OnP1Skill1KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.W)) OnP1Skill2KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.E)) OnP1Skill3KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.DownArrow)) OnP2PourKeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.Keypad1)) OnP2Skill1KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.Keypad2)) OnP2Skill2KeyInput?.Invoke(KeyState.Hold);
        if (Input.GetKey(KeyCode.Keypad3)) OnP2Skill3KeyInput?.Invoke(KeyState.Hold);  
        
        if (Input.GetKeyUp(KeyCode.Space)) OnP1PourKeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.Q)) OnP1Skill1KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.W)) OnP1Skill2KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.E)) OnP1Skill3KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.DownArrow)) OnP2PourKeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.Keypad1)) OnP2Skill1KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.Keypad2)) OnP2Skill2KeyInput?.Invoke(KeyState.Up);
        if (Input.GetKeyUp(KeyCode.Keypad3)) OnP2Skill3KeyInput?.Invoke(KeyState.Up);  
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
