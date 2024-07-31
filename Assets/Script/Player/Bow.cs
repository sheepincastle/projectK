using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    [SerializeField] Sprite bow_idle;

     void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        animator.enabled = false;
    }

    void Update()
    {
        if(animator.enabled)
            Invoke("BowIdle", 0.417f);
    }

    void BowIdle()
    {
        animator.enabled = false;
        sprite.sprite = bow_idle;
    }

}
