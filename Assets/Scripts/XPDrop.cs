using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    public int xpValue = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLevel>().AddExp(xpValue);
            Destroy(gameObject);
        }
    }
}

// Player Script (XP and Leveling Up)
//public class Player : MonoBehaviour
//{
//private int currentXP = 0;
//private int xpForNextLevel = 100;
//private int playerLevel = 1;

//public void AddXP(int xp)
//{
//    currentXP += xp;
//    if (currentXP >= xpForNextLevel)
//    {
//        LevelUp();
//    }
//}

//private void LevelUp()
//{
//    playerLevel++;
//    currentXP = 0; // Reset XP
//    xpForNextLevel += 50; // Increase XP requirement for the next level
//    UpgradeStats(); // Increase player stats like health, speed, damage, etc.
//}

//private void UpgradeStats()
//{
//    // Example stat upgrades
//    moveSpeed += 1f;
//    // Upgrade health, damage, etc.
//}
//}
