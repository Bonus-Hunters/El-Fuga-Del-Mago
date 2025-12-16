using UnityEngine;
using System.Collections;

public class LookAtNPC : MonoBehaviour
{
    public float rotationSpeed = 5f;
    Coroutine lookCoroutine;

    public void LookAt(Transform target)
    {
        if (lookCoroutine != null)
            StopCoroutine(lookCoroutine);

        lookCoroutine = StartCoroutine(RotateTowards(target));
    }

    IEnumerator RotateTowards(Transform target)
    {
        while (true)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f;

            if (direction.magnitude < 0.1f)
                yield break;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Stop when close enough
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
                yield break;

            yield return null;
        }
    }
}
