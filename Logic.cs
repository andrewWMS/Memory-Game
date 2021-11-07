using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public Text scoreCounter;
    public int score = 0;
    public GameObject isCurrentlySelected, isTryingToPair;

    public GameObject[] G_Object1;
    public GameObject[] G_Object2;
    public GameObject[] G_Object3;
    public GameObject[] SpawnPoint;
    public List<int> TakeList = new List<int>();
    public int randomnumber;
    public int pileNumber;
    public int player1points, player2points;
    public Text player1, player2;
    public bool isPlayer1, isPlayer2;
    public GameObject player1Cover, player2Cover;
    public int cardsUncovered;
    public GameObject player1Won, player2Won, draw;
    public bool isEasy, isMedium, isHard;
    public Dropdown dropdown;
    public bool isMenu;

    public float timeLeft;
    private float minutes, seconds;
    public Text time;

    void Start()
    {
        if (!isMenu)
        {
            pileNumber = Random.Range(1, 4);
            RandomNumber();
            if (isPlayer1)
            {
                player1Cover.SetActive(false);
            }
            else if (isPlayer2)
            {
                player2Cover.SetActive(false);
            }
        }
    }
    void Update()
    {
        if (!isMenu)
        {
            player1.text = player1points.ToString();
            player2.text = player2points.ToString();
            timeLeft += 1 * Time.deltaTime;
            minutes = Mathf.FloorToInt(timeLeft / 60);
            seconds = Mathf.FloorToInt(timeLeft % 60);
            time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void SelectCard()
    {
        if (!isCurrentlySelected)
        {
            isCurrentlySelected = EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().thisCard;
            Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().thisCard.tag);
            EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().isSelected = true;
            EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().thisCard.GetComponent<Animation>().Play("OpenCard");
        }
        else if (isCurrentlySelected)
        {
            isTryingToPair = EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().thisCard;
            if (isCurrentlySelected == isTryingToPair)
            {
                isTryingToPair.GetComponent<Animation>().Play("CloseCard");
                isCurrentlySelected = null;
                isTryingToPair = null;
            }
            else if(isCurrentlySelected != isTryingToPair)
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().isSelected = true;
                EventSystem.current.currentSelectedGameObject.GetComponent<CardInfo>().thisCard.GetComponent<Animation>().Play("OpenCard");
                if (isCurrentlySelected.tag == isTryingToPair.tag)
                {
                    Destroy(isCurrentlySelected.GetComponent<ButtonDestroyer>().button, 0.5f);
                    Destroy(isTryingToPair.GetComponent<ButtonDestroyer>().button, 0.5f);
                    Destroy(isCurrentlySelected.gameObject, 0.5f);
                    Destroy(isTryingToPair.gameObject, 0.5f);
                    if (isPlayer1)
                    {
                        AddPointsToPlayer1();
                    }
                    else if (isPlayer2)
                    {
                        AddPointsToPlayer2();
                    }
                    cardsUncovered += 2;
                    if (isEasy && cardsUncovered >= 8 || isMedium && cardsUncovered >= 12 || isHard && cardsUncovered >= 18)
                    {
                        if (player1points > player2points)
                        {
                            player1Won.SetActive(true);
                        }
                        else if (player2points > player1points)
                        {
                            player2Won.SetActive(true);
                        }
                        else if (player2points == player1points)
                        {
                            draw.SetActive(true);
                        }
                    }
                    SwitchTurn();
                }
                else if (isCurrentlySelected.tag != isTryingToPair.tag)
                {
                    Invoke("CloseBothCards", 0.3f);
                }
            }
        }
    }
    public void CloseBothCards()
    {
        isCurrentlySelected.GetComponent<Animation>().Play("CloseCard");
        isTryingToPair.GetComponent<Animation>().Play("CloseCard");
        isCurrentlySelected = null;
        isTryingToPair = null;
        SwitchTurn();
    }
    public void BothSelected()
    {

    }
    public void RandomNumber()
    {
        if(pileNumber == 1)
        {
            TakeList = new List<int>(new int[G_Object1.Length]);

            for (int i = 0; i < G_Object1.Length; i++)
            {
                randomnumber = UnityEngine.Random.Range(1, (SpawnPoint.Length) + 1);
                while (TakeList.Contains(randomnumber))
                {
                    randomnumber = UnityEngine.Random.Range(1, (SpawnPoint.Length) + 1);
                }
                TakeList[i] = randomnumber;
                GameObject spawnedCard = Instantiate(G_Object1[i], SpawnPoint[(TakeList[i] - 1)].transform) as GameObject;
                if (isEasy) 
                {
                    spawnedCard.transform.localScale = Vector3.one;
                }
                else if (isMedium || isHard)
                {
                    spawnedCard.transform.localScale = new Vector3(0.82f, 0.82f, 0.82f);
                }
                spawnedCard.transform.localRotation = Quaternion.Euler(0, 0, 0);
                spawnedCard.transform.localPosition = Vector3.zero;
            }
        }
        else if (pileNumber == 2)
        {
            TakeList = new List<int>(new int[G_Object2.Length]);

            for (int i = 0; i < G_Object2.Length; i++)
            {
                randomnumber = UnityEngine.Random.Range(1, (SpawnPoint.Length) + 1);
                while (TakeList.Contains(randomnumber))
                {
                    randomnumber = UnityEngine.Random.Range(1, (SpawnPoint.Length) + 1);
                }
                TakeList[i] = randomnumber;
                GameObject spawnedCard = Instantiate(G_Object2[i], SpawnPoint[(TakeList[i] - 1)].transform) as GameObject;
                if (isEasy)
                {
                    spawnedCard.transform.localScale = Vector3.one;
                }
                else if (isMedium || isHard)
                {
                    spawnedCard.transform.localScale = new Vector3(0.82f, 0.82f, 0.82f);
                }
                spawnedCard.transform.localRotation = Quaternion.Euler(0, 0, 0);
                spawnedCard.transform.localPosition = Vector3.zero;
            }
        }
        else if (pileNumber == 3)
        {
            TakeList = new List<int>(new int[G_Object2.Length]);

            for (int i = 0; i < G_Object3.Length; i++)
            {
                randomnumber = UnityEngine.Random.Range(1, (SpawnPoint.Length) + 1);
                while (TakeList.Contains(randomnumber))
                {
                    randomnumber = UnityEngine.Random.Range(1, (SpawnPoint.Length) + 1);
                }
                TakeList[i] = randomnumber;
                GameObject spawnedCard = Instantiate(G_Object3[i], SpawnPoint[(TakeList[i] - 1)].transform) as GameObject;
                if (isEasy)
                {
                    spawnedCard.transform.localScale = Vector3.one;
                }
                else if (isMedium || isHard)
                {
                    spawnedCard.transform.localScale = new Vector3(0.82f, 0.82f, 0.82f);
                }
                spawnedCard.transform.localRotation = Quaternion.Euler(0, 0, 0);
                spawnedCard.transform.localPosition = Vector3.zero;
            }
        }
    }
    public void AddPointsToPlayer1()
    {
        player1points++;
    }
    public void AddPointsToPlayer2()
    {
        player2points++;
    }
    public void SwitchTurn()
    {
        isPlayer1 = !isPlayer1;
        isPlayer2 = !isPlayer2;
        player1Cover.SetActive(!isPlayer1);
        player2Cover.SetActive(!isPlayer2);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlayEasy()
    {
        SceneManager.LoadScene("FourTimesTwo");
    }
    public void PlayMedium()
    {
        SceneManager.LoadScene("SixTimesTwo");
    }
    public void PlayHard()
    {
        SceneManager.LoadScene("SixTimesThree");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        if(dropdown.options[dropdown.value].text == "EASY")
        {
            PlayEasy();
        }
        else if (dropdown.options[dropdown.value].text == "MEDIUM")
        {
            PlayMedium();
        }
        else if (dropdown.options[dropdown.value].text == "HARD")
        {
            PlayHard();
        }
        else
        {
            Debug.Log("Majmunee");
        }
    }
}
