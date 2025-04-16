using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditEntryManager : MonoBehaviour
{
    [SerializeField] Button cancel = default;
    [SerializeField] Button save = default;
    [SerializeField] Button edit = default;
    [SerializeField] TMP_InputField amount = default;
    [SerializeField] TMP_InputField date = default;
    [SerializeField] TMP_InputField fromClient = default;
    [SerializeField] TMP_InputField toClient = default;
    [SerializeField] TMP_Dropdown payment = default;
    [SerializeField] TMP_InputField phoneNumber = default;
    [SerializeField] TMP_InputField notes = default;

    void OnEnable()
    {
        SetActive(cancel, false);
        SetActive(save, false);
        SetActive(edit, true);
    }

    void OnDisable()
    {
        SetActive(cancel, false);
        SetActive(save, false);
        SetActive(edit, true);
    }

    public void OnEditClicked()
    {
        SetActive(cancel, true);
        SetActive(save, true);
        SetActive(edit, false);
        SetInputsInteractable(true);
    }

    public void OnSaveClicked()
    {
        SetActive(cancel, false);
        SetActive(save, false);
        SetActive(edit, true);
        SetInputsInteractable(false);
    }

    public void OnCancelClicked()
    {
        SetActive(cancel, false);
        SetActive(save, false);
        SetActive(edit, true);
        SetInputsInteractable(false);

    }

    private void SetInputsInteractable(bool interactable)
    {
        if (amount != null) amount.interactable = interactable;
        if (date != null) date.interactable = interactable;
        if (fromClient != null) fromClient.interactable = interactable;
        if (toClient != null) toClient.interactable = interactable;
        if (payment != null) payment.interactable = interactable;
        if (phoneNumber != null) phoneNumber.interactable = interactable;
        if (notes != null) notes.interactable = interactable;

    }

    private void SetActive(Button button, bool isActive)
    {
        if (button != null && button.gameObject != null)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    private void SetActive(TMP_InputField button, bool isActive)
    {
        if (button != null && button.gameObject != null)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    private void SetActive(TMP_Dropdown button, bool isActive)
    {
        if (button != null && button.gameObject != null)
        {
            button.gameObject.SetActive(isActive);
        }
    }
}
