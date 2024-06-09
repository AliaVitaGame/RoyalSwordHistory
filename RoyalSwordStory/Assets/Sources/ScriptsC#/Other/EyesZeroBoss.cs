using UnityEngine;

public class EyesZeroBoss : MonoBehaviour
{
    [SerializeField] private float speed = 200;
    [SerializeField] private float deadZone = 0.07f;
    [SerializeField] private Transform[] eyes;

    private bool _startGame;
    private Transform _player;
    private Vector2[] eyesStartPosition;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMove>().transform;
        eyesStartPosition = new Vector2[eyes.Length];
        for (int i = 0; i < eyesStartPosition.Length; i++)
        {
            eyesStartPosition[i] = eyes[i].position;
        }

        _startGame = true;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < eyes.Length; i++)
        {
            eyes[i].position = Vector2.MoveTowards(eyes[i].position, _player.position, speed * Time.fixedDeltaTime);
            var X = Clamp(eyes[i].position.x, eyesStartPosition[i].x); 
            var Y = Clamp(eyes[i].position.y, eyesStartPosition[i].y);
            eyes[i].position = new Vector3(X, Y);
        }
    }

    private float Clamp(float position, float startPosition) 
        => Mathf.Clamp(position, -deadZone + startPosition, deadZone + startPosition);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (eyes == null) return;
        if (eyes.Length <= 0) return;

        for (int i = 0; i < eyes.Length; i++)
        {
            if (eyes[i])
            {
                var sphere = _startGame ? eyesStartPosition[i] : (Vector2)eyes[i].position;
                Gizmos.DrawWireSphere(sphere, deadZone);
            }
        }
    }
}
