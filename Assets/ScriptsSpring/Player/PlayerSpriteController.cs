using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public SpriteRenderer trueRenderer;

    public AnimationController attack;
    public AnimationController messUp;
    public AnimationController hurt;




    // Start is called before the first frame update
    void Start()
    {

        trueRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            Attack(1.0f);
        }

        if (Input.GetKey(KeyCode.P))
        {
            Hurt();
        }
    }

    //Attack is usually played as animation on beat
    public void Attack(float beatFraction)
    {
        trueRenderer.sprite = attack.frame0;
        attack.PlayAnimationOnBeat(beatFraction);
    }


    //Mess Up plays from 0 bc it should play immediately when players mess up. 
    public void MessUp()
    {
        trueRenderer.sprite = messUp.frame0;
        messUp.PlayAnimation();
    }

    //Getting hurt animation should also be played immediately. 
    //Might replace with a block/Parry
    public void Hurt()
    {
        trueRenderer.sprite = hurt.frame0;
        hurt.PlayAnimation();
    }
}
