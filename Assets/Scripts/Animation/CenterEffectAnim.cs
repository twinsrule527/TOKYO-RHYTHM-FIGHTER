using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterEffectAnim : MonoBehaviour
{
    [SerializeField] float delayFraction;
    [SerializeField] List<Sprite> effectList;
    [SerializeField] SpriteRenderer rendererCur;

    public virtual void OnEnable()
    {
        if (effectList.Count > 0)
        {
            Camera.main.gameObject.GetComponent<Shake>().ShakeIt(0.5f, 0.2f, 50f);
            StartCoroutine(PlayAnimation());
        }
    }

    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameObject.SetActive(true);

            
        }*/
    }

    public virtual IEnumerator PlayAnimation()
    {
        if (effectList.Count > 0)
        {
            for (int i = 0; i < effectList.Count; i++)
            {
                rendererCur.sprite = effectList[i];
                yield return StartCoroutine(BeatController.WaitForBeat(delayFraction));

            }
            rendererCur.sprite = null;
            gameObject.SetActive(false);
        }
    }


}


