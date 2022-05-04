using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{    
[CreateAssetMenu(fileName="Pattern",menuName="SO/Pattern")]
[System.Serializable]
public class Pattern : ScriptableObject
{
    public SpawnMode mode;
    public float radius=1.0f;
    public bool spin=false,clockwise=false;
    public float rotationSpeed=1.0f;
    public GameObject bulletPrefab=null;
    public int noofSpawners=1;
    public float startAngle=90,spreadAngle=180;
    public float bulletSpeed=1.0f,speedVariance=0,bulletLifeTime;
    public float spawnRate=1.0f;
    public int times=1;
    public void InitialiseSpawnPoint(out Transform[] spawnPoints,Transform transform)
    {
        spawnPoints=new Transform[noofSpawners];
        float currentAngle=startAngle;
        for(int i=0;i<noofSpawners;i++)
        {

            spawnPoints[i]=new GameObject("Spawn").transform;
            spawnPoints[i].SetParent(transform);
            spawnPoints[i].localPosition=Vector3.zero;
            float y=radius*Mathf.Sin(Mathf.Deg2Rad*currentAngle);
            float x=radius*Mathf.Cos(Mathf.Deg2Rad*currentAngle);
            spawnPoints[i].localPosition=new Vector3(x,y,0);
            spawnPoints[i].localRotation=(Quaternion.LookRotation(spawnPoints[i].position-transform.position,-transform.forward));
            currentAngle+=spreadAngle/(noofSpawners-1);
        }
    }
}
public enum SpawnMode{
    Sync,Async
}
}
