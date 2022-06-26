using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Action Indicator for the Double Hit - shows up overlayed on a 
public class DoubleHitIndicator : ActionIndicator
{
    private Vector3 basePos;
    [SerializeField] private float secondHitTime;
    [SerializeField] private float indicatorToMatchTime;
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
        Vector3 nextBeatIndicator = Global.BeatIndicatorBrain.GetPlayerIndicator(startTime + indicatorToMatchTime).transform.position;
        float t = startTime + (1 - secondHitTime);
        transform.position = Vector3.Lerp(nextBeatIndicator, basePos, (t - startTime)/(indicatorToMatchTime));
        while(t < startTime + indicatorToMatchTime) {
            transform.position = Vector3.Lerp(nextBeatIndicator, basePos, (t - startTime)/(indicatorToMatchTime));
            t = BeatController.GetBeat() + (1 - secondHitTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
