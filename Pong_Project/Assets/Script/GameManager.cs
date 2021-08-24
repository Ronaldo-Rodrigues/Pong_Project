using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;

    private int _playerScore, _computerScore;

    public Text playerScoreText, computerScoreText;

    public Paddle playerPaddle, computerPaddle;
    public GameObject resetPlayerPaddle, resetCompPaddle;

    public Image pfireMagicOFF, paquaMagicOFF, pgrassMagicOFF, cfireMagicOFF, caquaMagicOFF, cgrassMagicOFF;

    public GameObject countDown;

   
    public bool isGoingUP = false, isGoingDown = false;

    void Start()
    {
        ResetRound();
    }
    private void Update()
    {
        PlayerMagicDisplay();
        ComputerMagicDisplay();
    }
    public void PlayerMagicDisplay()
    {
        var playerP = playerPaddle.GetComponent<Player_Paddle>();

        if (playerP.canCastAqua == false)
        {
            paquaMagicOFF.enabled = true;
        }
        else if (playerP.canCastMagic == false)
        {
            paquaMagicOFF.enabled = true;
        }
        else { paquaMagicOFF.enabled = false; }
        


       if (playerP.canCastFire == false )
        {
            pfireMagicOFF.enabled = true;
        }
        else if(playerP.canCastMagic == false)
        {
            pfireMagicOFF.enabled = true;
        }
        else{ pfireMagicOFF.enabled = false; }



        if (playerP.canCastGrass == false)
        {
            pgrassMagicOFF.enabled = true;
        }
       else if(playerP.canCastMagic == false)
        {
            pgrassMagicOFF.enabled = true;
        }
        else{ pgrassMagicOFF.enabled = false;}
    }
    public void ComputerMagicDisplay()
    {
        var computerP = computerPaddle.GetComponent<Computer_Padle>();

        if (computerP.canCastAqua == false)
        {
            caquaMagicOFF.enabled = true;
        }
        else if (computerP.canCastMagic == false)
        {
           caquaMagicOFF.enabled = true;
        }
        else { caquaMagicOFF.enabled = false; }



        if (computerP.canCastFire == false)
        {
            cfireMagicOFF.enabled = true;
        }
        else if (computerP.canCastMagic == false)
        {
            cfireMagicOFF.enabled = true;
        }
        else { cfireMagicOFF.enabled = false; }



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

    public void PlayerScore()
    {
        
        _playerScore++;
        this.playerScoreText.text = _playerScore.ToString();
        ResetPaddles();
        ResetRound();
    }
    public void ComputerScore()
    {
        _computerScore++;
        this.computerScoreText.text = _computerScore.ToString();
        ResetPaddles();
        ResetRound();
    }

    public void ResetRound()
    {
        var findCountDown = GameObject.FindGameObjectWithTag("Count Down");
        
            if (findCountDown != null)
            {
                Destroy(findCountDown.gameObject);
            }
        
        Instantiate(countDown, new Vector2(0, 0), Quaternion.identity);
        this.ball.ResetPosition();
        this.playerPaddle.ResetPaddlePosition();
        this.computerPaddle.ResetPaddlePosition();
    }

    public void ResetPaddles()
    {
        resetPlayerPaddle = GameObject.FindGameObjectWithTag("Player Paddle");
        var resetPlayer = resetPlayerPaddle.GetComponent<Player_Paddle>();
        resetCompPaddle = GameObject.FindGameObjectWithTag("Computer Paddle");
        var resetComp = resetCompPaddle.GetComponent<Computer_Padle>();
        resetComp.ResetCompPaddle();
        resetPlayer.PlayerPaddleReset();
    }

    public void forcaInicial()
    {
        this.ball.AddStartingForce();
    }


    public void buttonUp()
    {
        isGoingUP = true;
        isGoingDown = false;
    }
    public void buttonDown()
    {
        isGoingDown = true;
        isGoingUP = false;
    }
}
