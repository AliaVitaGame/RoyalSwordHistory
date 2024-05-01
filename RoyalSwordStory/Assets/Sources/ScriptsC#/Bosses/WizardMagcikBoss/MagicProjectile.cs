using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 5f; 
    public GameObject impactEffect; // ������ ��� ���������

    private Transform target; 

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //  � ������� ����
        Vector2 direction = (target.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ��������� ������ ��������� � ������
            Destroy(gameObject);
        }
        else
        {
            // ������� ������ ���������
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);

            Destroy(gameObject);
        }
    }
}
