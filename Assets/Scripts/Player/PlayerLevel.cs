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

    private int mMaxLevel;
    private Queue<float> mLevelExpQueue= new Queue<float>();
    public List<float> mExps = new(){ 100, 200, 300, 400, 500, 600, 700, 800, 900 };

    [SerializeField] private Animation levelUpAnim;

    public bool isActive1Unlock;
    public bool isActive2Unlock;

    private void Awake()
    {
        mMaxLevel = mExps.Count;
    }

    private void Start()
    {
        for (int i=0; i<mExps.Count; i++)
        {
            mLevelExpQueue.Enqueue(mExps[i]);
        }

        UpdateRequriedExp();
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
            UpdateRequriedExp();
		}
	}

    private void LevelUp()
    {
        mCurrentLevel++;
        // levelUpAnim.Play();
        EventManager.InvokeEvent(EventManager.Events.PlayerLevelUp);
        Time.timeScale = 0;
    }

    private void UpdateRequriedExp()
    {
        if (mLevelExpQueue.Count > 0)
        {
            mRequiredExp = mLevelExpQueue.Dequeue();
        }
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
