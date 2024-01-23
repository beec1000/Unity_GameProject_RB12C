using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem dyingParticle;
    bool isDead = false;

    public void PlayDyingParticle()
    {
        if (isDead) dyingParticle.Play();    
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DamagingObstacle"))
        {
            isDead = true;
        }
    }
}
