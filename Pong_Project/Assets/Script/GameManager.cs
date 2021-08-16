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

    void Start()
    {
        ResetRound();
    }
    public void PlayerScore()
    {
        _playerScore++;
        this.playerScoreText.text = _playerScore.ToString();
        ResetRound();
    }
    public void ComputerScore()
    {
        _computerScore++;
        this.computerScoreText.text = _computerScore.ToString();
        ResetRound();
    }

    public void ResetRound()
    {
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
        this.playerPaddle.ResetPaddlePosition();
        this.computerPaddle.ResetPaddlePosition();
    }
   
}
