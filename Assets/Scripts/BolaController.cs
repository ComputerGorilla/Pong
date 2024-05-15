using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BolaController : MonoBehaviour
{
    Transform myTrans;
    public Vector2 direction;
    public float speed;
    public int humanScore;
    public int cpuScore;
    public Text playerScoreText;
    public Text CPUScoreText;
    public Text winnerText;
    public Text restartText;
    public float ballTime;
    // Start is called before the first frame update
    void Start()
    {
        myTrans = gameObject.GetComponent<Transform>();
        direction = new Vector2(1, 0);
        humanScore = 0;
        cpuScore = 0;
        UpdateScore();
        winnerText.enabled = false;
        restartText.enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        myTrans.position += new Vector3(direction.x * Time.deltaTime * speed * (1+ballTime), direction.y * Time.deltaTime * speed * (1+ballTime), 0);
        ballTime += (Time.deltaTime * 0.025f);

        if(humanScore >= 7 || cpuScore >= 7){
            if(humanScore >= 7){
                HumanWins(true);
            }
            else{
                HumanWins(false);
            }
        }
    }
    void OnTriggerEnter(Collider other){
        if (other.name == "HumanPlayer" || other.name == "CPUPlayer"){
            direction.x *= -1;
            float deltaYpos = (myTrans.position.y - other.transform.position.y)/3;
            direction.y = deltaYpos;
            direction = direction.normalized;
        }
        if (other.tag == "Bordes"){
            if (other.name == "Borde Inferior" || other.name == "Borde Superior"){
                direction.y *= -1;
            }
            else {
                if(myTrans.position.x < 0){
                    humanScore++;
                    UpdateScore();
                    StartCoroutine(RestartBall());
                }
                else {
                    cpuScore++;
                    UpdateScore();
                    StartCoroutine(RestartBall());
                }
                myTrans.position = new Vector3(0, 0, 0);
            }
        }
    }
    void UpdateScore(){
        playerScoreText.text = humanScore.ToString();
        CPUScoreText.text = cpuScore.ToString();
    }
    IEnumerator RestartBall(){
        myTrans.position = new Vector3(0, 0, 0);
        Vector2 temp = direction;
        temp.y *= (float)0.5;
        temp = temp.normalized;
        direction = Vector2.zero;
        yield return new WaitForSeconds(1);
        direction = temp;
        ballTime = 0;
    }
    void HumanWins(bool humanWin){

        myTrans.position = new Vector3(0, 0, 0);
        direction = Vector2.zero;
        Time.timeScale = 0;
        winnerText.enabled = true;
            if(humanWin){
                winnerText.text = "You Win!";
            }
            else{
                winnerText.text = "You Lose!";
            }
        restartText.enabled = true;

        if(Input.GetKey(KeyCode.R)){
            SceneManager.LoadScene("main");
        }
        else if(Input.GetKey(KeyCode.Q)){
            Application.Quit();
        }
    }
}
