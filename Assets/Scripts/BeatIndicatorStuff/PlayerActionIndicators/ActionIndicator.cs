using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A super-class from which all player actions inherit their visual feedback whenever they perform actions
public abstract class ActionIndicator : MonoBehaviour
{
    [SerializeField] private PlayerAction parentAction;//The action which performs this indicator
    void Start()
    {
       if(parentAction == null) {
           parentAction = GetComponentInParent<PlayerAction>();
       }
       transform.position = Global.BeatIndicatorBrain.VisualEndPos.transform.position;
       gameObject.SetActive(false);
    }

    public abstract void PerformAction();

    protected abstract IEnumerator IndicatorCoroutine();
}
