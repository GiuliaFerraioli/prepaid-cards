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
    public TMP_InputField notesInput;
    public TextMeshProUGUI errorText;
    private void Start()
    {
        // Initialize your input fields or other setup if needed
    }

    public void SubmitButtonPressed()
    {
        string cardCode = cardCodeInput.text;
        string amountText = amountInput.text;
        string notes = notesInput.text;

        // Check if card code is empty
        if (string.IsNullOrEmpty(cardCode))
        {
            errorText.text = "Inserisci un codice carta";
            return;
        }
        
        // Check if card code contains only numbers
        if (!IsCardCodeValid(cardCode))
        {
            errorText.text = "Il codice carta deve contenere solo numeri";
            return;
        }

        // Check if amount is empty or not a valid number
        if (string.IsNullOrEmpty(amountText) || !IsAmountValid(amountText, out float amount))
        {
            errorText.text = "Ricontrolla che la somma in euro non sia vuota o invalida";
            return;
        }

        // Check if the card code already exists
        if (CardCodeExists(cardCode))
        {
            errorText.text = "Il codice inserito esiste gia nel database";
            return;
        }

        // Create a CardModel object
        CardModel card = new CardModel
        {
            CardCode = cardCode,
            Amount = amount,
            Notes = notes,
            DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // Save the card to a text file
        SaveCardToTxt(card);

        // Optionally, reset input fields or perform other actions after submission
        ResetInputFields();
        errorText.text = "Carta caricata";

    }
    
    private bool IsAmountValid(string amountText, out float amount)
    {
        // Check if the amount contains only digits or a single dot
        if (float.TryParse(amountText, out amount))
        {
            string[] splitAmount = amountText.Split('.');
            return splitAmount.Length <= 2 && splitAmount.All(s => s.All(char.IsDigit));
        }

        return false;
    }
    private bool IsCardCodeValid(string cardCode)
    {
        // Check if the card code contains only numeric characters
        return cardCode.All(char.IsDigit);
    }
    private bool CardCodeExists(string cardCode)
    {
        // Load existing cards from the text file and check if the card code already exists
        List<CardModel> existingCards = LoadCardsFromTxt();
        return existingCards.Exists(c => c.CardCode == cardCode);
    }

    private List<CardModel> LoadCardsFromTxt()
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
                cards.Add(new CardModel
                {
                    CardCode = values[0],
                    Amount = float.Parse(values[1]),
                    Notes = values[2],
                    DateTime = values[3]
                });
            }
        }

        return cards;
    }

    private void SaveCardToTxt(CardModel card)
    {
        string fileName = "FileCarte.txt"; 
        string filePath = Path.Combine(Application.dataPath, "..", fileName);
        if (String.IsNullOrEmpty(card.Notes))
        {
            card.Notes = "Nessuna nota";
        }
        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine($"{card.CardCode}|{card.Amount}|{card.Notes}|{card.DateTime}");
        }
    }

    private void ResetInputFields()
    {
        cardCodeInput.text = "";
        amountInput.text = "";
        notesInput.text = "";
    }
}

[Serializable]
public class CardModel
{
    public string CardCode;
    public float Amount;
    public string Notes;
    public string DateTime;
}



