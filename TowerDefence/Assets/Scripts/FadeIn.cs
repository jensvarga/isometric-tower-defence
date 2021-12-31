using UnityEngine;

public class FadeIn : MonoBehaviour
{
    void Update()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

         if (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.unscaledDeltaTime / 2f;
        }
    }
}
