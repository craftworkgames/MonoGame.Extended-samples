# Demos

These demos *require* MonoGame.

To run each demo in Visual Studio, open the project file (*.csproj).

## Tweening

This tutorial shows off the `MonoGame.Extended.Tweening` functionality.  A tween is a change between 2 values over time, where the movement is defined by a function.  This could be linear, quadratic, or others.  Adding a `Tween` to movement can give a game entity more feeling and liveliness.

## Sandbox

This is an Entity Component System example.  In the example it has "rain" falling.

There are 2 components:
1. Expiry
1. Raindrop

There are 4 systems:
1. ExpirySystem
1. HudSystem
1. RainfallSystem
1. RenderSystem

## Tutorials

This is a set of tutorials using `Monogame.Extended`.

### Tutorials:Animation

This does nothing yet and you will get an error

### Tutorials:Batching

An alternative to the default Monogame SpriteBatch.

### Tutorials:BitmapFonts

Showing how to use the `Monogame.Extended.BitmapFonts` to load custom fonts from PNG and FNT files.

Additionally how to create a Mono-spaced (Equal-spaced) font using a PNG file.

### Tutorials:Camera

Shows how to use `OrthographicCamera` to draw sprites and create parallax scrolling affect.

### Tutorials:Collision

Shows how to use the `Monogame.Extended.Collisions` to create three types of actors.  Stationary, Moveable, and Player controlled.  Also shows interaction with walls.

### Tutorials:InputListener

Basic examples for the Mouse and Keyboard inputs functionality within `Monogame.Extended`.

### Tutorials:Particles

Quick demo showing how to add a particle system with emitter.

### Tutorials:Shapes

Showing how to use the primitive shapes in `Monogame.Extended`

* DrawPoint
* DrawRectangle
* DrawSolidRectangle
* DrawCircle
* DrawSolidCircle
* DrawEllipse
* DrawSolidEllipse
* DrawSegment
* DrawPolygon
* DrawSolidPolygon

### Tutorials:Sprites

Shows off some features like `Texture2DRegion` and`NinePatch`.  

Also shows how to do clipping.

### Tutorials:TiledMaps

Shows how to load tiles from Tiled maps TMX files.  

### Tutorials:ViewportAdapter

Demonstrates the adapters:
* BoxingViewportAdapter
* ViewportAdapter
* DefaultViewportAdapter
* ScalingViewportAdapter

Shows how they behave for rescaling the screen.

## GUI

**Deprecated**
```
The GUI demo is no longer supported and will be removed.  
Monogame.Extended.Gui has been deprecated.  
Monogame.Extended is now recommending and using the GUM GUI framework by FlatRedBall.
```

For using an in-game GUI, see the [Monogame Extended documentation](https://www.monogameextended.net/docs/features/ui/gum/gum-forms/)

Other links:
* See blog entry [Monogame Chews Gum](https://www.monogameextended.net/blog/monogame-extended-gum/)
* And the official GUM documentation [here](https://docs.flatredball.com/gum/monogame/setup).


