﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	void Start ()
	{
		if (rigidbody != null) {
			rigidbody.useGravity = false;
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "BallProjectile" || collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "LaserProjectile" || collision.gameObject.tag == "Player" || 
			collision.gameObject.tag == "Pyramid" || collision.gameObject.tag == "Diamond" ||
			collision.gameObject.tag == "Star" || collision.gameObject.tag == "Cube") {
			Destroy (gameObject, 1);
		}
		
		if (rigidbody != null) {
			rigidbody.useGravity = true;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BallProjectile" || other.tag == "ShotgunProjectile" ||
			other.tag == "LaserProjectile" || other.tag == "Player" || 
			other.tag == "Pyramid" || other.tag == "Diamond" ||
			other.tag == "Star" || other.tag == "Cube") {
			Destroy (gameObject, 1);
		}
		
		if (rigidbody != null) {
			rigidbody.useGravity = true;
		}
	}
}