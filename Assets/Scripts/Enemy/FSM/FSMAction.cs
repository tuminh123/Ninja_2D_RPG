using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMAction : MonoBehaviour
{
    protected const string moveX = "movex";
    protected const string moveY = "movey";

    public abstract void Action();
}
