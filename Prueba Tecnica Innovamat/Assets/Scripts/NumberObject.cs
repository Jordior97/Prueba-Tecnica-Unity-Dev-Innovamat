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
        StartCoroutine(AnimHelper.Instance.AnimCoroutine(animTime, (alpha) =>
        {
            canvas.alpha = alpha;
        },
        () =>
        {
            // On finish, enable interaction
            button.interactable = true;
        }));

        // Add a callback to call it when button clicked
        callback = call;
        button.onClick.AddListener(() => { call?.Invoke(); });
    }

    public void CheckResult(bool correct)
    {
        numText.color = correct
            ? correctColor
            : incorrectColor;
    }

    #endregion


}
