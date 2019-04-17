using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeThePlayerToJump : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    public float upSpeed = 13.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.velocity = (Vector3.up * upSpeed);
        }
    }
}
