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

    void Start()
    {
      ResetRound();
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
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
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
   
}
