using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hanabi{

public class Utils 
{
    public static System.Action<Transform> getMove(MoveType moveType)
    {
        System.Action<Transform> action=null;

        switch(moveType)
        {
            case MoveType.Linear:
                action+=MoveDef.Linear;
                break;
            case MoveType.Radial:
                action+=MoveDef.Radial;
                break;
        }

        return action;
    }
}
}

