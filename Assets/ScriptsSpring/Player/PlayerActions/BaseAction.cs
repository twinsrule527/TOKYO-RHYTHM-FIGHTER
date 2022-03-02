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
        if(Global.Player.CurrentAction == null) {
            Accuracy curAccuracy = BeatController.GetAccuracy(beatFraction);
            //Call PlayerSpriteController.DisplayAccuracy(Accuracy);
            if(curAccuracy.priority > 0) {
                    Success();
            }
            else {
                MessUp();
            }
        }
    }

    protected override void Success()
    {
        base.Success();
        Global.Boss.ChangeBossHP(-damage);
    }
}
