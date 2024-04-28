using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private int countAttack = 2;
    [SerializeField] private Animator animator;

    private string _nameMove = "Move";
    private string _nameJump = "Jump";
    private string _nameAttack = "Attack";
    private string _nameFall = "Fall";
    private string _nameHit = "Hit";


    private int _lastAnimationAttack;

    private void Start()
    {
        InitializationAnimator();
    }

    public void MoveAnimation(bool moving)
    {
        AnimatorSetBool(_nameMove, moving);
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

    public void RandomSwingAnimation()
    {
        var randomValue = Random.Range(1, countAttack + 1);
        AnimatorSetBool($"{_nameAttack}{randomValue}", true);
        _lastAnimationAttack = randomValue;
    }

    public void AnimationAttack()
    {
        AnimatorSetBool($"{_nameAttack}{_lastAnimationAttack}", true);
    }

    public void EndetAttack()
    {
        for (int i = 1; i <= countAttack; i++)
            AnimatorSetBool($"{_nameAttack}{i}", false);
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
