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

    private const int mMaxLevel = 15;

    public UnityEvent mLevelUpEvent;
    private Queue<float> mLevelExpQueue= new Queue<float>();

    private void Awake()
    {
        // TODO: Make a level dataset
        for (int i=1; i<=mMaxLevel; i++)
        {
            mLevelExpQueue.Enqueue(i * 2);
        }

        if (mLevelUpEvent == null)
        {
            mLevelUpEvent = new UnityEvent();
        }

        UpdateRequriedExp();
        mLevelUpEvent.AddListener(OnLevelUp); // For test, whoever want to be notified when level up can register this Event
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // For test
            AddExp(1f);
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
        mLevelUpEvent.Invoke();
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
