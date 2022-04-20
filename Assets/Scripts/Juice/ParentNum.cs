using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParentNum : MonoBehaviour
{
    [SerializeField] private TMP_Text DmgTextSub;
    // Start is called before the first frame update
    void Start()
    {
        DmgTextSub = GetComponent<TMP_Text>();


    }

    // Update is called once per frame
    void Update()
    {
        DmgTextSub.text = GetComponentInParent<TMP_Text>().text;
        

    }
}
