using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
public class Spawner : MonoBehaviour
{
    /*TEMP*/ public bool destinationreached=false;

    public bool move,spin,clockwise;
    public float rotationSpeed;

    [SerializeField] GameObject bulletPrefab;
    public int noofSpawners=1;
    [SerializeField] Transform[] spawnPoints;
    public float startAngle=90,spreadAngle;
    public float bulletSpeed=1.0f;

    public float spawnRate;
    float timer;
    [SerializeField]MovementTranslator translator=null;
    void Start()
    {
        spawnPoints=new Transform[noofSpawners];
        float currentAngle=startAngle;
        for(int i=0;i<noofSpawners;i++)
        {

            spawnPoints[i]=new GameObject("Spawn").transform;
            spawnPoints[i].SetParent(transform);
            float y=Mathf.Sin(Mathf.Deg2Rad*currentAngle);
            float x=Mathf.Cos(Mathf.Deg2Rad*currentAngle);
            spawnPoints[i].position+=new Vector3(x,y,0);
            spawnPoints[i].rotation=(Quaternion.LookRotation(spawnPoints[i].position-transform.position,-transform.forward));
            currentAngle+=spreadAngle/(noofSpawners-1);
        }
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
   if(spin)
        {
            if(clockwise)
            {
                transform.Rotate(new Vector3(0,0,rotationSpeed));
            }
            else
            {
                transform.Rotate(new Vector3(0,0,-rotationSpeed));
            }
        }
    }
    void Spawn()
    {
        timer+=Time.deltaTime;
        if(timer>1.0f/spawnRate)
        {
            timer=0f;
            foreach(Transform point in spawnPoints)
            {
                Instantiate(bulletPrefab,point.position,point.rotation).GetComponent<Bullet>().Init(bulletSpeed);
            }
        }
    }
    /*TEMP*/void OnReach()
    {
        destinationreached=true;
    }
}
}

