using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarLeft;
    [SerializeField] Image healthBarRight;


    [SerializeField] Image healthBarLeftLerp;
    [SerializeField] Image healthBarRightLerp;
    //Between 0,1 and this decides the rate at which health is visually decreased from the health bar (the higher the decimal, the faster it changes)
    [SerializeField] float interpolationSpeed;
    ShakeRect shaker;
    private float shakeMagnitude = 6f;
    private float shakeTimeMultiplier = -0.25f;



    // Start is called before the first frame update
    void Start()
    {
        shaker = GetComponent<ShakeRect>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        ////Testing function
        if (Input.GetKey(KeyCode.Space))
        {
           ChangeHealth(5);
        }
        */
    }

    public void ChangeHealth(float healthDifference)
    {
        // /*
        StopCoroutine(HealthBarChange());
        StartCoroutine(HealthBarChange());
        // */

        
        shaker.ShakeIt(healthDifference * shakeMagnitude, healthDifference * shakeTimeMultiplier);
    }

    public void ChangeHealthLerp(float healthDifference)
    {
        // /*
        StopCoroutine(HealthBarChangeLerp());
        StartCoroutine(HealthBarChangeLerp());
        // */

        shaker.ShakeIt(healthDifference * shakeMagnitude, healthDifference * shakeTimeMultiplier);
    }


    public IEnumerator HealthBarChange()
    {
        
        for (float t = 0f; t <= 2f; t += Time.deltaTime)
        {
            
            healthBarLeft.fillAmount = Mathf.Lerp(healthBarLeft.fillAmount, (((float)Global.Boss.bossHP) / ((float)Global.Boss.currentStageStartingHP)), t *interpolationSpeed );
            healthBarRight.fillAmount = Mathf.Lerp(healthBarRight.fillAmount, (((float)Global.Boss.bossHP) / ((float)Global.Boss.currentStageStartingHP)), t *interpolationSpeed);


            yield return null;
        }
    }

    public IEnumerator HealthBarChangeLerp()
    {
        for (float t = 0f; t <= 3f; t += Time.deltaTime)
        {

            healthBarLeftLerp.fillAmount = Mathf.Lerp(healthBarLeftLerp.fillAmount, (((float)Global.Boss.bossVisualHP) / ((float)Global.Boss.currentStageStartingHP)), t);
            healthBarRightLerp.fillAmount = Mathf.Lerp(healthBarRightLerp.fillAmount, (((float)Global.Boss.bossVisualHP) / ((float)Global.Boss.currentStageStartingHP)), t);


            yield return null;
        }
    }
}
