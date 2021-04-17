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

    [SerializeField] private int numOptions = 3;
    [SerializeField] private string prefabPath = "";


    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Public 
    #region Public 

    public void AddOptions(List<int> options)
    {
        gameObject.SetActive(true);

        foreach (var item in options)
        {
            NumberObject obj = Instantiate(Resources.Load(prefabPath) as GameObject, transform).GetComponent<NumberObject>();
            obj.Init(item);
        }
    }

    #endregion


}
