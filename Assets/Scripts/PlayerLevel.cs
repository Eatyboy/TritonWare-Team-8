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
    private float[] mExps = { 100, 200, 400, 800, 1000 };

    private void Awake()
    {
        mMaxLevel = mExps.Length;
    }

    private void Start()
    {
        for (int i=0; i<mExps.Length; i++)
        {
            mLevelExpQueue.Enqueue(mExps[i]);
        }

        UpdateRequriedExp();
        EventManager.RegisterToPlayerLevelUp(OnLevelUp); // For test, whoever want to be notified when level up can register this Event
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // For test
            AddExp(30f);
		}
        HandleLevelUp();
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
        EventManager.InvokeEvent(EventManager.Events.PlayerLevelUp);
    }

    private void UpdateRequriedExp()
    {
        if (mLevelExpQueue.Count > 0)
        {
            mRequiredExp = mLevelExpQueue.Dequeue();
        }
    }

    // For test, other object can have OnLevelUp()
    private void OnLevelUp()
    {
        Debug.Log("LevelUp! (" + mCurrentLevel + ")");
	}
}
