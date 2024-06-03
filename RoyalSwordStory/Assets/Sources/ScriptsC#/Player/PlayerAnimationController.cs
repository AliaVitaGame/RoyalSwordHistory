using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Range(1,3)]
    [SerializeField] private int countAttack = 2;
    [SerializeField] private Animator animator;

    private string _nameMove = "Move";
    private string _nameJump = "Jump";
    private string _nameAttack = "Attack";
    private string _nameFall = "Fall";
    private string _nameHit = "Hit";
    private string _nameDead = "Dead";
    private string _nameDeadTigger = "DeadTigger";

    private void Start()
    {
        InitializationAnimator();
    }

    public void MoveAnimation(float X)
    {
        AnimatorSetBool(_nameMove, X != 0);
    }

    public void JumpAnimation(bool isGround)
    {
        AnimatorSetBool(_nameJump, isGround);
    }

    public void FallAnimation(bool isFall)
    {
        AnimatorSetBool(_nameFall, isFall);
    }
    public void HitAnimation(bool isStun)
    {
        AnimatorSetBool(_nameHit, isStun);
    }

    public void RandomAnimationAttack()
    {
        var randomValue = Random.Range(1, countAttack + 1);
        AnimatorSetBool($"{_nameAttack}{randomValue}", true);
    }

    public void EndetAttack()
    {
        for (int i = 1; i <= countAttack; i++)
            AnimatorSetBool($"{_nameAttack}{i}", false);
    }

    public void Dead(bool isDead)
    {
        AnimatorSetBool(_nameDead, isDead);

        if(isDead) animator.SetTrigger(_nameDeadTigger);

        EndetAttack();
        HitAnimation(false);
        FallAnimation(false);
        JumpAnimation(false);
    }

    public void AnimatorSetBool(string name, bool value)
        => animator.SetBool(name, value);


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

    public Animator GetAnimator()
    {
        InitializationAnimator();
        return animator;
    }
}
