using UnityEngine;
using UnityEngine.UI;
public class winningConcept : MonoBehaviour
{
    [Header("References")]
    public Text countDownText;
    public bool countBeganPlayer=false;
    public bool countBeganEnemy = false;
    public float countDuration = 10f;
    public float counter;
    string Name = "";
    public bool win = false;
    public bool carriedByPlayer = false;
    public bool carriedByEnemy = false;
    GameObject player;
    public NavMeshMove navMeshScript;
    GameObject currentEnemy;
    public string enemyCurrentName;
   public Vector3 originalPos;
    BoxCollider bc;
    float pushX, pushZ;
    public float pushBackSpeed=10;
    Rigidbody rb;
    public GameObject currentEnemyObject;
    public bool carriedByOtherEnemy = false;
    SphereCollider enemyCoolider;
    private bool takenForTheFirstTime = false;
    void Start()
    {
        originalPos = this.transform.position;
        counter = countDuration;
        player = GameObject.FindGameObjectWithTag("Player");
        bc = this.GetComponent<BoxCollider>();
        rb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
      
        if(carriedByPlayer)
        {
            //lets make the flag move with the player with getting the player position
            Vector3 playerPos = player.transform.position;
            playerPos.y += 2f;
            this.transform.position = playerPos;
            bc.isTrigger = true;
            carriedByOtherEnemy = false;
            carriedByEnemy = false;
     
            //if player got hit while carrying the flag then the flag will get back to its original place .. 
         

        }
        else
        {
            bc.isTrigger = false;
        }
        if(carriedByEnemy)//if name got changed then no change in position 
        {
            carriedByOtherEnemy = false;
            carriedByPlayer = false;
            if(currentEnemy!=null)
            {
                Vector3 currentEnemyPos = currentEnemy.transform.position;
                currentEnemyPos.y += 2.2f;
           
                this.transform.position = currentEnemyPos;
                

            }
       
            
           
        }
        else
        {
           
        }
        if(carriedByOtherEnemy)
        {
            carriedByEnemy = false;
            carriedByPlayer = false;
            Vector3 cuurentEnemyPos = currentEnemyObject.transform.position;
            cuurentEnemyPos.y += 2.2f;
            this.transform.position = cuurentEnemyPos;
        }
        

       
    }


   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !takenForTheFirstTime)
        {

            //player is on final surface then countdown start 
            //1.countDownStart 
           
            countBeganPlayer = true;
            countBeganEnemy = false;
            name = collision.gameObject.name;
            //it will get carried by the player 
            carriedByPlayer = true;



        }
        if (collision.gameObject.tag == "enemy" && !takenForTheFirstTime)
        {
            
            countBeganEnemy = true;
            carriedByOtherEnemy = false;
            countBeganPlayer = false;
            //count logic will get implemented after chasing logic ,Pending for now 
            carriedByPlayer = false;
            carriedByEnemy = true;
            currentEnemy = collision.gameObject;
            enemyCurrentName = collision.gameObject.name;
        }
        if(collision.gameObject.tag =="DeadZone")
        {
            //lets spawn this at some other location 
            //x=13 and 11 
            //z=-18 and 8 
            this.transform.position = originalPos;
          
            
        }
    
    }






}
