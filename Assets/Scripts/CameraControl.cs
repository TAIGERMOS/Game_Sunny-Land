using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{   
    //获取Player的transform
    public Transform Playertransf;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Playertransf.position.x, 0, -10f);
    }
}
