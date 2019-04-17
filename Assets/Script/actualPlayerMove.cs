using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class actualPlayerMove : MonoBehaviour
{
    [Header("References")]
    public float rotationY;
    Quaternion targetAngle;
    Rigidbody rb;
    public float speed=8.0f;
    public Text scoreText;
    public Button startButton;
    public Text startText;
    bool restart = false;
    float pushPlayerX;
    float pushPlayerZ;
    float pushX, pushZ;
    public float flagSpeed = 10f;
    public float pushSpeed=1.0f;

    [Header("WinningRefer")]
    public winningConcept winningScript;
    Animator anim;

    [Header("FireHolders")]
    public GameObject[] fireHolders;
    GameObject finalFlag;
    public Text countDownText;
    float countDuration = 10;
    float counter;
    Rigidbody rb1;
    public bool greenSuracfe = false;
    GameObject mainCam;
    [Range(0.0f,1.0f)]
    public float transitionSpeed=0.5f;
    public bool whenItisinGround = false;
    //all about animation
    GameObject find;
    Animator playerAnim;
    SphereCollider bx;
    public PivotCamera pivotCameraScript;
    public bool turnOffThePlayerCollision = false;
    private void Awake()
    {
        GameObject find = GameObject.FindGameObjectWithTag("playerChar");
         playerAnim= find.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        counter = countDuration;
        Time.timeScale = 1;
        startButton.enabled = false;
        startText.enabled = false;
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        finalFlag = GameObject.FindWithTag("nationalFlag");
        rb1 = finalFlag.GetComponent<Rigidbody>();
        mainCam = Camera.main.gameObject;
        bx = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(winningScript.originalPos == winningScript.transform.position)
        {
            counter = countDuration;
        }
        //transform.eulerAngles = new Vector3(0f, player.transform.localEulerAngles.y,0f);
        //rb.velocity=(transform.forward * Time.deltaTime * speed);
        transform.position += transform.forward * Time.deltaTime * speed;
      
       if(winningScript.carriedByPlayer)
        {
            //let begin the countdown 
            countDownText.text = this.transform.name + ": " + (int)counter;
         counter -= Time.deltaTime;
            if(counter<=0)
            {
                countDownText.text = this.transform.name + ": " + "win";
                startButton.enabled = true;
                startText.enabled = true;
                startText.text = "Press anywhere to restart";
                Time.timeScale = 0;
               
            }

            //if the player carried the flag and someone hits then the flag will go toward that enemy and make the carried by player to false...
            
           
         
        }
   
     


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GroundSurface")
        {
           
            Vector3 camPos = mainCam.transform.localPosition;
            // camPos.z = -44f;
            //mainCam.transform.localPosition = camPos;
            //Lets do the zooming effect 
            Debug.Log("Z cam value" + mainCam.transform.localPosition.z);

        }
       

        if (winningScript.carriedByPlayer)
        {
            //actually holding the flag 
            //now check if it got hit or  not by other enemy 
            if (collision.gameObject.tag == "enemy")
            {
                counter = countDuration;
                //get the flag back to its original pos 
                //finalFlag.transform.position = winningScript.originalPos;
                foreach (ContactPoint cp in collision.contacts)
                {
                    pushX = cp.normal.x;
                    pushZ = cp.normal.z;

                }
                //rb1.velocity = new Vector3(pushPlayerX, 2, pushPlayerZ) * flagSpeed;
                rb.AddForce (new Vector3(pushPlayerX, 0.8f, pushPlayerZ) * flagSpeed) ;
               
                winningScript.carriedByPlayer = false;
               
                winningScript.currentEnemyObject = collision.gameObject;
                winningScript.carriedByOtherEnemy = true;
                turnOffThePlayerCollision = true;
                StartCoroutine(turnOnPlayerCollision());


                //after this lets  by bool value turn off the winning script of carried player to false

                




                //lets make the player trigger on so it cannot collide with the flag instantly 
               // bx.isTrigger = true;
                //StartCoroutine(startCollision());


            }
        }
        if (collision.gameObject.tag == "flag")
        {
            //lets make the player teleport 
            Destroy(collision.gameObject);
            scoreText.text = "You Win";
            Time.timeScale = 0;




        }

        if (collision.gameObject.tag == "DeadZone")
        {
            Destroy(this.gameObject);
            //scoreText.text = "Game Over";
            startButton.enabled = true;
            startText.enabled = true;
            startText.text = "Press anywhere to restart";
            Time.timeScale = 0;
            //lets display the canvas which contains reset button to restart the game 

        }
        if (collision.gameObject.tag == "enemy" && !winningScript.carriedByPlayer && !turnOffThePlayerCollision)
        {
            foreach (ContactPoint cp in collision.contacts)
            {
                pushPlayerX = cp.normal.x;//x pos

                pushPlayerZ = cp.normal.z;//z pos
            }
            //lets now make the player to get pushBack in that direction 
            rb.velocity= (new Vector3(pushPlayerX, 0.7f, pushPlayerZ) * pushSpeed);
            counter = countDuration;


        }
        if (collision.gameObject.tag == "surface")
        {
            //lets stop the countDown of player or enemy which is Currently have the flag 
            winningScript.countDownText.text = " ";
            winningScript.counter = winningScript.countDuration;
            winningScript.countBeganPlayer = false;

        }
        if (collision.gameObject.tag == "nationalFlag")
        {
            counter = countDuration;
            
        }
        if (collision.gameObject.tag == "trickPush")
        {
            foreach (ContactPoint cp in collision.contacts)
            {
                pushPlayerX = cp.normal.x;//x pos

                pushPlayerZ = cp.normal.z;//z pos
            }
            //lets now make the player to get pushBack in that direction 
            rb.velocity = new Vector3(pushPlayerX, 4f, pushPlayerZ) * pushSpeed;

        
         
        }
        if(collision.gameObject.tag == "pushFlag")
        {
            foreach (ContactPoint cp in collision.contacts)
            {
                pushPlayerX = cp.normal.x;//x pos

                pushPlayerZ = cp.normal.z;//z pos
            }
            //lets now make the player to get pushBack in that direction 
            rb.velocity = new Vector3(pushPlayerX, 0f, pushPlayerZ) * pushSpeed;
            
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "greenSurface")
        {

            greenSuracfe = true;
        }
        else
        {
            greenSuracfe = false;
        }
        if(collision.gameObject.tag=="GroundSurface")
        {
            if (winningScript.carriedByEnemy || winningScript.carriedByPlayer)
            {
                whenItisinGround = true;
                speed = 8.5f;
               
                playerAnim.speed = 2.4f;

               // Debug.Log("Animator Logic is coming..");
            }
         
        }
        if (collision.gameObject.tag == "greenSurface" || collision.gameObject.tag == "pinkSurface")
        {
      
            playerAnim.speed = 1f;
            if (winningScript.carriedByEnemy || winningScript.carriedByPlayer)
            {
                whenItisinGround = false;
                speed = 6.8f;
            }

        }
        if(collision.gameObject.tag=="pinkSurface")
        {
            //lets access the pivot camera variable to make the camera movement well 
            pivotCameraScript.finalOneTouched = true;
        }
        if(collision.gameObject.tag =="GroundSurface")
        {
            pivotCameraScript.finalOneTouched = false;
        }
        if(collision.gameObject.tag == "greenSurface")
        {
            pivotCameraScript.finalOneTouched = false;
        }

    }
    IEnumerator startCollision()
    {
        yield return new WaitForSeconds(0.05f);
       // bx.isTrigger = false;
    }
    IEnumerator turnOnPlayerCollision()
    {
        yield return new WaitForSeconds(0.4f);
        Debug.Log("collision of the Player is getting on and off..");
        turnOffThePlayerCollision = false;
    }


}
