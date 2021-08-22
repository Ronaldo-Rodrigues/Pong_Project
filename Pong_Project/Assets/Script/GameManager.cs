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

    public GameObject countDown;

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
   
}
