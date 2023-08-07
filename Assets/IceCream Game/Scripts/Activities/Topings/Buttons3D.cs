
using UnityEngine;
using System.Collections;

public class Buttons3D : MonoBehaviour
{
    
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private float scaleLimit = 0.5f; // The scale limit to which the object should be scaled down
    [SerializeField] private float duration = 0.5f; // The duration of the scaling animation

    private Vector3 originalScale; // The original scale of the object
    private bool isScalingDown = false; // Flag to check if the object is currently scaling down

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void onTap(int index)
    {
        gameEvent.Raise(this,index);
        print("yo");


        StartCoroutine(ScaleDownCoroutine());
    }


      private IEnumerator ScaleDownCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Scale down the object
                float t = Mathf.Clamp01(elapsedTime / duration);
                float scale = Mathf.Lerp(originalScale.x, originalScale.x * scaleLimit, t);
                transform.localScale = new Vector3(scale, scale, scale);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Scale back up to the original size
            isScalingDown = false;
            StartCoroutine(ScaleUpCoroutine());
        }

        private IEnumerator ScaleUpCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Scale up the object
                float t = Mathf.Clamp01(elapsedTime / duration);
                float scale = Mathf.Lerp(transform.localScale.x, originalScale.x, t);
                transform.localScale = new Vector3(scale, scale, scale);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the scale to the original size
            transform.localScale = originalScale;
        }
 
}
