using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasePlayer : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject cameraOne;
    public GameObject cameraTwo;
    public float offsetZ = 3.0f;
    public float offsetX = 2.0f;
    public float offsetY = 5.0f;

    public float rOffsetX = 1.0f;
    public float rOffsetY = 1.0f;
    public float rOffsetZ = 1.0f;
    public bool camOneActive = false;
    public bool camTwoActive = true;
    [Header("References")]
    public AllAboutAtan2 front;
    public AllAboutAtanBack back;
    //rotation offset
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if(player !=null && camOneActive)
        {
            //lets active itself
            cameraOne.SetActive(true);
            cameraOne.GetComponent<AudioListener>().enabled = true;
            camTwoActive = false;
            //lets disable the camTwo 
            cameraTwo.SetActive(false);
            cameraTwo.GetComponent<AudioListener>().enabled = false;
            //switching when player reached its pos
            if (player.transform.position.z >= 5f)
            {
                // player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                camTwoActive = true;
                camOneActive = false;

                //activate time
                Invoke("startCamTwo", 0.7f);


            }










            cameraOne.transform.position = new Vector3(Mathf.Lerp(cameraOne.transform.position.x, player.transform.position.x + offsetX, 0.5f),
          Mathf.Lerp(cameraOne.transform.position.y, player.transform.position.y + offsetY, 0.5f), Mathf.Lerp(cameraOne.transform.position.z, player.transform.position.z - 17, 0.5f));
           // came/ra.transform.eulerAngles = new Vector3(Mathf.Lerp(camera.transform.eulerAngles.x, player.transform.eulerAngles.z, 0.5f),
               // Mathf.Lerp(camera.transform.eulerAngles.y, camera.transform.eulerAngles.y, 0.5f),
              //  Mathf.Lerp(camera.transform.eulerAngles.z, player.transform.eulerAngles.z, 0.5f));
        }
        else if(player!=null && camTwoActive)
        {
            //lets active itself
            cameraTwo.SetActive(true);
            cameraTwo.GetComponent<AudioListener>().enabled = true;

            //lest disable the camOne
            camOneActive = false;
            cameraOne.SetActive(false);
            cameraOne.GetComponent<AudioListener>().enabled = false;
            //when switching occurs we will waitforsomeseconds 
            if (player.transform.position.z <= -1f)
            {
                // player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                camTwoActive = false;
                camOneActive = true;

                //active
                Invoke("startCamOne", 0.7f);

            }





            cameraTwo.transform.position = new Vector3(Mathf.Lerp(cameraTwo.transform.position.x, player.transform.position.x + offsetX, 0.5f),
          Mathf.Lerp(cameraTwo.transform.position.y, player.transform.position.y + offsetY, 0.5f), Mathf.Lerp(cameraTwo.transform.position.z, player.transform.position.z + 17, 0.5f));
        }
       

    }
void startCamOne()
    {

        //lets active the correct script
        if (camOneActive)
        {
            //lets active its right control and disable the wrong control
            front.enabled = true;
            back.enabled = false;
            
        }
    }
    void startCamTwo()
    {
        //switching when player reached its pos
        if (camTwoActive)
        {
            //lets active its right control and disable the wrong control
            front.enabled = false;
            back.enabled = true;
          
        }
    }
}
