using System.Collections;
using UnityEngine;

public class OldMan_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector2 _targetPosition;
    private bool _isRight;
    private Vector2 _startPosition;
    private Animator _oldManAnimator;
    private bool _isMoving = true;


    
    void Start()
    {
        _startPosition = transform.position;
        _oldManAnimator = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (_isRight)
        {
            Move(_startPosition);
            transform.localScale = new Vector2(1f, 1f);


        }
        else
        {
            Move(_targetPosition);
            transform.localScale = new Vector2(-1f, 1f);

        }

    }

    private void Move(Vector2 target)
    {

        if (_isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 3 * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 1)
            {
                _isRight = !_isRight;
                StartCoroutine(AnimationHolder());

            }

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_targetPosition, 0.5f);
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_startPosition, 0.5f);
        }
    }


    private IEnumerator AnimationHolder()
    {
        _isMoving = false;
        _oldManAnimator.SetBool("IsIdle",true);
         yield return new WaitForSeconds(5.1f);
        _isMoving = true;
        _oldManAnimator.SetBool("IsIdle", false);



    }




    
}
