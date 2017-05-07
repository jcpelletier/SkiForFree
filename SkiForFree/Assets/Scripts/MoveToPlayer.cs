using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public Transform player;

    private float _speed = 100f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("tree collision");
        Debug.Log(collision.gameObject.name);


        string[] avoid = { "Tree-Dead_A", "Stump_A", "Stump_B" };
        Debug.Log(Array.IndexOf(avoid, collision.gameObject.name));

        if (Array.IndexOf(avoid, collision.gameObject.name) >= 0)
        {
            Debug.Log("tree collision");
            Physics.IgnoreCollision(transform.GetComponent<Collider>(), collision.collider);
        }
    }
}
