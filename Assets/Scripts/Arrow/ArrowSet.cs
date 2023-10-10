using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSet : MonoBehaviour
{
    public Arrow prefab;
    public int amount = 5;
    private List<Arrow> arrows;
    private AudioSource audioSource;
    
    private int pos = 0;
    private bool complete = false;

    private float stopTime = 0f; 
        
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        drawArrows();
        resetArrows();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (audioSource.time > stopTime && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        if (Input.GetKeyDown("space"))
        {
            pos = 0;
            complete = false;
            resetArrows();
            drawArrows();
        }

        if (arrows.Count < pos+1)
        {
            return;
        }
        Arrow currentArrow = arrows[pos];

        string keyPressed = "";
        if (Input.GetKeyDown("up"))
        {
            keyPressed = "up";
            playAudio(0f, 0.3f);
        }
        else if (Input.GetKeyDown("down"))
        {
            keyPressed = "down";
            playAudio(0.5f, 0.8f);
        }
        else if (Input.GetKeyDown("left"))
        {
            keyPressed = "left";
            playAudio(0.95f, 1.2f);
        }
        else if (Input.GetKeyDown("right"))
        {
            keyPressed = "right";
            playAudio(1.5f, 1.7f);
        }
        else
        {
            return;
        }

        if (keyPressed.Equals(currentArrow.getDirectionString()))
        {
            currentArrow.setPressed(true);
            pos++;
            if (arrows.Count < pos + 1)
            {
                complete = true;
                GameObject.Find("Battle System").GetComponent<BattleSystem>().SuccessSkillAttack();
                Destroy(this.gameObject);
            }
        } else
        {
            GameObject.Find("Battle System").GetComponent<BattleSystem>().FailSkillAttack();
            Destroy(this.gameObject);
        }
    }
    private void playAudio(float start, float end)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        audioSource.time = start;
        audioSource.Play();
        stopTime = end;
    }
    private void drawArrows()
    {
        //TODO
        arrows = new List<Arrow>();
        for (int ctr = 0; ctr < amount; ctr++)
        {
            float width = 2.0f * (this.amount - 1);
            Vector3 centering = new Vector3(-width / 3.3f, 0);
            Vector3 rowPosition = new Vector3(centering.x, centering.y, 2.0f);
            Vector3 position = rowPosition;
            position.x += ctr * 1.2f;
            Arrow arrow = Instantiate(this.prefab, this.transform);

            //someOtherSprite = Resources.Load<Sprite>("arrow_ok");
            //arrow.sprite = Resources.Load<Sprite>(newImageTitle);

            arrow.randomizeDirection();
            arrow.transform.localPosition = position;
            arrows.Add(arrow);
        }

        string strs = "";
        foreach (Arrow a in arrows)
        {
            strs += a.getDirectionString() + ", ";
        }
        Debug.Log(strs);
    }

    private void resetArrows()
    {
        foreach (Arrow a in arrows)
        {
            a.setPressed(false);
        }
    }
}
