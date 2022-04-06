using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldActionIndicator : ActionIndicator
{
    [SerializeField] private float holdLength;
    [SerializeField] private GameObject holdObj;
    public override void PerformAction()
    {
        StartCoroutine(IndicatorCoroutine());
    }

    protected override IEnumerator IndicatorCoroutine()
    {
        holdObj.SetActive(true);
        yield return BeatController.WaitForBeat(BeatController.GetBeat() + holdLength);
        holdObj.SetActive(false);
    }
}
