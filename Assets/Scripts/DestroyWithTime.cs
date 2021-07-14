using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTime : MonoBehaviour
{
    public float aliveTime = 10;

    private void FixedUpdate() {
        aliveTime -= Time.fixedDeltaTime;
        if (aliveTime <= 0) {
            Destroy(this.gameObject);
        }
    }
}
