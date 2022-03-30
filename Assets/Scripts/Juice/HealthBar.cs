using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    //Between 0,1 and this decides the rate at which health is visually decreased from the health bar (the higher the decimal, the faster it changes)
    [SerializeField] float barInterpolate;



    // Start is called before the first frame update
    void Start()
    {

        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (((float) Global.Boss.bossHP)/ ((float)Global.Boss.currentStageStartingHP)), barInterpolate) ;
    }
}
