using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    float bulletSpeed=1;
    float counter=0f;
    float lifetime=3.0f;
    // Start is called before the first frame update
    void Awake()
    {
        rb=GetComponent<Rigidbody>();
    }
    public void Init(float speed)
    {
        bulletSpeed=speed;
    }
    void Update()
    {
        counter+=Time.deltaTime;
        if(counter>lifetime)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition((transform.position+transform.forward*bulletSpeed));   
    }
}
