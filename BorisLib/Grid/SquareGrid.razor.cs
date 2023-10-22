using Microsoft.AspNetCore.Components;

namespace BorisLib.Grid;

public partial class SquareGrid : ComponentBase
{
    /// <summary>
    /// The number of columns in the grid.
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 12;

    /// <summary>
    /// List of BorisGridItem that will be added to the grid.
    /// </summary>
    [Parameter]
    public RenderFragment? Items { get; set; }

    /// <summary>
    /// Template for the grid items.
    /// </summary>
    [Parameter]
    public RenderFragment<SquareGridItem>? ItemTemplate { get; set; }

    /// <summary>
    /// CSS style to add to the grid.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }
    
    /// <summary>
    /// CSS class to add to the grid.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }
    
    private List<SquareGridItem> _items = null!;
    
    protected override void OnInitialized()
    {
        _items = new List<SquareGridItem>();
    }

    public void Refresh() => StateHasChanged();
    
    /// <summary>
    /// Adds an item to the grid.
    /// </summary>
    /// <param name="item">Item to add.</param>
    public void AddItem(SquareGridItem item)
    {
        _items.Add(item);
        StateHasChanged();
    }

    /// <summary>
    /// Removes an item from the grid.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    public void RemoveItem(SquareGridItem item)
    {
        _items.Remove(item);
        StateHasChanged();
    }
}