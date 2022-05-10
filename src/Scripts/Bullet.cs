using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanabi{
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float bulletSpeed=1;
    int damage=0;
    float counter=0f;
    float lifetime=3.0f;
    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    public void Init(float _speed,float _time,int _damage)
    {
        bulletSpeed=_speed;
        lifetime=_time;
        damage=_damage;
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
        rb.velocity=transform.forward*bulletSpeed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if((1<<other.gameObject.layer & LayerMask.GetMask("Ground"))!=0)
        { 
            Destroy(gameObject);

        }
    }
}
interface IDamagable{
    public void TakeDamage(int damage);
}
}