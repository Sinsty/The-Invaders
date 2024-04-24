using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayakRotation : MonoBehaviour
{
     public Transform rotateObj;
     public Transform aroundObj;
    public float rotSpeed=0.1f;

       void Update()
       {
        Rotator();
       }
      
       void Rotator()
       {
       rotateObj.RotateAround(aroundObj.position, new Vector3(0,1,0),rotSpeed);
       }     
}
