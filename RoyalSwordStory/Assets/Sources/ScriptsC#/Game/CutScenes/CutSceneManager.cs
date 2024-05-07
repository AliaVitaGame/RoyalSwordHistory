using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector[] _cutSceneObject;

    private PlayerAttacking _playerAttacking; 
    private PlayerMove _playerMove; 

    public static CutSceneManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _cutSceneObject.Length; i++)
            _cutSceneObject[i].gameObject.SetActive(false);
    }

    public void Play(int ID)
    {
        if (ID >= _cutSceneObject.Length)
        {
            Debug.LogWarning("There is no such cut scene");
            return;
        }

        if(_playerMove == null)
            _playerMove = FindObjectOfType<PlayerMove>();
        if(_playerAttacking == null && _playerMove) 
            _playerAttacking = _playerMove.GetComponent<PlayerAttacking>();

        if (_playerMove)
        StartCoroutine(Active(_cutSceneObject[ID]));
    }

    private IEnumerator Active(PlayableDirector playableDirector)
    {
        _playerMove.SetStopMove(true);
        _playerMove.SetHorizontal(0);
        _playerMove.SetVelosity(Vector2.zero);
        _playerAttacking.IsAttacking = true;

        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();
        yield return new WaitForSeconds((float)playableDirector.duration);
        playableDirector.Stop();
        playableDirector.gameObject.SetActive(false);

        _playerAttacking.IsAttacking = false;
        _playerMove.SetStopMove(false);
    }


}
