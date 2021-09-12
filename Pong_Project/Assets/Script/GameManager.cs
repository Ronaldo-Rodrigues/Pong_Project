using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;

    //UI DE SCORES
    private int _playerScore, _computerScore;
    public Text playerScoreText, computerScoreText;

    //PADDLES E CONTROLE DE PADDLES
    public Paddle playerPaddle, computerPaddle;
    public GameObject resetPlayerPaddle, resetCompPaddle;

    //UI DE MAGIAS
    public Image pfireMagicOFF, paquaMagicOFF, 
        pgrassMagicOFF, cfireMagicOFF, caquaMagicOFF, cgrassMagicOFF;
    

    //UI DE GAME OVER
    public GameObject gameOverUI;
    private Button _startBTN;
    public bool isGameOver = true;
    public bool deuInicio;
    public Text quadroResult;
    public Text placarResult;

    //OBJ PARA COUNTDOWN ANTES DE CADA RODADA
    public GameObject countDown;
    public bool countdownIsCreated = false;

    //SONS
    public AudioSource musicBG;
    public AudioClip sparkleClip;
    public AudioClip loseClip;
    public AudioClip winClip;

    //CAMERA SHAKE
    Vector3 _cameraInicialPosition;
    public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
    public Camera mainCamara;


    //=========================================================
    void Start()
    {
        musicBG = GameObject.Find("BgMusic").GetComponent<AudioSource>();
        _startBTN = GameObject.Find("StartBtn").GetComponent<Button>();
        gameOverUI = GameObject.Find("GameOverPanel");
        quadroResult = GameObject.Find("QuemGanhou").GetComponent<Text>();
        placarResult = GameObject.Find("Ultimo Placar").GetComponent<Text>();
        deuInicio = false;

    }
    private void Update()
    {
        PlayerMagicDisplay();
        ComputerMagicDisplay();
        GameHasWinner();
        GameOverUI();

       
        if (deuInicio == false && Input.GetKey(KeyCode.Space))
        {
            StartGameBtn();
        }

    }



    //=====================================================
    
    //FUNÇÃO PARA PONTUÇÕES
    public void PlayerScore()
    {
        _playerScore++;
        AudioManager.instance.Sparkle(sparkleClip);
        PlayerPrefs.SetInt("playerPlacar", _playerScore);
        this.playerScoreText.text = _playerScore.ToString();
        deuInicio = false;
        ResetRound();
    }
    public void ComputerScore()
    {
        _computerScore++;

        AudioManager.instance.Sparkle(loseClip);
        PlayerPrefs.SetInt("computerPlacar", _computerScore);
        this.computerScoreText.text = _computerScore.ToString();
        deuInicio = false;
        ResetRound();
    }

    //FUNÇÃO PARA SABER SE ALGUEM GANHOU
    public void GameHasWinner()
    {
        if (_computerScore == 3 )
        {
            PlayerPrefs.SetInt("computerPlacar", _computerScore);
            PlayerPrefs.SetInt("playerPlacar", _playerScore);
            this.ball.ResetPosition();

           
            PlayerPrefs.SetString("Resultado", "Winner!");
            musicBG.Stop();
            isGameOver = true;

        }
        if (_playerScore == 3)
        {
            PlayerPrefs.SetInt("computerPlacar", _computerScore);
            PlayerPrefs.SetInt("playerPlacar", _playerScore);
            this.ball.ResetPosition();
                    
            PlayerPrefs.SetString("Resultado", "Winner!");
            
            isGameOver = true;
            musicBG.Stop();

        }
        else { return; }

        var telaSuja = GameObject.Find("FloatingParent(Clone)");
        if(telaSuja != null)
        {
            Destroy(telaSuja.gameObject);
        }
        else { return; }
    }
    //FUNÇÃO DE RESETAR A PARTIDA
    public void ResetRound()
    {
        deuInicio = true;
        ResetPaddles();
        var findCountDown = GameObject.Find("CountDownParent(Clone)");
        if (findCountDown != null)
        {
            Destroy(findCountDown.gameObject);
        }
        countdownIsCreated = false;
        this.ball.ResetPosition();
        
        if (countdownIsCreated == false)
        {
            Instantiate(countDown, new Vector2(0, 0), Quaternion.identity);
            countdownIsCreated = true;
        }
        else { return; }
    }

    //FUNÇÃO DE RESETAR OS PADDLES
    public void ResetPaddles()
    {
        
        resetPlayerPaddle = GameObject.FindGameObjectWithTag("Player Paddle");
        var resetPlayer = resetPlayerPaddle.GetComponent<Player_Paddle>();
        resetCompPaddle = GameObject.FindGameObjectWithTag("Computer Paddle");
        var resetComp = resetCompPaddle.GetComponent<Computer_Padle>();
        resetComp.ResetCompPaddle();
        resetPlayer.PlayerPaddleReset();
    }

    //FORÇA INICIAL NA BOLA
    public void ForcaInicial()
    {
        this.ball.AddStartingForce();
    }

    //UI: DISPLAY DE GAMEOVER
    public void GameOverUI()
    {
        _startBTN.onClick.AddListener(StartGameBtn);
        AudioManager.instance.Result(winClip); 
        if (isGameOver == true)
        {
            gameOverUI.SetActive(true);
            deuInicio = false;        
            Time.timeScale = 0;
        }
        if (isGameOver == false && deuInicio == false)
        {
            gameOverUI.SetActive(false);
            Time.timeScale = 1;
            ResetRound();  
        }

            quadroResult.text = PlayerPrefs.GetString("Resultado").ToString();
        placarResult.text = PlayerPrefs.GetInt("computerPlacar") + " x " + PlayerPrefs.GetInt("playerPlacar").ToString();

    }

    //Botao de Start
    public void StartGameBtn()
    {
        isGameOver = false;
        AudioManager.instance.Sparkle(sparkleClip);
        musicBG.Play();

        this._computerScore = 0;
        this._playerScore = 0;
        this.playerScoreText.text = _playerScore.ToString();
        this.computerScoreText.text = _computerScore.ToString();
    }

    //UI: DISPLAY DE MAGIAS DO PLAYER
    public void PlayerMagicDisplay()
    {
        var playerP = playerPaddle.GetComponent<Player_Paddle>();

        //Aqua
        if (playerP.canCastAqua == false)
        {
            paquaMagicOFF.enabled = true;
        }
        else if (playerP.canCastMagic == false)
        {
            paquaMagicOFF.enabled = true;
        }
        else { paquaMagicOFF.enabled = false; }
        //Fire
        if (playerP.canCastFire == false)
        {
            pfireMagicOFF.enabled = true;
        }
        else if (playerP.canCastMagic == false)
        {
            pfireMagicOFF.enabled = true;
        }
        else { pfireMagicOFF.enabled = false; }
        //Grass
        if (playerP.canCastGrass == false)
        {
            pgrassMagicOFF.enabled = true;
        }
        else if (playerP.canCastMagic == false)
        {
            pgrassMagicOFF.enabled = true;
        }
        else { pgrassMagicOFF.enabled = false; }
    }

    //UI: DISPLAY DE MAGIAS DO PLAYER
    public void ComputerMagicDisplay()
    {
        var computerP = computerPaddle.GetComponent<Computer_Padle>();
        //Aqua
        if (computerP.canCastAqua == false)
        {
            caquaMagicOFF.enabled = true;
        }
        else if (computerP.canCastMagic == false)
        {
            caquaMagicOFF.enabled = true;
        }
        else { caquaMagicOFF.enabled = false; }
        //Fire
        if (computerP.canCastFire == false)
        {
            cfireMagicOFF.enabled = true;
        }
        else if (computerP.canCastMagic == false)
        {
            cfireMagicOFF.enabled = true;
        }
        else { cfireMagicOFF.enabled = false; }
        //Grass
        if (computerP.canCastGrass == false)
        {
            cgrassMagicOFF.enabled = true;
        }
        else if (computerP.canCastMagic == false)
        {
            cgrassMagicOFF.enabled = true;
        }
        else { cgrassMagicOFF.enabled = false; }
    }

    //EFEITOS DE CAMERA SHAKE
    public void ShakeIt()
    {
        _cameraInicialPosition = mainCamara.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.08f);
        Invoke("StopCameraShaking", shakeTime);
    }
    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2f - shakeMagnetude;
        float cameraShakingOffsetY = 0.52f * shakeMagnetude * 2f - shakeMagnetude;
        Vector3 cameraIntermediatePosition = mainCamara.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamara.transform.position = cameraIntermediatePosition;
    }
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamara.transform.position = _cameraInicialPosition;
    }
}
