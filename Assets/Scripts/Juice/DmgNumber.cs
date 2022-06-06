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

    //private bool resetPlayerText;
    //private bool resetBossText;

    private float distanceToCenter;
    [SerializeField] private float PlayerHeightMultiplier;
    [SerializeField] private float BossHeightMultiplier;

    private float bossTextTime = 0.75f;
    private float playerTextTime = 0.75f; //TODO make these in beats time 
    private float showBossText = 0f;
    private float showPlayerText = 0f;
    private bool playerRunning = false;
    private bool bossRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPlayerDmg = PlayerDmgText.transform.position;
        originalBossDmg = BossDmgText.transform.position;
  
        //resetBossText = false;
        //resetPlayerText = false;

        PlayerDmgText.text = "";
        BossDmgText.text = "";

        distanceToCenter = Screen.width / 5;

    }

    public void PlayerDMGChange(float damage)
    {
        PlayerDmgText.text = damage.ToString();
        showPlayerText = playerTextTime; //refill the show text counter
        PlayerDmgText.transform.position = originalPlayerDmg;

        if(!playerRunning) {
            StartCoroutine(PlayerDmg(damage));
        }
    }

    public void BossDMGChange(float damage)
    {
        //if we're refilling boss hp by adding, don't show 
        if(damage > 0) {
            return;
        }

        BossDmgText.text = (-1 * damage).ToString(); //show positive numbers so player doesnt think negative # = bad 
        showBossText = bossTextTime;
        BossDmgText.transform.position = originalBossDmg;

        if(!bossRunning) {
            StartCoroutine(BossDmg(damage));
        }
    }


    IEnumerator PlayerDmg(float damage)
    {
        //resetPlayerText = true;
         
        //if (resetPlayerText)
        //{
            //for (float t = 0f; t <= 0.8f; t += Time.deltaTime) {
            while(showPlayerText > 0){
                showPlayerText = showPlayerText - Time.deltaTime;
                PlayerDmgText.transform.position = Vector3.Lerp(PlayerDmgText.transform.position, transform.position + new Vector3(-distanceToCenter,-distanceToCenter/PlayerHeightMultiplier,0), (playerTextTime - showPlayerText));
                yield return null;
            }
            PlayerDmgText.transform.position = originalPlayerDmg;
            PlayerDmgText.text = "";

            //resetPlayerText = false;
        //}
    }

    public static float curContinuousPlayerDamage;
    public void StartPlayerDamageContinuous(float timeToCheck) {
        StartCoroutine(PlayerDmgContinuous(timeToCheck));
    }

    private IEnumerator PlayerDmgContinuous(float timeToCheck) {
        BossDmgText.text = (curContinuousPlayerDamage).ToString();
        float startTime = BeatController.GetBeat();
        float t = startTime;
        while(t < startTime + timeToCheck) {
            yield return null;
            BossDmgText.text = (curContinuousPlayerDamage).ToString(); //show positive num
            t = BeatController.GetBeat();
        }
    }

    IEnumerator BossDmg(float damage)
    {
        
        //resetBossText = true;
        
        bossRunning = true;

        //if (resetBossText)
        //{
            //for (float t = 0f; t <= 0.8f; t += Time.deltaTime)
            //{
            while(showBossText > 0) {
                showBossText -= Time.deltaTime;
                BossDmgText.transform.position = Vector3.Lerp(BossDmgText.transform.position, transform.position + new Vector3(distanceToCenter*BossHeightMultiplier, distanceToCenter*BossHeightMultiplier, 0), (bossTextTime - showBossText));
                yield return null;
            }
            BossDmgText.transform.position = originalBossDmg;
            BossDmgText.text = "";

            bossRunning = false;

            //resetBossText = false;
        //}
    }
}
