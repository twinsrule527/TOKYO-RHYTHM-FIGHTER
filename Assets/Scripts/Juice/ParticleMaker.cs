using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaker : MonoBehaviour
{
    [SerializeField] protected List<Sprite> sprites;
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        Global.ParticleMaker = this;
        render = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Space))
        {
            SpawnPlayerParticles(transform.position, transform.rotation);
            Debug.Log("BRUH FOR ERIC");
        }*/
       
    }


    //one time animation method
    public void playAnimation()
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            render.sprite = sprites[i];
        }

        render.sprite = null;
    }


    public static void SpawnBossParticles()
    {
    }


    
}
