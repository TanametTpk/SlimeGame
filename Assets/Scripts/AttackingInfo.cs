using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackingInfo
{
    public RPGCharacter attacked;
    public RPGCharacter attacker;
    public float damage;
    public List<AttackEffect> effects = new List<AttackEffect>();
}
