using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    [SerializeField]private int maxNumberRooms = 30 ;
    [SerializeField] private int actualsRooms = 0;
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
   public int MaxNumberRooms()
   {
       return maxNumberRooms; 
   }
    public void AddRoomCount()
    {
        actualsRooms++;
    }
    public bool ActualRoomLessThanMax()
    {
        if(actualsRooms < maxNumberRooms)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
