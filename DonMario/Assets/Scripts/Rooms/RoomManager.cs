using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{
    public float delayTimer = 0.5f;

    [Header("Dependencies")]
    public RoomNavigation navigation;
    public RoomUI ui;
    public RoomActions actions;

    private void Start()
    {
        actions.Initialize(this, navigation, ui);
        navigation.Initialize(this);
        StartCoroutine(InitRoom());
    }

    public void AfterMiniGameEnd()
    {
        StartCoroutine(InitRoom());
    }

    public IEnumerator InitRoom()
    {
        ui.HideAllUI();
        yield return new WaitForSeconds(delayTimer);

        navigation.UpdateAdjacentRooms();
        ui.UpdateNavigationUI(navigation);

        yield return new WaitForSeconds(delayTimer);

        if (navigation.actualRoom.isPlayed)
        {
            ui.SetNavigationState(true);
        }
        else
        {
            ui.SetActionState(true);
        }
    }

    // Llamado por RoomNavigation después de cambiar de habitación
    public void OnRoomChanged()
    {
        StartCoroutine(InitRoom());
    }
}