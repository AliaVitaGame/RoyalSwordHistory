using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Vector2 _targetPosition;
    private bool _isRight;
    private Vector2 _startPosition;
    private Animator _catAnimator;
    private bool _isMoving = true;
    private int randint;
    void Start()
    {
        _startPosition = transform.position;
        _catAnimator = GetComponent<Animator>();    
    }

  
    void Update()
    {

        if (_isRight)
        {
            Move(_startPosition);
            transform.localScale = new Vector2(-7f, 7f);


        }
        else
        {
            Move(_targetPosition);
            transform.localScale = new Vector2(7f, 7f);

        }


    }




    private void Move(Vector2 target) {

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


    private IEnumerator AnimationHolder() { 
        _isMoving = false;
        AnimationRandomizer(true);
        yield return new WaitForSeconds(4);
        _isMoving = true;
        AnimationRandomizer(false);
        
       
        
    }


  

    private void AnimationRandomizer(bool active)
    {
        if (_isMoving == false)
        {
             randint = Random.Range(0, 4);
        }

        switch (randint)
            {
                case 0:
                    _catAnimator.SetBool("Laying", active);
                    break;
                case 1:
                    _catAnimator.SetBool("Licking", active);
                    break;
                case 2:
                    _catAnimator.SetBool("Itch", active);
                    break;
                case 3:
                    _catAnimator.SetBool("Idle", active);
                    break;
            }

       

    }

}
