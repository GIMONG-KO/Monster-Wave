using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
   public enum GameType { INTRO, BUILD, PLAY, OUTRO }
   public GameType e_GameType;


   protected override void Awake()
   {
      base.Awake();
   }
}