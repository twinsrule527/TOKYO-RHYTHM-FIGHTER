using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//An indicator for the basic hit action
public class BaseActionIndicator : ActionIndicator
{
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashLength;//How long the flash is for, in beat time
    public override void PerformAction()
    {
        StartCoroutine(IndicatorCoroutine());
    }

    protected override IEnumerator IndicatorCoroutine()
    {
        SpriteRenderer centerSprite = Global.BeatIndicatorBrain.VisualEndPos.GetComponent<SpriteRenderer>();
        Color centerBaseColor = centerSprite.color;
        centerSprite.color = flashColor;
        yield return BeatController.WaitForBeat(BeatController.GetBeat() + flashLength);
        centerSprite.color = centerBaseColor;
        gameObject.SetActive(false);
    }
}
