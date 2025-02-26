using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
   public enum GameType { INTRO, BUILD, PLAY, OUTRO }
   public GameType e_GameType = GameType.INTRO;

   private Action gameManagerAction;

   #region Manager
   private BoardManager _boardManager;
   public BoardManager boardManager
   {
      get
      {
         if (_boardManager == null)
            _boardManager = FindObjectOfType<BoardManager>(true);
         
         return _boardManager;
      }
   }
   
   private UIManager _uiManager;
   public UIManager uiManager
   {
      get
      {
         if (_uiManager == null)
            _uiManager = FindObjectOfType<UIManager>(true);
         
         return _uiManager;
      }
   }
   #endregion

   protected override void Awake()
   {
      base.Awake();
   }

   void Start()
   {
      boardManager.CreateBoard();
   }

   void Update()
   {
      switch (e_GameType)
      {
         case GameType.INTRO:
            // 기능 없음
            break;
         case GameType.BUILD:
            boardManager.RayToBoard();
            break;
         case GameType.PLAY:
            break;
         case GameType.OUTRO:
            break;
      }
   }

   public void OnChageType(GameType gameType)
   {
      if (e_GameType != gameType)
         e_GameType = gameType;
   }
   
}