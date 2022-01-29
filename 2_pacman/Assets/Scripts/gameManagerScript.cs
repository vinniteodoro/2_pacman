using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
   [SerializeField] GameObject wallsObject;
   [SerializeField] GameObject coinsObject;
   [SerializeField] GameObject playerObject;
   [SerializeField] GameObject mainMenuObject;
   [SerializeField] GameObject winMenuObject;
   [SerializeField] coinScript coinScript;

   private void Update()
   {
      CheckWinConditions();
   }

   public void PlayGame()
   {
      mainMenuObject.SetActive(false);
      playerObject.SetActive(true);
      coinsObject.SetActive(true);
      wallsObject.SetActive(true);
   }

   public void CheckWinConditions()
   {
      if(coinScript.coinAmount == 0) 
      {
         playerObject.SetActive(false);
         coinsObject.SetActive(false);
         wallsObject.SetActive(false);
         winMenuObject.SetActive(true);

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
}