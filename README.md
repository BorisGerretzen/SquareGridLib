# SquareGridLib
A Blazor library that provides you with a 2d grid component. You can place items of different height/width on this grid.

## How to use
This short snippet shows the basic functionality of the component:
```html
<style>
    .my-item {
        border: 1px solid black;
    }
</style>
<SquareGrid>
    <Items>
        <SquareGridItem>
            <div class="my-item">This is a dynamically positioned item.</div>
        </SquareGridItem>
        <SquareGridItem X="3" Y="4">
            <div class="my-item">You can specify the item's position using 'X' and 'Y'.</div>
        </SquareGridItem>
        <SquareGridItem Height="3" Width="2" X="2" Y="2">
            <div class="my-item">Height and width can be changed as well.</div>
        </SquareGridItem>
        <SquareGridItem>
            <div class="my-item">This is a dynamically positioned item.</div>
        </SquareGridItem>
        <SquareGridItem>
            <div class="my-item">This is a dynamically positioned item.</div>
        </SquareGridItem>
    </Items>
</SquareGrid>
```
This is what the output looks like, it's very ugly but the structure is what matters.
![image](https://github.com/BorisGerretzen/SquareGridLib/assets/15902678/112806a6-1260-48a4-9649-e3dd6ef95b20)
