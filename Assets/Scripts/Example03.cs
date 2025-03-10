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


    void Start()
    {
        EntriesJsonManager.Instance.entriesLoaded += PopulateScrollview;

    }

    private void PopulateScrollview(bool loaded)
    {

        var array = new ItemData[EntriesJsonManager.Instance.GetEntriesCount()];
        scrollView.UpdateData(array);
        scrollView.SelectCell(0);
    }
}

