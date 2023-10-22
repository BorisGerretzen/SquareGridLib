using Microsoft.AspNetCore.Components;

namespace BorisLib.Grid;

public partial class BorisGrid : ComponentBase
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
    public RenderFragment<BorisGridItem>? ItemTemplate { get; set; }

    /// <summary>
    /// CSS style to add to the grid.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }
    
    private List<BorisGridItem> _items = null!;
    
    protected override void OnInitialized()
    {
        _items = new List<BorisGridItem>();
    }

    /// <summary>
    /// Adds an item to the grid.
    /// </summary>
    /// <param name="item">Item to add.</param>
    public void AddItem(BorisGridItem item)
    {
        _items.Add(item);
        StateHasChanged();
    }

    /// <summary>
    /// Removes an item from the grid.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    public void RemoveItem(BorisGridItem item)
    {
        _items.Remove(item);
        StateHasChanged();
    }
}