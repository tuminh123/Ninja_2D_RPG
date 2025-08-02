using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FSMTransition
{
    public FSMDecision decision;//vd: is player in a range attack->true or false
    public string trueState;// true->current state->attack state
    public string falseState;//false->current state->patrol state
}
