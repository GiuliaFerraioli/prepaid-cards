/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

class Cell : FancyCell<Entry, Context>
{
    [SerializeField] Animator animator = default;
    [SerializeField] Text message = default;
    [SerializeField] Text sum = default;
    [SerializeField] Image image = default;
    [SerializeField] Image imageLarge = default;
    [SerializeField] Button button = default;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    void Start()
    {
        button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
    }

    public override void UpdateContent(Entry entry)
    {
        message.text = entry.amount.ToString();
        //sum.text = entry.amount.ToString();
        // messageLarge.text = Index.ToString();

        var selected = Context.SelectedIndex == Index;
        imageLarge.color = image.color = selected
            ? new Color32(0, 255, 255, 100)
            : new Color32(255, 255, 255, 77);
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

