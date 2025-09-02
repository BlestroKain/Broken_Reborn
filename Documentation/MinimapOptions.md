# Minimap Options

## EnableMinimapWindow

Toggles whether the minimap window is available. When disabled, the minimap is hidden entirely.

## ShowWaypoints

Determines if waypoint markers are displayed on the minimap.

## TileSize

Sets the pixel size used to render each tile on the minimap, affecting how much of the map is visible.

## MinimumZoom

Specifies the lowest zoom level allowed when viewing the minimap.

## MaximumZoom

Specifies the highest zoom level allowed when viewing the minimap.

## DefaultZoom

The initial zoom level applied when the minimap window opens.

## ZoomStep

Amount the zoom level changes with each zoom in or out command.

## IconScale

Adjusts the size of entity icons displayed on the minimap. Values below `0.8` are clamped to `0.8` to ensure icons remain visible.

## PoiIconScale

Controls the size of point of interest icons on the minimap. This value is applied directly without clamping.

## PoiIcons

Maps point of interest types to their icon textures so banks, shops, and other POIs can have distinct icons.

## MinimapImages

Specifies image files used for minimap elements such as players or NPCs. If an image is not provided, the corresponding color is used.

## MinimapColors

Defines the colors used to draw minimap elements when images are unavailable.

## RenderLayers

Lists the map layers included when rendering the minimap, controlling which layers are visible.

