using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    public bool rotateAroundPlayer = true;
    public float rotationSpeed = 5.0f;
    public Transform camera;
    Vector3 camOffset;
    public bool lookAt = false;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        camOffset = camera.transform.position - this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        /*if(rotateAroundPlayer)
        {
            Quaternion camturnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            camOffset = camturnAngle * camOffset;
           
        }
        Vector3 newPos = transform.position + camOffset;
        camera.transform.position = Vector3.Slerp(camera.transform.position, newPos, smoothFactor);

        if(lookAt)
        {
            camera.transform.LookAt(transform.position);
        }*/
        float vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        //  Vector3 playerPos = transform.localPosition;
        //playerPos.z= camera.transform.localPosition.z;
        //this.transform.position += new Vector3(Horizontal, 0f, vertical) * Time.deltaTime * speed;
        this.transform.position += transform.forward * Time.deltaTime * speed;
        //making the player to move 
        // this.transform.position += transform.forward * Time.deltaTime * speed;
    }
}
