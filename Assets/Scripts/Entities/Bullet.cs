using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolElement
{
    //static properties
	public static string Tag = "Bullet";

	//public/inspector properties
	[SerializeField] private float speed;

    //unity methods
    void Update()
    {
        transform.position += Vector3.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(GameManager.BarrierTag))
        {
            PoolManager.BulletPool.Release(this);
        }
    }

    //public metods

	public override void Init()
	{
		throw new System.NotImplementedException();
	}

    public void Shot(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

}
