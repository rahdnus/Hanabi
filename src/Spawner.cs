using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{


public class Spawner : MonoBehaviour
{

    [SerializeField]MovementTranslator translator;
    void Start()
    {
        translator.Init();
    }
    void Update()
    {        
        translator.operation();
    }
}
}

