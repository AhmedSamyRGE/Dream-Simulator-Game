using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPassiveMovement : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate( Vector3.up , 50 * Time.deltaTime , Space.World );
    }
}
