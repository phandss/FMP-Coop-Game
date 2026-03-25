using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour
{

    public string content;
    public string header;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TooltipManager.Show(content, header);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TooltipManager.Hide();
        }
    }
}
