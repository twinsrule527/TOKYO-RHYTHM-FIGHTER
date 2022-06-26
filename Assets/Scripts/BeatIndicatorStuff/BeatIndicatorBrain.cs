using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Shows the beat indicator wherever it is on the screen
    //Tells which beats to be present from boss attacks & telegraphing

//This struct gives information on what needs to be given to the beat indicator object
public struct beatIndicatorInfo {
    public float beatToHit;//The beat on which the indicator hits the center
    //Also needs information on the type of indicator
    public Sprite indicatorSprite;//The sprite which should be shown by this indicator
    public bool playsAnimationAtEnd;//Whether the indicator plays a specific animation at its end - needs to be true for some boss animations
}

public class BeatIndicatorBrain : MonoBehaviour
{
    [SerializeField] private BossBeatIndicator bossIndicatorPrefab;//Uses this prefab to instantiate more when needed
    [SerializeField] private PlayerBeatIndicator playerIndicatorPrefab;
    [SerializeField] private Transform startPosBossTransform;//A transform indicator for the start position of the boss indicator
    [SerializeField] private Transform startPosPlayerTransform;//A transform indicator for the start position of the player indicator
    [SerializeField] private Transform endPosTransform;//Similarly to above, but for end pos
    public Transform EndPosTransform {
        get {
            return endPosTransform;
        }
    }
    [SerializeField] private GameObject _visualEndPos;
    public GameObject VisualEndPos {
        get {
            return _visualEndPos;
        }
    }
    public static Vector3 BossIndicatorStartPos;
    public static Vector3 BossIndicatorEndPos;
    public static Vector3 PlayerIndicatorStartPos;
    public static Vector3 PlayerIndicatorEndPos;
    public static readonly float beatsInAdvanceShown = 4f;//How much in advance beats appear on the beat indicator
    private float curBossStartBeat;//Keeps track of what beat each boss attack should start on
    private float curBaseBeat;
    [SerializeField] private Sprite baseBeatSprite;
    private List<BossBeatIndicator> BossIndicators;//a list of exisitng indicators - so that when they deactive, this can start using them again
    public List<PlayerBeatIndicator> PlayerIndicators;
    private List<beatIndicatorInfo> BossBeats;
    private List<beatIndicatorInfo> PlayerLeftBeats;
    private List<beatIndicatorInfo> PlayerRightBeats;
    //Test lists
    [SerializeField] private List<float> TestBeats;
    [SerializeField] private List<Sprite> TestSprites;
    //Information that the BeatIndicatorBrain needs to give to a beat indicator:
        //How long (in beat-form) from now does that note actually occur
        //what type of beat it is


    [SerializeField] BossHitsPlayerScript _BossHitsPlayerEffect;
    public static BossHitsPlayerScript BossHitsPlayerEffect;
   
    void Awake() {
        Global.BeatIndicatorBrain = this;
        BossBeats = new List<beatIndicatorInfo>();
        BossIndicators = new List<BossBeatIndicator>();
        PlayerLeftBeats = new List<beatIndicatorInfo>();
        PlayerRightBeats = new List<beatIndicatorInfo>();
        PlayerIndicators = new List<PlayerBeatIndicator>();
        BossIndicatorStartPos = startPosBossTransform.position;//TODO: Change the way that the bossindicator start pos is set
        BossIndicatorEndPos = endPosTransform.position + Vector3.back;
        PlayerIndicatorStartPos = startPosPlayerTransform.position;
        PlayerIndicatorEndPos = endPosTransform.position + Vector3.back;

        BossHitsPlayerEffect = _BossHitsPlayerEffect;
        
        enabled = false;
    }
    public void SongStarted() {
        for(int i = 0; i < 10; i++) {
            AddBaseBeat(PlayerLeftBeats, PlayerRightBeats);
        }
    }
    void Update() {
        if(BossBeats.Count > 0) {
            float curBeat = BeatController.GetBeat();
            //Checks to see if the current beat is close enough to the beat to hit - if it is, the beat shows up
            if(curBeat >= BossBeats[0].beatToHit - beatsInAdvanceShown) {
                //Activates a BeatIndicator and gives it the information it needs
                BossBeatIndicator newIndicator = null;
                //So that it doesn't usually create a new indicator, it checks to see if it has a disabled indicator it can use
                for(int i = 0; i < BossIndicators.Count; i++) {
                    if(BossIndicators[i].enabled == false) {
                        newIndicator = BossIndicators[i];
                        newIndicator.enabled = true;
                        newIndicator.moving = true;
                        break;
                    }
                }
                if(newIndicator == null) {
                    newIndicator = Instantiate(bossIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    BossIndicators.Add(newIndicator);
                    newIndicator.transform.rotation = startPosBossTransform.rotation;
                    newIndicator.startRot = newIndicator.transform.rotation;
                }
                //Now, it gives the boss beat indicator the information it needs to operate
                newIndicator.SetIndicatorStart(BossBeats[0]);
                //Checks to see if it needs to add a base beat
                    //Does so if the next beat that will appear doesn't exist
                /*if(BossBeats.Count <= 10) {
                    AddBaseBeat(BossBeats);
                }*/
                BossBeats.RemoveAt(0);
            }
        }
        //ALso plays out the player beat indicators
        if(PlayerLeftBeats.Count > 0) {
            float curBeat = BeatController.GetBeat();
            //Checks to see if the current beat is close enough to the beat to hit - if it is, the beat shows up
            if(curBeat >= PlayerLeftBeats[0].beatToHit - beatsInAdvanceShown) {
                //Activates a BeatIndicator and gives it the information it needs
                PlayerBeatIndicator newIndicator1 = null;
                PlayerBeatIndicator newIndicator2 = null;
                //So that it doesn't usually create a new indicator, it checks to see if it has a disabled indicator it can use
                for(int i = 0; i < PlayerIndicators.Count; i++) {
                    if(PlayerIndicators[i].enabled == false) {
                        newIndicator1 = PlayerIndicators[i];
                        newIndicator1.enabled = true;
                        newIndicator1.transform.rotation = startPosPlayerTransform.rotation;
                        newIndicator1.startRot = newIndicator1.transform.rotation;
                        break;
                    }
                }
                for(int i = 0; i < PlayerIndicators.Count; i++) {
                    if(PlayerIndicators[i].enabled == false) {
                        newIndicator2 = PlayerIndicators[i];
                        newIndicator2.enabled = true;
                        newIndicator2.transform.rotation = startPosBossTransform.rotation;
                        newIndicator2.startRot = newIndicator2.transform.rotation;
                        break;
                    }
                }
                if(newIndicator1 == null) {
                    newIndicator1 = Instantiate(playerIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    PlayerIndicators.Add(newIndicator1);
                    newIndicator1.transform.rotation = startPosPlayerTransform.rotation;
                    newIndicator1.startRot = newIndicator1.transform.rotation;
                }
                if(newIndicator2 == null) {
                    newIndicator2 = Instantiate(playerIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    PlayerIndicators.Add(newIndicator2);
                    newIndicator2.transform.rotation = startPosBossTransform.rotation;
                    newIndicator2.startRot = newIndicator2.transform.rotation;
                }
                //Now, it gives the boss beat indicator the information it needs to operate
                newIndicator1.SetPlayerIndicatorStart(PlayerLeftBeats[0], true);
                newIndicator2.SetPlayerIndicatorStart(PlayerRightBeats[0], false);
                //Checks to see if it needs to add a base beat
                    //Does so if the next beat that will appear doesn't exist
                /*if(BossBeats.Count <= 10) {
                    AddBaseBeat(BossBeats);
                }*/
                PlayerLeftBeats.RemoveAt(0);
                PlayerRightBeats.RemoveAt(0);
                AddBaseBeat(PlayerLeftBeats, PlayerRightBeats);
            }
            
        }
    }
    //This adds a boss beat to the boss beat indicator
    public void AddBossBeat(float beatLength, Sprite sprite, bool bossHitOccurs = true) {
        //Whenever adding a beat, it checks to see if it needs to add a base beat in between
        beatIndicatorInfo newInfo;
        newInfo.indicatorSprite = sprite;
        curBossStartBeat += beatLength;
        newInfo.beatToHit = curBossStartBeat;
        newInfo.playsAnimationAtEnd = bossHitOccurs;
        BossBeats.Add(newInfo);
    }

    //Adds a base beat to a list of beat indicators
    public void AddBaseBeat(List<beatIndicatorInfo> LeftBeats, List<beatIndicatorInfo> RightBeats) {
        curBaseBeat++;
        beatIndicatorInfo newInfo1;
        beatIndicatorInfo newInfo2;
        newInfo1.indicatorSprite = baseBeatSprite;
        newInfo1.beatToHit = curBaseBeat;
        newInfo1.playsAnimationAtEnd = false;
        newInfo2.indicatorSprite = baseBeatSprite;
        newInfo2.beatToHit = curBaseBeat;
        newInfo2.playsAnimationAtEnd = false;
        LeftBeats.Add(newInfo1);
        RightBeats.Add(newInfo2);
        //Then sorts the newInfo into the spot it should go in
        SortLatestBeat(LeftBeats);
        SortLatestBeat(RightBeats);
    }

    //Makes sure the newest beat added to the list is added to the end of the list
    private void SortLatestBeat(List<beatIndicatorInfo> beats) {
        int newElement = beats.Count - 1;
        if(newElement > 0) {
            while(beats[newElement].beatToHit < beats[newElement - 1].beatToHit) {
                //Switches positions of two beats
                beatIndicatorInfo tempInfo = beats[newElement - 1];
                beats[newElement - 1] = beats[newElement];
                beats[newElement] = tempInfo;
                newElement--;
                if(newElement == 0) {
                    break;
                }
            }
            //then, checks to see if this new Base beat just needs to be removed
                //Removes the base beat if there exists another beat in the same position
            /*if(newElement > 0 && beats[newElement].beatToHit == beats[newElement - 1].beatToHit) {
                beats.RemoveAt(newElement);
                Debug.Log("happened");
            }*/
        }
    }

    //Greys out the Player BeatIndicators for the corresponding attack length
    public void DisablePlayerIndicators(float beatTil) {
        foreach(PlayerBeatIndicator beatIndicator in PlayerIndicators) {
            if(beatIndicator.enabled) {
                if(beatIndicator.beatToHit < beatTil) {
                    beatIndicator.mySprite.color = new Color(beatIndicator.mySprite.color.r, beatIndicator.mySprite.color.g, beatIndicator.mySprite.color.b, 0);
                    beatIndicator.showOutline(false);
                } else if(beatIndicator.leftIndicator && beatIndicator.beatToHit < beatTil + 1) {
                    //show outlines on player's side
                    beatIndicator.showOutline();

                }
            }
        }
    }

    public PlayerBeatIndicator GetPlayerIndicator(float beatToGet) {
        float distFromBeat = 1000;
        PlayerBeatIndicator chosenIndicator = null;
        for(int i = 0; i < PlayerIndicators.Count; i++) {
            float newDist = Mathf.Abs(PlayerIndicators[i].beatToHit - beatToGet);
            if(newDist < distFromBeat) {
                distFromBeat = newDist;
                chosenIndicator = PlayerIndicators[i];
            }
        }
        return chosenIndicator;
    }

    public void ShowNextIndicatorOutline() {
        float curBeat = BeatController.GetBeat();
        if(Global.Player.CurrentAction == null) {
            foreach(PlayerBeatIndicator indicator in PlayerIndicators) {
                if(indicator.leftIndicator && indicator.beatToHit < curBeat + 1) {
                    indicator.showOutline();
                }
            }
        }
    }

    public void ResetBeat(float curBeat, float prevBeat = -1) {
        if(prevBeat < 0) {
            prevBeat = curBaseBeat;
        }
        foreach(BeatIndicator indicator in BossIndicators) {
            indicator.SetBeatToHit(indicator.beatToHit - prevBeat + curBeat);
        }
        curBaseBeat = curBeat;
    }

}
