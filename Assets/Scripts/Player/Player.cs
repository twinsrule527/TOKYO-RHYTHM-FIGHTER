using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerSpriteController spriteController;
    public PlayerSFXController sfxController;
    PlayerAction[] actions;
    public MessUpAction messUpAction;//The player has a mess-up action that their current action gets set to if they fail to perform an action

    public int comboCounter;

    public PlayerAction CurrentAction;//The action the player is currently performing
    public float currActionEndBeat;//The exact beat in the song that the current action is supposed to end on
    
    //How much health the player starts with
    [SerializeField] float _playerStartHealth = 50;
    [SerializeField] private HurtAnimation playerHurtAnimation;
    public DmgNumber dmgNumber;
    [SerializeField] PlayerHealthBar healthBarPlayer;


    public float playerStartHealth {get; private set;}
    public float playerHealth {get; private set;}
    public float playerVisualHealth { get; private set; }


    void Awake() {
        //Things that have to occur at the very very beginning all occur in Awake
        //There can only be 1 player, and it will be the Instance of the player
        Global.Player = this;
        playerStartHealth = _playerStartHealth;
        playerHealth = playerStartHealth;
        playerVisualHealth = playerStartHealth;
        //load PlayerActions, which will be components on the Player object or its children 
        actions = GetComponentsInChildren<PlayerAction>();
    }
    // Start is called before the first frame update
    void Start()
    {   
        
        dmgNumber = GameObject.FindGameObjectWithTag("DmgManager").GetComponent<DmgNumber>();
        healthBarPlayer = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerHealthBar>();
        enabled = false;
    }

    public void SongStarted() {
        //Calls the sprite controller's song started
        spriteController.SongStarted();
    }

    // Update is called once per frame
    void Update()
    {
        ClearCurrentAction();
        //check for input that might activate a PlayerAction
        //we can control the order these are executed in here if we need to 
        foreach(PlayerAction a in actions) {
            a.CheckInput();
        }
    }

    //How outside objects should affect the player's health
    public void ChangeHP(float amt = 0) {

        if(!GameManager.gameplayRunning) {
            return;
        }

        if(!Global.Tutorial) {
            playerHealth += amt;
        }
        dmgNumber.PlayerDMGChange(amt);
        healthBarPlayer.ChangeHealth(amt);
        //Global.UIManager.SetHealthText();
        playerHurtAnimation.Hurt();
        sfxController.PlayHurtSound();
        Global.ComboIndicator.SetCombo(0);

        if(playerHealth <= 0) {
            GameManager.PlayerLoses();
        }
    }


    public void ChangeVisualHP(float amt = 0)
    {
        if (!Global.Tutorial)
        {
            playerVisualHealth += amt;
        }
        //playerHurtAnimation.Hurt();
        healthBarPlayer.ChangeHealthLerp(amt);
    }

    public void EndOfBeat1() {
        //If the player doesn't act, they reset the comboIndicator
        if(CurrentAction == null) {
            Global.ComboIndicator.SetCombo(0);
        }
        //CurrentAction = null; //TODO bandaid fix 
    }

    public void EndOfBeat05() {

    }

    public void EndOfBeat025() {
    }

    //A function that is triggered at the end of every possible beatFraction, checking to see if the current action being performed needs to be cleared
    private void ClearCurrentAction() {
        if(CurrentAction != null) {
            //Checks to see if the beat's end time has passed
            if(currActionEndBeat - BeatController.MINIMUM.thresholdBeforeBeat < BeatController.GetBeat()) {
                CurrentAction = null;
            }
        }
    }



}
