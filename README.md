# SquareGridLib
A Blazor library that provides you with a 2d grid component. You can place items of different height/width on this grid.

## How to use
```html
<SquareGrid>
    <Items>
        <SquareGridItem>
            <div>This is a dynamically positioned item.</div>
        </SquareGridItem>
        <SquareGridItem X="3">
            <div>You can specify the item's position using 'X' and 'Y'.</div>
        </SquareGridItem>
        <SquareGridItem Height="3" Width="2">
            <div>Height and width can be changed as well.</div>
        </SquareGridItem>
    </Items>
</SquareGrid>
```

`Style` and `Class` are supported on the `SquareGrid` and `SquareGridItem`. They will also propagate all unmapped attributes down to the rendered HTML.