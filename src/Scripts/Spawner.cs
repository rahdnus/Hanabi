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
    int spawns=0;
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

        if(pattern.spin)
            Spin();
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            spawns=0;
            if(pattern.mode==SpawnMode.Async)
                StartCoroutine(AsyncSpawn());
            else if(pattern.mode==SpawnMode.Sync)
                SyncSpawn();
        }
    }
    void Spin()
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
    IEnumerator AsyncSpawn()
    {
        while(true)
        {
        yield return new WaitForSeconds(1.0f/pattern.spawnRate);
            if(pattern.times!=-1)
            {
                if(spawns>=pattern.times)
                {
                    yield break;

                }
                spawns++;
            }
            foreach(Transform point in spawnPoints)
            {
                Instantiate(pattern.bulletPrefab,point.position,point.rotation).GetComponent<Bullet>().Init(pattern.bulletSpeed);
            }
        }
    }
    void SyncSpawn()
    {
            float speed=pattern.bulletSpeed;

        while(spawns<pattern.times)
        {
            spawns++;
            foreach(Transform point in spawnPoints)
            {
                Instantiate(pattern.bulletPrefab,point.position,point.rotation).GetComponent<Bullet>().Init(speed);
            }
            speed-=pattern.speedVariance;
        }
           
    }
    /*TEMP*/void OnReach()
    {
        destinationreached=true;
    }
}
}

