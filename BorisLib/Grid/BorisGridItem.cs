using Microsoft.AspNetCore.Components;

namespace BorisLib.Grid;

public class BorisGridItem : ComponentBase, IDisposable
{
    private const string Auto = "auto";
    private const int DefaultWidth = 1;
    private const int DefaultHeight = 1;

    [Parameter]
    public int Width { get; set; } = DefaultWidth;

    [Parameter]
    public int Height { get; set; } = DefaultHeight;

    [Parameter]
    public int? X { get; set; }

    [Parameter]
    public int? Y { get; set; }

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [CascadingParameter]
    public BorisGrid? Grid { get; set; }

    public string StartColumn => X.HasValue ? X.Value.ToString() : Auto;

    public string EndColumn => X.HasValue ? (X.Value + Width).ToString() : Auto;

    public string StartRow => Y.HasValue ? Y.Value.ToString() : Auto;

    public string EndRow => Y.HasValue ? (Y.Value + Height).ToString() : Auto;

    public float AspectRatio => Width / (float)Height;
    
    protected override void OnInitialized()
    {
        if ((Width != DefaultWidth || Height != DefaultHeight) && (!X.HasValue || !Y.HasValue))
            throw new ArgumentException("Can only specify width and height for fixed position items.");
        Grid?.AddItem(this);
    }

    public void Dispose()
    {
        Grid?.RemoveItem(this);
    }
}