using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
    [System.Serializable]
    public class MovementTranslator
    {
        [SerializeField]MovementQuery query;

        public System.Action operation;

        public void InitOperation()
        {
            operation=null;
            switch(query.type)
            {
                case MovementType.Linear:
                    operation+=Linear;
                    break;
                case MovementType.Radial:
                    operation+=Radial;
                    break;
                case MovementType.Static:break;
            }
        }
        public void Linear()
        {
            
        }
        public void Radial()
        {

        }

    }
    [System.Serializable]
    public class MovementQuery{
        public MovementType type;
        public Transform transform;
        public float axis=-1,radius=-1;
    }
    public enum MovementType
    {
        Static, Linear, Radial
    }
}

