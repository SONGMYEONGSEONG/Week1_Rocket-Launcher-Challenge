using System;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    //private readonly float ROTATIONSPEED = 0.02f;
    private readonly float ROTATIONSPEED = 0.2f;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void ApplyMovement(float inputX)
    {
        Rotate(inputX);
    }

    public void ApplyBoost(/*Vector2 dir*/)
    {
        //추진력이기에 힘으로 받아서 전진하는게 맞음 
        _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
        //_rb2d.velocity = transform.up * SPEED;
    }

    private void Rotate(float inputX)
    {
        //a,d 방향값을 가져옴 여기서 해결해야됨

        float angle = inputX;

        Quaternion Rot = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.Slerp(transform.rotation, Rot, ROTATIONSPEED);
       
        //Vector2 dir = new Vector2(inputX,0);
        //_rb2d.AddForce(dir * ROTATIONSPEED, ForceMode2D.Impulse);

        // 움직임에 따라 회전을 바꿈 -> 회전을 바꾸고 그 방향으로 발사를 해야 그쪽으로 가겠죠?
        //float angle = (inputX + 90) * (Mathf.PI / 180.0f);
        //float x = Mathf.Cos(angle);
        //float y = Mathf.Sin(angle);

        //Vector2 dir = new Vector2(x, y).normalized;

        //transform.position = Vector3.Slerp(transform.position, dir,Time.fixedDeltaTime);

    }
}