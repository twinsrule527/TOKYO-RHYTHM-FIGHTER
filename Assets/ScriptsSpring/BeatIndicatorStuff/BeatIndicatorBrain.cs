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
    public static Vector3 BossIndicatorStartPos;
    public static Vector3 BossIndicatorEndPos;
    public static readonly int beatsInAdvanceShown = 3;//How much in advance beats appear on the beat indicator

    private List<BossBeatIndicator> BossIndicators;//a list of exisitng indicators - so that when they deactive, this can start using them again
    private List<beatIndicatorInfo> BossBeats;
    //Information that the BeatIndicatorBrain needs to give to a beat indicator:
        //How long (in beat-form) from now does that note actually occur
        //what type of beat it is
   
    void Start() {
        BossIndicatorStartPos = transform.position;//TODO: Change the way that the bossindicator start pos is set
        BossIndicatorEndPos = transform.position + transform.right * -5f;
        BossBeats = new List<beatIndicatorInfo>();
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
                BossBeats.RemoveAt(0);
            }
        }
    }

}
