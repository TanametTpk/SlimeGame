  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookScript : MonoBehaviour
{
    public string[] tagsToCheck;
    //Force applied to nova bomb upon spawn
    public float speed, returnSpeed;
    public float range, stopRange;

    //Private variables
    [HideInInspector]
    public Transform caster, collidedWith;
    private LineRenderer line;
    private bool hasCollided;
    private Vector2 initCasterPosition;
    public Vector2 direction;
    public GameObject hookedObject;

    private void Start()
    {
        line = transform.GetComponent<LineRenderer>();
        initCasterPosition = caster.transform.position;
    }

    private void Update()
    {
        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);
            //Check if we have impacted
            if (hasCollided)
            {
                direction = caster.position - transform.position;
                var dist = Vector2.Distance(transform.position, caster.position);
                if (dist < stopRange)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                var dist = Vector2.Distance(transform.position, initCasterPosition);
                if (dist > range)
                {
                    Collision(null);
                }
            }

            transform.Translate(direction.normalized * speed * Time.deltaTime);
            if (collidedWith) { collidedWith.transform.position = transform.position; }
        }
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Here add as many checks as you want for your nova bomb's collision
        if (!hasCollided && tagsToCheck.Contains(other.gameObject.tag))
        {
            Collision(other.transform);
            transform.localScale = transform.localScale * 5f;
        }
    }

    void Collision(Transform col)
    {
        speed = returnSpeed;
        //Stop movement
        hasCollided = true;
        if (col)
        {
            transform.position = col.position;
            collidedWith = col;
            hookedObject = col.gameObject;
        }
    }
}