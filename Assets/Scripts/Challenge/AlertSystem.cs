using UnityEngine;
using UnityEngine.UIElements;

public class AlertSystem : MonoBehaviour
{
    // fov가 45라면 45도 각도안에 있는 aesteriod를 인식할 수 있음.
    [SerializeField] private float fov = 45f;
    // radius가 10이라면 반지름 10 범위에서 aesteriod들을 인식할 수 있음.
    [SerializeField] private float radius = 10f;
    private float alertThreshold;

    [SerializeField] private LayerMask TargetLayer;

    private Animator animator;
    private static readonly int blinking = Animator.StringToHash("isBlinking");

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // FOV를 라디안으로 변환하고 코사인 값을 계산
        fov *= 0.5f;
        fov *= Mathf.Deg2Rad;
    }

    private void Update()
    {
        CheckAlert();
    }

    private void CheckAlert()
    {
        // 주변 반경의 소행성들을 확인하고 이를 감지하여 Alert를 발생시킴(isBlinking -> true)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, TargetLayer);

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                Vector2 dir = (collider.transform.position - transform.position).normalized;

                if (Vector2.Dot(transform.up, dir) > Mathf.Cos(fov))
                {
                    animator.SetBool(blinking, true);
                }
                else
                {
                    animator.SetBool(blinking, false);
                }
            }
        }
        else
        {
            animator.SetBool(blinking, false);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, 10);
    }


}