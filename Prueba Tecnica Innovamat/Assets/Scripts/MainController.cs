using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [Serializable]
    public class NumbersDatabase
    {
        public Number[] numbers;
    }

    [Serializable]
    public class Number
    {
        public string text;
        public int value;
    }

    // Variables
    #region Variables

    //[Header("UI")]
    //[SerializeField] private TextMeshProUGUI numberTitle;

    [Header("Database")]
    [SerializeField] private TextAsset jsonDatabase;


    // Controllers
    [Header("Controllers")]
    [SerializeField] private ShowNumberController numToShow;

    private NumbersDatabase database;
    private Number currentNumber;

    #endregion

    // Override
    #region Override

    private void Awake()
    {
        // Load database from json
        database = JsonUtility.FromJson<NumbersDatabase>(jsonDatabase.text);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            RandomNumber();
    }

    #endregion

    // Private 
    #region Private

    private void RandomNumber()
    {
        // TODO: check different from previous one
        int index = UnityEngine.Random.Range(0, database.numbers.Length - 1);
        if (currentNumber != null)
        {
            while (currentNumber == database.numbers[index])
            {
                index = UnityEngine.Random.Range(0, database.numbers.Length - 1);
            }
        }

        currentNumber = database.numbers[index];
        numToShow.ShowNumber(currentNumber.text);
        //numberTitle.text = currentNumber.text;


        Debug.Log("Current Number: " + currentNumber.text + " / " + currentNumber.value);
    }

    #endregion


}
