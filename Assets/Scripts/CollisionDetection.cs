using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection
{
    public Ray[] collisionRay = new Ray[8];
    public MeshCollider meshCollider;

    private void Start()
    {

        int[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };
        for (int i = 0; i < angles.Length; i++)
        {
            collisionRay[i].direction = Quaternion.Euler(0, 45 * i, 0) * collisionRay[i].direction; // rotate 45° around Y
            collisionRay[i].origin = meshCollider.transform.position;

        }
       
    

    }
    
}
