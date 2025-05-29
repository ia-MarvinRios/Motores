using UnityEngine;

public class RoomActions : MonoBehaviour
{
    private readonly Explore explore = new Explore();
    private readonly Fight fight = new Fight();
    private RoomManager manager;
    private RoomNavigation navigation;
    private RoomUI ui;



    private bool isExploring;
    private bool isFighting;


    public void Initialize(RoomManager manager, RoomNavigation nav, RoomUI roomUI)
    {
        this.manager = manager;
        navigation = nav;
        ui = roomUI;
        TypewriterTextUI.Instance.OnTextFinished += ChooseAfterText;
        MiniGamesManager.Instance.OnEndMiniGame += fight.CloseMinigame;
        MiniGamesManager.Instance.OnLoseMiniGame += fight.Damage;
    }
    private void OnDestroy()
    {
        TypewriterTextUI.Instance.OnTextFinished -= ChooseAfterText;
        MiniGamesManager.Instance.OnEndMiniGame -= fight.CloseMinigame;
        MiniGamesManager.Instance.OnLoseMiniGame -= fight.Damage;
    }
   

    private void ChooseAfterText()
    {
        if(isExploring) ContinueAfterEvent();
       

        else if (isFighting) ContinueAfterEvent();
       
    }

    public void Explore()
    {
        isExploring = true;
        ui.HideAllUI();
        explore.TriggerRandomEvent();
    }

    public void Fight()
    {
        isFighting = true;
        ui.HideAllUI();
        fight.ChooseMinigame();
        //manager.AfterMiniGameEnd();
    }

    public void Shop()
    {
        ui.HideAllUI();
       
        manager.AfterMiniGameEnd();
    }

    public void ContinueAfterEvent()
    {
        isExploring = false;
        isFighting = false;
        navigation.actualRoom.isPlayed = true;
        manager.AfterMiniGameEnd();
    }
}