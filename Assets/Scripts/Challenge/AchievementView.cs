using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;  // 업적 슬롯 프리팹
    [SerializeField] private GameObject contentObject;

    private Dictionary<int, AchievementSlot> achievementSlots = new();

    public void CreateAchievementSlots(AchievementSO[] achievements)
    {
        // achievement 데이터에 따라 슬롯을 생성함
        int threshold = 0;
        foreach (AchievementSO achievement in achievements)
        {
            GameObject slot = Instantiate(achievementSlotPrefab,contentObject.transform);
            slot.AddComponent<AchievementSlot>();

            slot.GetComponent<AchievementSlot>().Init(achievement);
            achievementSlots.Add(threshold++, slot.GetComponent<AchievementSlot>());
        }

    }

    public void UnlockAchievement(int threshold)
    {
        // UI 반영 로직
        achievementSlots[threshold].MarkAsChecked();

    }
}