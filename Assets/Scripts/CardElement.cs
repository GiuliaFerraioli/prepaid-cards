using UnityEngine;
using TMPro;

public class CardElement : MonoBehaviour
{
    public TextMeshProUGUI cardCodeText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI fromText; 
    public TextMeshProUGUI toText;

    public void SetCardInfo(float cardCode, float amount, string date, string from, string to) 
    {
        cardCodeText.text = cardCode.ToString();
        amountText.text = amount.ToString();
        fromText.text = from; 
        toText.text = to;
        dateText.text = date;
    }
}