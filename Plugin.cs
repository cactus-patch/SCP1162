using Exiled.API.Features;

namespace SCP1162;

public class Plugin : Plugin<Config> {
  public override string Name => "SCP1162";
  public override string Author => "xRoier, furry";
  public override Version Version => new(9, 0, 2);
  public override Version RequiredExiledVersion => new(9, 0, 0);
  public static Plugin? Instance;

  public override void OnEnabled() {
    Instance = this;
    Exiled.Events.Handlers.Player.DroppingItem += EventHandlers.OnItemDropped;
    base.OnEnabled();
  }

  public override void OnDisabled() {
    Instance = null;
    Exiled.Events.Handlers.Player.DroppingItem -= EventHandlers.OnItemDropped;
    base.OnDisabled();
  }
}