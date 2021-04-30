using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{

    public int squareIndex { get; set; }

    private AlphabetData.LetterData _normalLetterData;
    private AlphabetData.LetterData _selectedLetterData;
    private AlphabetData.LetterData _correctLetterData;

    private SpriteRenderer displayedImage;

    private bool _selected;
    private bool _clicked;
    private int _index = -1;
    private bool _correct;

    public void SetIndex(int index)
    {
        _index = index;
    }

    public int GetIndex()
    {
        return _index;
    }

    void Start()
    {
        _selected = false;
        _clicked = false;
        _correct = false;
        displayedImage = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GameEvents.OnEnableSquareSelection += OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        GameEvents.OnSelectSquare += SelectSquare;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableSquareSelection -= OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        GameEvents.OnSelectSquare -= SelectSquare;
    }

    public void OnEnableSquareSelection()
    {
        _clicked = true;
        _selected = false;
    }

    public void OnDisableSquareSelection()
    {
        _clicked = false;
        _selected = false;

        if (_correct == true)
            displayedImage.sprite = _correctLetterData.image;
        else
            displayedImage.sprite = _normalLetterData.image;
    }

    private void SelectSquare(Vector3 position)
    {
        if (this.gameObject.transform.position == position)
            displayedImage.sprite = _selectedLetterData.image;
    }

    public void SetSprite(AlphabetData.LetterData normalLetterData, AlphabetData.LetterData selectedLetterData, AlphabetData.LetterData correctLetterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = normalLetterData.image;
    }

    private void OnMouseDown()
    {
        OnEnableSquareSelection();
        GameEvents.EnableSquareSelectionMethod();
        CheckSquare();
        displayedImage.sprite = _selectedLetterData.image;
    }

    private void OnMouseEnter()
    {
        CheckSquare();
    }

    private void OnMouseUp()
    {
        GameEvents.ClearSelectionMethod();
        GameEvents.DisableSquareSelectionMethod();
    }

    public void CheckSquare()
    {
        if (_selected == false && _clicked == true)
        {
            _selected = true;
            GameEvents.CheckSquareMethod(_normalLetterData.letter, gameObject.transform.position, _index);
        }
    }
}
