using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform square, circle, hexagon;
    private Camera cam;
    private GameData gameData;
    private MementoManager mementoManager;

    private void Start()
    {
        cam = Camera.main;
        gameData = new GameData(square.position, circle.position, hexagon.position);
        mementoManager = new MementoManager(gameData);
        gameData.OnRestore += OnRestoreHandler;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            square.position = cam.ScreenToWorldPoint(Input.mousePosition);
            gameData.squarePosition = square.position;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            circle.position = cam.ScreenToWorldPoint(Input.mousePosition);
            gameData.circlePosition = circle.position;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            hexagon.position = cam.ScreenToWorldPoint(Input.mousePosition);
            gameData.hexagonPosition = hexagon.position;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mementoManager.Backup();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mementoManager.Restore();
        }
    }

    private void OnRestoreHandler()
    {
        square.position = gameData.squarePosition;
        circle.position = gameData.circlePosition;
        hexagon.position = gameData.hexagonPosition;
    }
}

public class GameData : IMementable
{
    public Vector2 squarePosition, circlePosition, hexagonPosition;
    public event Action OnRestore;

    public GameData(Vector2 squarePosition, Vector2 circlePosition, Vector2 hexagonPosition)
    {
        this.squarePosition = squarePosition;
        this.circlePosition = circlePosition;
        this.hexagonPosition = hexagonPosition;
    }

    public void Restore(Memento memento)
    {
        GameData gameData = (GameData)memento.State;

        Debug.Log($"Restore memento from {squarePosition} {gameData.squarePosition}");
        squarePosition = gameData.squarePosition;
        circlePosition = gameData.circlePosition;
        hexagonPosition = gameData.hexagonPosition;
        OnRestore?.Invoke();
    }

    public Memento Save()
    {
        Memento memento = new Memento(new GameData(squarePosition, circlePosition, hexagonPosition));
        return memento;
    }
}