using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
    [System.Serializable]
    public class MovementTranslator
    {
        [SerializeField]MovementQuery query;
        [SerializeField] Transform transform;
        [SerializeField] float moveSpeed=1;

        Vector3 destination;
        float T=0f,scaler=0.00001f,step=1;
        public System.Action operation;


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
        
        private void CalculateLinear()
        {
            destination=new Vector3(0,Mathf.Sin(Mathf.Deg2Rad* query.angle)*query.distance,Mathf.Cos(Mathf.Deg2Rad*query.angle)*query.distance)+transform.position;
        }
        public void Linear()
        {
            transform.position=Vector3.Lerp(transform.position,destination,T);
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

