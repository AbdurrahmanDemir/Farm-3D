using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(ParticleSystem))]
public class WaterParticles : MonoBehaviour
{
    public static Action<Vector3[]> onWaterCollided;

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        List<ParticleCollisionEvent> collisionEvets = new List<ParticleCollisionEvent>();
        int collisionAmount = ps.GetCollisionEvents(other, collisionEvets);

        Vector3[] collisionPositions = new Vector3[collisionAmount];

        for (int i = 0; i < collisionAmount; i++)
        {
            collisionPositions[i] = collisionEvets[i].intersection;
        }


        onWaterCollided?.Invoke(collisionPositions);

    }

}
