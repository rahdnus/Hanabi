using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
public class Spawner : MonoBehaviour
{
    [SerializeField]AudioClip[] spawnclip;
    [SerializeField] Pattern pattern;
    [SerializeField]MovementTranslator translator=null;

    Transform[] spawnPoints;
    /*TEMP*/ public bool destinationreached=false;
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
        
        // if(Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     if(pattern.mode==SpawnMode.Async)
        //         StartCoroutine(AsyncSpawn());
        //     else if(pattern.mode==SpawnMode.Sync)
        //     {
        //         SyncSpawn();

        //     }
    // }
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
    public IEnumerator AsyncSpawn()
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
                Instantiate(pattern.bulletPrefab,point.position,point.rotation).GetComponent<Bullet>().Init(pattern.bulletSpeed,pattern.bulletLifeTime);
            }
        }
    }
    public void SyncSpawn()
    {
        float speed=pattern.bulletSpeed;
         int index=Random.Range(0,spawnclip.Length);
                AudioSource.PlayClipAtPoint(spawnclip[index],transform.position);
        while(spawns<pattern.times)
        {
            spawns++;
            foreach(Transform point in spawnPoints)
            {
                Instantiate(pattern.bulletPrefab,point.position,point.rotation).GetComponent<Bullet>().Init(speed,pattern.bulletLifeTime);
            }
            speed+=pattern.speedVariance;
        }
          spawns=0; 
    }
    /*TEMP*/void OnReach()
    {
        destinationreached=true;
    }

}
}

