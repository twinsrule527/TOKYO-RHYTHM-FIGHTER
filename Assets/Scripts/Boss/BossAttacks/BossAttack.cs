using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enum is used for InterruptInputs, to see what general type of input was given
public enum InputType {
    Attack,
    Block
}
//This is a struct that has all the information an Interrupt function might need 
public struct InterruptInput {
    public InputType input;
    public float amtOnBeat;//How on beat
    public InterruptInput NullInput() {
        InterruptInput newInput;
        newInput.input = InputType.Attack;
        newInput.amtOnBeat = 0;
        return newInput;
    }
}

//This is an abstract class which manages a single attack that a boss can make
    //It contains 4 very important functions:
        //An Attack coroutine that runs through the attack
        //An Interrupt function in case the attack can be interrupted
        //A Cancel coroutine that the Interrupt function runs to stagger the boss when they're interrupted
        //A CheckAttackSuccess Coroutine which checks to see if the attack hits after the beat leeway time passes
public abstract class BossAttack : MonoBehaviour
{
    [SerializeField] private char _creatorKey;//The char used to create this from a text doc
    public char CreatorKey {
        get {
            return _creatorKey;
        }
    }
    [SerializeField] private bool _parryable;//Whether the player's parry action works against this attack or not
    public bool Parryable {
        get {
            return _parryable;
        }
    }
    void Awake() {
        myAnimationController.spriteRenderer = GetComponentInParent<SpriteRenderer>();
        _creatorKey = name[0];
    }
    public abstract IEnumerator Attack();

    public abstract void Interrupt(PlayerAction action);

    public abstract IEnumerator Cancel();

    public abstract void CheckAttackSuccess();

    public abstract void AddBeatToIndicator();
    //Whenever a boss attack is parried, it should call this
    protected virtual void isParried() {
        if(Global.Tutorial) {
            TutorialStage_Parry.IncreaseParryNumber();
        }
    }
    public float length = 1f;//How long in beats this attack should take
    [SerializeField] private bool _isAnAttack;//Boolean value used to determine if it counts as an attack for the sake of interrupting actions, etc
    public bool IsAnAttack {
        get {
            return _isAnAttack;
        }
    }
    public Sprite attackIndicatorSprite;//The Sprite the beat Indicator should take when making this attack
    public bool playsHitAnimation;
    [SerializeField] protected AnimationController myAnimationController;
    
    [SerializeField] protected string atkName;//what the boss sprite controller calls this

}
