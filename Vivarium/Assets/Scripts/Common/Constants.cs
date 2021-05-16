using UnityEngine;
using System.Collections;

public static class Constants
{

    public const float CHAR_MOVE_SPEED = 10f;
    public const float CHAR_ROTATION_SPEED = 1000f;
    public const float AI_DELAY_BETWEEN_ACTIONS = 0.1f;
    public const float PROJECTILE_SPEED = 25f;
    public const int MAX_CHARACTER_ITEMS = 3;
    public const int MAX_PLAYER_ITEMS = 21;
    public const float CAMERA_FOLLOW_SKEW = 8f;
    public const float PROJECTILE_HEIGHT = 0.5f;
    public const float AOE_CALCULATION_OFFSET = 0.01f;

    public const string PLAYER_CHAR_TAG = "PlayerCharacter";
    public const string ENEMY_CHAR_TAG = "EnemyCharacter";
    public const string LEVEL_CONTAINER_TAG = "LevelContainer";
    public const string TILE_GRID_TAG = "GridTile";
    public const string PLAYER_CONTROLLER_TAG = "PlayerController";
    public const string CANVAS_TAG = "InGameUICanvas";
    public const string MELEE_WEAPON_TAG = "WeaponMelee";
    public const string RANGED_WEAPON_TAG = "WeaponRanged";
    public const string BUTTON_INDICATOR_TAG = "ButtonIndicator";

    //Common sound names.
    public const string GRID_CELL_CLICK_SOUND = "GridCellClick";
    public const string DEATH_SOUND = "Death";
    public const string DAMAGE_TAKEN_SOUND = "DamageTaken";
    public const string WALK_SOUND = "Walk";
    public const string BUTTON_CLICK_SOUND = "ButtonClick";
    public const string EQUIP_SOUND = "ItemEquip";
    public const string CONSUME_SOUND = "Consume";
    public const string QUEEN_BEE_ROAR = "QueenBeeRoar";
    public const string BGM_SOUND = "BGM";
    public const string BOSS_BGM_SOUND = "BossBGM";
}
