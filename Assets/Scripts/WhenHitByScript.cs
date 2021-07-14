using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenHitByScript : MonoBehaviour
{
    [SerializeField] public string[] tags;

    public virtual void OnHitTrigger2D(Collider2D other) {
        if (IsInTags(other.tag))
            PerformTrigger2D(other);
    }
    
    public virtual void OnHitCollision2D(Collision2D other) {
        if (IsInTags(other.transform.tag))
            PerformCollision2D(other);
    }

    public virtual void OnHitTrigger(Collider other) {
        if (IsInTags(other.tag))
            PerformTrigger(other);
    }
    
    public virtual void OnHitCollision(Collision other) {
        if (IsInTags(other.transform.tag))
            PerformCollision(other);
    }

    protected bool IsInTags(string tag) {
        foreach (string target in tags)
        {
            if (target.Equals(tag))
            {
                return true;
            }
        }
        return false;
    }

    public virtual void PerformTrigger2D(Collider2D other) {

    }

    public virtual void PerformCollision2D(Collision2D other) {

    }

    public virtual void PerformTrigger(Collider other) {

    }

    public virtual void PerformCollision(Collision other) {

    }
}
