using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2dScript : MonoBehaviour
{
    [SerializeField] public UnityEvent<Collider2D> OnTrigger;
    [SerializeField] public bool isDebug;

    private void OnTriggerEnter2D(Collider2D other) {
        if (isDebug) {
            Debug.Log("trigger by " + other.tag.ToString());
        }
        OnTrigger.Invoke(other);
    }
}
