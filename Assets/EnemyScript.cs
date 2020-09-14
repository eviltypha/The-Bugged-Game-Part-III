using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Camera ARCam;
    GameObject[] target, Enemyreference;
    public Text score, Gamestatus;
    public GameObject RestartBtn, fireBtn, quitBtn;
    int points, position;
    float[] enemyspeed = new float[4] { 3f, 3f, 3f, 3f };

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Enemy");
        Enemyreference = GameObject.FindGameObjectsWithTag("initialPos");
        for (int i = 0; i < 4; i++)
        {
            target[i].SetActive(true);
            target[i].transform.position = Enemyreference[i].transform.position;
        }
        position = 0;
        points = 0;
        score.text = "Kills : " + points.ToString();
        Gamestatus.text = "Playing";
        RestartBtn.SetActive(false);
        quitBtn.SetActive(false);
        fireBtn.SetActive(true);
    }

    public void shoot()
    {
        GetComponent<AudioSource>().Play();
        RaycastHit Hit;
        if (Physics.Raycast(ARCam.transform.position, ARCam.transform.forward, out Hit))
        {
            if (Hit.transform.gameObject.CompareTag("Enemy"))
            {
                points++;
                score.text = "Kills : " + points.ToString();
                returntoInitial(Hit.transform.name);
            }
        }
    }
    private void returntoInitial(string targName)
    {
        int i;
        for (i = 0; i < 4; i++)
            if (targName == target[i].name)
                break;
        target[i].transform.position = Enemyreference[position++].transform.position;
        enemyspeed[i] = UnityEngine.Random.Range(2f, 7f);
    }

    void Update()
    {
        if (position == 4)
            position = 0;
        for (int i = 0; i < 4; i++)
        {
            target[i].transform.Translate(new Vector3(0, 0, enemyspeed[i]) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            for (int i = 0; i < 4; i++)
            {
                target[i].SetActive(false);
            }
            RestartBtn.SetActive(true);
            quitBtn.SetActive(true);
            fireBtn.SetActive(false);
            Gamestatus.text = "GAME OVER";
        }
    }

    public void restart()
    {

        for (int i = 0; i < 4; i++)
        {
            target[i].SetActive(true);
            target[i].transform.position = Enemyreference[i].transform.position;
        }
        points = 0;
        score.text = "Kills : " + points.ToString();
        Gamestatus.text = "Playing";
        RestartBtn.SetActive(false);
        quitBtn.SetActive(false);
        fireBtn.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
}
