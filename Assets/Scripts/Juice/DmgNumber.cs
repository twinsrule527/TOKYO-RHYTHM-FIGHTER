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

    [SerializeField] private float distanceToCenter;
    [SerializeField] private float bossHeightMultiplier;



    // Start is called before the first frame update
    void Start()
    {
        originalPlayerDmg = PlayerDmgText.transform.position;
        originalBossDmg = BossDmgText.transform.position;
  


        resetBossText = false;
        resetPlayerText = false;

        PlayerDmgText.text = "";
        BossDmgText.text = "";




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


    public IEnumerator PlayerDmg(float damage)
    {
        resetPlayerText = true;
        PlayerDmgText.text = damage.ToString();

        if (resetPlayerText)
        {
            for (float t = 0f; t <= 0.8f; t += Time.deltaTime)
            {
                PlayerDmgText.transform.position = Vector3.Lerp(PlayerDmgText.transform.position, transform.position + new Vector3(-distanceToCenter,-distanceToCenter,0), t);
                yield return null;
            }
            PlayerDmgText.transform.position = originalPlayerDmg;
            PlayerDmgText.text = "";

            resetPlayerText = false;
        }
    }

    public IEnumerator BossDmg(float damage)
    {
        resetBossText = true;
        BossDmgText.text = damage.ToString();

        if (resetBossText)
        {
            for (float t = 0f; t <= 0.8f; t += Time.deltaTime)
            {
                BossDmgText.transform.position = Vector3.Lerp(BossDmgText.transform.position, transform.position + new Vector3(distanceToCenter, distanceToCenter*bossHeightMultiplier, 0), t);
                yield return null;
            }
            BossDmgText.transform.position = originalBossDmg;
            BossDmgText.text = "";

            resetBossText = false;
        }
    }
}
