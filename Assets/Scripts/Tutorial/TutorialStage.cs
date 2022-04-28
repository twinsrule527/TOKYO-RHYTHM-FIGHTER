using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//A parent class that other tutorials inherit from - functions similarly to a Finite State Machine, in that only 1 stage is active at a time
public class TutorialStage : MonoBehaviour
{
    //For the time being, you can go to the next stage of the tutorial using a button
    [SerializeField] protected GameObject _nextStageButton;
    public GameObject NextStageButton {
        get {
            return _nextStageButton;
        }
    }

    //Has a list of objects which are deactivated for the length of the stage
        //This allows us to only have the player action we want to be active be active
    [SerializeField] protected List<GameObject> _deactivatedObjects;
    public List<GameObject> DeactivatedObjects {
        get {
            return _deactivatedObjects;
        }
    }

    //Same as above, but for objects which are only active when this stage is active
    [SerializeField] protected List<GameObject> _activatedObjects;
    public List<GameObject> ActivatedObjects {
        get {
            return _activatedObjects;
        }
    }

    public virtual void StageConditionsMet() {
        _nextStageButton.SetActive(true);
    }
}
