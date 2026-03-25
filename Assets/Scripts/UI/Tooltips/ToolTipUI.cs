using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class ToolTipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerTxt;
    [SerializeField] private TextMeshProUGUI contentTxt;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private int characterWrapLimit;

    private void Update()
    {
        int _headerLength = headerTxt.text.Length;
        int _contentLength = contentTxt.text.Length;

        layoutElement.enabled = (_headerLength > characterWrapLimit || _contentLength > characterWrapLimit) ? true : false;
    }
}
