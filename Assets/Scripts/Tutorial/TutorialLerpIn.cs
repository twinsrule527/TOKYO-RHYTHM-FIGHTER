using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attached to objects which are lerp
public class TutorialLerpIn : MonoBehaviour
{
    [SerializeField] private Transform startPosTransform;
    [SerializeField] private float timeToLerp;
    private Vector3 startPos;
    private Vector3 finalPos;
    public int stageToLerpInOn;

    void Start() {
        startPos = startPosTransform.position;
        finalPos = transform.position;
        transform.position = startPos;
    }

    public IEnumerator LerpToPos() {
        float startTime = BeatController.GetBeat();
        float t = startTime;
        while(t < startTime + timeToLerp) {
            transform.position = Vector3.Lerp(startPos, finalPos, (t - startTime) / timeToLerp);
            yield return null;
            t = BeatController.GetBeat();
        }
        transform.position = finalPos;
    }


}
