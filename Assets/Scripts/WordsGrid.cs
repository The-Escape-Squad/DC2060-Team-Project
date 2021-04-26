using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsGrid : MonoBehaviour
{

    public GameData currentGameData;
    public GameObject gridSquarePrefab;
    public AlphabetData alphabetData;

    public float squareOffset = 0.0f;
    public float topPosition;

    private List<GameObject> squareList = new List<GameObject>();

    void Start()
    {
        CreateGridSquares();
        SetSquaresPosition();
    }

    // loop through squares and set the position
    private void SetSquaresPosition()
    {
        var squareRect = squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = squareList[0].GetComponent<Transform>();

        var offset = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f
        };

        var startPosition = GetFirstSquare();
        int columnNumber = 0;
        int rowNumber = 0;

        foreach( var square in squareList)
        {
            if (rowNumber + 1 > currentGameData.selectedBoardData.Rows)
            {
                columnNumber++;
                rowNumber = 0;
            }

            var positionX = startPosition.x + offset.x * columnNumber;
            var positionY = startPosition.y - offset.y * rowNumber;

            square.GetComponent<Transform>().position = new Vector2(positionX, positionY);
            rowNumber++;
        }
    }

    private Vector2 GetFirstSquare()
    {
        var startPosition = new Vector2(0f, transform.position.y);
        var squareRect = squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = squareList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);


        squareSize.x = squareRect.width * squareTransform.localScale.x;
        squareSize.y = squareRect.height * squareTransform.localScale.y;

        var midWidthPosition = (((currentGameData.selectedBoardData.Columns - 1) * squareSize.x) / 2) * 0.01f;
        var midWidthHieght = (((currentGameData.selectedBoardData.Rows - 1) * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1 : midWidthPosition;
        startPosition.y += midWidthHieght;

        return startPosition;
    }


    // as board is dynamic need to know the size 
    private void CreateGridSquares()
    {
        if(currentGameData != null)
        {
            var squareScale = GetSquareScale(new Vector3(1.5f, 1.5f, 0.1f));
            foreach(var squares in currentGameData.selectedBoardData.Board)
            {
                // loop through all the letters
                foreach(var squareLetter in squares.Row)
                {
                    // lambda function to get all the types of letters 
                    var normalLetterData = alphabetData.AlphabetNormal.Find(data => data.letter == squareLetter);
                    var selectedLetterData = alphabetData.AlphabetHighlighted.Find(data => data.letter == squareLetter);
                    var correctLetterData = alphabetData.AlphabetWrong.Find(data => data.letter == squareLetter);

                    if(normalLetterData.image == null || selectedLetterData.image == null)
                    {
                        // display which letter couldn't find 
                        Debug.Log("Some letters are blank. Letter: " + squareLetter);

                        // if in play mode immediately stop the game
                        #if UNITY_EDITOR
                        
                        if(UnityEditor.EditorApplication.isPlaying) 
                        {
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
                        #endif
                    }
                    else
                    {
                        squareList.Add(Instantiate(gridSquarePrefab));
                        squareList[squareList.Count - 1].GetComponent<GridSquare>().SetSprite(normalLetterData, correctLetterData, selectedLetterData);
                        squareList[squareList.Count - 1].transform.SetParent(this.transform);
                        squareList[squareList.Count - 1].GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
                        squareList[squareList.Count - 1].transform.localScale = squareScale; 
                    }
                }
            }
        }
    }

    // if grid outside of screen scale down until it fits
    private Vector3 GetSquareScale(Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        // scale down or up each square
        var adjustment = 0.01f;

        while(ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if(finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;
            }
        }
        return finalScale;
    }

    //checks if board will fit on screen
    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0f, 0f);
        var startPosition = new Vector2(0f, 0f);

        // calculate square size
        squareSize.x = (squareRect.width * targetScale.x) + squareOffset;
        squareSize.y = (squareRect.height * targetScale.y) + squareOffset;

        var midWidthPosition = ((currentGameData.selectedBoardData.Columns * squareSize.x) / 2) * 0.01f;
        var midHeightPosition = ((currentGameData.selectedBoardData.Rows * squareSize.y) / 2) * 0.01f;

        // if statement for start position of grid
        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1 : midWidthPosition;
        startPosition.y = midHeightPosition;

        // check if grid is still in screen
        return startPosition.x < GetHelfScreenWidth() * -1 || startPosition.y > topPosition;

    }

    private float GetHelfScreenWidth()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = (1.7f * height) * Screen.width / Screen.height;
        return width / 2;
    }
}
