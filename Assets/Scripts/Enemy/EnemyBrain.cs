using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initState;
    [SerializeField] private FSMState[] states;

    public FSMState currentState { get; private set; }

    [SerializeField] private Transform player;
    public Transform Player => player;

    private void Start()
    {
        ChangeState(initState);
    }

    private void Update()
    {
        if (currentState == null) return;
        currentState.UpdateState(this);
    }
    public void ChangeState(string newStateID)
    {
        FSMState newState = GetState(newStateID);
        if (newState == null) return;
        currentState = newState;
    }
    private FSMState GetState(string newStateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].id == newStateID)
            {
                return states[i];
            }
        }
        return null;
    }
    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
}

