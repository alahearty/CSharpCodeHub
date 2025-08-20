using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AdvancedGISWPF.Models;
using AdvancedGISWPF.Services;
using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Serilog;

namespace AdvancedGISWPF.ViewModels;

// Main view model for the GIS application
public partial class MainViewModel : ObservableObject, IDisposable
{
    private readonly DatabaseService _databaseService;
    private bool _disposed = false;
    
    // Observable collections
    [ObservableProperty]
    private ObservableCollection<Layer> _layers = new();
    
    [ObservableProperty]
    private ObservableCollection<SpatialFeature> _spatialFeatures = new();
    
    [ObservableProperty]
    private ObservableCollection<StatisticsItem> _statisticsData = new();
    
    // Selected items
    [ObservableProperty]
    private Layer? _selectedLayer;
    
    [ObservableProperty]
    private SpatialFeature? _selectedFeature;
    
    [ObservableProperty]
    private string _selectedLayerName = string.Empty;
    
    [ObservableProperty]
    private string _selectedLayerDescription = string.Empty;
    
    [ObservableProperty]
    private string _selectedGeometryType = string.Empty;
    
    // Chart data
    [ObservableProperty]
    private ChartValues<double> _temperatureData = new();
    
    [ObservableProperty]
    private List<string> _timeLabels = new();
    
    [ObservableProperty]
    private PlotModel _spatialDataPlot = new();
    
    // Geometry types for combo box
    [ObservableProperty]
    private List<string> _geometryTypes = new() { "Point", "LineString", "Polygon", "MultiPoint", "MultiLineString", "MultiPolygon" };
    
    // Current date/time for status bar
    [ObservableProperty]
    private DateTime _currentDateTime = DateTime.Now;
    
    // Commands
    public ICommand NewProjectCommand { get; }
    public ICommand OpenProjectCommand { get; }
    public ICommand SaveProjectCommand { get; }
    public ICommand ImportDataCommand { get; }
    public ICommand ExportDataCommand { get; }
    public ICommand DatabaseCommand { get; }
    public ICommand SpatialAnalysisCommand { get; }
    public ICommand StatisticsCommand { get; }
    public ICommand ChartsCommand { get; }
    public ICommand ZoomInCommand { get; }
    public ICommand ZoomOutCommand { get; }
    public ICommand PanCommand { get; }
    public ICommand FullExtentCommand { get; }
    public ICommand AddLayerCommand { get; }
    public ICommand RemoveLayerCommand { get; }
    public ICommand LayerPropertiesCommand { get; }
    public ICommand SelectCommand { get; }
    public ICommand ClearSelectionCommand { get; }
    public ICommand BufferCommand { get; }
    public ICommand IntersectCommand { get; }
    public ICommand UnionCommand { get; }
    public ICommand DistanceCommand { get; }
    public ICommand DescriptiveStatsCommand { get; }
    public ICommand SpatialAutocorrCommand { get; }
    public ICommand HotSpotCommand { get; }
    public ICommand PrintLayoutCommand { get; }
    public ICommand ExportMapCommand { get; }
    public ICommand LayerManagerCommand { get; }
    public ICommand AttributeTableCommand { get; }
    public ICommand ToolboxCommand { get; }
    public ICommand ShowMapCommand { get; }
    public ICommand ShowChartsCommand { get; }
    public ICommand ShowDataCommand { get; }
    public ICommand ShowAnalysisCommand { get; }
    public ICommand SettingsCommand { get; }
    public ICommand HelpCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand ApplyPropertiesCommand { get; }
    
    public MainViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        
        // Initialize commands
        InitializeCommands();
        
        // Initialize chart data
        InitializeChartData();
        
        // Setup timer for date/time updates
        var timer = new Timer(_ => CurrentDateTime = DateTime.Now, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        
        Log.Information("MainViewModel initialized");
    }
    
    private void InitializeCommands()
    {
        NewProjectCommand = new RelayCommand(OnNewProject);
        OpenProjectCommand = new RelayCommand(OnOpenProject);
        SaveProjectCommand = new RelayCommand(OnSaveProject);
        ImportDataCommand = new RelayCommand(OnImportData);
        ExportDataCommand = new RelayCommand(OnExportData);
        DatabaseCommand = new RelayCommand(OnDatabase);
        SpatialAnalysisCommand = new RelayCommand(OnSpatialAnalysis);
        StatisticsCommand = new RelayCommand(OnStatistics);
        ChartsCommand = new RelayCommand(OnCharts);
        ZoomInCommand = new RelayCommand(OnZoomIn);
        ZoomOutCommand = new RelayCommand(OnZoomOut);
        PanCommand = new RelayCommand(OnPan);
        FullExtentCommand = new RelayCommand(OnFullExtent);
        AddLayerCommand = new RelayCommand(OnAddLayer);
        RemoveLayerCommand = new RelayCommand(OnRemoveLayer);
        LayerPropertiesCommand = new RelayCommand(OnLayerProperties);
        SelectCommand = new RelayCommand(OnSelect);
        ClearSelectionCommand = new RelayCommand(OnClearSelection);
        BufferCommand = new RelayCommand(OnBuffer);
        IntersectCommand = new RelayCommand(OnIntersect);
        UnionCommand = new RelayCommand(OnUnion);
        DistanceCommand = new RelayCommand(OnDistance);
        DescriptiveStatsCommand = new RelayCommand(OnDescriptiveStats);
        SpatialAutocorrCommand = new RelayCommand(OnSpatialAutocorr);
        HotSpotCommand = new RelayCommand(OnHotSpot);
        PrintLayoutCommand = new RelayCommand(OnPrintLayout);
        ExportMapCommand = new RelayCommand(OnExportMap);
        LayerManagerCommand = new RelayCommand(OnLayerManager);
        AttributeTableCommand = new RelayCommand(OnAttributeTable);
        ToolboxCommand = new RelayCommand(OnToolbox);
        ShowMapCommand = new RelayCommand(OnShowMap);
        ShowChartsCommand = new RelayCommand(OnShowCharts);
        ShowDataCommand = new RelayCommand(OnShowData);
        ShowAnalysisCommand = new RelayCommand(OnShowAnalysis);
        SettingsCommand = new RelayCommand(OnSettings);
        HelpCommand = new RelayCommand(OnHelp);
        ExitCommand = new RelayCommand(OnExit);
        ApplyPropertiesCommand = new RelayCommand(OnApplyProperties);
    }
    
    private void InitializeChartData()
    {
        try
        {
            // Initialize temperature data (sample data)
            var random = new Random();
            for (int i = 0; i < 24; i++)
            {
                TemperatureData.Add(15 + random.NextDouble() * 20); // 15-35°C
                TimeLabels.Add($"{i:D2}:00");
            }
            
            // Initialize OxyPlot
            InitializeOxyPlot();
            
            Log.Information("Chart data initialized successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize chart data");
        }
    }
    
    private void InitializeOxyPlot()
    {
        try
        {
            SpatialDataPlot = new PlotModel { Title = "Spatial Data Distribution" };
            
            // Add axes
            SpatialDataPlot.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "X Coordinate" });
            SpatialDataPlot.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Y Coordinate" });
            
            // Add sample data series
            var pointSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 5 };
            
            // Sample spatial points
            var random = new Random();
            for (int i = 0; i < 50; i++)
            {
                var x = random.NextDouble() * 100;
                var y = random.NextDouble() * 100;
                pointSeries.Points.Add(new ScatterPoint(x, y));
            }
            
            SpatialDataPlot.Series.Add(pointSeries);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize OxyPlot");
        }
    }
    
    public async Task InitializeAsync()
    {
        try
        {
            // Initialize database
            await _databaseService.InitializeDatabaseAsync();
            
            // Load layers
            await LoadLayersAsync();
            
            // Load statistics
            await LoadStatisticsAsync();
            
            Log.Information("MainViewModel initialized asynchronously");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to initialize MainViewModel");
        }
    }
    
    private async Task LoadLayersAsync()
    {
        try
        {
            var layers = await _databaseService.GetAllLayersAsync();
            
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Layers.Clear();
                foreach (var layer in layers)
                {
                    Layers.Add(layer);
                }
            });
            
            Log.Information($"Loaded {layers.Count} layers");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to load layers");
        }
    }
    
    private async Task LoadStatisticsAsync()
    {
        try
        {
            var stats = await _databaseService.GetDatabaseStatisticsAsync();
            
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                StatisticsData.Clear();
                
                if (stats.ContainsKey("TotalLayers"))
                    StatisticsData.Add(new StatisticsItem("Total Layers", stats["TotalLayers"].ToString()));
                
                if (stats.ContainsKey("TotalFeatures"))
                    StatisticsData.Add(new StatisticsItem("Total Features", stats["TotalFeatures"].ToString()));
                
                if (stats.ContainsKey("FeatureTypes"))
                {
                    var featureTypes = stats["FeatureTypes"] as List<object>;
                    if (featureTypes != null)
                    {
                        foreach (var item in featureTypes)
                        {
                            // Parse feature type statistics
                            var typeInfo = item.ToString();
                            if (!string.IsNullOrEmpty(typeInfo))
                            {
                                StatisticsData.Add(new StatisticsItem("Feature Type", typeInfo));
                            }
                        }
                    }
                }
            });
            
            Log.Information("Statistics loaded successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to load statistics");
        }
    }
    
    public void UpdateSelectedLayer(object layerObject)
    {
        try
        {
            if (layerObject is Layer layer)
            {
                SelectedLayer = layer;
                SelectedLayerName = layer.Name;
                SelectedLayerDescription = layer.Description;
                SelectedGeometryType = layer.LayerType;
                
                Log.Information($"Selected layer: {layer.Name}");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to update selected layer");
        }
    }
    
    // Command implementations
    private void OnNewProject()
    {
        try
        {
            Log.Information("New project command executed");
            // Implementation for new project
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute new project command");
        }
    }
    
    private void OnOpenProject()
    {
        try
        {
            Log.Information("Open project command executed");
            // Implementation for open project
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute open project command");
        }
    }
    
    private void OnSaveProject()
    {
        try
        {
            Log.Information("Save project command executed");
            // Implementation for save project
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute save project command");
        }
    }
    
    private void OnImportData()
    {
        try
        {
            Log.Information("Import data command executed");
            // Implementation for import data
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute import data command");
        }
    }
    
    private void OnExportData()
    {
        try
        {
            Log.Information("Export data command executed");
            // Implementation for export data
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute export data command");
        }
    }
    
    private void OnDatabase()
    {
        try
        {
            Log.Information("Database command executed");
            // Implementation for database operations
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute database command");
        }
    }
    
    private void OnSpatialAnalysis()
    {
        try
        {
            Log.Information("Spatial analysis command executed");
            // Implementation for spatial analysis
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute spatial analysis command");
        }
    }
    
    private void OnStatistics()
    {
        try
        {
            Log.Information("Statistics command executed");
            // Implementation for statistics
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute statistics command");
        }
    }
    
    private void OnCharts()
    {
        try
        {
            Log.Information("Charts command executed");
            // Implementation for charts
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute charts command");
        }
    }
    
    private void OnZoomIn()
    {
        try
        {
            Log.Information("Zoom in command executed");
            // Implementation for zoom in
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute zoom in command");
        }
    }
    
    private void OnZoomOut()
    {
        try
        {
            Log.Information("Zoom out command executed");
            // Implementation for zoom out
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute zoom out command");
        }
    }
    
    private void OnPan()
    {
        try
        {
            Log.Information("Pan command executed");
            // Implementation for pan
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute pan command");
        }
    }
    
    private void OnFullExtent()
    {
        try
        {
            Log.Information("Full extent command executed");
            // Implementation for full extent
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute full extent command");
        }
    }
    
    private void OnAddLayer()
    {
        try
        {
            Log.Information("Add layer command executed");
            // Implementation for add layer
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute add layer command");
        }
    }
    
    private void OnRemoveLayer()
    {
        try
        {
            Log.Information("Remove layer command executed");
            // Implementation for remove layer
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute remove layer command");
        }
    }
    
    private void OnLayerProperties()
    {
        try
        {
            Log.Information("Layer properties command executed");
            // Implementation for layer properties
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute layer properties command");
        }
    }
    
    private void OnSelect()
    {
        try
        {
            Log.Information("Select command executed");
            // Implementation for select
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute select command");
        }
    }
    
    private void OnClearSelection()
    {
        try
        {
            Log.Information("Clear selection command executed");
            // Implementation for clear selection
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute clear selection command");
        }
    }
    
    private void OnBuffer()
    {
        try
        {
            Log.Information("Buffer command executed");
            // Implementation for buffer analysis
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute buffer command");
        }
    }
    
    private void OnIntersect()
    {
        try
        {
            Log.Information("Intersect command executed");
            // Implementation for intersect analysis
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute intersect command");
        }
    }
    
    private void OnUnion()
    {
        try
        {
            Log.Information("Union command executed");
            // Implementation for union analysis
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute union command");
        }
    }
    
    private void OnDistance()
    {
        try
        {
            Log.Information("Distance command executed");
            // Implementation for distance calculation
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute distance command");
        }
    }
    
    private void OnDescriptiveStats()
    {
        try
        {
            Log.Information("Descriptive stats command executed");
            // Implementation for descriptive statistics
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute descriptive stats command");
        }
    }
    
    private void OnSpatialAutocorr()
    {
        try
        {
            Log.Information("Spatial autocorrelation command executed");
            // Implementation for spatial autocorrelation
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute spatial autocorrelation command");
        }
    }
    
    private void OnHotSpot()
    {
        try
        {
            Log.Information("Hot spot analysis command executed");
            // Implementation for hot spot analysis
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute hot spot analysis command");
        }
    }
    
    private void OnPrintLayout()
    {
        try
        {
            Log.Information("Print layout command executed");
            // Implementation for print layout
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute print layout command");
        }
    }
    
    private void OnExportMap()
    {
        try
        {
            Log.Information("Export map command executed");
            // Implementation for export map
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute export map command");
        }
    }
    
    private void OnLayerManager()
    {
        try
        {
            Log.Information("Layer manager command executed");
            // Implementation for layer manager
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute layer manager command");
        }
    }
    
    private void OnAttributeTable()
    {
        try
        {
            Log.Information("Attribute table command executed");
            // Implementation for attribute table
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute attribute table command");
        }
    }
    
    private void OnToolbox()
    {
        try
        {
            Log.Information("Toolbox command executed");
            // Implementation for toolbox
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute toolbox command");
        }
    }
    
    private void OnShowMap()
    {
        try
        {
            Log.Information("Show map command executed");
            // Implementation for show map
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute show map command");
        }
    }
    
    private void OnShowCharts()
    {
        try
        {
            Log.Information("Show charts command executed");
            // Implementation for show charts
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute show charts command");
        }
    }
    
    private void OnShowData()
    {
        try
        {
            Log.Information("Show data command executed");
            // Implementation for show data
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute show data command");
        }
    }
    
    private void OnShowAnalysis()
    {
        try
        {
            Log.Information("Show analysis command executed");
            // Implementation for show analysis
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute show analysis command");
        }
    }
    
    private void OnSettings()
    {
        try
        {
            Log.Information("Settings command executed");
            // Implementation for settings
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute settings command");
        }
    }
    
    private void OnHelp()
    {
        try
        {
            Log.Information("Help command executed");
            // Implementation for help
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute help command");
        }
    }
    
    private void OnExit()
    {
        try
        {
            Log.Information("Exit command executed");
            Application.Current.Shutdown();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute exit command");
        }
    }
    
    private void OnApplyProperties()
    {
        try
        {
            Log.Information("Apply properties command executed");
            // Implementation for applying properties
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to execute apply properties command");
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _disposed = true;
        }
    }
}

// Statistics item for data binding
public class StatisticsItem
{
    public string Metric { get; set; }
    public string Value { get; set; }
    
    public StatisticsItem(string metric, string value)
    {
        Metric = metric;
        Value = value;
    }
}
