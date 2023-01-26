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

    public static UnityEvent OnEnemyKill = new();
    public static UnityEvent OnPlayerCollision = new();

    //unity methods
    void Update()
    {
        if (GameManager.Instance.GamePaused)
		{
			return;
		}
        transform.position += Vector3.left * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!gameObject.activeSelf)
        {
            return;
        }
        if(other.CompareTag(GameManager.BarrierTag))
        {
            PoolManager.EnemyPool.Release(this);
        }
        if(other.CompareTag(Bullet.Tag))
        {
            Die();
        }
        if(other.CompareTag(Player.Tag))
        {
            Attack();
        }
    }

    //public methods

	public override void Init()
	{
		throw new System.NotImplementedException();
	}

    public void Spawn(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

    public void Attack()
    {
        PoolManager.EnemyPool.Release(this);
        OnPlayerCollision.Invoke();
    }
    public void Die()
    {
        PoolManager.EnemyPool.Release(this);
        OnEnemyKill.Invoke();
    }

     public override void Return()
    {
        if(gameObject.activeSelf)
        {
            PoolManager.EnemyPool.Release(this);
        }
    }


}
