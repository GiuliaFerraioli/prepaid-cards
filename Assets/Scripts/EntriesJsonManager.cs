using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.Examples;

public class EntriesJsonManager : MonoBehaviour
{
    public static EntriesJsonManager Instance { get; private set; }

    //public GameObject ContentParent;
    private Dictionary<string, GameObject> instantiatedCardElements = new Dictionary<string, GameObject>();
    //public TMP_InputField cardNumberInput;
    private string previousCardNumber = "";

    public Action<bool> entriesLoaded;

    private List<Entry> entries = new List<Entry>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PopulateEntriesFromFile();
    }

    public int GetEntriesCount()
    {
        return entries.Count;
    }

    public List<Entry> GetEntries()
    {
        return entries;
    }

    public void PopulateEntriesFromFile()
    {

        string fileName = "FileCarte.txt";
        string filePath = Path.Combine(Application.dataPath, "..", fileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {

                    // Split the line into values
                    string[] values = line.Split('|');

                    // Parse the values
                    int cardCode = int.Parse(values[0]);
                    // if (!float.TryParse(values[0], out cardCode))
                    // {
                    //     Debug.Log("hello");
                    //     continue; // Skip this iteration and move to the next line
                    // }
                    Payments paymentEnum;
                    try
                    {
                        paymentEnum = (Payments)Enum.Parse(typeof(Payments), values[5].Replace(" ", ""), ignoreCase: true);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Invalid payment type '{values[5]}' on line: {line}");
                        continue;
                    }

                    float amount = float.Parse(values[1]);
                    string date = values[2];
                    string from = values[3];
                    string to = values[4];
                    string payment = values[5];
                    string phoneNumber = values[6];
                    string notes = values[7];
                    var entry = new Entry
                    {
                        cardCode = int.Parse(values[0]),
                        amount = float.Parse(values[1]),
                        date = DateTime.Parse(values[2]),
                        fromClient = values[3],
                        toClient = values[4],
                        payment = paymentEnum,
                        phoneNumber = values[6],
                        notes = values[7]

                    };
                    entries.Add(entry);

                    // Check if the card element has already been instantiated
                    // if (!instantiatedCardElements.ContainsKey(values[0]))
                    // {
                    //     // Load the CardElement prefab from Resources
                    //     GameObject cardElementPrefab = Resources.Load<GameObject>("CardElement");

                    //     if (cardElementPrefab != null)
                    //     {
                    //         // Instantiate the card element prefab
                    //         GameObject cardElement = Instantiate(cardElementPrefab, Vector3.zero, Quaternion.identity);
                    //         //cardElement.transform.SetParent(ContentParent.transform);

                    //         // Assuming CardElement script is attached to the cardElementPrefab
                    //         CardElement cardElementScript = cardElement.GetComponent<CardElement>();
                    //         if (cardElementScript != null)
                    //         {
                    //             // Set the card information in the CardElement script
                    //             cardElementScript.SetCardInfo(cardCode, amount, date, from, to, payment, notes);
                    //         }
                    //         else
                    //         {
                    //             Debug.LogError("CardElement script not found on prefab");
                    //         }

                    //         // Add the instantiated card element to the dictionary
                    //         instantiatedCardElements.Add(values[0], cardElement);
                    //     }
                    // }
                }

                entriesLoaded?.Invoke(true);
            }
        }
        else
        {
            Debug.Log("Text file not found");
        }

    }

    // public void FilterCardElements()
    // {
    //     //string cardNumber = cardNumberInput.text;

    //     if (Input.GetKeyDown(KeyCode.Backspace) && string.IsNullOrEmpty(cardNumber))
    //     {
    //         foreach (var kvp in instantiatedCardElements)
    //         {
    //             kvp.Value.SetActive(true);
    //         }
    //     }
    //     else
    //     {
    //         foreach (var kvp in instantiatedCardElements)
    //         {
    //             string cardCode = kvp.Key;
    //             GameObject cardElement = kvp.Value;

    //             //cardElement.SetActive(cardCode.Equals(cardNumber));
    //         }
    //     }

    //     //previousCardNumber = cardNumber;
    // }

}
