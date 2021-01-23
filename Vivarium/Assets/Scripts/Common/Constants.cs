using UnityEngine;
using System.Collections;

public static class Constants
{

    public const float CHAR_MOVE_SPEED = 10f;
    public const float CHAR_ROTATION_SPEED = 700f;
    public const float AI_DELAY_BETWEEN_ACTIONS = 0.5f;
    public const float PROJECTILE_SPEED = 25f;
    public const int MAX_CHARACTER_ITEMS = 3;
    public const int MAX_PLAYER_ITEMS = 100;
    public const float CAMERA_FOLLOW_SKEW = 8f;

    public const string PLAYER_CHAR_TAG = "PlayerCharacter";
    public const string ENEMY_CHAR_TAG = "EnemyCharacter";
    public const string LEVEL_CONTAINER_TAG = "LevelContainer";
    public const string TILE_GRID_TAG = "GridTile";
    public const string PLAYER_CONTROLLER_TAG = "PlayerController";

    //Common sound names.
    public const string GRID_CELL_CLICK_SOUND = "GridCellClick";
    public const string DEATH_SOUND = "Death";
    public const string DAMAGE_TAKEN_SOUND = "DamageTaken";
    public const string WALK_SOUND = "Walk";
    public const string BUTTON_CLICK_SOUND = "ButtonClick";
    public const string EQUIP_SOUND = "ItemEquip";
    public const string CONSUME_SOUND = "Consume";
}
