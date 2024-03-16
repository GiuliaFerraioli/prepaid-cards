using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CardSubmission : MonoBehaviour
{
    public TMP_InputField cardCodeInput;
    public TMP_InputField amountInput;
    public TMP_InputField fromInput; // Added
    public TMP_InputField toInput; // Added
    public TextMeshProUGUI errorText;
    public GameObject MainMenu;
    public GameObject RegisterCardMenu;

    public void SubmitButtonPressed()
    {
        // Check if card code is empty
        if (string.IsNullOrEmpty(cardCodeInput.text))
        {
            errorText.text = "Inserisci un codice carta";
            return;
        }

        float cardCode;
        if (!float.TryParse(cardCodeInput.text, out cardCode))
        {
            errorText.text = "Il codice carta deve contenere solo numeri";
            return;
        }

        // Check if amount is empty or not a valid number
        if (string.IsNullOrEmpty(amountInput.text) || !IsAmountValid(amountInput.text))
        {
            errorText.text = "Ricontrolla che la somma in euro non sia vuota o invalida";
            return;
        }

        float amount;
        if (!float.TryParse(amountInput.text, out amount))
        {
            errorText.text = "Ricontrolla che la somma in euro sia un numero valido";
            return;
        }

        // Check if the card code already exists
        if (CardCodeExists(cardCodeInput.text))
        {
            errorText.text = "Il codice inserito esiste già nel database";
            return;
        }

        // Create a CardModel object
        CardModel card = new CardModel
        {
            CardCode = cardCode,
            Amount = amount,
            From = fromInput.text,
            To = toInput.text,
            DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };

        // Save the card to a text file
        SaveCardToTxt(card);

        // Optionally, reset input fields or perform other actions after submission
        ResetInputFields();
        errorText.text = "Carta caricata";
    }

    private bool CardCodeExists(string cardCode)
    {
        // Load existing cards from the text file and check if the card code already exists
        List<CardModel> existingCards = LoadCardsFromTxt();
        float parsedCardCode;
        if (float.TryParse(cardCode, out parsedCardCode))
        {
            return existingCards.Exists(c => c.CardCode == parsedCardCode);
        }
        return false;
    }


    private bool IsAmountValid(string amountText)
    {
        // Check if the amount contains only digits or a single dot
        string[] splitAmount = amountText.Split('.');
        return splitAmount.Length <= 2 && splitAmount.All(s => s.All(char.IsDigit));
    }

    public List<CardModel> LoadCardsFromTxt()
    {
        List<CardModel> cards = new List<CardModel>();

        string fileName = "FileCarte.txt";
        string filePath = Path.Combine(Application.dataPath, "..", fileName);
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] values = line.Split('|');
                float cardCode;
                float amount;
                if (float.TryParse(values[0], out cardCode) && float.TryParse(values[1], out amount))
                {
                    cards.Add(new CardModel
                    {
                        CardCode = cardCode,
                        Amount = amount,
                        DateTime = values[4], 
                        From = values[2],
                        To = values[3],
                    });
                }
            }
        }

        return cards;
    }

    private void SaveCardToTxt(CardModel card)
    {
        string fileName = "FileCarte.txt";
        string filePath = Path.Combine(Application.dataPath, "..", fileName);
        // Check if the file exists and is not empty
        if (!File.Exists(filePath))
        {
            string line =$"{card.CardCode}|{card.Amount}|{card.From}|{card.To}|{card.DateTime}";
            File.WriteAllText(filePath, line);
        }
        else
        {
            // If the file exists and is not empty, append the card information
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{card.CardCode}|{card.Amount}|{card.From}|{card.To}|{card.DateTime}");
            }
        }
        
    }

    private void ResetInputFields()
    {
        cardCodeInput.text = "";
        amountInput.text = "";
        fromInput.text = ""; 
        toInput.text = ""; 
        errorText.text = "";
    }

    public void Cancel()
    {
        MainMenu.SetActive(true);
        RegisterCardMenu.SetActive(false);
        ResetInputFields();
    }
}

[Serializable]
public class CardModel
{
    public float CardCode;
    public float Amount;
    public string DateTime;
    public string From;
    public string To;
}



