using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public GameObject pausedScreen;
    public bool ispaused;
    public GameObject menuScreen;
    public GameObject gameUi;
    public GameObject fruitOnStage;
    public Vector3 fruitsPosition;
    public GameObject[] fruits;
    public bool isFruitOnStage;
    public int pelletsEaten;
    private FileStream fileStream;
    public string filePath = "Highscore.txt";
    public int highScore;
    public TextMeshProUGUI highScoreText;
    public AudioSource audio;
    public AudioClip death;
    public AudioClip chomp;
    public AudioClip fruit;
    public AudioClip ghost;
    public AudioClip cutscene;
    public AudioClip intro;
    public GameObject gameOverScreen;
    public int lives;
    public PlayerControls pacman;
    public static GameManager Instance;

    public int score = 0;

    public Transform pellets;

    public TextMeshProUGUI scoreText;

    private int ghostMultiplier = 1;
    public GameObject[] livesImages;

    // Start is called before the first frame update
    void Awake(){
        pellets.gameObject.SetActive(false);
        fruitsPosition = pacman.transform.position;
        if(Instance != null){
            DestroyImmediate(gameObject);
        }
        else {
            Instance = this;
        }
        if(!File.Exists(filePath)){
            using(FileStream fileStream = File.Create(filePath))
            {
                AddText(fileStream, "0");
            }
        } else {
            highScore = Convert.ToInt32(File.ReadAllText(filePath));
            highScoreText.text = "Highscore: " + highScore;
        } 

        gameOverScreen.SetActive(false);
        audio = Camera.main.GetComponent<AudioSource>();
        Time.timeScale = 0;
    }

    private void AddText(FileStream fs, string value){
        byte[] data = new UTF8Encoding(true).GetBytes(value);
        fs.Write(data, 0, data.Length);
    }
    

    public void PelletEaten(Pellet pellet){
        pelletsEaten++;
        audio.PlayOneShot(chomp);
        pellet.gameObject.SetActive(false);
        UpdateScore(pellet.points);
        if(!HasRemainingPellets()){
            audio.PlayOneShot(intro);
            Invoke("NewRound", 5);
        }
        if(pelletsEaten == 70 || pelletsEaten == 170){
            FruitCreate();
        }
    }

    void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if(score > highScore){
            highScoreText.text = "Highscore: " + score;
        }
    }

    private bool HasRemainingPellets(){
        foreach(Transform pellet in pellets){
            if(pellet.gameObject.activeSelf){
                return true;
            }
        } 
        return false;       
    }

    void NewRound(){
        foreach(Transform pellet in pellets){
            pellet.gameObject.SetActive(true);
        }
        pelletsEaten = 0;
        ResetState();
    }

    void ResetState(){
        for(int i = 0; i < ghosts.Length; i++){
            ghosts[i].ResetState();
        }
        isFruitOnStage = false;
        Destroy(fruitOnStage);
        pacman.ResetState();
    }

    public void PowerPelletEaten(PowerPellet pellet){
        audio.PlayOneShot(cutscene);
        for(int i = 0; i < ghosts.Length; i++){
            ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke("ResetGhostMultiplier");
        Invoke("ResetGhostMultiplier", pellet.duration);
    }

    void ResetGhostMultiplier(){
        ghostMultiplier = 1;
    }

    public void GhostEaten(Ghost ghost){
        audio.PlayOneShot(this.ghost);
        int points = ghost.points * ghostMultiplier;
        UpdateScore(points);
        ghostMultiplier++;
    }

    public void PacmanEaten(){
        audio.PlayOneShot(death);
        lives--;
        livesImages[lives].SetActive(false);
        pacman.DeathSequence();
        if(lives > 0){
            Invoke("ResetState", 3);
        }
        else{
            GameOver();
        }
    }

    void GameOver(){
        for(int i = 0; i < ghosts.Length; i++){
            ghosts[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        if(score > highScore){
            highScore = score;
            File.WriteAllText(filePath, highScore.ToString());
        }
    }

    public void RestartGame(){
        audio.PlayOneShot(intro);
        score = 0;
        lives = 3;
        Invoke("NewRound", 5);
        scoreText.text = "Score: " + score;
        for(int i = 0; i < lives; i++){
            livesImages[i].SetActive(true);
        }
        gameOverScreen.SetActive(false);
    }

    public void FruitEat(Fruits fruit){
        UpdateScore(fruit.points);
        Destroy(fruit.gameObject);
        audio.PlayOneShot(this.fruit);
        isFruitOnStage = false;

    }

    void FruitCreate(){
        if(!isFruitOnStage){
            int index = UnityEngine.Random.Range(0, fruits.Length);
            fruitOnStage = Instantiate(fruits[index], fruitsPosition, fruits[index].transform.rotation);
            isFruitOnStage = true;
        }
    }

    public void StartGame(){
        pellets.gameObject.SetActive(true);
        NewRound();
        Time.timeScale = 1;
        menuScreen.SetActive(false);
        gameUi.SetActive(true);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void Pause(){
        pellets.gameObject.SetActive(ispaused);
        for(int i = 0; i < ghosts.Length; i++){
            ghosts[i].gameObject.SetActive(ispaused);
        }
        pacman.gameObject.SetActive(ispaused);
        if(fruitOnStage){
            fruitOnStage.SetActive(ispaused);
        }
        if(ispaused){
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
        ispaused = !ispaused;
        pausedScreen.SetActive(ispaused);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }


}
