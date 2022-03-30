using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;

    // Start is called before the first frame update
    void Start()
    {

        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = ((int) Global.Boss.bossHP)/ ((int)Global.Boss.bossHP) ;
    }
}
