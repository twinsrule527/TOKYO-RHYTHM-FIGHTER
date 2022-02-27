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
}

public class BeatIndicatorBrain : MonoBehaviour
{
    [SerializeField] private BossBeatIndicator bossIndicatorPrefab;//Uses this prefab to instantiate more when needed
    [SerializeField] private Transform startPosTransform;//A transform indicator for the start position of the boss indicator
    [SerializeField] private Transform endPosTransform;//Similarly to above, but for end pos
    public static Vector3 BossIndicatorStartPos;
    public static Vector3 BossIndicatorEndPos;
    public static readonly float beatsInAdvanceShown = 3.5f;//How much in advance beats appear on the beat indicator
    private float curBossStartBeat;//Keeps track of what beat each boss attack should start on
    private float curBaseBeat;
    [SerializeField] private Sprite baseBeatSprite;
    private List<BossBeatIndicator> BossIndicators;//a list of exisitng indicators - so that when they deactive, this can start using them again
    private List<beatIndicatorInfo> BossBeats;
    //Test lists
    [SerializeField] private List<float> TestBeats;
    [SerializeField] private List<Sprite> TestSprites;
    //Information that the BeatIndicatorBrain needs to give to a beat indicator:
        //How long (in beat-form) from now does that note actually occur
        //what type of beat it is
   
    void Start() {
        BossIndicatorStartPos = startPosTransform.position;//TODO: Change the way that the bossindicator start pos is set
        BossIndicatorEndPos = endPosTransform.position;
        BossBeats = new List<beatIndicatorInfo>();
        BossIndicators = new List<BossBeatIndicator>();
        for(int i = 0; i < TestBeats.Count; i++) {
            AddBossBeat(TestBeats[i], TestSprites[i]);
            AddBaseBeat(BossBeats);
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
                        break;
                    }
                }
                if(newIndicator == null) {
                    newIndicator = Instantiate(bossIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    BossIndicators.Add(newIndicator);
                }
                //Now, it gives the boss beat indicator the information it needs to operate
                newIndicator.SetIndicatorStart(BossBeats[0]);
                //Checks to see if it needs to add a base beat
                    //Does so if the next beat that will appear doesn't exist
                if(BossBeats[BossBeats.Count - 1].beatToHit <= curBeat + beatsInAdvanceShown + 1) {
                    AddBaseBeat(BossBeats);
                }
                BossBeats.RemoveAt(0);
            }
        }
    }
    //This adds a boss beat to the boss beat indicator
    public void AddBossBeat(float beatLength, Sprite sprite) {
        //Whenever adding a beat, it checks to see if it needs to add a base beat in between
        beatIndicatorInfo newInfo;
        newInfo.indicatorSprite = sprite;
        curBossStartBeat += beatLength;
        newInfo.beatToHit = curBossStartBeat;
        BossBeats.Add(newInfo);
    }

    //Adds a base beat to a list of beat indicators
    public void AddBaseBeat(List<beatIndicatorInfo> beats) {
        curBaseBeat++;
        beatIndicatorInfo newInfo;
        newInfo.indicatorSprite = baseBeatSprite;
        newInfo.beatToHit = curBaseBeat;
        beats.Add(newInfo);
        //Then sorts the newInfo into the spot it should go in
        SortLatestBeat(beats);
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
            if(newElement > 0 && beats[newElement].beatToHit == beats[newElement - 1].beatToHit) {
                beats.RemoveAt(newElement);
            }
        }
    }

}
