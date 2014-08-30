﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour
{
	
	private float timePlayed = 0;
	private float timeStartGame = 0;
	private int bullets = 50;
	private int itemShotConsecutivelyWithoutBeingHit;
	private int missedShotConsecutively = 0;
	private int accurateShotConsecutively = 0;
	private int level;
	private bool isGamePaused = false;
	private int itemAchieved = 0;
	private int remainTimeForLaser = 10;
	private float currentTime = 0;

	void Start ()
	{
		PlayGamesPlatform.Activate ();

		UpdateBulletsCount ();
		UpdateBulletIndicator ();
	}

	void Update ()
	{
		if (itemAchieved == 3) {
			if (currentTime == 0) {
				currentTime = Time.time;
			} else {
				if (Time.time - currentTime >= remainTimeForLaser / 10) {
					itemShotConsecutivelyWithoutBeingHit--;
					currentTime = Time.time;
				}
				if (itemShotConsecutivelyWithoutBeingHit <= 0) {
					itemShotConsecutivelyWithoutBeingHit = 0;
					itemAchieved = 2;
				}
			}
		}

		UpdateBulletIndicator ();
	}

	public void setTimeStartGame ()
	{
		timeStartGame = Time.time;
	}

	public void calculateActualGamePlayed ()
	{
		timePlayed = Time.time - timeStartGame;
	}

	public int getBullets ()
	{
		return bullets;
	}

	public void bulletShot ()
	{
		bullets--;

		UpdateBulletsCount ();

		if (bullets == 0) {
			calculateActualGamePlayed ();
			Social.ReportScore ((long)timePlayed, "CgkI68ebh5kcEAIQEA", (bool successs) => {});
			Social.ReportProgress ("CgkI68ebh5kcEAIQEQ", 100.0f, (bool success) => {});
		}
	}

	public void increaseBullet (string shotObject)
	{
		if (shotObject == "Pyramid") {
			bullets += 3;
		} else if (shotObject == "Diamond") {
			bullets += 5;
		} else if (shotObject == "Star") {
			bullets += 10;
		}
		UpdateBulletsCount ();
	}

	public void increaseItemShotConsecutivelyWithoutBeingHit ()
	{
		if (itemAchieved < 3) {
			itemShotConsecutivelyWithoutBeingHit++;

			if (itemShotConsecutivelyWithoutBeingHit == 10 && itemAchieved != 4) {
				itemAchieved++;
				if (itemAchieved == 1) {
					Social.ReportProgress ("CgkI68ebh5kcEAIQCw", 100.0f, (bool success) => {});
				} else if (itemAchieved == 2) {
					Social.ReportProgress ("CgkI68ebh5kcEAIQCQ", 100.0f, (bool success) => {});
				} else if (itemAchieved == 3) {
					Social.ReportProgress ("CgkI68ebh5kcEAIQCg", 100.0f, (bool success) => {});
				}

				if (itemAchieved != 3) {	
					itemShotConsecutivelyWithoutBeingHit = 0;
				}
			}
		}
	}

	public int GetItemAchieved ()
	{
		return itemAchieved;
	}

	public void resetItemAchieved ()
	{
		itemAchieved = 0;
	}

	public void resetItemShotConsecutivelyWithoutBeingHit ()
	{
		itemShotConsecutivelyWithoutBeingHit = 0;

		itemAchieved = 0;
	}

	public void increaseMissedShot ()
	{
		missedShotConsecutively++;
		accurateShotConsecutively = 0;
		if (missedShotConsecutively == 10) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQDQ", 100.0f, (bool success) => {});
		}
	}

	public void increaseAccurateShot ()
	{
		accurateShotConsecutively++;
		missedShotConsecutively = 0;
		if (accurateShotConsecutively == 10) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQCA", 100.0f, (bool success) => {});
		}
	}

	public void increaseLevel ()
	{
		level++;
		if (level == 2) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQAg", 100.0f, (bool success) => {});
		} else if (level == 2) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQAw", 100.0f, (bool success) => {});
		} else if (level == 4) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQBA", 100.0f, (bool success) => {});
		} else if (level == 5) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQBQ", 100.0f, (bool success) => {});
		}
	}

	public bool IsGamePaused ()
	{
		return isGamePaused;
	}

	public void changeGamePausedStatus ()
	{
		isGamePaused = !isGamePaused;
	}

	public void UpdateBulletIndicator ()
	{
		GameObject.FindWithTag ("BulletHitIndicator").guiTexture.texture = (Texture2D)Resources.Load ("button-projectile-process-" + itemShotConsecutivelyWithoutBeingHit);
	}

	public void UpdateBulletsCount ()
	{
		GameObject.FindWithTag ("BulletCount").guiText.text = "" + bullets;
	}
}