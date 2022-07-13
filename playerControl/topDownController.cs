using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topDownController : MonoBehaviour
{
    //members
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public float walkSpeed;
    public float frameRate;
    private float idleTime;

    //lists for each sprite direction
    public List<Sprite> idleSprite;
    public List<Sprite> towardsSprite;
    public List<Sprite> awaySprite;
    public List<Sprite> leftSprite;
    public List<Sprite> rightSprite;

    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get direction
        direction = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //set velocity based on direction
        body.velocity = direction * walkSpeed;

        handleSpriteFlips();
        setSprite();
    }

    void handleSpriteFlips()
    {
        if (!(sprite.flipX) && (direction.x < 0))
        {
            sprite.flipX = true;
        }
        else if ((sprite.flipX) && (direction.x > 0))
        {
            sprite.flipX = false;
        }

    }

    List<Sprite> getSpriteDirection()
    {
        List<Sprite> chosenSprites = null;

        if (direction.y > 0)                                //if moving up
        {
            chosenSprites = awaySprite;
        }
        else if (direction.y < 0)                           //if moving down
        {
            chosenSprites = towardsSprite;
        }
        else if (direction.y == 0 && direction.x == 0)      //if standing still
        {
            chosenSprites = idleSprite;
        }
        else                                                //if moving side to side (since we have flip, we only need to handle right)
        {
            chosenSprites = rightSprite;
        }

        return chosenSprites;
    }


    void setSprite()
    {
        List<Sprite> directionSprites = getSpriteDirection();

        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int frame = (int)((playTime * frameRate) % directionSprites.Count);
            sprite.sprite = directionSprites [frame];
        }
        else
        {
            idleTime = Time.time;
        }
    }
}
