using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> OnPickup;
    [SerializeField] private bool isDebug;

    private void OnTriggerEnter2D(Collider2D other) {
        if (isDebug) {
            Debug.Log("pick up by " + other.tag.ToString());
        }
        OnPickup.Invoke(other);
    }
}
