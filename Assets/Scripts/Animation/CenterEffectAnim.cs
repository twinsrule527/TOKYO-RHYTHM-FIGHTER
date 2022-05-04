using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterEffectAnim : MonoBehaviour
{
    [SerializeField] List<Sprite> effectList;
    private SpriteRenderer renderer;

    void OnEnable()
    {
        
        Camera.main.gameObject.GetComponent<Shake>().ShakeIt(2f, 0.2f, 50f);
        PlayAnimation();
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation()
    {
        for (int i = 0; i < effectList.Count; i++)
        {
            renderer.sprite = effectList[i];
        }
    }
}
