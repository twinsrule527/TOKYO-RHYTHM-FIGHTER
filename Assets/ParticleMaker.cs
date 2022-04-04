using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaker : MonoBehaviour
{

    [SerializeField] ParticleSystem playerParticles;
    [SerializeField] ParticleSystem bossParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.Player.CurrentAction != null)
        {
            Instantiate(playerParticles);
        }


        if (Global.Boss.CurrentMakingAttack != null)
        {
            Instantiate(playerParticles);
        }
    }
}
