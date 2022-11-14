using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToe : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button button;
    [SerializeField] private RectTransform rectTransform;
    private const float Offset = 2f;
    private Button[,] _myButtons;
    private int _index;
    private readonly int[,] _grid = new int[3, 3];
    private int _hor, _ver;
    private int _horLast, _verLast;

    void Start()
    {
        newGameButton.onClick.AddListener(StartNewGame);
        BuildGrid();
        StartNewGame();
    }

    void StartNewGame()
    {
        newGameButton.interactable = false;
        _ver = 0;
        _hor = 0;
        _horLast = 100;
        _verLast = 100;
        _index = UnityEngine.Random.Range(0, 2);
        SetInfo(_index);
        foreach (Button b in _myButtons)
        {
            b.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
            b.interactable = true;
        }

        ResetButtons();
    }

    void ResetButtons()
    {
        int i = 0;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                _myButtons[y, x].onClick.RemoveAllListeners();
                Button tmpButton = _myButtons[y, x];
                int tmpI = -(i + 1);
                _myButtons[y, x].onClick.AddListener(() =>
                {
                    SetButton(tmpButton);
                    SetGrid(tmpI);
                });
                _grid[y, x] = tmpI;
                i++;
            }
        }
    }

    void SetInfo(int id)
    {
        infoText.text = id == 1 ? "X" : "O";
    }

    void BuildGrid()
    {
        float sizeX = button.GetComponent<RectTransform>().sizeDelta.x + Offset;
        float sizeY = button.GetComponent<RectTransform>().sizeDelta.y + Offset;
        float posX = -sizeX * 3 / 2 - sizeX / 2;
        float posY = Math.Abs(posX);
        float xReset = posX;
        int i = 0;
        _myButtons = new Button[3, 3];
        for (int y = 0; y < 3; y++)
        {
            posY -= sizeY;
            for (int x = 0; x < 3; x++)
            {
                posX += sizeX;
                _myButtons[y, x] = Instantiate(button);
                RectTransform buttonRectTransform = _myButtons[y, x].GetComponent<RectTransform>();
                buttonRectTransform.SetParent(rectTransform);
                buttonRectTransform.localScale = Vector3.one;
                buttonRectTransform.anchoredPosition = new Vector2(posX, posY);
                buttonRectTransform.name = "Button Id_" + i;
                i++;
            }

            posX = xReset;
        }
    }

    void CheckHorizontal(int id)
    {
        if (id == 0 && _horLast == 100 || id == 1 && _horLast == 100)
        {
            _horLast = id;
            _hor++;
        }
        else if (_horLast == id)
        {
            _hor++;
        }
        else
        {
            _hor = 0;
        }

        if (_hor == 3)
        {
            Winner();
        }
    }

    void CheckVertical(int id)
    {
        if (id == 0 && _verLast == 100 || id == 1 && _verLast == 100)
        {
            _verLast = id;
            _ver++;
        }
        else if (_verLast == id)
        {
            _ver++;
        }
        else
        {
            _ver = 0;
        }

        if (_ver == 3)
        {
            Winner();
        }
    }

    void Winner()
    {
        newGameButton.interactable = true;
        infoText.text = _index == 1 ? "O won!" : "X won!";
        foreach (Button b in _myButtons)
        {
            b.interactable = false;
        }
    }

    void Tie()
    {
        newGameButton.interactable = true;
        infoText.text = "Tie!";
        newGameButton.interactable = true;
        foreach (Button b in _myButtons)
        {
            b.interactable = false;
        }
    }

    void CheckResult()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                CheckHorizontal(_grid[y, x]);
                CheckVertical(_grid[x, y]);
            }

            _hor = 0;
            _ver = 0;
            _horLast = 100;
            _verLast = 100;
        }

        if (_grid[0, 0] == 0 & _grid[1, 1] == 0 & _grid[2, 2] == 0 || _grid[0, 0] == 1 & _grid[1, 1] == 1 &
                                                                   _grid[2, 2] == 1
                                                                   || _grid[2, 0] == 0 & _grid[1, 1] == 0 &
                                                                   _grid[0, 2] == 0 || _grid[2, 0] == 1 &
                                                                   _grid[1, 1] == 1 & _grid[0, 2] == 1)
        {
            Winner();
        }

        int end = 0;
        foreach (int ind in _grid)
        {
            if (ind == 0 | ind == 1)
            {
                end++;
            }

            if (end == _grid.Length)
            {
                Tie();
            }
        }
    }

    void SetGrid(int j)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (_grid[y, x] == j)
                {
                    _grid[y, x] = _index;
                }
            }
        }

        CheckResult();
    }

    void SetButton(Button but)
    {
        but.GetComponentInChildren<TextMeshProUGUI>().text = infoText.text;
        _index = _index == 1 ? 0 : 1;
        SetInfo(_index);
        but.interactable = false;
    }
}