using UnityEngine;
using UnityEngine.InputSystem;

public class DualPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject humanPrefab;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Transform startPoint;

    private bool humanJoined = false;
    private bool ghostJoined = false;

    private void Update()
    {
        if(Keyboard.current == null) return;

    }
}
