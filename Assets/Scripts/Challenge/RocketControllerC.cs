using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;

    private bool _isMoving;
    private float _movementDirection;

    private Coroutine _moveCoroutine;
    private InputActionMap _inputActionMap;

    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
        _inputActionMap = GetComponent<PlayerInput>().actions.FindActionMap("Rocket");
    }

    private void Start()
    {
        _inputActionMap["Move"].performed += OnMovePerfomed;
        _inputActionMap["Move"].canceled += OnMoveCancled;

        _inputActionMap["Boost"].performed += OnBoostPerfromed;

        _movementDirection = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;

        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    // private void OnMove...
    private void OnMovePerfomed(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();
        Debug.Log("Move 키 입력 : " + dir);
        _movementDirection += dir;
        if (_moveCoroutine == null)
        {
            _moveCoroutine = StartCoroutine(Moving(dir));
        }

    }

    private void OnMoveCancled(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();
        Debug.Log("Move 키 입력 : " + dir);
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    IEnumerator Moving(float dir)
    {
        while (true)
        {
            _movementDirection += dir;
            yield return new WaitForFixedUpdate();
        }
    }

    // OnBoost 구현
    // private void OnBoost...

    private void OnBoostPerfromed(InputAction.CallbackContext context)
    {
        if (_energySystem.UseEnergy(ENERGY_BURST))
        {
            _rocketMovement.ApplyBoost();
        }
    }


}