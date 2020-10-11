using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeOut : MonoBehaviour {
    
    public static IEnumerator FadeImage(Image img, bool fadeAway=true)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (img)
                {
                    // set color with i as alpha
                    img.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if (img)
                {
                    // set color with i as alpha
                    img.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
    }
    
    public static IEnumerator FadeText(TextMeshProUGUI text, bool fadeAway=true)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                if (text)
                {
                    // set color with i as alpha
                    text.color = new Color(1, 1, 1, i);
                    yield return null;    
                }
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                if (text)
                {
                    // set color with i as alpha
                    text.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
    }
}