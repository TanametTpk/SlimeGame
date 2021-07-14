using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loging Action", menuName = "Loging Action")]
public class LogingAction : CharacterAction
{
    public string message;
    public override void PerformBy(RPGCharacter character)
    {
        Debug.Log("do " + message);
    }

    public override void StopPerformBy(RPGCharacter character)
    {
        Debug.Log("stop " + message);
    }
}
