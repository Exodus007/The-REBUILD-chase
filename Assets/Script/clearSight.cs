using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearSight : MonoBehaviour
{
    public float DistanceToPlayer = 2.0f;
    public Material TransparentMaterial = null;
    public float FadeInTimeout = 0.6f;
    public float FadeOutTimeout = 0.2f;
    public float TargetTransparency = 0.3f;
    public GameObject[] objectsToFade;
    public GameObject Player;
    Material[] m1,m2,m3;
    bool greenSurface = false;
    public actualPlayerMove playerScript;
    public float Switch = 1;
    Renderer R;

    private void Start()
    {
        //getting all the material from the object 
        m1 = objectsToFade[0].GetComponent<MeshRenderer>().materials;
        m2= objectsToFade[1].GetComponent<MeshRenderer>().materials;
        m3 = objectsToFade[2].GetComponent<MeshRenderer>().materials;


    }
    private void Update()
    {
        RaycastHit[] hits; // you can also use CapsuleCastAll() 
                           // TODO: setup your layermask it improve performance and filter your hits. 
        hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer);
        foreach (RaycastHit hit in hits)
        {
            R = hit.collider.GetComponent<Renderer>();
        }
            if (R == null)
            {

               
            }
            else
            {
              
            }
            if(Physics.Raycast(transform.position,transform.forward,1f))
        {
            Switch *= -1;
            //here i will make the camera active and disactive from the side 
            switch (Switch)
            {
                case 1:
                    {
                        //active the camOne
                        Debug.Log("Activating the camOne");
                        break;
                    }
                case -1:
                    {
                        //active the camTwo
                        Debug.Log("Activating the camTwo");
                        break;
                    }
            }
        }
        
    }
}
