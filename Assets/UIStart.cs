using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIStart : MonoBehaviour
{
    public Animator animator;

    private bool stopped = false;

    void Start()
    {
        animator.Play("Press Start");
    }

    void Update()
    {
        if (PlayerInput.all.Count >= 2)
        {
            gameObject.SetActive(false);
        }
    }
}
