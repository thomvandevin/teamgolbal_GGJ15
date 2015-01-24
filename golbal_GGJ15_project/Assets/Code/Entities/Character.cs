using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity {

    private enum Facing
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    private Facing Direction;    
    private GamePad.Index gamePadIndex;

	public void Initialize (LevelData levelData, Vector2 playerPosition) 
    {
        gamePadIndex = GamePad.Index.One;
        Direction = Facing.RIGHT;

        entityPosition = playerPosition;
        transform.position = entityPosition;

        Initialize(levelData);
	}

	private void Update () 
    {
        if (Input.GetKey(KeyCode.W) && !moving)
            CheckPosition(new Vector2(0, 1));
        if (Input.GetKey(KeyCode.S) && !moving)
            CheckPosition(new Vector2(0, -1));
        if (Input.GetKey(KeyCode.A) && !moving)
            CheckPosition(new Vector2(-1, 0));
        if (Input.GetKey(KeyCode.D) && !moving)
            CheckPosition(new Vector2(1, 0));
	}

    private void Flip()
    {
        if (Direction == Facing.LEFT)
            Direction = Facing.RIGHT;
        else if (Direction == Facing.RIGHT)
            Direction = Facing.LEFT;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }    
}
