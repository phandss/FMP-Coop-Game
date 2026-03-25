using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    private static TooltipManager _instance;

    public ToolTipUI toolTip;

    private void Awake()
    {
        _instance = this;
    }

    public static void Show()
    {
        _instance.toolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _instance.toolTip.gameObject.SetActive(false);
    }
}
