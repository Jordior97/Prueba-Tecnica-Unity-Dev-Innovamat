using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOptionsController : MonoBehaviour
{
    // Properties
    #region Properties

    public int NumOptions { get { return numOptions; } }

    #endregion

    // Variables
    #region Variables

    [Header("Variables")]
    [SerializeField] private int numOptions = 3;
    [SerializeField] private string prefabPath = "";

    private List<NumberObject> currOptions = new List<NumberObject>();


    #endregion

    private void Awake()
    {
        // Cap numOptions to max size of the database
        numOptions = Mathf.Clamp(numOptions, 0, MainController.Instance.DBSize);

        MainController.Instance.ResultChecked += OnResultChecked;
    }

    // Public 
    #region Public 

    public void AddOptions(List<int> options)
    {
        currOptions.Clear();
        gameObject.SetActive(true);

        // For each value, create an option object to be shown
        foreach (var item in options)
        {
            NumberObject obj = Instantiate(Resources.Load(prefabPath) as GameObject, transform).GetComponent<NumberObject>();
            obj.Init(item, () =>
            {
                SelectOption(obj.NumValue, obj);
            });

            currOptions.Add(obj);
        }
    }

    public void SelectOption(int value, NumberObject selectedOption)
    {
        Debug.Log("Option Selected: " + value);

        bool answer = value == MainController.Instance.CurrAnswer;

        // Disable all interaction
        foreach (NumberObject button in currOptions)
        {
            button.SetInteraction(false);
        }

        selectedOption.CheckResult(answer);

        // Update score
        MainController.Instance.UpdateScore(answer);
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    #endregion

    // Events
    #region Events

    private void OnResultChecked(bool correct, NumberObject selectedOption)
    {
        if (correct) // End round and show a new number
        {
            foreach (NumberObject opt in currOptions)
            {
                opt.Fade(false);
            }
            currOptions.Clear();

            // Start again
            MainController.Instance.StartAgain();
        }
        else // Check if it's the last incorrect answer to select
        {
            if (currOptions.Count > 2)
            {
                currOptions.Remove(selectedOption);
                selectedOption.Fade(false, () =>
                {
                    // Enable interaction again
                    foreach (NumberObject opt in currOptions)
                    {
                         opt.SetInteraction(true);
                    }
                });
            }
            else
            {
                // 1. Mark the correct option with green
                foreach (NumberObject opt in currOptions)
                {
                    if(opt.NumValue == MainController.Instance.CurrAnswer)
                    {
                        opt.UpdateColor(true);
                        break;
                    }
                }

                // 2. Fade out these 2 remaining options
                foreach (NumberObject opt in currOptions)
                {
                    opt.Fade(false);
                }
                currOptions.Clear();

                // 3. Start again
                MainController.Instance.StartAgain();
            }
        }
    }

    #endregion


}
