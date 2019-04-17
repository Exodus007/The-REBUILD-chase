using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScript : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    public float upSpeed = 13.0f;
    public NavMeshMove enemyScript;
    public winningConcept winningScript;
    bool deciderStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
           // rb.velocity=(Vector3.up * upSpeed);
        }
        if(other.gameObject.tag == "enemy")
        {

            if(enemyScript.telePadGround && winningScript.carriedByEnemy && winningScript.enemyCurrentName == other.gameObject.transform.name)
            {
                enemyScript = other.gameObject.GetComponent<NavMeshMove>();
                //lets give the control to bool so it can call that method continuously 
                enemyScript.decider();

            }
            if(winningScript.carriedByEnemy && winningScript.enemyCurrentName == other.gameObject.transform.name && enemyScript.telePadGreen)
            {
                //lets make the decider 1 active here
                enemyScript = other.gameObject.GetComponent<NavMeshMove>();
                enemyScript.Decider1();
            }
           
        }
       
       
    }
}
