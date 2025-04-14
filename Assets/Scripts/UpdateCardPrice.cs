using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCardPrice : MonoBehaviour
{
    public TMP_InputField euroInput;
    public TMP_InputField cardNumber;

    public TMP_InputField notesInput;



    private void Start()
    {
        GameObject cardSubmissionObject = GameObject.Find("UIManager");


    }

    public void CancelCardElement()
    {
        RemoveCardFromTxt(cardNumber.text);
        Destroy(this.gameObject);
    }

    private void RemoveCardFromTxt(string cardNumberToRemove)
    {
        // Load existing cards from the text file
        List<CardModel> existingCards = CardSubmissionManager.Instance.LoadCardsFromTxt();

        // Find the index of the card to remove
        int indexToRemove = existingCards.FindIndex(c => c.CardCode.ToString() == cardNumberToRemove);

        if (indexToRemove != -1)
        {
            // Remove the card from the list
            existingCards.RemoveAt(indexToRemove);

            // Rewrite the entire file without the removed card
            string fileName = "FileCarte.txt";
            string filePath = Path.Combine(Application.dataPath, "..", fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (CardModel card in existingCards)
                {
                    writer.WriteLine($"{card.CardCode}|{card.Amount}|{card.From}|{card.To}|{card.DateTime}|{card.Payment}|{card.Notes}");
                }
            }
        }
    }

    public void ChangeAmount(Toggle toggle)
    {
        if (toggle.isOn)
        {
            toggle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Salva";
            euroInput.interactable = true;
            notesInput.interactable = true;

        }
        else
        {
            toggle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Aggiorna";
            euroInput.interactable = false;
            notesInput.interactable = false;

            SaveNewEuroAmountToTxt(euroInput.text, notesInput.text);

        }
    }

    private void SaveNewEuroAmountToTxt(string newAmount, string notes)
    {
        // Get the entered card number from the input field
        string updatedCardNumber = cardNumber.text;

        // Load existing cards from the text file
        List<CardModel> existingCards = CardSubmissionManager.Instance.LoadCardsFromTxt();

        // Find the card model corresponding to the entered card number
        CardModel cardToUpdate = existingCards.Find(c => c.CardCode.ToString() == updatedCardNumber);

        if (cardToUpdate != null)
        {
            // Update the euro amount
            cardToUpdate.Amount = float.Parse(newAmount);
            cardToUpdate.Notes = notes;

            // Rewrite the entire file with the updated card information
            string fileName = "FileCarte.txt";
            string filePath = Path.Combine(Application.dataPath, "..", fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (CardModel card in existingCards)
                {
                    writer.WriteLine($"{card.CardCode}|{card.Amount}|{card.From}|{card.To}|{card.DateTime}|{card.Payment}|{card.Notes}");
                }
            }
        }
    }

}
