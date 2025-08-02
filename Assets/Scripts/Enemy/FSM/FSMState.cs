using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FSMState
{
    public string id;
    public FSMAction[] actions;
    public FSMTransition[] transitions;

    public void UpdateState(EnemyBrain enemyBrain)
    {
        this.ExecuteActions();
        this.ExecuteTranssitions(enemyBrain);
    }

    private void ExecuteActions()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Action();
        }
    }

    private void ExecuteTranssitions(EnemyBrain enemyBrain)
    {
        if (transitions == null || transitions.Length <= 0) return;
        for (int i = 0; i < transitions.Length; i++)
        {
            bool value = transitions[i].decision.Decide();
            if (value)
            {
                enemyBrain.ChangeState(transitions[i].trueState);
            }
            else
            {
                enemyBrain.ChangeState(transitions[i].falseState);
            }
        }
    }
}
