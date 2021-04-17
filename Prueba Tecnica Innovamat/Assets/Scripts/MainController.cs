using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Database ----------------------
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
    // ------------------------------

    // Properties
    #region Properties

    public static MainController Instance { get { return instance; } }
    public int DBSize { get { return database.numbers.Length; } }
    public int CurrAnswer { get { return currentNumber.value; } }

    #endregion

    // Variables
    #region Variables

    [Header("Database")]
    [SerializeField] private TextAsset jsonDatabase;

    [Header("Controllers")]
    [SerializeField] private ShowNumberController numToShow;
    [SerializeField] private ShowOptionsController optionsToShow;


    private NumbersDatabase database;
    private Number currentNumber;

    private static MainController instance = null;

    // Events

    #endregion

    // Override
    #region Override

    private void Awake()
    {
        instance = this;

        // Load database from json
        database = JsonUtility.FromJson<NumbersDatabase>(jsonDatabase.text);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            RandomNumber();
    }

    #endregion

    // Public 
    #region Public

    public void ShowOptions()
    {
        // Generate random numbers equivalent to numOptions -1
        // We need to show the correct option too
        int numOptions = optionsToShow.NumOptions;

        List<int> numbers = new List<int>();
        numbers.Add(currentNumber.value);

        while (numbers.Count < numOptions)
        {
            Number randomOption = GetRandomNumber();
            if (!numbers.Contains(randomOption.value))
            {
                numbers.Add(randomOption.value);
            }
        }
       
        numbers.Shuffle();

        optionsToShow.AddOptions(numbers);
    }

    #endregion

    // Private 
    #region Private

    private Number GetRandomNumber()
    {
        return database.numbers[UnityEngine.Random.Range(0, database.numbers.Length - 1)];
    }

    private void RandomNumber()
    {
        int index = UnityEngine.Random.Range(0, database.numbers.Length - 1);
        if (currentNumber != null)
        {
            while (currentNumber == database.numbers[index])
            {
                index = UnityEngine.Random.Range(0, database.numbers.Length - 1);
            }
        }
        // Set the correct option as current number
        currentNumber = database.numbers[index];

        // Show number UI functionality
        numToShow.ShowNumber(currentNumber.text);

        Debug.Log("Current Number: " + currentNumber.text + " / " + currentNumber.value);
    }

    #endregion


}
