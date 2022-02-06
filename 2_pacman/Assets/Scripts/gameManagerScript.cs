using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
   public GameObject wallsObject;
   public GameObject coinsObject;
   public GameObject playerObject;
   public GameObject mainMenuObject;
   public GameObject gameOverObject;
   public GameObject winMenuObject;
   public coinScript coinScript;
   public GameObject OrangeGhost;
   public GameObject intersectionsObject;
   public GameObject BlueGhost;
   public GameObject PinkGhost;
   public GameObject RedGhost;
   private int ghostsQuantity;

   private void Update()
   {
      CheckWinConditions();
   }

   public void GameOver()
   {
      gameOverObject.SetActive(true);
      playerObject.SetActive(false);
      coinsObject.SetActive(false);
      wallsObject.SetActive(false);
      intersectionsObject.SetActive(false);
      DeSpawnGhosts();
   }

   public void PlayGame()
   {
      mainMenuObject.SetActive(false);
      playerObject.SetActive(true);
      coinsObject.SetActive(true);
      wallsObject.SetActive(true);
      intersectionsObject.SetActive(true);
   }

   public void CheckWinConditions()
   {
      if(coinScript.coinAmount == 0) 
      {
         playerObject.SetActive(false);
         coinsObject.SetActive(false);
         wallsObject.SetActive(false);
         intersectionsObject.SetActive(false);
         winMenuObject.SetActive(true);
         DeSpawnGhosts();
         DoNotTriggerWinConditionsAgain();
      }
   }

   public void DoNotTriggerWinConditionsAgain()
   {
      coinScript.coinAmount = 1;
   }

   public void PlayAgain()
   {
      SceneManager.LoadScene("Main");
   }

   public void spawnOneGhost()
   {
      ghostsQuantity = 1;
      SpawnGhosts();
   }

   public void spawnTwoGhosts()
   {
      ghostsQuantity = 2;
      SpawnGhosts();
   }

   public void spawnThreeGhosts()
   {
      ghostsQuantity = 3;
      SpawnGhosts();
   }

   public void spawnFourGhosts()
   {
      ghostsQuantity = 4;
      SpawnGhosts();
   }

   public void DeSpawnGhosts()
   {
      if(ghostsQuantity == 1) 
      {
         RedGhost.SetActive(false);
      }
      else if(ghostsQuantity == 2) 
      {
         RedGhost.SetActive(false);
         OrangeGhost.SetActive(false);
      }
      else if(ghostsQuantity == 3) 
      {
         RedGhost.SetActive(false);
         OrangeGhost.SetActive(false);
         BlueGhost.SetActive(false);
      }
      else
      {
         RedGhost.SetActive(false);
         OrangeGhost.SetActive(false);
         BlueGhost.SetActive(false);
         PinkGhost.SetActive(false);
      }
   }

   public void SpawnGhosts()
   {
      if(ghostsQuantity == 1) 
      {
         RedGhost.SetActive(true);
         PlayGame();
      }
      else if(ghostsQuantity == 2) 
      {
         RedGhost.SetActive(true);
         OrangeGhost.SetActive(true);
         PlayGame();
      }
      else if(ghostsQuantity == 3) 
      {
         RedGhost.SetActive(true);
         OrangeGhost.SetActive(true);
         BlueGhost.SetActive(true);
         PlayGame();
      }
      else
      {
         RedGhost.SetActive(true);
         OrangeGhost.SetActive(true);
         BlueGhost.SetActive(true);
         PinkGhost.SetActive(true);
         PlayGame();
      }
   }
}