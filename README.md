# Game Jam Starter Kit
This Repository serves as a template project for new Unity Projects during Game Jams. It contains several Scripts and Components for usually always needed components, like Sfx/Music Player, Resource System. It also already has several useful Packages installed, like the new Input System or Cinemachine. Also basic Main Menu UI is included.

This is not meant to solve general problems for any game. Instead it should take a few hours of that always same tedious work off your shoulders and provide some basic, everytime used contents so you can just focus on the main logic of the game. Of course there are efforts needed to adjust some details to your wishes, but at least you are having a foundation already.

# Usage Instruction

There are two ways to use this template.

## Using this repository

On the main page of this Repository find the "Use this Template" button, click that and configure your new repository as you wish.

## Package Installation

If you just want to use the contents, you can use the given [.unitypackage](https://github.com/ThePeat94/Game-Jam-Starter-Kit/releases/tag/Package) and import it into your empty (or existing) Unity Project. Just import what you need but be aware of the dependencies. This will not install the here installed packages. It will provide you just the contents.

# Contents

On the root you will find different folders for different purposes, layed out for a collaboration context, where a Digital Artist or Sound Engineer also knows the basic of Git (totally basic like push and pull). They have their own designated folders - 2D/3D for digital art and audio for Music/SFX respective. Why so? This way when the Developers work in Unity there won't be any conflicts with the other non-developers. 

The GameJam-Game Folder is the root project folder for Unity. Tell Unity to open that folder and it will load.

Version: 2021.2.17f1. It can be upgraded or downgraded as you wish, but if the version difference is too high, I cannot guarantee that it will work as it is supposed to.

## Audio
- [MusicData](GameJam-Game/Assets/Scripts/Scriptables/Audio/MusicData.cs): Use this to configure a Music Track. You can set a volume and the looping flag. It also enables you to define a series of Music Tracks and to define Music Loops (Just refer the beginning Music Data at the last Music Data)
- [SfxData](GameJam-Game/Assets/Scripts/Scriptables/Audio/SfxData.cs): Similar to MusicData just for Sfx.

- [MusicPlayer](GameJam-Game/Assets/Scripts/Audio/MusicPlayer.cs): A Singleton Music Player, used to serve as a globally active Music Player which you can address any time to play other Music Tracks if needed.
- [SfxPlayer](GameJam-Game/Assets/Scripts/Audio/SfxPlayer.cs): A individual Sound Effect Player. Use it on your GameObjects, which might play some Sound Effects. It allows you to play Sfx looped or just oneshot.
- [RandomClipPlayer](GameJam-Game/Assets/Scripts/Audio/RandomClipPlayer.cs): A individual Sound Effect Player. Use it to play a randomly chosen sound effect from a given source.

## Resource Management

- [Resource](GameJam-Game/Assets/Scripts/Scriptables/Resource.cs): Globally available resources which you can drag and drop onto your GameObjects if they refer a Resource. Meant for a global or singular use instead individually usage.

- [ResourceController](GameJam-Game/Assets/Scripts/ResourceController.cs): Manages the amount of a resource. Modifying the CurrentValue and MaxValue of it will invoke events with the new given value.
- [ResourceData](GameJam-Game/Assets/Scripts/Scriptables/ResourceData.cs): Used to configure a Resource or ResourceController initially.

## Basic Player Controller
There is the super basic [PlayerController](GameJam-Game/Assets/Scripts/PlayerController.cs) which you can also use for a quick setup for a PlayerController.

## Input Management

This template uses Unity's new Input System. Define, modify, remove actions and Bindings in [Input > PlayerInput.inputaction](GameJam-Game/Assets/Input/PlayerInput.inputaction).

- InputProcessor: This is used to access the defined actions in Input > PlayerInput.inputaction. You can add, remove and modify the bindings in the PlayerInput.inputaction file but if you want to keep track if an action was triggered, performed, cancelled, started, ... you can adapt the InputProcessor and add your actions there. You can either put it on any GameObject and another MonoBehavior references it, where you then have to drag the script into

```csharp
public class ApplicationQuitter : MonoBehavior 
{
    [SerializeField] private InputProcessor m_inputProcessor;

    public void Update()
    {
        if (this.m_inputProcessor.QuitTriggered)
            Application.Quit();
    }
}
```

... or put it on the same GameObject and grab the Component during Awake or Start.

```csharp
public class ApplicationQuitter : MonoBehavior 
{
    private InputProcessor m_inputProcessor;

    private void Awake()
    {
        this.m_inputProcessor = this.GetComponent<InputProcessor>();
    }

    private void Update()
    {
        if (this.m_inputProcessor.QuitTriggered)
            Application.Quit();
    }
}
```

# Checklist

- [ ] Install Unity Package OR
- [ ] Use this Repo
- [ ] Replace the Game Logo in the Start Menu
- [ ] Replace your Logo in the Start Menu
- [ ] Adjust the Project Settings in Edit > Project Settings > Player and set a Company Name and a Product Name
- [ ] Restyle the Buttons
- [ ] Put another background


# Issues? Wishes?
Just open a new Issue. I will take a look into it and might bring it into this template.
