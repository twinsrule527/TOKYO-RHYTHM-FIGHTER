using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByHealthPlayer : MonoBehaviour
{

    [SerializeField] float scaleFullX, scaleFullY, scaleFullZ, scaleDeadX, scaleDeadY, scaleDeadZ;
    [SerializeField] float transFullX, transFullY, transFullZ, transDeadX, transDeadY, transDeadZ;

    // Start is called before the first frame update
    void Start()
    {

        //hm. come to think of it these could have been vector3s maybe. oh well 

        //if empty, initialize full to starting scale 
        if(scaleFullX == 0)
            scaleFullX = transform.localScale.x;
        if(scaleFullY == 0)
            scaleFullY = transform.localScale.y;
        if(scaleFullZ == 0)
            scaleFullZ = transform.localScale.z;
        if(transFullX == 0)
            transFullX = transform.position.x;
        if(transFullY == 0) 
            transFullY = transform.position.y;
        if(transFullZ == 0)
            transFullZ = transform.position.z;

        if(scaleDeadX == 0)
            scaleDeadX = transform.localScale.x;
        if(scaleDeadY == 0)
            scaleDeadY = transform.localScale.y;
        if(scaleDeadZ == 0)
            scaleDeadZ = transform.localScale.z;
        if(transDeadX == 0)
            transDeadX = transform.position.x;
        if(transDeadY == 0) 
            transDeadY = transform.position.y;
        if(transDeadZ == 0)
            transDeadZ = transform.position.z;
    }


    // Update is called once per frame
    void Update()
    {
        //TODO could make this number only change when player health is changed by calling a func in that place. 
        
        //TODO it's lerp min and max hp 

        //TODO MAKE THIS move the bars based on transform actually, since scale is bounce 
        //
    }
}
