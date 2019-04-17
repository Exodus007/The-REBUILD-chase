using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camPivot : MonoBehaviour
{
    public GameObject Pivot;
    float heading;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180f;
        Pivot.transform.rotation = Quaternion.Euler(0f, heading, 0f);
        //Making camera relative to its player movement
        Vector3 camf, camR;
        camf = cam.transform.forward;
        camR = cam.transform.right;

        camf.y = 0f;
        camR.y = 0f;
        camf = camf.normalized;
        camR = camR.normalized;
    }
}
