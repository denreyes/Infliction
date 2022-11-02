using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ArrowSet : MonoBehaviour
{
    public Arrow prefab;
    public int amount = 5;
    private List<Arrow> arrows;

    private int pos = 0;
    private bool complete = false;

    void Start()
    {
        drawArrows();
    }


    // Update is called once per frame
    void Update()
    {
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
        }
        else if (Input.GetKeyDown("down"))
        {
            keyPressed = "down";
        }
        else if (Input.GetKeyDown("left"))
        {
            keyPressed = "left";
        }
        else if (Input.GetKeyDown("right"))
        {
            keyPressed = "right";
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
            }
        } else
        {
            pos = 0;
            resetArrows();
        }
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
