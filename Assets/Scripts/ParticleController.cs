using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem dyingParticle;
    bool isDead;

    public void PlayDyingParticle()
    {
        dyingParticle.Play();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DamagingObstacle"))
        {
            isDead = true;
        }
    }
}
