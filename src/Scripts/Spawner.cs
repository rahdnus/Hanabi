using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
public class Spawner : MonoBehaviour
{
    [SerializeField] Pattern pattern;
    [SerializeField]MovementTranslator translator=null;
    /*TEMP*/ 
    Transform[] spawnPoints;
    public bool destinationreached=false;
    public bool move;
    float timer;
    void Start()
    {
        pattern.InitialiseSpawnPoint(out spawnPoints,transform);
        
        if(!move)
            return;

        translator.Init();
        translator.Arrived+=OnReach;
    }
    void Update()
    {        
        if(destinationreached)
            return;
        if(move)
            translator.Operation();

        Spin();
        Spawn();

    }
    void Spin()
    {
    if(pattern.spin)
        {
            if(pattern.clockwise)
            {
                transform.Rotate(new Vector3(0,0,pattern.rotationSpeed));
            }
            else
            {
                transform.Rotate(new Vector3(0,0,-pattern.rotationSpeed));
            }
        }
    }
    void Spawn()
    {
        timer+=Time.deltaTime;
        if(timer>1.0f/pattern.spawnRate)
        {
            timer=0f;
            foreach(Transform point in spawnPoints)
            {
                Instantiate(pattern.bulletPrefab,point.position,point.rotation).GetComponent<Bullet>().Init(pattern.bulletSpeed);
            }
        }
    }
    /*TEMP*/void OnReach()
    {
        destinationreached=true;
    }
}
}

