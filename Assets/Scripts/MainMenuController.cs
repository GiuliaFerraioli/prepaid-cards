using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject ContentParent;
    private Dictionary<string, GameObject> instantiatedCardElements = new Dictionary<string, GameObject>();
    public TMP_InputField cardNumberInput;
    private string previousCardNumber = "";
    public void OnEnable()
    {
        InstantiateCardElementsFromFile();
    }

public void InstantiateCardElementsFromFile()
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
                float cardCode;
                if (!float.TryParse(values[0], out cardCode))
                {
                    continue; // Skip this iteration and move to the next line
                }
                float amount = float.Parse(values[1]);
                string date = values[4];
                string from = values[2];
                string to = values[3];
                string payment = values[5];
                string notes = values[6];

                // Check if the card element has already been instantiated
                if (!instantiatedCardElements.ContainsKey(values[0]))
                {
                    // Load the CardElement prefab from Resources
                    GameObject cardElementPrefab = Resources.Load<GameObject>("CardElement");

                    if (cardElementPrefab != null)
                    {
                        // Instantiate the card element prefab
                        GameObject cardElement = Instantiate(cardElementPrefab, Vector3.zero, Quaternion.identity);
                        cardElement.transform.SetParent(ContentParent.transform);

                        // Assuming CardElement script is attached to the cardElementPrefab
                        CardElement cardElementScript = cardElement.GetComponent<CardElement>();
                        if (cardElementScript != null)
                        {
                            // Set the card information in the CardElement script
                            cardElementScript.SetCardInfo(cardCode, amount, date, from, to, payment, notes);
                        }
                        else
                        {
                            Debug.LogError("CardElement script not found on prefab");
                        }

                        // Add the instantiated card element to the dictionary
                        instantiatedCardElements.Add(values[0], cardElement);
                    }
                }
            }
        }

    }

}

    public void FilterCardElements()
    {
        string cardNumber = cardNumberInput.text;
        
        if (Input.GetKeyDown(KeyCode.Backspace) && string.IsNullOrEmpty(cardNumber))
        {
            foreach (var kvp in instantiatedCardElements)
            {
                kvp.Value.SetActive(true);
            }
        }
        else
        {
            foreach (var kvp in instantiatedCardElements)
            {
                string cardCode = kvp.Key;
                GameObject cardElement = kvp.Value;
                
                cardElement.SetActive(cardCode.Equals(cardNumber));
            }
        }

        previousCardNumber = cardNumber;
    }

}
