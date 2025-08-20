using System.Windows;
using AdvancedGISWPF.ViewModels;
using AdvancedGISWPF.Services;
using Serilog;

namespace AdvancedGISWPF.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    private readonly DatabaseService _databaseService;
    private readonly Timer _updateTimer;

    public MainWindow()
    {
        InitializeComponent();
        
        try
        {
            // Initialize services
            _databaseService = new DatabaseService();
            
            // Initialize view model
            _viewModel = new MainViewModel(_databaseService);
            DataContext = _viewModel;
            
            // Setup timer for UI updates
            _updateTimer = new Timer(UpdateUI, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            
            // Setup event handlers
            SetupEventHandlers();
            
            Log.Information("MainWindow initialized successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize MainWindow");
            MessageBox.Show($"Failed to initialize application: {ex.Message}", "Initialization Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SetupEventHandlers()
    {
        // Window events
        Loaded += MainWindow_Loaded;
        Closing += MainWindow_Closing;
        
        // Map events
        if (MainContentArea != null)
        {
            MainContentArea.MouseMove += MainContentArea_MouseMove;
            MainContentArea.MouseWheel += MainContentArea_MouseWheel;
        }
        
        // Layer tree events
        if (LayerTreeView != null)
        {
            LayerTreeView.SelectedItemChanged += LayerTreeView_SelectedItemChanged;
        }
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Initialize the view model
            _viewModel.InitializeAsync();
            
            // Update status
            UpdateStatus("Application loaded successfully");
            
            Log.Information("MainWindow loaded successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error during MainWindow load");
            UpdateStatus($"Error during load: {ex.Message}");
        }
    }

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        try
        {
            // Cleanup
            _updateTimer?.Dispose();
            _viewModel?.Dispose();
            
            Log.Information("MainWindow closing");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error during MainWindow cleanup");
        }
    }

    private void MainContentArea_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        try
        {
            var position = e.GetPosition(MainContentArea);
            
            // Update coordinates display
            if (CoordinatesText != null)
            {
                CoordinatesText.Text = $"Coordinates: {position.X:F1}, {position.Y:F1}";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating coordinates");
        }
    }

    private void MainContentArea_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        try
        {
            if (e.Delta > 0)
            {
                _viewModel.ZoomInCommand.Execute(null);
            }
            else
            {
                _viewModel.ZoomOutCommand.Execute(null);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error handling mouse wheel");
        }
    }

    private void LayerTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        try
        {
            if (e.NewValue != null)
            {
                // Update properties panel
                _viewModel.UpdateSelectedLayer(e.NewValue);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating selected layer");
        }
    }

    private void UpdateUI(object? state)
    {
        try
        {
            // Update UI elements on UI thread
            Dispatcher.Invoke(() =>
            {
                // Update current date/time
                if (DateTimeText != null)
                {
                    DateTimeText.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                
                // Update memory usage
                if (MemoryStatusText != null)
                {
                    var memoryUsage = GC.GetTotalMemory(false) / (1024 * 1024);
                    MemoryStatusText.Text = $"Memory Usage: {memoryUsage} MB";
                }
                
                // Update database status
                if (DatabaseStatusText != null)
                {
                    var isConnected = _databaseService?.IsConnected ?? false;
                    DatabaseStatusText.Text = $"Database: {(isConnected ? "Connected" : "Disconnected")}";
                }
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating UI");
        }
    }

    private void UpdateStatus(string message)
    {
        try
        {
            if (StatusText != null)
            {
                StatusText.Text = message;
            }
            
            Log.Information($"Status updated: {message}");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating status");
        }
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        
        try
        {
            // Additional initialization after source is initialized
            Log.Information("MainWindow source initialized");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error during source initialization");
        }
    }
}
