using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
    [CreateAssetMenu(fileName="Pattern",menuName="SO/Pattern")]
public class Pattern : ScriptableObject
{
    public bool spin=false,clockwise=false;
    public float rotationSpeed=1.0f;
    public GameObject bulletPrefab=null;
    public int noofSpawners=1;
    // Transform[] spawnPoints;
    public float startAngle=90,spreadAngle=180;
    public float bulletSpeed=1.0f;
    public float spawnRate=1.0f;


    public void InitialiseSpawnPoint(out Transform[] spawnPoints,Transform transform)
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
    }
  
}
}
