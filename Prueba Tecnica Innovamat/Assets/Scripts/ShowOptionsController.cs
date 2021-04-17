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
        selectedOption.CheckResult(value == MainController.Instance.CurrAnswer);

    }

    #endregion


}
