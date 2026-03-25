using UnityEngine;
using UnityEngine.Rendering;

public class TooltipManager : MonoBehaviour
{
    private static TooltipManager _instance;

    public ToolTipUI toolTip;

    private void Awake()
    {
        _instance = this;
    }

    public static void Show(string content, string header = "")
    {
        _instance.toolTip.SetText(content, header);
        _instance.toolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _instance.toolTip.gameObject.SetActive(false);
    }
}
