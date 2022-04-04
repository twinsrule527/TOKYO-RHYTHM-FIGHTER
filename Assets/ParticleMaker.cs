using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaker : MonoBehaviour
{

    [SerializeField] ParticleSystem playerParticles;
    [SerializeField] ParticleSystem bossParticles;
    static ParticleSystem StatPlayerParticles;
    static ParticleSystem StatBossParticles;

    // Start is called before the first frame update
    void Start()
    {
        StatPlayerParticles = playerParticles;
        StatBossParticles = bossParticles;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public static void SpawnPlayerParticles(Vector3 pos, Quaternion rot)
    {
        Instantiate(StatPlayerParticles, pos, rot);
    }


    public static void SpawnBossParticles(Vector3 pos, Quaternion rot)
    {
        Instantiate(StatBossParticles, pos, rot);
    }
}
