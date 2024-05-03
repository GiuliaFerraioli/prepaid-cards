using UnityEngine;
using TMPro;

public class CardElement : MonoBehaviour
{
    public TMP_InputField cardCodeText;
    public TMP_InputField amountText;
    public TMP_InputField dateText;
    public TMP_InputField fromText; 
    public TMP_InputField toText;
    public TMP_InputField paymentText; 
    public TMP_InputField notesText; 
    public void SetCardInfo(float cardCode, float amount, string date, string from, string to, string payment, string notes) 
    {
        cardCodeText.text = cardCode.ToString();
        amountText.text = amount.ToString();
        fromText.text = from; 
        toText.text = to;
        dateText.text = date;
        paymentText.text = payment;
        notesText.text = notes;
    }
}