using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDemoGridListItem : MonoBehaviour
{
    public Image mBgImg;
    public Button mBtn;
    public Image mIconImg;
    public TMP_Text mTitleText;

    int mItemDataIndex = -1;
    Action<int> mClickHandler;

    public void Init()
    {
        mBtn.onClick.AddListener(OnButtonClicked);
    }

    public void SetClickCallBack(Action<int> clickHandler)
    {
        mClickHandler = clickHandler;
    }

    public void SetItemData(int index)
    {
        mItemDataIndex = index;
    }

    void OnButtonClicked()
    {
        if (mClickHandler != null)
            mClickHandler(mItemDataIndex);
    }
}

public class UIDemoGridItemData
{
    public string bg;
    public string icon;
    public string title;
    public int index;

    public UIDemoGridItemData(string bg, string icon, string title, int index)
    {
        this.bg = bg;
        this.icon = icon;
        this.title = title;
        this.index = index;
    }
}
