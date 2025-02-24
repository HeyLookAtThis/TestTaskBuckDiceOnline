using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PanelsSwitcher _panelsSwitcher;
    [SerializeField] private Launcher _launcher;

    private void Start()
    {
        //if(Application.isPlaying)
        _panelsSwitcher.ShowProgressPanel();
        _launcher.TryConnecting();
    }
}
