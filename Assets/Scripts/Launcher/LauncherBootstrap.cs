using UnityEngine;

public class LauncherBootstrap : MonoBehaviour
{
    [SerializeField] private PanelsSwitcher _panelsSwitcher;
    [SerializeField] private Launcher _launcher;

    private void Awake()
    {
        if (_launcher.IsConnecting == false)
        {
            _panelsSwitcher.ShowLoadingPanel();
            _launcher.TryConnectingToMaster();
        }
    }
}
