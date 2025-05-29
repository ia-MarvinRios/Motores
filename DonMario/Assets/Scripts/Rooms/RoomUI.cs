using UnityEngine;

public class RoomUI : MonoBehaviour
{
    public GameObject actionPanel;
    public GameObject changeRoomsButtonsPanel;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;

    public void UpdateNavigationUI(RoomNavigation navigation)
    {
        upButton.SetActive(navigation.UpRoom != null);
        downButton.SetActive(navigation.DownRoom != null);
        leftButton.SetActive(navigation.LeftRoom != null);
        rightButton.SetActive(navigation.RightRoom != null);
    }

    public void SetActionState(bool active)
    {
        actionPanel.SetActive(active);
        changeRoomsButtonsPanel.SetActive(!active);
    }

    public void SetNavigationState(bool active)
    {
        changeRoomsButtonsPanel.SetActive(active);
        actionPanel.SetActive(false);
    }

    public void HideAllUI()
    {
        actionPanel.SetActive(false);
        changeRoomsButtonsPanel.SetActive(false);
    }
}