using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolElement
{
    //static properties
	public static string Tag = "Enemy";

	//public/inspector properties
	[SerializeField] private float speed;

    public static UnityEvent OnEnemyDies;

    //unity methods
    void Update()
    {
        transform.position += Vector3.left * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(GameManager.BarrierTag))
        {
            PoolManager.EnemyPool.Release(this);
        }
        if(other.CompareTag(Bullet.Tag))
        {
            Die();
        }
    }

    //public methods

	public override void Init()
	{
		throw new System.NotImplementedException();
	}

    public void Die()
    {
        OnEnemyDies.Invoke();
        PoolManager.EnemyPool.Release(this);
    }

}
