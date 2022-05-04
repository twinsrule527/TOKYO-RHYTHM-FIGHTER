using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterEffectManager : MonoBehaviour
{

    [SerializeField] GameObject bossHit;
    [SerializeField] GameObject playerHit;
    [SerializeField] GameObject playerMiss;
    [SerializeField] GameObject harmonize;


    // Start is called before the first frame update
    void Start()
    {
        Global.CenterEffectManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum CenterEffect
    {
        BossHits, PlayerHits, PlayerMisses, Harmonizes
    }

    public void CallCenterEffect(CenterEffect effect)
    {
        if (effect == CenterEffect.BossHits)
        {
            bossHit.SetActive(true);
        } else if (effect == CenterEffect.PlayerHits)
        {
            playerHit.SetActive(true);
        } else if (effect == CenterEffect.PlayerMisses)
        {
            playerMiss.SetActive(true);
        } else if (effect == CenterEffect.Harmonizes)
        {
            harmonize.SetActive(true);
        }


    }
}
