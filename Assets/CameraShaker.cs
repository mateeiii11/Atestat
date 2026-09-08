using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Rigidbody2D rb;
    public AnimationCurve curve;
    public float duration;
    public IEnumerator shake()
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;

            yield return null;
        }
        transform.position = startPos;

    }
}
