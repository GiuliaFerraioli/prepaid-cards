using UnityEngine;
using TMPro;

public class CardElement : MonoBehaviour
{
    public TMP_InputField cardCodeText;
    public TMP_InputField amountText;
    public TMP_InputField dateText;
    public TMP_InputField fromText; 
    public TMP_InputField toText;
    public void SetCardInfo(float cardCode, float amount, string date, string from, string to) 
    {
        cardCodeText.text = cardCode.ToString();
        amountText.text = amount.ToString();
        fromText.text = from; 
        toText.text = to;
        dateText.text = date;
    }
}