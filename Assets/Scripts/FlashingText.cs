using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText: MonoBehaviour {
    private Text warningText;
    private bool useThisText = false;
    private bool useThisTextText = false;
    private string flashingString;
    private float textPause = 0.5f;
    private void Start()
    {
        if (useThisText)
        {
            warningText = GetComponent<Text>();
        }
        if (useThisTextText)
        {
            flashingString = warningText.text;
        }
        warningText.text = "";
        StartCoroutine(TypeText(warningText, flashingString, textPause));
    }
    private IEnumerator TypeText(Text text, string stringToUse, float timePause)
    {
        bool show = true;
        while (true)
        {
            if (show)
            {
                warningText.text = "W A R N I N G";
            }
            else
            {
                warningText.text = "";
            }
            show = !show;
            yield return 0;
            yield return new WaitForSeconds(timePause);
        }
    }
    public void WriteText(Text newText = null, string newTextToShow = null, float newTextPause = -1.0f)
    {
        if (newText != null && newTextToShow != null && newTextPause > 0.0f)
        {
            StartCoroutine(TypeText(newText, newTextToShow, newTextPause));
            return;
        }
        if (newText != null && newTextToShow != null)
        {
            StartCoroutine(TypeText(newText, newTextToShow, textPause));
            return;
        }
        if (newText != null && newTextPause > 0.0f)
        {
            StartCoroutine(TypeText(newText, flashingString, newTextPause));
            return;
        }
        if (newTextToShow != null && newTextPause > 0.0f)
        {
            StartCoroutine(TypeText(warningText, newTextToShow, newTextPause));
            return;
        }
        if (newTextToShow != null)
        {
            StartCoroutine(TypeText(warningText, newTextToShow, textPause));
            return;
        }
        if (newTextPause > 0.0f)
        {
            StartCoroutine(TypeText(warningText, flashingString, textPause));
            return;
        }
    }
}