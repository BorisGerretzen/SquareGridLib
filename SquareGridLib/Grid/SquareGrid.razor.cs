﻿using Microsoft.AspNetCore.Components;

namespace SquareGridLib.Grid;

public partial class SquareGrid : ComponentBase
{
    private List<SquareGridItem> _items = null!;

    /// <summary>
    ///     Items that have been registered, i.e. added through <see cref="AddItem" /> or through <see cref="Items" />.
    /// </summary>
    public IReadOnlyCollection<SquareGridItem> RegisteredItems => _items;

    /// <summary>
    ///     The number of columns in the grid.
    /// </summary>
    [Parameter]
    public int Columns { get; set; } = 12;

    /// <summary>
    ///     List of SquareGridItems that will be added to the grid.
    /// </summary>
    [Parameter]
    public RenderFragment? Items { get; set; }

    /// <summary>
    ///     Template for the grid items.
    /// </summary>
    [Parameter]
    public RenderFragment<SquareGridItem>? ItemTemplate { get; set; }

    /// <summary>
    ///     CSS style to add to the grid.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    ///     CSS class to add to the grid.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? UnmatchedAttributes { get; set; }

    protected override void OnInitialized()
    {
        _items = new List<SquareGridItem>();
    }

    public void Refresh()
    {
        StateHasChanged();
    }

    /// <summary>
    ///     Adds an item to the grid.
    /// </summary>
    /// <param name="item">Item to add.</param>
    public void AddItem(SquareGridItem item)
    {
        _items.Add(item);
        StateHasChanged();
    }

    /// <summary>
    ///     Removes an item from the grid.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    public void RemoveItem(SquareGridItem item)
    {
        _items.Remove(item);
        StateHasChanged();
    }
}