using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{

public enum MoveType
{
    Static,Linear,Radial
}
public class Spawner : MonoBehaviour
{
    [SerializeField]MoveType moveType;
    private System.Action<Transform> move;
    void Start()
    {

        move=Utils.getMove(moveType);
    }
    void Update()
    {

        if(moveType==MoveType.Static)
            return;
        
        move(this.transform);
    }
}
}

