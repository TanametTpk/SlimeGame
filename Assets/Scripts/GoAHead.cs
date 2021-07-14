using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAHead : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime, Space.Self);
    }
}
