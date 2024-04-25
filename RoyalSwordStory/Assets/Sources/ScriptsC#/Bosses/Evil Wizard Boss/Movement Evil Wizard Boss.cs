using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementEvilWizardBoss : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField]private Transform[] _targetTransforms; 

    private Vector2 _originalPosition;
    private Vector2 _targetPosition;
    private bool _movingToTarget = false;

    private Animator _bossAnimator;

    void Start()
    {
        _originalPosition = transform.position;
        _bossAnimator = GetComponent<Animator>();
    }


    void Update()
    {

        Movement();

    }

    private void Movement()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !_movingToTarget)
        {

            RandomTargetPosition();

        }

            if (_movingToTarget)
            {

                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

                _bossAnimator.SetBool("IsMoving", true);

                if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
                {
                    transform.localScale = new Vector2(1f, 1f);

                    StartCoroutine(AttackHolding());


                }
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, _originalPosition, _moveSpeed * Time.deltaTime);
                _bossAnimator.SetBool("IsMoving", true);
            }
            if (Vector2.Distance(transform.position, _originalPosition) < 0.1f)
            {
                _bossAnimator.SetBool("IsMoving", false);
                transform.localScale = new Vector2(-1f, 1f);

            }
        if (Vector2.Distance(transform.position, _targetTransforms[1].position) < 0.1f)
        {
            _bossAnimator.SetBool("IsMoving", false);
            transform.localScale = new Vector2(-1f, 1f);

        }


    }
    

    private IEnumerator AttackHolding() {
        _bossAnimator.SetBool("IsMoving", false);
        _bossAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(1.2f);
        _movingToTarget = false;
        _bossAnimator.SetBool("IsAttacking", false);
       


    }


    private void RandomTargetPosition()
    {
       
        int randomIndex = Random.Range(0, _targetTransforms.Length);
      
        _targetPosition = _targetTransforms[randomIndex].position;
        _movingToTarget = true;
        
    }
}