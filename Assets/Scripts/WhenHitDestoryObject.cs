using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenHitDestoryObject : WhenHitByScript
{
    public string sound;
    public override void PerformTrigger2D(Collider2D other) {
        Destroy(this.gameObject);
        if (sound.Length > 0) {
            AudioManager.instance.Play(sound);
        }
    }
}
