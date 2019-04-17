using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class NavMeshMove : MonoBehaviour
{
    [SerializeField]
    public Transform destination;
    
   public  NavMeshAgent navMeshAgent;
    Rigidbody rb;
    //locations of striking 
     float pushX, pushZ;
    public float pushSpeed = 1.0f;
    public GameObject parent;
    [Header("Player")]
    public GameObject player;
    [Header("PlayerScript")]
    public actualPlayerMove playerScript;
    public GameObject[] enemies;
    public winningConcept winningScript;
    bool distanceNotCovered = false;
    bool isOnfinalSurface = false;
    GameObject finalFlag;
    public Text countDownText;
    public float countDuration = 10f;
    float counter;
    public Text startText;
    public Button startButton;
    float distanceEnemyNeeded1;
    float distanceEnemyNeeded2;
    float distanceEnemyNeeded3;
    float distanceEnemyNeeded4;
    public bool finalSurface = true;
    public GameObject[] targetDown;
    public float forwardSpeed = 5.0f;
    bool groundSurface = false;
    int randomTarget;
    Rigidbody rb1;
    public float flagSpeed = 10f;
    public GameObject follow;
    float random;Vector3 targetRotation;
    public float rotationSpeed = 0.6f;
    public GameObject wall;
    float distance;
    bool fleeOff = false;
    float up, down, left, right;
    Vector3 oldPosition;public bool rotate = false;
    public bool offOthers = false;
    bool canStop = false;
    public GameObject[] randomLocation;
    bool makeGeneral = true;
  int random1;int random2;
    bool dontExecute,dontExceuteInner;
    public GameObject arrow;
    Vector3 backtoPos;
    public GameObject arrowPart1, arrowPart2;
    //Decider variables
    public GameObject[] jumpPads;
    public GameObject[] secondLayerTarget;
    bool isGrounded = true;bool secondLayer = false;public bool telePadGround = true;
    bool groundIsDone = false;bool secondLayerIsDone = false;
    public GameObject thirdLayerTarget;
    public bool whenItisinGround = false;
    //all about animation
    GameObject find1, find2, find3, find4, find;
    Animator anim1, anim2, anim3, anim4, anim;
    public bool hitByOtherEnemy = false;
    SphereCollider sx;
    bool waitForSomeTime = false;bool pushTouched = false;
    bool notBothTouched = false;
    public bool makeTheLogicOff = false;
    bool nowIsOnGreenSurface = false;
    bool backToNormal = false;
    public bool telePadGreen = false;
    bool decider1 = false;bool ddecider = false;
    bool turnOffcurrentEnemyCollision = false;
    int deciderRandom;
    // Start is called before the first frame update
    private void Awake()
    {
        arrow = GameObject.Find("arrow");
        sx = this.GetComponent<SphereCollider>();
        backtoPos = arrow.transform.position;
        oldPosition = this.transform.position;
        up = 0f;
        down = 180f;
        left = -90f;
        right = 90f;
        finalSurface = false;
        Time.timeScale = 1;
        counter = countDuration;
        finalFlag = GameObject.FindWithTag("nationalFlag");
        rb = this.GetComponent<Rigidbody>();
        rb1 = finalFlag.GetComponent<Rigidbody>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
         find4 = GameObject.FindGameObjectWithTag("enemy4Char");
        find3 = GameObject.FindGameObjectWithTag("enemy3Char");
        find2 = GameObject.FindGameObjectWithTag("enemy2Char");
        find1 = GameObject.FindGameObjectWithTag("enemy1Char");
        find = GameObject.FindGameObjectWithTag("enemyChar");
        //getting animator component
        anim = find.GetComponent<Animator>();
        anim1 = find1.GetComponent<Animator>();
        anim2 = find2.GetComponent<Animator>();
        anim3 = find3.GetComponent<Animator>();
        anim4 = find4.GetComponent<Animator>();
    }
   
    private void FixedUpdate()
    {
        if(this.transform.name==winningScript.enemyCurrentName && winningScript.carriedByEnemy==true || this.transform.name==winningScript.enemyCurrentName && winningScript.carriedByOtherEnemy)
        {
            countDownText.text = winningScript.enemyCurrentName + ": " + (int)counter;
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                countDownText.text = winningScript.enemyCurrentName + ": " + "win";
                startButton.enabled = true;
                startText.enabled = true;
                startText.text = "Press anywhere to restart";
                Time.timeScale = 0;

            }


        }


        if (decider1==true && navMeshAgent.enabled==true)
        {
            //lets force the enemy to go upwards
            
            navMeshAgent.SetDestination(thirdLayerTarget.transform.position);
        }



        //make general to true when the enemies are not holding the flag 

     if(backToNormal==true && winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName && navMeshAgent.enabled==true)
        {
            navMeshAgent.SetDestination(follow.transform.position);
            makeGeneral = false;
            makeTheLogicOff = true;

        }
        

        arrow.transform.position = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y + 3f, player.transform.localPosition.z);
        arrow.transform.LookAt(destination.transform.position);

        if (!winningScript.carriedByEnemy && !winningScript.carriedByPlayer)
        {
            //back to normal speed
            navMeshAgent.speed = 6.5f;
            playerScript.speed = 6.5f;
            navMeshAgent.angularSpeed = 140f;
            navMeshAgent.acceleration = 15f;
            makeGeneral = true;
        }
      

        if (winningScript.carriedByPlayer)
        {
            arrow.transform.position = new Vector3(0f,0f,0f);
         
            //if they are in bottom layer then we will increase 20 percent more speed 
            if(whenItisinGround)
            {
                navMeshAgent.speed = 11f;
                navMeshAgent.angularSpeed = 600f;
                navMeshAgent.acceleration = 42f;
                //here we will increase the speed so animation will get increase 
                
                if(this.gameObject.name=="enemy4")
                {
                 
                    anim4.speed = 2.4f;
                 
                }
                if(this.gameObject.name=="enemy3")
                {
                    
                    anim3.speed = 2.4f;
             

                }
                if (this.gameObject.name == "enemy2")
                {
               
                    anim2.speed = 2.4f;
                  

                }
                if (this.gameObject.name == "enemy1")
                {
                
                    anim1.speed = 2.4f;
                    

                }
                if (this.gameObject.name == "enemy")
                {
                 
                    anim.speed = 2.4f;
                   

                }
                // playerScript.speed = 8.2f;
            }
            else
            {
               navMeshAgent.speed = 6.5f;
               navMeshAgent.angularSpeed = 600f;
               navMeshAgent.acceleration = 42f;
                if (this.gameObject.name == "enemy4")
                {
                    
                    anim4.speed = 1f;
              
                }
                if (this.gameObject.name == "enemy3")
                {
                
                    anim3.speed = 1f;
                    

                }
                if (this.gameObject.name == "enemy2")
                {
                   
                    anim2.speed = 1f;
                    

                }
                if (this.gameObject.name == "enemy1")
                {
                   
                    anim1.speed = 1f;
                    

                }
                if (this.gameObject.name == "enemy")
                {
               
                    anim.speed = 1f;
                

                }
                //playerScript.speed = 6.8f;
            }
          
           
           // winningScript.GetComponent<BoxCollider>().isTrigger = false;
         


        }
     
        

        
      
       
        

        if (navMeshAgent == null)
        {
            Debug.Log("The navmesg agent is not attached to it ");
        }
        else
        {
            if(makeGeneral==true && navMeshAgent.enabled==true)
            navMeshAgent.SetDestination(destination.transform.position);
            //if(!nowIsOnGreenSurface) 
            // setLocation(destination.transform.position);
        }
        if(winningScript.carriedByEnemy)
        {
          
                if(whenItisinGround)
                {
                    //lets give 2x speed here 
                    navMeshAgent.speed = 8.1f;
                    navMeshAgent.angularSpeed = 600f;
                    navMeshAgent.acceleration = 42f;
                    if (this.gameObject.name == "enemy4")
                    {
                   
                        anim4.speed = 2.4f;
                       
                    }
                    if (this.gameObject.name == "enemy3")
                    {
                   
                        anim3.speed = 2.4f;
                      

                    }
                    if (this.gameObject.name == "enemy2")
                    {
                       
                        anim2.speed = 2.4f;
                      

                    }
                    if (this.gameObject.name == "enemy1")
                    {
                       
                        anim1.speed = 2.4f;
                      

                    }
                    if (this.gameObject.name == "enemy")
                    {

                        anim.speed = 2.4f;
                       

                    }
                    // playerScript.speed = 8.4f;

                }
                else
                {
                    navMeshAgent.speed = 6.5f;
                    navMeshAgent.angularSpeed = 600f;
                    navMeshAgent.acceleration = 42f;
                    if (this.gameObject.name == "enemy4")
                    {
                       
                        anim4.speed = 1f;
                      
                    }
                    if (this.gameObject.name == "enemy3")
                    {
                      
                        anim3.speed = 1f;
                       

                    }
                    if (this.gameObject.name == "enemy2")
                    {
                       
                        anim2.speed = 1f;
                      

                    }
                    if (this.gameObject.name == "enemy1")
                    {
                        anim1.speed = 1f;
             

                    }
                    if (this.gameObject.name == "enemy")
                    {
                 
                        anim.speed = 1f;
                 

                    }
                    // playerScript.speed = 6.8f;

                }
             
             
            
        }
       


        //lets start this only when the flag is holded by the current enemy 
        if (winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
        {
            //offOthers = false;
            if(whenItisinGround)
            {
                //lets give 2x speed here 
                navMeshAgent.speed = 9.12f;
                navMeshAgent.angularSpeed = 600f;
                navMeshAgent.acceleration = 42f;
                if (this.gameObject.name == "enemy4")
                {
                    GameObject find = GameObject.FindGameObjectWithTag("enemy4Char");
                    Animator anim = find.GetComponent<Animator>();
                    anim4.speed = 2.4f;
                  
                }
                if (this.gameObject.name == "enemy3")
                {
                 ;
                    anim3.speed = 2.4f;
                    

                }
                if (this.gameObject.name == "enemy2")
                {
                 
                    anim2.speed = 2.4f;
                    

                }
                if (this.gameObject.name == "enemy1")
                {
                    
                    anim1.speed = 2.4f;
                 

                }
                if (this.gameObject.name == "enemy")
                {
                    
                    anim.speed = 2.4f;
                 

                }
                // playerScript.speed = 8.4f;
            }
           else
            {
                navMeshAgent.speed = 6.5f;
                navMeshAgent.angularSpeed = 600f;
                navMeshAgent.acceleration = 42f;
                if (this.gameObject.name == "enemy4")
                {
                 
                    anim4.speed = 1f;
             
                }
                if (this.gameObject.name == "enemy3")
                {
                    
                    anim3.speed = 1f;
                  

                }
                if (this.gameObject.name == "enemy2")
                {
                   
                    anim2.speed = 1f;
                   

                }
                if (this.gameObject.name == "enemy1")
                {
                   ;
                    anim1.speed = 1f;
                   

                }
                if (this.gameObject.name == "enemy")
                {
                   
                    anim.speed = 1f;
                    

                }
                // playerScript.speed = 6.8f;
            }
          
            

            //lets indicate the player 
            arrowPart1.GetComponent<MeshRenderer>().material.SetColor("_Color", this.transform.gameObject.GetComponent<MeshRenderer>().material.color);
            arrowPart2.GetComponent<MeshRenderer>().material.SetColor("_Color", this.transform.gameObject.GetComponent<MeshRenderer>().material.color);


          

            if (whenItisinGround && navMeshAgent.enabled==true && this.transform.name==winningScript.enemyCurrentName && winningScript.carriedByEnemy && !makeTheLogicOff)
            {
                if (rb.velocity.magnitude == 0 || this.transform.position==oldPosition)
                {
                    //rotate = true;
                   //navMeshAgent.SetDestination(follow.transform.position);

                    offOthers = false;
                    dontExecute = false;
                    dontExceuteInner = false;
                    notBothTouched = true;
                    StartCoroutine(turnOff());
               
                    // Debug.Log("PlayerStopped moving");
                    //enemy stopped moving
                    if (rotate == true)
                    {

                        // random1 = UnityEngine.Random.Range(0, 2);
                        // navMeshAgent.SetDestination(randomLocation[random1].transform.position);

                      
                        // offOthers = true;
                    }
                    makeGeneral = false;

                    Debug.Log("Player is stopped now");

                }
                else
                {
                    oldPosition = this.transform.position;
                    if (rb.velocity.magnitude > 0 && !notBothTouched)
                    {
                  
                        //if(!waitForSomeTime && !secondLayerIsDone && !pushTouched)
                        offOthers = true;
                       // Debug.Log("Player is running");
                        dontExecute = false;
                        dontExceuteInner = false;


                        navMeshAgent.SetDestination(follow.transform.position);
                        makeGeneral = false;
                    }
                    //enemy is moving
              
                    //offOthers = true;
                    ///dontExecute = true;
                    //dontExceuteInner = true;

                    //lets make the player movet to its follow



                }
                //it will make keep moving
                if (offOthers == false)
                {
                    waitForSomeTime = true;
                    //Debug.Log("Off other is coming");
                    //lets tweak this 
                    if (!dontExecute)
                        random1 = UnityEngine.Random.Range(0, 2);
                    if (random1 == 0)
                    {
                        pushTouched = false;
                        waitForSomeTime = false;
                        dontExecute = true;
                       
                        
                        if (!dontExceuteInner)
                            random2 = UnityEngine.Random.Range(0, 2);
                        if (random2 == 0)
                        {
                            
                            navMeshAgent.SetDestination(randomLocation[0].transform.position);
                            dontExceuteInner = true;
                            //offOthers = true;

                        }

                        if (random2 == 1)
                        {
                           
                            navMeshAgent.SetDestination(randomLocation[1].transform.position);
                            dontExceuteInner = true;
                           // offOthers = true;

                        }



                    }
                    if (random1 == 1)
                    {
                        pushTouched = false;
                        waitForSomeTime = false;
                        dontExecute = true;
                       
                        navMeshAgent.SetDestination(follow.transform.position);
                        //offOthers = true;

                    }

                    makeGeneral = false;
                    //navMeshAgent.speed = 7f;

                }
                else
                {
                    //reset
                    dontExecute = false;
                    dontExceuteInner = false;
                    
                }
            }
            float distanceEnemy = Vector3.Distance(transform.position, player.transform.position);
            if(enemies[1].gameObject!=null)
            distanceEnemyNeeded1 = Vector3.Distance(transform.position, enemies[1].transform.position);
            if(enemies[2].gameObject!=null)
            distanceEnemyNeeded2 = Vector3.Distance(transform.position, enemies[2].transform.position);
            if(enemies[3].gameObject!=null)
            distanceEnemyNeeded3 = Vector3.Distance(transform.position, enemies[3].transform.position);
            if(enemies[4].gameObject!=null)
            distanceEnemyNeeded4 = Vector3.Distance(transform.position, enemies[4].transform.position);
          
            //  navMeshAgent.acceleration = 130f;
            // navMeshAgent.SetDestination(transform.forward * Time.deltaTime * forwardSpeed);
           
            //winningScript.GetComponent<BoxCollider>().isTrigger = false;
            //let begin the countdown 
           
            if (!fleeOff && navMeshAgent.enabled==true)
            {
                if (distanceEnemy <= 4f)
                {
                    // groundSurface = false;

                    //attack the player 
                   // Debug.Log("Distance is less now ");
                   if(this.transform.gameObject!=null)
                    {
                        Vector3 dirToPlayer = this.transform.position - player.transform.position;
                        Vector3 dest = transform.position + dirToPlayer;

                        //navMeshAgent.speed = 7f;
                        navMeshAgent.SetDestination(dest);
                        distanceNotCovered = true;
                       // offOthers = true;
                        rotate = false;
                        canStop = false;
                        makeGeneral = false;
                        telePadGround = false;


                    }

                }
                else
                {
                    
                }
                if (distanceEnemyNeeded1 <= 4f)
                {
                    if(enemies[1].gameObject!=null)
                    {
                        Vector3 dirToPlayer = this.transform.position - enemies[1].transform.position;
                        Vector3 dest = transform.position + dirToPlayer;
                        // navMeshAgent.speed = 7f;
                        if (navMeshAgent.enabled ==true) 
                        navMeshAgent.SetDestination(dest);
                        distanceNotCovered = true;
                       // offOthers = true;
                        rotate = false;
                        canStop = false;
                        telePadGround = false;
                      
                        makeGeneral = false;
                    }
                
                    // groundSurface = false;
                }
                else
                {
                    canStop = true;
                }


                if (distanceEnemyNeeded2 <= 4f)
                {
                    if(enemies[2].gameObject!=null)
                    {
                        Vector3 dirToPlayer = this.transform.position - enemies[2].transform.position;
                        Vector3 dest = transform.position + dirToPlayer;
                        //navMeshAgent.speed = 7f;
                        navMeshAgent.SetDestination(dest);
                        distanceNotCovered = true;
                       // offOthers = true;
                        rotate = false;
                        canStop = false;
                        makeGeneral = false;
                        telePadGround = false;
                    }
                  
                    //groundSurface = false;
                }
                else
                {
                    canStop = true;
                }


                if (distanceEnemyNeeded3 <= 4f)
                {
                    if(enemies[3].gameObject!=null)
                    {
                        Vector3 dirToPlayer = this.transform.position - enemies[3].transform.position;
                        Vector3 dest = transform.position + dirToPlayer;
                        //navMeshAgent.speed = 7f;
                        navMeshAgent.SetDestination(dest);
                        distanceNotCovered = true;
                       // offOthers = true;
                        rotate = false;
                        canStop = false;
                        makeGeneral = false;
                        telePadGround = false;

                    }
                 
                    //groundSurface = false;
                }
                else
                {
                    canStop = true;
                }


                if (distanceEnemyNeeded1 <= 4f)
                {
                    if(enemies[4].gameObject!=null)
                    {
                        Vector3 dirToPlayer = this.transform.position - enemies[4].transform.position;
                        Vector3 dest = transform.position + dirToPlayer;
                       // navMeshAgent.speed = 7f;
                       
                        navMeshAgent.SetDestination(dest);
                        distanceNotCovered = true;
                       // offOthers = true;
                        rotate = false;
                        makeGeneral = false;
                        canStop = false;
                        telePadGround = false;
                    }
                  
                    //groundSurface = false;
                }
                else
                {
                    canStop = true;
                }
           

            }
        



            


            //lets diable the current enemy attack logic by simply disabling the renderer off
            this.gameObject.transform.Find("attackPlayer").GetComponent<attackPlayer>().enabled = false;

            
           


          if(groundSurface)
            {
                ///  navMeshAgent.SetDestination(transform.forward * Time.deltaTime);
                // Debug.Log("ground surface comign------------------------------------");
                //navMeshAgent.Move(transform.forward * Time.deltaTime * forwardSpeed);
                //navMeshAgent.SetDestination(transform.forward * Time.deltaTime * forwardSpeed);

                fleeOff = false;
                finalSurface = false;
               // whenItisinGround = true;
                telePadGround = true;
                makeTheLogicOff = false;
               
              //  rotate = true;
                }
            
                //now check for the surfaces 
           
                //if they are really close then make the player to jump downwards thats it// 
                if(finalSurface && navMeshAgent.enabled == true)
                {
                
                fleeOff = true;
                navMeshAgent.SetDestination(targetDown[0].transform.position);
                navMeshAgent.speed = 6.5f;
               // groundSurface = false;


                //lets jump once step down 

                   // navMeshAgent.SetDestination(transform.forward * forwardSpeed * Time.deltaTime);
                    //this.transform.position += transform.forward * forwardSpeed * Time.deltaTime;




                
            
               
               
            }
    
           
           
        }
        else
        {
             this.gameObject.transform.Find("attackPlayer").GetComponent<attackPlayer>().enabled = true;
            //arrow.transform.position = backtoPos;
            //le
        }
        //Attacking the Player 
        if (winningScript.enemyCurrentName != this.transform.name)
        {
            if (player != null && this.transform.gameObject != null)
            {


                float distance = Vector3.Distance(player.transform.position, this.transform.position);

                if (distance <= 3f)
                {
                    //attacking the player 
                    if (navMeshAgent.enabled == true)
                        navMeshAgent.SetDestination(player.transform.position);
                    navMeshAgent.speed = 7.1f;
                    if (this.gameObject.name == "enemy4")
                    {
                        GameObject find = GameObject.FindGameObjectWithTag("enemy4Char");
                        Animator anim = find.GetComponent<Animator>();
                        anim4.speed = 2f;

                    }
                    if (this.gameObject.name == "enemy3")
                    {
                        ;
                        anim3.speed = 2f;


                    }
                    if (this.gameObject.name == "enemy2")
                    {

                        anim2.speed = 2f;


                    }
                    if (this.gameObject.name == "enemy1")
                    {

                        anim1.speed = 2f;


                    }
                    if (this.gameObject.name == "enemy")
                    {

                        anim.speed = 2f;


                    }

                    // playerScript.speed = 6.0f;

                }
                else
                {
                   // setLocation(destination.transform.position);
                }



            }
        }

    }

    public  void setLocation(Vector3 targetPos)
    {

        try
        {
            if(navMeshAgent.enabled==true && makeGeneral)
            navMeshAgent.SetDestination(targetPos);
            //navMeshAgent.destination = transform.forward * forwardSpeed * Time.deltaTime;
            Debug.Log("set location part is coming.. ");
        }
        catch(Exception e)
        {

        }
       
    }



    IEnumerator WaitFor()
    {
        if(!winningScript.carriedByEnemy && !winningScript.carriedByPlayer || winningScript.carriedByEnemy && this.transform.name!=winningScript.enemyCurrentName || winningScript.carriedByPlayer)
        {
            yield return new WaitForSeconds(0.55f);

            navMeshAgent.enabled = true;
            setLocation(destination.transform.position);
            nowIsOnGreenSurface = false;
           // makeTheLogicOff = true;

        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            navMeshAgent.enabled = true;
          setLocation(destination.transform.position);
           // makeTheLogicOff = true;
            nowIsOnGreenSurface = false;
        }
     

      
       
           
        
       

    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "pinkSurface" && !nowIsOnGreenSurface)
        {

            //all about final surface logic
            finalSurface = true;
            groundSurface = false;
            whenItisinGround = false;
            secondLayer = false;
            secondLayerIsDone = false;
            decider1 = false;
            makeTheLogicOff = true;

            Debug.Log("Final surface arrived--------------------");
            randomTarget = UnityEngine.Random.RandomRange(0, 4);
         



        }

        if (winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
        {
          
            // Debug.Log("Coming in collider");
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "enemy")
            {
                makeTheLogicOff = false;
                navMeshAgent.enabled = false;
                backToNormal = false;
                makeGeneral = true;
                StartCoroutine(WaitFor());
                turnOffcurrentEnemyCollision = true;
                StartCoroutine(turnOnEnemyCollision());

                // finalFlag.transform.position = winningScript.originalPos;
                foreach (ContactPoint cp in collision.contacts)
                {
                    pushX = cp.normal.x;
                    pushZ = cp.normal.z;
                }
                //make the navmesh to fallback 
                //navMeshAgent.Stop(true);


               
               // rb1.velocity=(new Vector3(pushX, 0.8f, pushZ) * pushSpeed);
                counter = countDuration;
                //lets get back the flag to its position 
                //winningScript.carriedByEnemy = false;
                if (collision.gameObject.tag == "Player")
                {
                    winningScript.carriedByPlayer = true;
                    rb.AddForce(new Vector3(pushX, 0f, pushZ) * flagSpeed);


                }
                  
                if(collision.gameObject.tag =="enemy")
                {
                    
                    winningScript.currentEnemyObject = collision.gameObject;
                    winningScript.carriedByOtherEnemy = true;
                    rb.AddForce(new Vector3(pushX, 0f, pushZ) * flagSpeed);


                }

                winningScript.carriedByEnemy = false;
           

            }
           
         

        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "enemy" && !winningScript.carriedByEnemy && !turnOffcurrentEnemyCollision)
        {

            //Debug.Log("enemy hitted by player");
            foreach (ContactPoint cp in collision.contacts)
            {
                pushX = cp.normal.x;
                pushZ = cp.normal.z;
            }
            //make the navmesh to fallback 
            //navMeshAgent.Stop(true);

            navMeshAgent.enabled = false;

            rb.velocity=(new Vector3(pushX, 0f, pushZ) * pushSpeed);
            //counter = countDuration;
            StartCoroutine(WaitFor());
        }


        
        if (collision.gameObject.tag == "nationalFlag")
        {
            //counter = countDuration;
            
            
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
       
       
                if(collision.gameObject.tag=="pinkSurface")
                {
            if (secondLayerIsDone)
            {
                if (winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
                {
                    //sstelePadGround = false;
                    BackToNormal();
                    decider1 = false;
                    groundIsDone = false;
                    secondLayerIsDone = false;

                }

            }
              
                    
                    
                }
       
        if(groundIsDone)
        if(winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
        if(collision.gameObject.tag == "greenSurface")
        {
            //telePadGround = false;
            BackToNormal();
                     makeTheLogicOff = true;
            secondLayerIsDone = true;
                    offOthers = false;
                    nowIsOnGreenSurface = true;
                    finalSurface = false;
                    telePadGreen = true;
                    telePadGround = false;
                    whenItisinGround = false;
                
                    
                    groundIsDone = false;

                   // Debug.Log("Green surface touched");
        }

        if (collision.gameObject.tag == "secondLayer")
        {
            secondLayer = true;

        }
        if(collision.gameObject.tag =="greenSurface" || collision.gameObject.tag=="pinkSurface" )
        {
            if (winningScript.carriedByEnemy && this.transform.name==winningScript.enemyCurrentName)
            {
                whenItisinGround = false;
                Debug.Log("It is coming false now");
                
                
            }
            else
            {
                if(winningScript.carriedByEnemy)
                {
                    whenItisinGround = false;
                }
               
            }
           
        }
        if(collision.gameObject.tag=="greenSurface"  && whenItisinGround)
        {
            //lets activate the moving object...
            if(winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
            {
                offOthers = false;
                notBothTouched = true;
                finalSurface = false;
                StartCoroutine(turnOff());
            }
        

        }
     
     
      
      
   
        if(collision.gameObject.tag == "GroundSurface")
        {
            if(winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
            {
                whenItisinGround = true;
            }
            if(winningScript.carriedByPlayer)
            {
                whenItisinGround = true;
            }
            if(this.transform.name!=winningScript.enemyCurrentName)
            {
                whenItisinGround = true;
            }
       
         
        }
        if (winningScript.carriedByEnemy && this.transform.name == winningScript.enemyCurrentName)
        {
            if (collision.gameObject.tag == "GroundSurface")
            {

                makeTheLogicOff = true;
                secondLayerIsDone = false;
                
                nowIsOnGreenSurface = false;


                telePadGreen = false;
                telePadGround = true;
                groundSurface = true;
                whenItisinGround = true;
                backToNormal = false;
                telePadGround = true;
                secondLayer = false;
                finalSurface = false;
                groundIsDone = true;
                makeTheLogicOff = false;
                makeGeneral = false;
                if(isGrounded==true)
                groundSurface = true;
                Debug.Log("ground surface arrived");

            }
         
        }
        if(collision.gameObject.tag == "pushFlag")
        {
            if(winningScript.carriedByEnemy && this.transform.name==winningScript.enemyCurrentName)
            {
                pushTouched = true;
                offOthers = false;
                notBothTouched = true;
                StartCoroutine(turnOff());
                Debug.Log("OffOther is now false");
            }
        
           

        }

        if(collision.gameObject.tag == "trickPush")
        {
            Destroy(this.gameObject);
        }

       

    }
  

    //lets make the method in which the enemies will decide when to jump up while having the flag ...
    //players only can jump when it comes in the shaded part
    public void decider()
    {
        groundIsDone = true;

        makeTheLogicOff = true;
                //enemy will jump ..
                //lets set the destination to  up so it will jump one step above
                //lets turn off the ground surface
               // navMeshAgent.enabled = false;
                isGrounded = false;
                groundSurface = false;
        // fleeOff = true;
        // offOthers = true;
        ddecider = true;
        offOthers = false;
        
                makeGeneral = false;
                notBothTouched = true;
        Debug.Log("Decider is running .....");

        
                deciderRandom= UnityEngine.Random.Range(0, 1);
        if (navMeshAgent.enabled == true)
            this.navMeshAgent.SetDestination(secondLayerTarget[deciderRandom].transform.position);

        //when  reached to second layer 
        if (secondLayer==true)
        {
            //and if the player is not moving at all 
         
           // navMeshAgent.SetDestination(follow.transform.position);
        
        }
        
                

            

        
       
        
    }
    public void BackToNormal()
    {
        ddecider = false;
       
        isGrounded = true;
        makeGeneral = false;
       groundSurface = true;
        backToNormal = true;
       if(nowIsOnGreenSurface)
       navMeshAgent.SetDestination(follow.transform.position);
        //makeGeneral = true;
        //fleeOff = false;
        // offOthers = false;
        // makeGeneral = true;
        Debug.Log("Back To normal is running");


    }
    public void Decider1()
    {
        //groundIsDone = true;
        ddecider = false;

        //enemy will jump ..
        //lets set the destination to  up so it will jump one step above
        //lets turn off the ground surface
        // navMeshAgent.enabled = false;
        isGrounded = false;
        groundSurface = false;
        backToNormal = false;
        nowIsOnGreenSurface = false;
        
        // fleeOff = true;
        //  offOthers = true;
        makeGeneral = false;
        Debug.Log("Decider1 is running .....");
        decider1 = true;

        //int random = UnityEngine.Random.Range(0, 1);
        //navMeshAgent.Warp(thirdLayerTarget.transform.position);
        if(navMeshAgent.enabled==true)
        navMeshAgent.SetDestination(thirdLayerTarget.transform.position);
        //navMeshAgent.SetDestination();
    }
    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Its coming here");
        notBothTouched = false;
    }
    IEnumerator turnOnEnemyCollision()
    {
        yield return new WaitForSeconds(0.4f);
        turnOffcurrentEnemyCollision = false;
        Debug.Log("enemy collision on and off is owrking ");
    }

}
