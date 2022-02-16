using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class that manages boss health and AI
public class Boss : MonoBehaviour
{
    //public static Boss CurrentBoss;//Declares whichever boss is the current boss, for reference with player input & such
    protected float bossHP;
    public bool makeAttackThisBeat;
    public BossAttack CurrentMakingAttack;//Whichever attack is the one actually making an attack this beat (in case it ends before it has a chance to check)
        //Probably there's a better way to do this - should check w/ Jaden

    public void ChangeBossHP(int amt) {//Function to be called by others when increasing/decreasing hp
        bossHP += amt;
        if(bossHP <= 0) {
            GameManager.PlayerWins();
        }
    }

    [SerializeField] public BossAI AttackAI;

    public virtual void Awake() {
        //Going to remove this later:
        Global.Boss = this;
    }

    //Allows player attacks to interupt the boss' current attack
    public static void InterruptAttack(PlayerAction action) {
        Global.Boss.AttackAI.CurrentAttack.Interrupt(action);
    }

    public void EndOfBeat1() {
        //At the end of this beat, if an attack is being made, it checks to see if it succeeds
        if(makeAttackThisBeat) {
            makeAttackThisBeat = false;
            CurrentMakingAttack.CheckAttackSuccess();
        }
    }

    public void EndOfBeat05() {

    }

    public void EndOfBeat025() {

    }
}
