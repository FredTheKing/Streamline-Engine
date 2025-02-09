using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Manager;
#if !RESOURCES 
using StreamlineEngine.Engine.Node;
using Raylib_cs;
#endif

namespace StreamlineEngine.Engine.Etc;

/// <summary>
/// Main game class. Contain all game <c>context</c>.
/// </summary>
public class Context
{
  public Managers Managers { get; }
  #if !RESOURCES
  public Global Global { get; } = new();
  public Root Root { get; } = new(Config.RootFolders);
  #endif
  
  public Context() { Managers = new Managers(this); }
  
  public void Run()
  {
    #if RESOURCES
    Managers.Package.Pack(Extra.GetJsonToPackAsDict(Config.ResourcesJsonFilename));
    #else
      Init();
      Loop();
      Close();
    #endif
  }
  #if !RESOURCES
  private void Init()
  {
    Raylib.SetConfigFlags(Config.WindowConfigFlags);
    Raylib.InitWindow((int)Config.WindowSize.X, (int)Config.WindowSize.Y, Config.WindowTitleInit);
    Raylib.InitAudioDevice();
    Managers.Debug.Separator(ConsoleColor.Yellow, "Window and audio created. Starting importing game assets...");
    Registration.MaterialsInitChanges(this);
    Registration.ItemsInitChanges(this);
    Registration.FoldersInitChanges(this);
    Managers.Resource.RegisterFromStruct(this);
    Managers.Item.RegisterFromStruct();
    Managers.Folder.RegisterFromStruct();
    Managers.Debug.Separator(ConsoleColor.Yellow, "Structs import done! Starting root initialisation...");
    Looper.Init(this);
    Managers.Debug.Separator(ConsoleColor.Green, "Initialisation fully ended! Enjoy! :D");
  }

  private void Loop()
  {
    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Config.WindowBackgroundColor);
      
      Looper.EarlyUpdate(this);
      Looper.Update(this);
      Looper.LateUpdate(this);
      Looper.PreDraw(this);
      Looper.Draw(this);
      
      Raylib.EndDrawing();
    }
  }

  private void Close()
  {
    Managers.Debug.Separator(ConsoleColor.Blue, "Terminating program and unloading resources...");
    Root.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
    Managers.Debug.Separator(ConsoleColor.Green, Config.ByePhrase[new Random().Next(Config.ByePhrase.Length)]);
  }
  #endif
}