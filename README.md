# SquareGridLib

[![NuGet](https://img.shields.io/nuget/v/SquareGridLib.svg)](https://www.nuget.org/packages/SquareGridLib/)\
Provides a 2D grid component on which you can place panels of customizable size.
Similar to the syncfusion blazor dashboard but simpler.

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
Elements without a fixed position will flow around the elements that do.
![image](https://github.com/BorisGerretzen/SquareGridLib/assets/15902678/35af7983-5678-4779-b1b9-1aadd96a264c)

For more details check out the `SquareGridLibDemo` project, specifically `Pages/Grid.razor`.

## To do

- [ ] Method on the component `void Pack()` that tries to pack all items compactly.
- [x] Missing something? Open an issue!
