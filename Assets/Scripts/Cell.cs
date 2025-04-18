﻿/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */


using TMPro;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

class Cell : FancyCell<Entry, Context>
{
    [SerializeField] Animator animator = default;
    [SerializeField] Text cardCode = default;
    [SerializeField] TMP_InputField amount = default;
    [SerializeField] TMP_InputField date = default;
    [SerializeField] TMP_InputField fromClient = default;
    [SerializeField] TMP_InputField toClient = default;
    [SerializeField] TMP_Dropdown payment = default;
    [SerializeField] TMP_InputField phoneNumber = default;
    [SerializeField] TMP_InputField notes = default;
    [SerializeField] Image image = default;
    [SerializeField] Image imageLarge = default;
    [SerializeField] Button button = default;
    [SerializeField] Button buttonEditEntry = default;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    void Start()
    {
        button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        amount.interactable = false;

    }

    public override void UpdateContent(Entry entry)
    {
        cardCode.text = entry.cardCode.ToString();
        amount.text = entry.amount.ToString();
        date.text = entry.date.ToString("dd/MM/yyyy");
        fromClient.text = entry.fromClient.ToString();
        toClient.text = entry.toClient.ToString();
        payment.value = (int)entry.payment;
        phoneNumber.text = entry.phoneNumber.ToString();
        notes.text = entry.notes.ToString();

        var selected = Context.SelectedIndex == Index;
        imageLarge.color = image.color = selected
            ? new Color32(0, 255, 255, 100)
            : new Color32(255, 255, 255, 77);

        buttonEditEntry.gameObject.SetActive(selected);

    }

    private void CellSelected(bool selected)
    {
        if (selected)
        {

        }
    }

    public override void UpdatePosition(float position)
    {
        currentPosition = position;

        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }

        animator.speed = 0;
    }

    // GameObject が非アクティブになると Animator がリセットされてしまうため
    // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
    float currentPosition = 0;

    void OnEnable() => UpdatePosition(currentPosition);
}

