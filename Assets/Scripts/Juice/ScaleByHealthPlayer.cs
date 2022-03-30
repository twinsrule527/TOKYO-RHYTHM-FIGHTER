using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByHealthPlayer : MonoBehaviour
{

    //TODO: differences or hard values?
    //right now only transform, cause writing this for the letterbox and its scale is for bounce
    [SerializeField] float transFullX, transFullY, transFullZ, transEmptyX, transEmptyY, transEmptyZ, transDeadX, transDeadY, transDeadZ;

    Vector3 deadPos;

    // Start is called before the first frame update
    void Start()
    {

        //hm. come to think of it these could have been vector3s maybe. oh well 

        //if empty, initialize full to starting scale 

        if(transFullX == 0) {
            transFullX = transform.localPosition.x;
        }
        if(transFullY == 0) {
            transFullY = transform.localPosition.y;
        }
        if(transFullZ == 0) {
            transFullZ = transform.localPosition.z;
        }

        if(transEmptyX == 0) {
            transEmptyX = transform.localPosition.x;
        }
        if(transEmptyY == 0) {
            transEmptyY = transform.localPosition.y;
        }
        if(transEmptyZ == 0) {
            transEmptyZ = transform.localPosition.z;
        }
            
        if(transDeadX == 0) {
            transDeadX = transform.localPosition.x;
        }
        if(transDeadY == 0) {
            transDeadY = transform.localPosition.y;
        }
        if(transDeadZ == 0) {
            transDeadZ = transform.localPosition.z;
        }

        deadPos = new Vector3(transDeadX, transDeadY, transDeadZ);
            
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make this only happen when the health changes. 
        float healthPercent = Global.Player.playerHealth / Global.Player.playerStartHealth;

        if(healthPercent < 0 || healthPercent == float.NaN) {
            //if we divided by zero or a negative number 
            transform.localPosition = deadPos;
        } else {
            float vx = Mathf.Lerp(transEmptyX, transFullX, healthPercent);
            float vy = Mathf.Lerp(transEmptyY, transFullY, healthPercent);
            float vz = Mathf.Lerp(transEmptyZ, transFullZ, healthPercent);
            transform.localPosition = new Vector3(vx, vy, vz);
        }
        

        //TODO coroutine to lerp into a new value, which can get interrupted by subsequent dmg
        //or a target value? 

        //TODO: should this hit a threshold like it hits the edges of the indicator near zero?
        //like youre in the red zone 

    }
}
