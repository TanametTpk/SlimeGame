using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    abstract public void PerformBy(RPGCharacter character);
    public virtual void StopPerformBy(RPGCharacter character){
        // do nothing
    }

    public virtual void Reset(RPGCharacter character) {

    }

    public virtual void UpdateBy(RPGCharacter character){

    }

    public virtual void FixedUpdateBy(RPGCharacter character){

    }
}
