using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class that manages boss health and AI
public class Boss : MonoBehaviour
{
    //public static Boss CurrentBoss;//Declares whichever boss is the current boss, for reference with player input & such
    [SerializeField] float [] bossStartingHPArray = {50f}; //starting HP for each stage, in order 
    public float [] BossStartingHPArray {
        get {
            return bossStartingHPArray;
        }
    }
    public float currentStageStartingHP;
    public float bossHP {get; protected set;}
    public float bossVisualHP { get; protected set; }
    public bool makeAttackThisBeat;
    public BossAttack CurrentMakingAttack;//Whichever attack is the one actually making an attack this beat (in case it ends before it has a chance to check)
        //Probably there's a better way to do this - should check w/ Jaden
    
    [SerializeField] HealthBar healthBar;
    [SerializeField] DmgNumber dmgNumber;
    [SerializeField] private HurtAnimation hurtAnimation;

    public void ChangeBossHP(float amt) {//Function to be called by others when increasing/decreasing hp
        bossHP += amt;
        bossVisualHP = bossHP;
        healthBar.ChangeHealthLerp(amt);
        Global.UIManager.SetHealthText();
        healthBar.ChangeHealth(amt);
        dmgNumber.BossDMGChange(amt);
        AttackAI.CheckStageChange();
        if(bossHP <= 0) {
            if(!Global.Tutorial) {
                GameManager.PlayerWins();
            }
            else {
                //Resets boss health to their health for the start of the stage if they would die during the tutorial
                bossHP = currentStageStartingHP;
            }
        }
    }

    public void ChangeVisualBossHP(float amt)
    {
        bossVisualHP += amt;
        hurtAnimation.Hurt();
        healthBar.ChangeHealthLerp(amt);
    }

    //Sets the Boss' HP to a certain amt
    public void SetBossHP(float amt) {
        bossHP = amt;
        bossVisualHP = bossHP;
        healthBar.ChangeHealthLerp(amt);
        Global.UIManager.SetHealthText();
        healthBar.ChangeHealth(amt);
        dmgNumber.BossDMGChange(amt);
    }

    public BossAI AttackAI;
    public BossSpriteController spriteController;
    public BossSFXController sfxController;
    public virtual void Awake() {
        //Going to remove this later:
        Global.Boss = this;
        currentStageStartingHP = bossStartingHPArray[0];
        bossHP = currentStageStartingHP;
        bossVisualHP = currentStageStartingHP;
        dmgNumber = GameObject.FindGameObjectWithTag("DmgManager").GetComponent<DmgNumber>();
        healthBar = GameObject.FindGameObjectWithTag("CenterHealth").GetComponent<HealthBar>();

    }

    public void SongStarted() {
        AttackAI.SongStarted();
        spriteController.SongStarted();
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
        //At the end of every beat, it makes sure the current set attack is the correct one
        if(AttackAI.CurrentAttackOutgoing != AttackAI.CurrentAttack) {
            //This is set here, so the player will succesfully get interrupted if they attempt to attack at the same time as the enemy
            AttackAI.CurrentAttackOutgoing = AttackAI.CurrentAttack;
        }
    }

    public void EndOfBeat05() {

    }

    public void EndOfBeat025() {

    }
}
