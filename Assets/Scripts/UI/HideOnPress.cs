using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void _HideOnPress() {
        gameObject.SetActive(false);
    }


}
