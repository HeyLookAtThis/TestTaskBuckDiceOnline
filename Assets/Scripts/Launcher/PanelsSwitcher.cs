using UnityEngine;

public class PanelsSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _loadingPanel;

    private void OnEnable()
    {
        HideProgressPanel();
    }

    public void ShowProgressPanel()
    {
        _loadingPanel.SetActive(true);
        _menuPanel.SetActive(false);
    }

    public void HideProgressPanel()
    {
        _loadingPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }
}
