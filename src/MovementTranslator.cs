using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
    [System.Serializable]public class MovementTranslator{

        public System.Action Operation,Arrived;
        [SerializeField] Transform transform;
        [Space(20)]
        [SerializeField]MovementQuery query;
        Vector3 start,destination;
        float T=-1,scaler=0.01f,step=1;
        int operationCount;

    public void Init()
    {
        step=query.speed*scaler;
        Operation=null;
        switch(query.type)
        {
            case MovementType.Linear:
                InitLinear();
                Operation+=Linear;
                break;
            case MovementType.Radial:
                InitRadial();
                Operation+=Radial;
                break;
            case MovementType.Static:break;
        }
    }
    public void Clear()
    {
        T=-1;
        Operation=null;
        operationCount=0;
    }
    private void InitLinear()
    {
        start=transform.position;
        
        float y=Mathf.Sin(Mathf.Deg2Rad*query.angle)*query.distance;
        float z=Mathf.Cos(Mathf.Deg2Rad*query.angle)*query.distance;

        destination=new Vector3(0,y,z)+start;
        operationCount=0;
    }
    public void Linear()
    {  
        CheckLinear();
    
        transform.position=Vector3.Lerp(start,destination,T);
        T+=step;
    }
    private void InitRadial()
    {
        start=transform.position;
        T=query.angle;
        float y=Mathf.Sin(Mathf.Deg2Rad*T)*query.distance;
        float z=Mathf.Cos(Mathf.Deg2Rad*T)*query.distance;
        
        destination=new Vector3(0,y,z)+start;
        Debug.Log(destination);
        operationCount=0;

        Debug.Log(step);
    }
    public void Radial()
    {
        T=(T+step)%360;

        float y=Mathf.Sin(Mathf.Deg2Rad*T)*query.distance;
        float z=Mathf.Cos(Mathf.Deg2Rad*T)*query.distance;
        
        transform.position=new Vector3(0,y,z)+start;

        CheckRadial();
    }
    void CheckRadial()
    {

        if(T-query.angle<step)
        {
            T=query.angle;
            transform.position=destination;
            
            operationCount+=1;

            if(operationCount==query.times)
            {
                Arrived();
                Clear();
            }
           
        }
    }
    void CheckLinear()
    {
        if (Vector3.Distance(transform.position, destination) <= float.Epsilon ||   operationCount==query.times)
        {
            Arrived();
            Clear();
        }
    }
}
    [System.Serializable]
    public class MovementQuery{
        public MovementType type;
        public float angle=-1,distance=-1,speed=1;
        public int times=1;
    }
    public enum MovementType
    {
        Static, Linear, Radial
    }
}

