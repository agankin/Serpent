using Chalkboard;
using Chalkboard.Samples;
using Serpent;

using static Chalkboard.UITreeBuilder<Serpent.AppStore>;

ColorScheme.SetDefault(Color.White, Color.Black);

var (width, height) = (Console.WindowWidth, Console.WindowHeight);
var renderer = new WindowsConsoleRenderer(width, height);

var (fieldWidth, fieldHeight) = (width - 2, height - 2);
var (headLeft, headTop) = (fieldWidth / 2, fieldHeight / 2);

var fieldSize = new Size(fieldWidth, fieldHeight);
var snakeModel = new SnakeModel(new(headLeft, headTop), p => p.TranslateTop(1), 5);

var store = new AppStore
{
    FieldSize = fieldSize,
    Snake = snakeModel,
    Direction = SnakeDirection.Up,
    Obstacles = ObstaclesGenerator.Generate(fieldSize, snakeModel, 20),
    Collided = false
};

var root = _<Border>(
    _<Field>(
        _<Snake>(),
        _<ObstacleMap>()
    )
);

var ui = new ChalkboardUI<AppStore>(root, renderer, store);

var tickDispatcher = ui.AddStoreReducer<TimerEvent>(AppStoreReducers.OnTick);
var keyPressedDispatcher = ui.AddStoreReducer<ConsoleKeyInfo>(AppStoreReducers.OnKeyPressed);

Console.CursorVisible = false;

using var timerObservable = TimerObservable.Start(tickMilliseconds: 100, dueTimeMilliseconds: 1000);
var timerObserver = new DispatchObserver<TimerEvent>().OnNext(e => tickDispatcher(e));
using var timerSubscription = timerObservable.Subscribe(timerObserver);

using var keyPressedObservable = KeyPressedObservable.Start();
var keyPressedObserver = new DispatchObserver<ConsoleKeyInfo>().OnNext(e => keyPressedDispatcher(e));
using var keyPressedSubscription = keyPressedObservable.Subscribe(keyPressedObserver);

var cts = new CancellationTokenSource();
var exitObserver = new DispatchObserver<ConsoleKeyInfo>()
    .OnNext(keyInfo =>
    {
        if (keyInfo.Key == ConsoleKey.Escape)
            cts.Cancel();
    });
using var exitSubscription = keyPressedObservable.Subscribe(exitObserver);

ui.EnterActionLoop(cts.Token);