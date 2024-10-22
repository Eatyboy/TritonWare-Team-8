using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int mCurrentLevel;
    [SerializeField] private float mCurrentExp;
    [SerializeField] private float mRequiredExp;
    public int CurrentLevel => mCurrentLevel;
    public float CurrentExp => mCurrentExp;
    public float RequiredExp => mRequiredExp;

    [SerializeField] private int mMaxLevel;
    [SerializeField] private float mBaseRequiredExp;
    [SerializeField] private float mLevelExpScalar;

    [SerializeField] private Animation levelUpAnim;

    private bool isActive1Unlock;
    private bool isActive2Unlock;

    private void Start()
    {
        mRequiredExp = mBaseRequiredExp;
        EventManager.RegisterToEvent(EventManager.Events.PlayerLevelUp, OnLevelUp); // For test, whoever want to be notified when level up can register this Event
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // For test
            AddExp(30f);
		}
        HandleLevelUp();
        HandleActiveAbility();
    }

    public float GetRequiredExp(int level)
    {
        if (level < 0) return 0; // Useful for XP bar
        return 10 * level * level + 110 * level + mBaseRequiredExp;
    }

    public void AddExp(float anAmount)
    {
        if (mCurrentLevel < mMaxLevel)
        { 
            mCurrentExp += anAmount;
		}
	}

    private void HandleLevelUp()
    { 
	    if (mCurrentExp >= mRequiredExp && mCurrentLevel < mMaxLevel)
        {
            LevelUp();
            mRequiredExp = GetRequiredExp(mCurrentLevel);
		}
	}

    private void LevelUp()
    {
        mCurrentLevel++;
        // levelUpAnim.Play();
        EventManager.InvokeEvent(EventManager.Events.PlayerLevelUp);
        Time.timeScale = 0;
    }

    private void HandleActiveAbility()
    { 
        if (mCurrentLevel == 2 && !isActive1Unlock)
        {
            isActive1Unlock = true;
            Debug.Log("Unlock Activity 1");
            GetComponent<Player>().active1 = GetComponent<ReflectingShieldAbility>();
		} 
        if (mCurrentLevel == 5 && !isActive2Unlock)
        { 
            isActive2Unlock = true;
            Debug.Log("Unlock Activity 2");
            GetComponent<Player>().active2 = GetComponent<TeleportStrike>();
		}
	}

    // For test, other object can have OnLevelUp()
    private void OnLevelUp()
    {
        Debug.Log("LevelUp! (" + mCurrentLevel + "/" + mMaxLevel + ")");
	}
}
