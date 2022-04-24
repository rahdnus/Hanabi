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
        float T=0,scaler=0.01f,step=1;
        int operationCount,direction=1;

    public void Init()
    {
        step=query.speed*scaler;
        Operation=null;

        operationCount=0;

        switch(query.type)
        {
            case MovementType.Linear:
                direction=1;
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
        Operation=null;
        operationCount=0;
        T=-1;
    }
    private void InitLinear()
    {
        start=transform.position;

        float y=Mathf.Sin(Mathf.Deg2Rad*query.angle)*query.distance;
        float z=Mathf.Cos(Mathf.Deg2Rad*query.angle)*query.distance;

        destination=new Vector3(0,y,z)+start;
    }
    public void Linear()
    {  
        transform.position=Vector3.Lerp(start,destination,T);
        T+=step*direction;

        CheckLinear();
    }
      void CheckLinear()
    {
        if ((T>=1 && direction==1) ||(T<=0 && direction==-1))
        {
            operationCount+=1;
            direction*=-1;
            
            if(operationCount==query.times)
            {
                Arrived();
                Clear();
            }
        }
    }
    private void InitRadial()
    {
        start=transform.position;
        T=query.angle;
        direction=query.anticlockwise;

        operationCount=0;

        float y=Mathf.Sin(Mathf.Deg2Rad*T)*query.distance;
        float z=Mathf.Cos(Mathf.Deg2Rad*T)*query.distance;
        
        destination=new Vector3(0,y,z)+start;
        Debug.Log(destination);

        Debug.Log(step);
    }
    public void Radial()
    {
        T=(T+step*query.anticlockwise)%360;

        float y=Mathf.Sin(Mathf.Deg2Rad*T)*query.distance;
        float z=Mathf.Cos(Mathf.Deg2Rad*T)*query.distance;
        
        transform.position=new Vector3(0,y,z)+start;

        CheckRadial();
    }
    void CheckRadial()
    {

        if(Mathf.Abs(T-query.angle)<step)
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
  
}
    [System.Serializable]
    public class MovementQuery{
        public MovementType type;
        public float angle=-1,distance=-1,speed=1;
        public int times=1,anticlockwise=1;
    }
    public enum MovementType
    {
        Static, Linear, Radial
    }
}

