using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByHealthPlayer : MonoBehaviour
{

    //TODO: differences or hard values?
    //right now only transform, cause writing this for the letterbox and its scale is for bounce
    //[SerializeField] float scaleFullX, scaleFullY, scaleDeadX, scaleDeadY;
    [SerializeField] float transFullX, transFullY, transFullZ, transDeadX, transDeadY, transDeadZ;

    // Start is called before the first frame update
    void Start()
    {

        //hm. come to think of it these could have been vector3s maybe. oh well 

        //if empty, initialize full to starting scale 
        /*
        if(scaleFullX == 0)
            scaleFullX = transform.localScale.x;
        if(scaleFullY == 0)
            scaleFullY = transform.localScale.y;
        if(scaleFullZ == 0)
            scaleFullZ = transform.localScale.z; */
        if(transFullX == 0)
            transFullX = transform.position.x;
        if(transFullY == 0) 
            transFullY = transform.position.y;
        if(transFullZ == 0)
            transFullZ = transform.position.z;

       /* if(scaleDeadX == 0)
            scaleDeadX = transform.localScale.x;
        if(scaleDeadY == 0)
            scaleDeadY = transform.localScale.y;
        if(scaleDeadZ == 0)
            scaleDeadZ = transform.localScale.z;*/
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
        //TODO: make this only happen when the health changes. 
        float healthPercent = Global.Player.playerHealth / Global.Player.playerStartHealth;
        if(healthPercent == float.NaN) {
            //if we divided by zero 
            healthPercent = 0;
        }
        float vx = Mathf.Lerp(transDeadX, transFullX, healthPercent);
        float vy = Mathf.Lerp(transDeadY, transFullY, healthPercent);
        float vz = Mathf.Lerp(transDeadZ, transFullZ, healthPercent);
        transform.position = new Vector3(vx, vy, vz);
        

        //TODO coroutine to lerp into a new value, which can get interrupted by subsequent dmg
        //or a target value? 

        //TODO: should this hit a threshold like it hits the edges of the indicator near zero?
        //like youre in the red zone 

    }
}
