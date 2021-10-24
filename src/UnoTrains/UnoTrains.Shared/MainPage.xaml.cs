using SkiaSharp.Views.UWP;
using Trains.NET.Instrumentation;
using Trains.NET.Rendering;
using Windows.UI.Xaml.Controls;
using Trains.NET.Instrumentation;
using Trains.NET.Rendering;
using Trains.NET.Rendering.Skia;
using SkiaSharp;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoTrains
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _game = DI.ServiceLocator.GetService<IGame>();
            _interactionManager = DI.ServiceLocator.GetService<IInteractionManager>();
        }

        private void OnPointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(SkiaCanvas);
            if (point.Properties.IsLeftButtonPressed)
            {
                System.Console.WriteLine("Pointer Drag [" + (int)point.RawPosition.X + "," + (int)point.RawPosition.Y + "]");
                _interactionManager.PointerDrag((int)point.RawPosition.X, (int)point.RawPosition.Y);
            }
            else
            {
                System.Console.WriteLine("Pointer Move [" + (int)point.RawPosition.X + "," + (int)point.RawPosition.Y + "]");
                _interactionManager.PointerMove((int)point.RawPosition.X, (int)point.RawPosition.Y);
            }
        }

        private void OnPointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(SkiaCanvas);
            System.Console.WriteLine("Pointer Release [" + (int)point.RawPosition.X + "," + (int)point.RawPosition.Y + "]");
            _interactionManager.PointerRelease((int)point.RawPosition.X, (int)point.RawPosition.Y);
        }

        private void OnPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(SkiaCanvas);
            System.Console.WriteLine("Pointer Click [" + (int)point.RawPosition.X + "," + (int)point.RawPosition.Y + "]");
            _interactionManager.PointerClick((int)point.RawPosition.X, (int)point.RawPosition.Y);
        }

        private IGame _game = null!;
        private IInteractionManager _interactionManager = null!;

        private readonly PerSecondTimedStat _fps = InstrumentationBag.Add<PerSecondTimedStat>("SkiaSharp-OnPaintSurfaceFPS");
        private readonly ElapsedMillisecondsTimedStat _renderTime = InstrumentationBag.Add<ElapsedMillisecondsTimedStat>("GameElement-GameRender");

        private void OnPaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            if (_game == null)
            {
                return;
            }

            using (_renderTime.Measure())
            {
                _game.SetSize(e.Info.Width, e.Info.Height);
                if (e.Surface.Context is GRContext context && context != null)
                {
                    // Set the context so all rendering happens in the same place
                    _game.SetContext(new SKContextWrapper(context));
                }
                _game.Render(new SKCanvasWrapper(e.Surface.Canvas));
            }

            _fps.Update();
        }
    }
}
