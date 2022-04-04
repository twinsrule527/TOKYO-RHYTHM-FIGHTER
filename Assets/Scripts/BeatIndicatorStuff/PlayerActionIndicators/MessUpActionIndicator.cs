using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessUpActionIndicator : ActionIndicator
{
    public override void PerformAction() {

    }

    protected override IEnumerator IndicatorCoroutine() {
        yield return null;
    }
}
