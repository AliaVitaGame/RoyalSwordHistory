using System.Collections;
using UnityEngine;

public class RandomTimeActivatorObject : MonoBehaviour
{
    [SerializeField] private float minRandomTime = -1;
    [SerializeField] private float maxRandomTime = 1;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private ParticleSystem[] particles;
    private void Start()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].SetActive(false);

        for (int i = 0; i < particles.Length; i++)
            particles[i].Stop();

        StartCoroutine(Activator());
    }

    private IEnumerator Activator()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
            yield return new WaitForSeconds(GetRandomValue());
        }

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
            yield return new WaitForSeconds(GetRandomValue());
        }
    }

    private float GetRandomValue() => Random.Range(minRandomTime, maxRandomTime);

}
