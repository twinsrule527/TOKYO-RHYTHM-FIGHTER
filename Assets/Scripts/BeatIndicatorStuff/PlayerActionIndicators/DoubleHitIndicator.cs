using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Action Indicator for the Double Hit - shows up overlayed on a 
public class DoubleHitIndicator : ActionIndicator
{
    private Vector3 basePos;
    [SerializeField] private float secondHitTime;
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
        float startTime = BeatController.GetBeat();
        Transform nextBeatIndicator = Global.BeatIndicatorBrain.GetPlayerIndicator(startTime + secondHitTime).transform;
        transform.position = nextBeatIndicator.position;
        Vector3[] linePos = new Vector3[2];
        linePos[0] = transform.position;
        linePos[1] = basePos;
        float t = startTime;
        while(t < startTime + secondHitTime) {
            transform.position = nextBeatIndicator.position;
            t = BeatController.GetBeat();
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
