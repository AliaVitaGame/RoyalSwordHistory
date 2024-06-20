using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveParallexEffect : MonoBehaviour
{
    private Material mat;
    private float distance;

    [Range(0f, 0.5f)]
    [SerializeField] private float speed = 0.2f;

   

    void Start()
    {

        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        if (x > 0.2f || x < -0.2f)
        {
            distance += (Time.deltaTime * speed) / 10f;
            mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        }
    }
}
