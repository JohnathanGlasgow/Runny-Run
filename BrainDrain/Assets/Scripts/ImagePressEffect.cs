using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImagePressEffect : MonoBehaviour
{
    public Image image;
    public float pressedScaleFactor = 1.2f;
    public float pressedDuration = 0.1f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = image.transform.localScale;
    }

    public void OnButtonPress()
    {
        // Pressed effect: Scale the image up and reset it immediately
        StartCoroutine(PressedImage());
        // Add any additional functionality here when the button is pressed.
    }

    private IEnumerator PressedImage()
    {
        image.transform.localScale = originalScale * pressedScaleFactor;
        yield return new WaitForSeconds(pressedDuration);
        image.transform.localScale = originalScale;
    }
}
