using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;


    [SerializeField] Image healthBarLerp;
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
        }*/
        
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
            
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (((float)Global.Player.playerHealth) / ((float)Global.Player.playerStartHealth)), t *interpolationSpeed);


            yield return null;
        }
    }

    public IEnumerator HealthBarChangeLerp()
    {
        for (float t = 0f; t <= 3f; t += Time.deltaTime)
        {

            healthBarLerp.fillAmount = Mathf.Lerp(healthBarLerp.fillAmount, (((float)Global.Player.playerVisualHealth) / ((float)Global.Player.playerVisualHealth)), t);


            yield return null;
        }
    }
}
