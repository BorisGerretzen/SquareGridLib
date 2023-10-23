using Microsoft.AspNetCore.Components;

namespace SquareGridLib.Grid;

public class SquareGridItem : ComponentBase, IDisposable
{
    /// <summary>
    ///     The grid that this item belongs to.
    /// </summary>
    [CascadingParameter]
    public SquareGrid? Grid { get; set; }

    /// <summary>
    ///     Width of the item, measured in grid columns.
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 1;

    /// <summary>
    ///     Height of the item, measured in grid rows.
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 1;

    /// <summary>
    ///     Column of the top-left corner of the item.
    /// </summary>
    [Parameter]
    public int? X { get; set; }

    /// <summary>
    ///     Row of the top-left corner of the item.
    /// </summary>
    [Parameter]
    public int? Y { get; set; }

    /// <summary>
    ///     What should be drawn in the grid area.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///     CSS class string to apply to the item.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    ///     CSS style string to apply to the item.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? UnmatchedAttributes { get; set; }

    /// <summary>
    ///     CSS string to set the grid related variables.
    /// </summary>
    public string GridStyleString => $"--start-column: {StartColumn}; --end-column: {EndColumn}; --start-row: {StartRow}; --end-row: {EndRow}; --aspect-ratio: {AspectRatio}; ";

    private string StartColumn => X.HasValue ? X.Value.ToString() : $"span {Width}";

    private string EndColumn => X.HasValue ? (X.Value + Width).ToString() : $"span {Width}";

    private string StartRow => Y.HasValue ? Y.Value.ToString() : $"span {Height}";

    private string EndRow => Y.HasValue ? (Y.Value + Height).ToString() : $"span {Height}";

    /// <summary>
    ///     The aspect ratio of the item.
    /// </summary>
    private float AspectRatio => Width / (float)Height;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Grid?.RemoveItem(this);
    }

    protected override void OnInitialized()
    {
        Grid?.AddItem(this);
    }
}