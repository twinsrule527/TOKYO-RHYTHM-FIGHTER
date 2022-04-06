using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldActionIndicator : ActionIndicator
{
    [SerializeField] private float holdLength;
    public override void PerformAction()
    {
        StartCoroutine(IndicatorCoroutine());
    }

    protected override IEnumerator IndicatorCoroutine()
    {
        
        yield return BeatController.WaitForBeat(BeatController.GetBeat() + holdLength);
    }
}
