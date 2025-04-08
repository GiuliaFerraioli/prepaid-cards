/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Linq;
using UnityEngine;


class Example03 : MonoBehaviour
{
    [SerializeField] ScrollView scrollView = default;


    void Awake()
    {
        EntriesJsonManager.Instance.entriesLoaded += PopulateScrollview;
    }

    private void PopulateScrollview(bool loaded)
    {
        var entries = EntriesJsonManager.Instance.GetEntries();

        scrollView.UpdateData(entries);
        scrollView.SelectCell(0);
    }
}

