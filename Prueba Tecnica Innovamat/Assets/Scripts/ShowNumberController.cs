using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowNumberController : MonoBehaviour
{
    // Variables
    #region Variables

    [Header("UI")]
    [SerializeField] private GameObject numberText;

    [Header("Animation")]
    [SerializeField] private float animInTime = 2f;
    [SerializeField] private float animIdleTime = 2f;
    [SerializeField] private float animOutTime = 2f;

    #endregion

    // Override
    #region Override

    private void Awake()
    {
        numberText.GetComponent<CanvasGroup>().alpha = 0;
    }

    #endregion

    #region Public 

    public void ShowNumber(string numStr)
    {
        numberText.GetComponent<TextMeshProUGUI>().text = numStr;
        StartCoroutine(GetComponent<AnimHelper>().AnimCoroutine(animInTime, (alpha) =>
        {
            numberText.GetComponent<CanvasGroup>().alpha = alpha;
        }, null));
    }

    #endregion

}
