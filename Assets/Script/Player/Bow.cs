using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    public Sprite bow_idle;
    public GameObject arrow;
    Vector3 mouse_position;
    public Transform arrow_location;
    bool was_animator_active = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        animator.enabled = false;
    }

    void Update()
    {
        // 애니메이터가 켜지면 is_animator_active를 참으로 함
        // -> is_animator_active는 참, was_animator_active는 거짓인 상태에서 화살 발사
        // -> was_animator_active를 참으로 바꿔 화살을 더 발사하지 않게 함
        // -> 애니메이터가 꺼진 후 was_animator_active를 거짓으로 하여 초기화
        if (animator != null)
        {
            bool is_animator_active = animator.enabled;
            if (is_animator_active && !was_animator_active)
            {
                mouse_position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                Invoke("Shoot", 0.5f);
            }
            was_animator_active = is_animator_active;
        }
        else
        {
            was_animator_active = false;
        }
    }

    // 활을 쐈을 때 기본 상태로 돌아감
    // 화살 발사
    void Shoot()
    {
        animator.enabled = false; // 활 애니메이션 종료
        sprite.sprite = bow_idle; // 활을 평상시 형태로

        // Define the spread angle (in degrees) and create the arrows
        float spreadAngle = 15f; // The angle between each arrow
        int numberOfArrows = 3;  // Number of arrows to shoot

        // Calculate the angles for the arrows
        for (int i = 0; i < numberOfArrows; i++)
        {
            float angleOffset = (i - 1) * spreadAngle; // -1, 0, +1 offset for 3 arrows
            Vector3 direction = (mouse_position - arrow_location.position).normalized;

            // Rotate the direction vector by the angle offset
            Vector3 rotatedDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

            // Create and shoot the arrow
            GameObject instant_arrow = Instantiate(arrow, arrow_location.position, Quaternion.LookRotation(Vector3.forward, rotatedDirection));
            Arrow arrow_script = instant_arrow.GetComponent<Arrow>();
            arrow_script.Fire(arrow_location.position + rotatedDirection); // Fire arrow in the rotated direction
        }
    }
}


