﻿using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour
{
	public float projectileSpeed;
	public float explosionForce;
	public float explosionRadius;
	
	void Start ()
	{
		rigidbody.velocity = transform.forward * projectileSpeed;
	}

	void OnCollisionEnter (Collision collision)
	{
		// if the bullet hit something that has a rigidbody
		Destroy (gameObject);
		
		Collider[] colliders = Physics.OverlapSphere (collision.contacts [0].point, explosionRadius);
		
		foreach (Collider c in colliders) {
			if (c.rigidbody != null) {
				c.rigidbody.AddExplosionForce (explosionForce, collision.contacts [0].point, 3, 0, ForceMode.Impulse);
			}
		}
	}
}
