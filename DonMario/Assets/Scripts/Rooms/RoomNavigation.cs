using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    [Header("References")]
    public MoveToTarget cameraPos;
    public MoveToTarget playerPos;

    public ScenaryBlock actualRoom;
    private RoomManager manager;

    public Transform UpRoom { get; set; }
    public Transform DownRoom { get; set; }
    public Transform LeftRoom { get; set; }
    public Transform RightRoom { get; set; }

    public void Initialize(RoomManager roomManager)
    {
        manager = roomManager;
    }

    public void UpdateAdjacentRooms()
    {
        UpRoom = actualRoom.upRoom;
        DownRoom = actualRoom.downRoom;
        LeftRoom = actualRoom.leftRoom;
        RightRoom = actualRoom.rightRoom;
    }

    public void ChangeRoom(Transform newRoom)
    {
        if (newRoom == null) return;

        actualRoom = newRoom.GetComponent<ScenaryBlock>();
        cameraPos.SetTargetAndMove(newRoom);
        playerPos.SetTargetAndMove(newRoom);

        // Notificar al manager que la habitación cambió
        manager.OnRoomChanged();
    }

    public void MoveUp() => ChangeRoom(UpRoom);
    public void MoveDown() => ChangeRoom(DownRoom);
    public void MoveLeft() => ChangeRoom(LeftRoom);
    public void MoveRight() => ChangeRoom(RightRoom);
}