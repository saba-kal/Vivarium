using System.Collections.Generic;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    private const float STAT_BUFF_AMOUNT = 1000f;

    public static ConsoleCommand<int> SET_LEVEL;
    public static ConsoleCommand INFINITE_MOVE;
    public static ConsoleCommand INFINITE_DAMAGE;
    public static ConsoleCommand REVERT_STATS;

    public float ConsoleInputHeight = 50f;
    public int FontSize = 20;

    private List<object> _commandList;
    private bool _showConsole = false;
    private string _consoleInput;
    private GUIStyle _guiStyle;

    // Use this for initialization
    void Awake()
    {
        _guiStyle = new GUIStyle();
        _guiStyle.fontSize = FontSize;
        _guiStyle.normal.textColor = Color.white;

        SET_LEVEL = new ConsoleCommand<int>("set_level", "Sets the current level.", "set_level <level>", SetLevel);
        INFINITE_MOVE = new ConsoleCommand("infinite_move", "Gives all player characters 1000 move range.", "infinite_move", InfiniteMove);
        INFINITE_DAMAGE = new ConsoleCommand("infinite_damage", "Gives all player characters 1000 damage.", "infinite_damage", InfiniteDamage);
        REVERT_STATS = new ConsoleCommand("revert_stats", "Reverts the infinite move and damage cheat codes.", "revert_stats", RevertStats);

        _commandList = new List<object>
        {
            SET_LEVEL,
            INFINITE_MOVE,
            INFINITE_DAMAGE,
            REVERT_STATS
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            _showConsole = !_showConsole;
        }
    }

    private void OnGUI()
    {
        if (!_showConsole)
        {
            if (Event.current.keyCode == KeyCode.BackQuote)
            {
                _consoleInput = "";
                _showConsole = true;
            }

            return;
        }

        var xPosition = 0f;
        var yPosition = Screen.height - ConsoleInputHeight;

        GUI.Box(new Rect(xPosition, yPosition, Screen.width, ConsoleInputHeight), "");
        GUI.backgroundColor = new Color(0, 0, 0);

        GUI.SetNextControlName("ConsoleTextField");

        _consoleInput = GUI.TextField(
            new Rect(10f, yPosition + 5f, Screen.width - ConsoleInputHeight, ConsoleInputHeight),
            _consoleInput,
            _guiStyle);

        GUI.FocusControl("ConsoleTextField");

        if (Event.current.keyCode == KeyCode.Return)
        {
            OnReturn();
        }
        else if (Event.current.keyCode == KeyCode.Escape)
        {
            GUI.FocusControl(null);
            _showConsole = false;
            _consoleInput = "";
        }
    }

    private void OnReturn()
    {
        if (!_showConsole || string.IsNullOrWhiteSpace(_consoleInput))
        {
            return;
        }

        var args = _consoleInput.Split(' ');

        foreach (var commandObject in _commandList)
        {
            var consoleCommandBase = commandObject as BaseConsoleCommand;
            if (consoleCommandBase == null || !_consoleInput.Contains(consoleCommandBase.Id))
            {
                continue;
            }

            if (commandObject is ConsoleCommand consoleCommand)
            {
                consoleCommand.Invoke();
            }
            else if (commandObject is ConsoleCommand<int> intConsoleCommand && args.Length > 1)
            {
                intConsoleCommand.Invoke(int.Parse(args[1]));
            }
        }

        _consoleInput = "";
    }

    private void SetLevel(int level)
    {
        if (level < 0 || level >= LevelManager.Instance.LevelGenerationProfiles.Count)
        {
            return;
        }

        Debug.Log($"Setting level to {level}.");
        LevelManager.Instance.StartLevel(level);
    }

    private void InfiniteMove()
    {
        Debug.Log("Infinite move activated.");
        foreach (var character in PlayerController.Instance.PlayerCharacters)
        {
            character.MovBuff(STAT_BUFF_AMOUNT);
        }
    }

    private void InfiniteDamage()
    {
        Debug.Log("Infinite damage activated.");
        foreach (var character in PlayerController.Instance.PlayerCharacters)
        {
            character.AtkBuff(STAT_BUFF_AMOUNT);
        }
    }

    private void RevertStats()
    {
        Debug.Log("Reverting character stat cheats.");
        foreach (var character in PlayerController.Instance.PlayerCharacters)
        {
            if (character.Character.MoveRange >= STAT_BUFF_AMOUNT)
            {
                character.MovBuff(-STAT_BUFF_AMOUNT);
            }
            if (character.Character.AttackDamage >= STAT_BUFF_AMOUNT)
            {
                character.AtkBuff(-STAT_BUFF_AMOUNT);
            }
        }
    }
}
