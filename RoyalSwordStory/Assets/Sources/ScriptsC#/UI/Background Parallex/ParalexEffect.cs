using UnityEngine;

public class ParalexEffect : MonoBehaviour
{

    private Material mat;
    private float distance;


    [Range(0f,0.5f)]
    [SerializeField] private float speed = 0.2f;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        distance += (Time.deltaTime * speed) / 10f;
        mat.SetTextureOffset("_MainTex",Vector2.right * distance);
    }
}
