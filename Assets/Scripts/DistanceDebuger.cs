using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDebuger : MonoBehaviour
{
    public float radius = 0;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
