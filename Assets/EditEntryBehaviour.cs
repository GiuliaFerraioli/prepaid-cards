using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditEntryBehaviour : MonoBehaviour
{

    [SerializeField] Button cancel = default;
    [SerializeField] Button save = default;
    [SerializeField] TMP_InputField amount = default;
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

    // Update is called once per frame
    void Update()
    {

    }

    public void EditClicked()
    {
        cancel.gameObject.SetActive(true);
        save.gameObject.SetActive(true);
        amount.interactable = true;
    }
}
