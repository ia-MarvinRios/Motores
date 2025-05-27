using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenRooms : MonoBehaviour
{
    private Explore explore = new Explore();

    public ScenaryBlock actualRoom;
    public MiniGamesManager miniGamesManager;
    public MoveToTarget cameraPos;
    public MoveToTarget playerPos;

    




    private Transform UpRoom { get;set;}
    private Transform DownRoom { get; set; }
    private Transform LeftRoom { get; set; }
    private Transform RightRoom { get; set; }


    [Space(20)]

    public GameObject actionPanel;
    public GameObject changeRoomsButtonsPanel;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rigthButton;
    public float delayTimer = 0.5f;


    private void OnEnable()
    {
        miniGamesManager.OnEndMiniGame += AfterMiniGameEnd;
    }
    private void OnDisable()
    {
        miniGamesManager.OnEndMiniGame -= AfterMiniGameEnd;
    }

    private void Start()
    {
       
        StartCoroutine(InitRoom());
    }
   public void AfterMiniGameEnd()
    {
        StartCoroutine(InitRoom());
    }

    IEnumerator InitRoom()
    {
       
        actionPanel.SetActive(false);
        changeRoomsButtonsPanel.SetActive(false);

        yield return new WaitForSeconds(delayTimer);
        CleanActualRoomInfo();

        SetNewRooms();
        EnableRoomButtons();


         yield return new WaitForSeconds(delayTimer);


        if (actualRoom.isPlayed)
        {
            changeRoomsButtonsPanel.SetActive(true);
            yield break;
        }
        actionPanel.SetActive(true);
    }

    public void CleanActualRoomInfo()
    {
        UpRoom = null;
        DownRoom = null;
        LeftRoom = null;
        RightRoom = null;
    }
    public void SetNewRooms()
    {
        UpRoom = actualRoom.upRoom;
        DownRoom = actualRoom.downRoom;
        LeftRoom = actualRoom.leftRoom;
        RightRoom = actualRoom.rightRoom;
    }
    public void EnableRoomButtons()
    {
        upButton.SetActive(UpRoom);
        downButton.SetActive(DownRoom);
        leftButton.SetActive(LeftRoom);
        rigthButton.SetActive(RightRoom);
    }


    //acciones ______________________________________
    public void Explore()
    {
        actionPanel.SetActive(false);
        changeRoomsButtonsPanel.SetActive(false);
        explore.TriggerRandomEvent();

    }
    //minigames
    public void Fight()
    {
        actionPanel.SetActive(false);
        changeRoomsButtonsPanel.SetActive(false);
        Debug.Log("Fight");
        actualRoom.isPlayed = true;
        StartCoroutine(InitRoom());
    }
    public void Shop()
    {
        actionPanel.SetActive(false);
        changeRoomsButtonsPanel.SetActive(false);
        Debug.Log("Shop");
        actualRoom.isPlayed = true;
        StartCoroutine(InitRoom());
    }

    // moverse entre el mapa ______________________________________
    public void MoveUp()
    {
        ChangeRoomButtonLogic(UpRoom);
    }
    public void MoveDown()
    {
        ChangeRoomButtonLogic(DownRoom);
    }
    public void MoveLeft()
    {
        ChangeRoomButtonLogic(LeftRoom);
    }
    public void MoveRight()
    {
        ChangeRoomButtonLogic(RightRoom);
    }

    public void ContinueAfterEvent()
    {
        actualRoom.isPlayed = true;
        StartCoroutine(InitRoom());
    }
    void ChangeRoomButtonLogic(Transform room)
    {
        if (room == null) return;
        actualRoom = room.transform.GetComponent<ScenaryBlock>();
        cameraPos.SetTargetAndMove(room);
        playerPos.SetTargetAndMove(room);
        StartCoroutine(InitRoom());
    }

}
