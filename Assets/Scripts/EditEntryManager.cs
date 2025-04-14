using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditEntryManager : MonoBehaviour
{
    [SerializeField] Button cancel = default;
    [SerializeField] Button save = default;
    [SerializeField] TMP_InputField amount = default;
    [SerializeField] TMP_InputField date = default;
    [SerializeField] TMP_InputField fromClient = default;
    [SerializeField] TMP_InputField toClient = default;
    [SerializeField] TMP_Dropdown payment = default;
    [SerializeField] TMP_InputField phoneNumber = default;
    [SerializeField] TMP_InputField notes = default;
    void OnEnable()
    {
        cancel.gameObject.SetActive(false);
        save.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        cancel.gameObject.SetActive(false);
        save.gameObject.SetActive(false);
    }

    public void OnEditClicked()
    {
        cancel.gameObject.SetActive(true);
        save.gameObject.SetActive(true);
        SetInputsInteractable(true);
    }

    public void OnSaveClicked()
    {
        cancel.gameObject.SetActive(false);
        SetInputsInteractable(false);
        save.gameObject.SetActive(false);
    }

    public void OnCancelClicked()
    {
        save.gameObject.SetActive(false);
        SetInputsInteractable(false);
        cancel.gameObject.SetActive(false);
    }

    private void SetInputsInteractable(bool interactable)
    {
        amount.interactable = interactable;
        date.interactable = interactable;
        fromClient.interactable = interactable;
        toClient.interactable = interactable;
        payment.interactable = interactable;
        phoneNumber.interactable = interactable;
        notes.interactable = interactable;

    }

}
