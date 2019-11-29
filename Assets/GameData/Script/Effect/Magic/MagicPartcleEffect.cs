using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPartcleEffect : MonoBehaviour
{
    
    ParticleSystem boomEffect;
    List<ParticleCollisionEvent> collisionEvents;
    ParticleSystem.Particle[] particle;
    private void Awake()
    {
        boomEffect = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(boomEffect, other, collisionEvents);
        if (other.CompareTag("Ground"))
        {
            boomEffect.GetParticles(particle);

            for (int i=0; i<particle.Length; i++)
            {
                particle[i].remainingLifetime = 0;
            }
            
        }
    }
}
