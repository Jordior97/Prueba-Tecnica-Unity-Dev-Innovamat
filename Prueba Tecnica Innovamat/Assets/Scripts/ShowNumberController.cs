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
        // Set number text
        numberText.GetComponent<TextMeshProUGUI>().text = numStr;

        // Start fade in animation
        StartCoroutine(AnimHelper.Instance.AnimCoroutine(animInTime, (alpha) =>
        {
            numberText.GetComponent<CanvasGroup>().alpha = alpha;
        },
        () =>
        {
            // On finish, start fade out animation
            StartCoroutine(HideNumber());
        }));
    }

    #endregion

    // Coroutines
    #region Coroutines

    private IEnumerator HideNumber()
    {
        // Wait animIdle seconds
        yield return new WaitForSeconds(animIdleTime);

        StartCoroutine(AnimHelper.Instance.AnimCoroutine(animOutTime, (alpha) =>
        {
            numberText.GetComponent<CanvasGroup>().alpha = 1 - alpha;
        },
        //On finish notify the controller to spawn the options
        () => {
            MainController.Instance.ShowOptions();
        }));
    }

    #endregion

}
