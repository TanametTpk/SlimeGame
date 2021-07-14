using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase: ScriptableObject {
    public string name;
    public string description;
    public float duration;
    public bool isOneTimeUse;
    public bool isUse;

    abstract public void OnAdd(RPGCharacter character);
    abstract public void OnRemove(RPGCharacter character);

    public virtual void Update() {
        // do something
    }
}