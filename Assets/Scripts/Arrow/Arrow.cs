using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    public Direction direction;
    public Sprite spriteNormal;
    public Sprite spritePressed;
    public Sprite spriteInverted;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {         
    }
    public void randomizeDirection()
    {
        int rnd = Random.Range(1, 5); // creates a number between 1 and 4
        if (rnd == 1)
            direction = Direction.Up;
        else if (rnd == 2)
            direction = Direction.Down;
        else if (rnd == 3)
            direction = Direction.Left;
        else
            direction = Direction.Right;

        rotate();
    }

    public void setPressed(bool pressed)
    {
        if (pressed)
        {
            _spriteRenderer.sprite = spritePressed;
        }
        else
        {
            _spriteRenderer.sprite = spriteNormal;
        }
    }
    private void rotate()
    {
        if (direction == Direction.Right)
        {
            transform.Rotate(Vector3.forward * -90);
        }
        else if (direction == Direction.Left)
        {
            transform.Rotate(Vector3.forward * 90);
        }
        else if (direction == Direction.Down)
        {
            transform.Rotate(Vector3.forward * 180);
        }
    }

    public string getDirectionString()
    {
        switch(direction)
        {
            case Direction.Up: return "up";
            case Direction.Down: return "down";
            case Direction.Left: return "left";
            default: return "right";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
