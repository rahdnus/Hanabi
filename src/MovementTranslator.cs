using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
    [System.Serializable]
    public class MovementTranslator
    {
        [SerializeField] Transform transform;
        [SerializeField] float moveSpeed=1;
        [Space(20)]
        [SerializeField]MovementQuery query;

        Vector3 start,destination;
        float T=0,scaler=0.01f,step=1;
        public System.Action operation;
        public System.Action Arrived;

        public void Init()
        {
            step=moveSpeed*scaler;
            operation=null;
            switch(query.type)
            {
                case MovementType.Linear:
                    CalculateLinear();
                    operation+=Linear;
                    break;
                case MovementType.Radial:
                    operation+=Radial;
                    break;
                case MovementType.Static:break;
            }
        }
        public void Clear()
        {
            operation=null;
        }
        private void CalculateLinear()
        {
            start=transform.position;
            destination=new Vector3(0,Mathf.Sin(Mathf.Deg2Rad*query.angle)*query.distance,Mathf.Cos(Mathf.Deg2Rad*query.angle)*query.distance)+start;
        }
        public void Linear()
        {
            if(Vector3.Distance(transform.position,destination)<=float.Epsilon)
            {
                Arrived();
                Clear();
            }
            transform.position=Vector3.Lerp(start,destination,T);
            T+=step;
        }
        public void Radial()
        {
            
        }

    }
    [System.Serializable]
    public class MovementQuery{
        public MovementType type;
        public float angle=-1,distance=-1;
    }
    public enum MovementType
    {
        Static, Linear, Radial
    }
}

