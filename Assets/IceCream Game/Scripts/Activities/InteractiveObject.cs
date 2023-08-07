using UnityEngine;
using System.Collections;
public enum IndicationType
{
    ShakeZ,
    ShakeX,
    ShakeY,
    PositionDisplacement,
    RotationDisplacement
}

public class InteractiveObject : MonoBehaviour
{
    public IndicationType indicationType;
    public float shakeAmount = 0.1f;
    public int shakeCount = 3;
    public Vector3 positionDisplacement;
    public Vector3 rotationDisplacement;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public void setup()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Act()
    {
        switch (indicationType)
        {
            case IndicationType.ShakeZ:
                ShakeObjectOnZ();
                break;
            case IndicationType.ShakeX:
                ShakeObjectOnX();
                break;
            case IndicationType.ShakeY:
                ShakeObjectOnY();
                break;
            case IndicationType.PositionDisplacement:
                DisplaceObjectPosition();
                break;
            case IndicationType.RotationDisplacement:
                DisplaceObjectRotation();
                break;
        }

        
    }

    private void ShakeObjectOnZ()
    {
        StartCoroutine(ShakeCoroutine(Vector3.forward));
    }

    private void ShakeObjectOnX()
    {
        StartCoroutine(ShakeCoroutine(Vector3.right));
    }

    private void ShakeObjectOnY()
    {
        StartCoroutine(ShakeCoroutine(Vector3.up));
    }

    private void DisplaceObjectPosition()
    {
        StartCoroutine(DisplacePositionCoroutine());
    }

    private void DisplaceObjectRotation()
    {
        StartCoroutine(DisplaceRotationCoroutine());
    }

    private IEnumerator ShakeCoroutine(Vector3 direction)
    {
        for (int i = 0; i < shakeCount; i++)
        {
            transform.position += direction * shakeAmount;
            yield return new WaitForSeconds(0.1f);
            transform.position -= direction * shakeAmount;
            yield return new WaitForSeconds(0.1f);
        }

        // Reset the object's position to the original position after shaking
        transform.position = originalPosition;
    }

    private IEnumerator DisplacePositionCoroutine()
    {
        Vector3 targetPosition = originalPosition + positionDisplacement;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Return the object to its original position
        transform.position = originalPosition;
    }

    private IEnumerator DisplaceRotationCoroutine()
    {
        Quaternion targetRotation = originalRotation;

        if (indicationType == IndicationType.RotationDisplacement)
        {
            targetRotation *= Quaternion.Euler(rotationDisplacement);
            StartCoroutine(ShakeRotationCoroutine(targetRotation));
        }

        yield return null;
    }

    private IEnumerator ShakeRotationCoroutine(Quaternion targetRotation)
    {
        for (int i = 0; i < shakeCount; i++)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, shakeAmount * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
            transform.rotation = Quaternion.Slerp(targetRotation, originalRotation, shakeAmount * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }

        // Reset the object's rotation to the original rotation after shaking
        transform.rotation = originalRotation;
    }
}
