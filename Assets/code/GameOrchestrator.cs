using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour
{
    private List<IInitialiser> mInitialisers;
    private List<IUpdater> mUpdaters;

    private CommandQueue mCommandQueue;

    private void Start()
    {
        Application.targetFrameRate = 60;

        mInitialisers = new List<IInitialiser>();
        mUpdaters = new List<IUpdater>();

        Create();
        Init();

        // Start game
        Dependencies.Get().Get<EventBus>().PostMessage(new StartGameEvent());
    }

    private void Create()
    {
        // Command queue
        mCommandQueue = new CommandQueue();

        // RNG
        Dependencies.Get().Register<RandomNumberGenerator>(new RandomNumberGenerator(System.DateTime.UtcNow.Second));

        // Event bus
        Dependencies.Get().Register<EventBus>(new EventBus());

        // Add model
        var tableModel = new TableModel();
        mInitialisers.Add(tableModel);
        Dependencies.Get().Register<IReadTableState>(tableModel);
        Dependencies.Get().Register<IWriteTableState>(tableModel);

        // Table
        var tablePrefab = (GameObject)Instantiate(Resources.Load("TableView"));
        var tableView = tablePrefab.GetComponent<TableView>();
        mInitialisers.Add(tableView);
        mUpdaters.Add(tableView);

        // Input
        if(Application.isMobilePlatform)
        {

        }
        else
        {
            var mouseInputController = new MouseInputController();
            mUpdaters.Add(mouseInputController);
            Dependencies.Get().Register<IInputController>(mouseInputController);
        }
    }

    private void Init()
    {
        mCommandQueue.OnInit();
        MapCommands();

        // Initialise dependencies
        foreach (var i in mInitialisers)
            i.OnInit();
    }

    private void MapCommands()
    {
        mCommandQueue.MapCommand(new StartGameEvent(), (Event e) => {
            mCommandQueue.Reset();
            return new StartGameCommand(mCommandQueue);
        });
        mCommandQueue.MapCommand(new TryPlaceCardsEvent(), (Event e) => {
            var ce = e as TryPlaceCardsEvent;
            return new TryPlacingCardsCommand(ce.mPositionToPlace, ce.mCardsToPlace);
        });
        mCommandQueue.MapCommand(new TryPlaceCardsEvent(), (Event e) => {
            return new TryWinningGameCommand();
        });
        mCommandQueue.MapCommand(new TakeCardFromStockEvent(), (Event e) => {
            return new TakeCardFromStockCommand();
        });
    }

    private void Update()
    {
        var dt = Time.deltaTime;
        foreach (var u in mUpdaters)
            u.OnUpdate(dt);

        // Keyboard shortcuts
        // Close game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Undo/redo
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mCommandQueue.Undo();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            mCommandQueue.Redo();
        }

        // Restart game
        if(Input.GetKeyDown(KeyCode.R))
        {
            Dependencies.Get().Get<EventBus>().PostMessage(new StartGameEvent());
        }
    }
}
