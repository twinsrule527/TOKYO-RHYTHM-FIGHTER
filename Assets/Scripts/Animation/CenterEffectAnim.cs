using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterEffectAnim : MonoBehaviour
{
    [SerializeField] List<Sprite> effectList;
    [SerializeField] SpriteRenderer rendererCur;

    public virtual void OnEnable()
    {
        Debug.Log("hello i am attached to " + gameObject.name);
        Camera.main.gameObject.GetComponent<Shake>().ShakeIt(1.5f, 0.2f, 50f);
        PlayAnimation();
    }

    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayAnimation();
        }
    }

    public virtual void PlayAnimation()
    {
        for (int i = 0; i < effectList.Count; i++)
        {
            rendererCur.sprite = effectList[i];
        }
    }
}
