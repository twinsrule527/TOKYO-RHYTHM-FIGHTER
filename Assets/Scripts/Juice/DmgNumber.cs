using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text PlayerDmgText;
    [SerializeField] private TMP_Text BossDmgText;

    

    private Vector3 originalPlayerDmg;
    private Vector3 originalBossDmg;

    private bool resetPlayerText;
    private bool resetBossText;

    private float distanceToCenter;
    [SerializeField] private float PlayerHeightMultiplier;
    [SerializeField] private float BossHeightMultiplier;




    // Start is called before the first frame update
    void Start()
    {
        originalPlayerDmg = PlayerDmgText.transform.position;
        originalBossDmg = BossDmgText.transform.position;
  


        resetBossText = false;
        resetPlayerText = false;

        PlayerDmgText.text = "";
        BossDmgText.text = "";

        distanceToCenter = Screen.width / 5;


    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Escape))
        {

            StartCoroutine(PlayerDmg(-4));
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(BossDmg(-3));
        }
        */

    }

    public void PlayerDMGChange(float damage)
    {
        StartCoroutine(PlayerDmg(damage));
    }

    public void BossDMGChange(float damage)
    {
        StartCoroutine(BossDmg(damage));
    }


    IEnumerator PlayerDmg(float damage)
    {
        resetPlayerText = true;
        PlayerDmgText.text = damage.ToString();

        if (resetPlayerText)
        {
            for (float t = 0f; t <= 0.8f; t += Time.deltaTime)
            {
                PlayerDmgText.transform.position = Vector3.Lerp(PlayerDmgText.transform.position, transform.position + new Vector3(-distanceToCenter,-distanceToCenter/PlayerHeightMultiplier,0), t);
                yield return null;
            }
            PlayerDmgText.transform.position = originalPlayerDmg;
            PlayerDmgText.text = "";

            resetPlayerText = false;
        }
    }

    public static float curContinuousPlayerDamage;

    public void StartPlayerDamageContinuous(float timeToCheck) {
        StartCoroutine(PlayerDmgContinuous(timeToCheck));
    }
    private IEnumerator PlayerDmgContinuous(float timeToCheck) {
        BossDmgText.text = curContinuousPlayerDamage.ToString();
        float startTime = BeatController.GetBeat();
        float t = startTime;
        while(t < startTime + timeToCheck) {
            yield return null;
            BossDmgText.text = curContinuousPlayerDamage.ToString();
            t = BeatController.GetBeat();
        }
    }

     IEnumerator BossDmg(float damage)
    {
        resetBossText = true;
        BossDmgText.text = damage.ToString();

        if (resetBossText)
        {
            for (float t = 0f; t <= 0.8f; t += Time.deltaTime)
            {
                BossDmgText.transform.position = Vector3.Lerp(BossDmgText.transform.position, transform.position + new Vector3(distanceToCenter*BossHeightMultiplier, distanceToCenter*BossHeightMultiplier, 0), t);
                yield return null;
            }
            BossDmgText.transform.position = originalBossDmg;
            BossDmgText.text = "";

            resetBossText = false;
        }
    }
}
