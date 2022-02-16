using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A starting action in the game, which works as both an attack & a parry
public class BaseAction : PlayerAction
{
    public float damage;//How much damage this attack does

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void TryAction()
    {
        if(BeatController.IsOnBeat(beatFraction)) {
            if(Global.Player.CurrentAction == null) {
                Success();
            }
        }
        else {
            MessUp();
        }
    }

    protected override void Success()
    {
        base.Success();
        Global.Boss.ChangeBossHP(-damage);
    }
}
