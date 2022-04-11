using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldActionIndicator : ActionIndicator
{
    //For the time being, I am using a lineRenderer to show how long you have to hold for
        //This object teleports to the position of the next beat indicator, and draws a line to center
    [SerializeField] private float holdLength;
    [SerializeField] private GameObject holdObj;
    [SerializeField] private LineRenderer holdLine;
    private Vector3 basePos;

    protected override void Start() {
        base.Start();
        basePos = transform.position;
    }
    public override void PerformAction()
    {
        StartCoroutine(IndicatorCoroutine());
    }

    protected override IEnumerator IndicatorCoroutine()
    {
        //Gets the closest beatIndicator to the hold length of this attack
        float startTime = BeatController.GetBeat();
        Transform nextBeatIndicator = Global.BeatIndicatorBrain.GetPlayerIndicator(startTime + holdLength).transform;
        transform.position = nextBeatIndicator.position;
        Vector3[] linePos = new Vector3[2];
        linePos[0] = transform.position;
        linePos[1] = basePos;
        holdLine.SetPositions(linePos);
        holdObj.SetActive(true);
        float t = startTime;
        while(t < startTime + holdLength) {
            transform.position = nextBeatIndicator.position;
            holdLine.SetPosition(0, transform.position);
            t = BeatController.GetBeat();
            yield return null;
        }
        holdObj.SetActive(false);
        gameObject.SetActive(false);
    }
}
