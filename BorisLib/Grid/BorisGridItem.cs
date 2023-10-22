using Microsoft.AspNetCore.Components;

namespace BorisLib.Grid;

public class BorisGridItem : ComponentBase, IDisposable
{
    /// <summary>
    /// Width of the item, measured in grid columns.
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 1;
    
    /// <summary>
    /// Height of the item, measured in grid rows.
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 1;

    /// <summary>
    /// Column of the top-left corner of the item.
    /// </summary>
    [Parameter]
    public int? X { get; set; }

    /// <summary>
    /// Row of the top-left corner of the item.
    /// </summary>
    [Parameter]
    public int? Y { get; set; }

    /// <summary>
    /// What should be drawn in the grid area.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// CSS style to apply to the grid item.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }
    
    /// <summary>
    /// The grid that this item belongs to.
    /// </summary>
    [CascadingParameter]
    public BorisGrid? Grid { get; set; }

    /// <summary>
    /// CSS string to set GridItem variables.
    /// </summary>
    public string StyleString => $"--start-column: {StartColumn}; --end-column: {EndColumn}; --start-row: {StartRow}; --end-row: {EndRow}; --aspect-ratio: {AspectRatio}";

    private string StartColumn => X.HasValue ? X.Value.ToString() : $"span {Width}";

    private string EndColumn => X.HasValue ? (X.Value + Width).ToString() : $"span {Width}";

    private string StartRow => Y.HasValue ? Y.Value.ToString() : $"span {Height}";

    private string EndRow => Y.HasValue ? (Y.Value + Height).ToString() : $"span {Height}";

    /// <summary>
    /// The aspect ratio of the item.
    /// </summary>
    private float AspectRatio => Width / (float)Height;
    
    protected override void OnInitialized()
    {
        Grid?.AddItem(this);
    }

    public void Dispose()
    {
        Grid?.RemoveItem(this);
    }
}