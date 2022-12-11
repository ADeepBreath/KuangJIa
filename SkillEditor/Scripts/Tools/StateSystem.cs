using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum StateType
{
    Walk,
    Idle,
    Follow,
    Attack
}
public class StateSystem
{
    public StateType type = StateType.Idle;

    public Dictionary<StateType, Action> states = new Dictionary<StateType, Action>();

    // Update is called once per frame
    public void Update()
    {
        states[type].Invoke();
    }
}
