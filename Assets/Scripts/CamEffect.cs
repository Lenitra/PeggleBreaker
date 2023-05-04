using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEffect : MonoBehaviour
{
    public float duration = 0.1f;
    public float magnitude = 0.1f;

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        Vector3 originalPos = new Vector3(0, 0, -10);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
