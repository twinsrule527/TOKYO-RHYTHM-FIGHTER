using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    //Between 0,1 and this decides the rate at which health is visually decreased from the health bar (the higher the decimal, the faster it changes)
    [SerializeField] float interpolationSpeed;
    Shake shaker;
    private float shakeMultiplier = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        shaker = GetComponentInParent<Shake>();
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeHealth(float healthDifference)
    {
        StartCoroutine(HealthBarChange());
        shaker.ShakeIt(healthDifference * shakeMultiplier, healthDifference * shakeMultiplier);
    }


    public IEnumerator HealthBarChange()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (((float)Global.Boss.bossHP) / ((float)Global.Boss.currentStageStartingHP)), interpolationSpeed);
        yield return null;
    }
}
