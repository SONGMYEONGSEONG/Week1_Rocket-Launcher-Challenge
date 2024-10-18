using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnergyDashboardC : MonoBehaviour
{
    [SerializeField] private EnergySystemC energySystem;
    [SerializeField] private RectTransform fillBar;

    private void Start()
    {
        // 에너지시스템의 에너지 사용에 대해 fillBar가 변경되도록 수정
        energySystem.OnEnergyChanged += EnergyDashboardUpdate;
       
    }

    private void EnergyDashboardUpdate(float _fuel)
    {
        fillBar.localScale = new Vector3 (_fuel / energySystem.MaxFuel ,1.0f,1.0f);
    }
}