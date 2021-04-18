using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberObject : MonoBehaviour
{
    // Properties
    #region Properties

    public int NumValue { get; private set; }

    #endregion

    // Variables
    #region Variables

    [Header("UI")]
    [SerializeField] private Color32 correctColor;
    [SerializeField] private Color32 incorrectColor;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private Button button;

    [Header("Variables")]
    [SerializeField] private float animTime = 2f;
    [SerializeField] private float waitTime = 1f;

    private CanvasGroup canvas;
    private Action callback = null;

    #endregion

    // Public 
    #region Public

    public void Init(int val, Action call)
    {
        canvas = GetComponent<CanvasGroup>();

        canvas.alpha = 0;
        button.interactable = false;
        NumValue = val;
        numText.text = val.ToString("F0");

        // Do Fade Animation
        Fade(true);

        // Add a callback to call it when button clicked
        callback = call;
        button.onClick.AddListener(() => { call?.Invoke(); });
    }

    public void CheckResult(bool correct)
    {
        UpdateColor(correct);

        // Wait X seconds and call OnCheckResultEnd Event
        StartCoroutine(CheckResultCoroutine(correct));
    }

    public void UpdateColor(bool correct)
    {
        numText.color = correct
            ? correctColor
            : incorrectColor;
    }

    public void SetInteraction(bool interactable)
    {
        button.interactable = interactable;
    }

    public void Fade(bool isIn, Action callback = null)
    {
        // Disable interaction
        SetInteraction(false);

       StartCoroutine(AnimHelper.Instance.AnimCoroutine(animTime, (alpha) =>
        {
            canvas.alpha = isIn ? alpha : 1 - alpha;
        }, () =>
        {
            callback?.Invoke();

            // On finish...
            if (isIn)  // enable interaction (if Fade In)    
                button.interactable = true;
            else        // enable interaction (if  Fade Out)
                Destroy(gameObject);
        }));
    }

    #endregion

    // Coroutines
    #region Coroutines

    private IEnumerator CheckResultCoroutine(bool correct)
    {
        yield return new WaitForSeconds(waitTime);
        MainController.Instance.CheckResultEnded(correct, this);
    }

    #endregion
}
