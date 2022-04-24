using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
public class Spawner : MonoBehaviour
{
    /*TEMP*/ public bool destinationreached=false;
    [SerializeField]MovementTranslator translator;
    void Start()
    {
        translator.Init();
        translator.Arrived+=OnReach;
    }
    void Update()
    {        
        if(destinationreached)
            return;
        translator.operation();
    }
    /*TEMP*/void OnReach()
    {
        destinationreached=true;
    }
}
}

