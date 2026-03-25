using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[ExecuteInEditMode()]
public class ToolTipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerTxt;
    [SerializeField] private TextMeshProUGUI contentTxt;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private int characterWrapLimit;


    public void SetText(string _content, string _header = "")
    {
        if(string.IsNullOrEmpty(_header))
        {
            headerTxt.gameObject.SetActive(false);
        }
        else
        {
            headerTxt.gameObject.SetActive(true);
            headerTxt.text = _header;
        }

        contentTxt.text = _content;
    }

    //private void Update()
    //{
    //    if(Application.isEditor)
    //    {
    //        HandleLayout();
    //    }
        
    //}

    //private void HandleLayout()
    //{
    //    int _headerLength = headerTxt.text.Length;
    //    int _contentLength = contentTxt.text.Length;

    //    layoutElement.enabled = (_headerLength > characterWrapLimit || _contentLength > characterWrapLimit) ? true : false;
    //}
}
