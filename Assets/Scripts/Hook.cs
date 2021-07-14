using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Transform target;
    public LineRenderer lineRenderer;
    public SpringJoint2D joint2D;
    public float pullSpeed = 1;

    void Start()
    {
        joint2D.enabled = false;
    }

    void Update()
    {
        DoHook();

        if (!joint2D.enabled) {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void FixedUpdate() {
        Vector3 direction = transform.position - target.position;
        target.Translate(direction.normalized * pullSpeed * Time.fixedDeltaTime);
    }

    public void DoHook() {
        lineRenderer.SetPosition(0, target.position);
        lineRenderer.SetPosition(1, transform.position);
        joint2D.connectedAnchor = target.position;
        joint2D.enabled = true;
        lineRenderer.enabled = true;
    }

    public void UnHook() {
        joint2D.enabled = false;
        lineRenderer.enabled = false;
    }
}
