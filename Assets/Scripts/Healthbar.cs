using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
	public delegate void EnemyKilled();
	public static event EnemyKilled OnEnemyKilled;
   public int health = 5;

	// Use this for initialization
	void Start () 
	{
	}

	void HitByBullet( int damage ) // process a message HitByBullet
	{
		health -= damage;
		if (health < 1)
		{
			Destroy(gameObject);
			die();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void die()
	{
		if(OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
	}
}
