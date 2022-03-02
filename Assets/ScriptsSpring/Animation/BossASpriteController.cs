using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossASpriteController : MonoBehaviour
{

    public AnimationController shortAttack;
    public AnimationController longAttack;
    public AnimationController hurt;


    private void Awake()
    {
        shortAttack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        longAttack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        hurt.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            ShortAttack(1.0f);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            longAttack.delayFraction = 1.0f;
            LongAttack();
        }
    }

    public void ShortAttack(float beatFraction)
    {
        shortAttack.spriteRenderer.sprite = shortAttack.Sprites[0];
        shortAttack.PlayAnimationOnBeat(beatFraction);
    }


    //Mess Up plays from 0 bc it should play immediately when players mess up. 
    public void LongAttack()
    {
        longAttack.spriteRenderer.sprite = longAttack.Sprites[0];
        longAttack.PlayAnimation();
    }

    //Getting hurt animation should also be played immediately. 
    //Might replace with a block/Parry
    public void Hurt()
    {
        hurt.spriteRenderer.sprite = hurt.Sprites[0];
        hurt.PlayAnimation();
    }
}
