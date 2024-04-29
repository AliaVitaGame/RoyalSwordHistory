using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private int countAttack = 2;
    [SerializeField] private Animator animator;

    private string _nameMove = "Move";
    //private string _nameIdle = "Idle";

    private void Start()
    {
        InitializationAnimator();
    }

    public void MoveAnimation(bool moving)
    {
        AnimatorSetBool(_nameMove, moving);
    }
   
    public void AnimatorSetBool(string name, bool value)
        => animator.SetBool(name, value);

    public void AnimatorSetTrigger(string name)
        => animator.SetTrigger(name);


    private void InitializationAnimator()
    {
        if (animator) return;

        if (gameObject.TryGetComponent(out Animator animatorParent))
        {
            animator = animatorParent;
            return;
        }

        animator = transform.GetComponentInChildren<Animator>();

        if (animator == null)
            Debug.LogWarning("√ƒ≈ ¿Õ»Ã¿“Œ– ?");
    }
}
