using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCamera : MonoBehaviour
{
    Transform player;
    Transform mainCam;
    [Range(0.01f, 5.0f)]
    public float transitionSpeed = 0.5f;
    [Range(0.01f, 5.0f)]
    public float transitionDownSpeed = 3.0f;
    bool turn = true;
   Quaternion prevRotationValue;
    public float rotationSpeed = 0.1f;
    public bool goingDown = false;
    [Range(0.01f, 5.0f)]
    public float transitionDownSpeed1 = 3.0f;
    public float rotationSpeed1 = 0.5f;
    [Range(0.01f, 5.0f)]
    public float LookAtSpeed = 3.0f;

    public float transitionDownGreenSpeed = 3.0f;
    //lets detect which one is running 
    public bool ground, green, red, final;
    public bool redIsReached=false;
    public bool finalOneTouched = false;

    //bool
    public bool rotateMan;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        mainCam = Camera.main.transform;
        prevRotationValue = mainCam.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameObject!=null && this.transform.gameObject!=null)
        {
            Vector3 LookPos = player.position - transform.position;
            LookPos.y = 0;
            // Quaternion rotation = Quaternion.LookRotation(-LookPos);
            //Debug.Log(-LookPos);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -LookPos, LookAtSpeed * Time.deltaTime, 0.0f);

            if (turn == true)
                transform.rotation = Quaternion.LookRotation(newDir);

        }

        //red or final part logic 
        if (finalOneTouched)
        {
            Debug.Log("Red part is coming");
            redIsReached = true;
            //lets capture its current rotation for now 
            Quaternion currentRedRotation = mainCam.transform.localRotation;
            Vector3 camPos = mainCam.transform.localPosition;
            //camPos.x = 1.0f;
            //camPos.y = 30f;
            //  camPos.z = -24.2f;

            // mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
            //back to normal rotation 
            // mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, prevRotationValue, rotationSpeed * Time.deltaTime);
            camPos.x = 0.14f;
            camPos.y = 34.07f;
            camPos.z = -27.44f;
            mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, currentRedRotation, rotationSpeed * Time.deltaTime);
            mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
        }

            //lets do the jump camera logic 
            if (player.transform.position.z >= -7.5f && player.transform.position.y >= 23.2f && !finalOneTouched)
        {
            Vector3 camPos = mainCam.transform.localPosition;
            camPos.z = -50f;
            mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionDownSpeed);
        }

        //lets zoom in and zoom out
        if(player.transform.position.y>=16.5f && player.transform.position.y<=18.5f)
        {
            turn = true;
            green = true;
            //making other as false 
            red = false;
            final = false;
            ground = false;
            if(green)
            {
                Vector3 camPos = mainCam.transform.localPosition;
                camPos.x = -2f;
                camPos.y = 32.8f;
                camPos.z = -33.2f;
                if(redIsReached)
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionDownGreenSpeed);
                else
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
                
                //back to normal rotation 
                mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, prevRotationValue, rotationSpeed * Time.deltaTime);
            }
            
      
        }
        if(player.transform.position.y<=16f)
        {
            turn = true;
            ground = true;
            //making or=ther as false 
            red = false;
            final = false;
            green = false;
            if(ground)
            {
                redIsReached = false;
                Vector3 camPos = mainCam.transform.localPosition;
                camPos.y = 24.5f;
                camPos.x = 0.6f;
                camPos.z = -47.5f;
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionDownSpeed);
                //back to normal rotation 
                mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, prevRotationValue, rotationSpeed * Time.deltaTime);
            }
          

        }
      /*  if (player.transform.position.y >= 21.5f && player.transform.position.y<=22.5f)
        {
            turn = true;
            green = false;
            //making other as false
            red = true;
            ground = false;
            final = false;
            if(red)
            {
                Vector3 camPos = mainCam.transform.localPosition;
                camPos.x = 0.7f;
                camPos.y = 31f;
                camPos.z = -24f;
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
                //back to normal rotation 
                mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, prevRotationValue, rotationSpeed * Time.deltaTime);


            }*/


        
        if (player.transform.position.y >= 21.5f && player.transform.position.y <= 24f)
        {
            red = true;
            //making other as false
            ground = false;
            green = false;
            final = false;
            turn = true;
            if(red && finalOneTouched)
            {
                Debug.Log("Red part is coming");
                redIsReached = true;
                //lets capture its current rotation for now 
                Quaternion currentRedRotation = mainCam.transform.localRotation;
                Vector3 camPos = mainCam.transform.localPosition;
                //camPos.x = 1.0f;
                //camPos.y = 30f;
              //  camPos.z = -24.2f;

               // mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
                //back to normal rotation 
               // mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, prevRotationValue, rotationSpeed * Time.deltaTime);
                camPos.x = 0.14f;
                camPos.y = 34.07f;
                camPos.z = -20.44f;
                mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, currentRedRotation, rotationSpeed * Time.deltaTime);
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
                if (rotateMan==true)
                {
                    camPos.x = 0.14f;
                    camPos.y = 34.07f;
                    camPos.z = -20.44f;
                   //mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, currentRedRotation, rotationSpeed1 * Time.deltaTime);
                   //mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
                    //rotateMan = false;

                }

            }
           

  
        


        }
        if (player.transform.position.y >= 26.65f && player.transform.position.y!=25f)
        {
        
            turn = true;
            final = true;
            //making other as falsr
            green = false;
            red = false;
            ground = false;
            if(final)
            {
                //but lets make the camera in top view ...
                Vector3 camPos = mainCam.transform.position;
                camPos.x = 0.08f;
                camPos.y = 50.84f;
                camPos.z = -18.07f;
                mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, camPos, Time.deltaTime * transitionSpeed);
                //lets change the rotatio
                Quaternion targetRoattion = Quaternion.Euler(new Vector3(41.711f, -360.761f, 0.002f));
                mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, targetRoattion, rotationSpeed * Time.deltaTime);
                //lest activate the rotation manipulation 
                rotateMan = true;

            }
           

            if (!goingDown)
            {

              
               // goingDown = true;
            }

        }

    }
}
