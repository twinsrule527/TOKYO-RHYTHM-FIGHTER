using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossASpriteController : BossSpriteController
{

    public AnimationController shortAttack;
    public AnimationController longAttack;
    public AnimationController hurt;

    public AnimationController waitAttack1;
    public AnimationController waitAttack2;
    public AnimationController length1Attack;
    public AnimationController length3Attack;
    public AnimationController oneBeatAttack;


     void Awake()
    {
        basePosition = GetComponentInParent<SpriteRenderer>().transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        shortAttack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        longAttack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        hurt.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        waitAttack1.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        waitAttack2.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        length1Attack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        length3Attack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        oneBeatAttack.spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.O))
        //{
        //    ShortAttack(1.0f);
        //}

        //if (Input.GetKeyUp(KeyCode.I))
        //{
        //    longAttack.delayFraction = 1.0f;
        //    LongAttack();
        //}
    }
    public override void CallAttack(string ATKname, float beatFraction)
    {
        if(ATKname == "ShortAttack") {
            ShortAttack();
        }
        else if(ATKname == "LongAttack") {
            LongAttack();
        }
        else if (ATKname == "WaitAttack1")
        {
            LongAttack();
        }
        else if (ATKname == "WaitAttack2")
        {
            LongAttack();
        }
        else if (ATKname == "OneBeatAttack")
        {
            LongAttack();
        }
        else if (ATKname == "Length1Attack")
        {
            LongAttack();
        }
        else if (ATKname == "Length3Attack")
        {
            LongAttack();
        }
        /*else if(ATKname == "WaitAttack") {
            Hurt();
        }*/
    }
    
    public override void SongStarted()
    {
        
    }
    public void ShortAttack()
    {
        //shortAttack.spriteRenderer.sprite = shortAttack.Sprites[0];
        shortAttack.PlayAnimation();//Beat(beatFraction);
    }


    //Mess Up plays from 0 bc it should play immediately when players mess up. 
    public void LongAttack()
    {
        longAttack.PlayAnimation();
    }

    //Getting hurt animation should also be played immediately. 
    //Might replace with a block/Parry
    public void Hurt()
    {
        hurt.PlayAnimation();
    }

    public void WaitAttack1()
    {
        waitAttack1.PlayAnimation();
    }


    public void WaitAttack2()
    {
        waitAttack2.PlayAnimation();
    }

    public void Length1Attack()
    {
        length3Attack.PlayAnimation();
    }
    public void Length3Attack()
    {
        length3Attack.PlayAnimation();//Beat(beatFraction);
    }


    //Mess Up plays from 0 bc it should play immediately when players mess up. 
    public void OneBeatAttack()
    {
        //oneBeatAttack.spriteRenderer.sprite = oneBeatAttack.Sprites[0];
        oneBeatAttack.PlayAnimation();
    }

    
}
