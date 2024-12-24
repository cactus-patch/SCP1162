using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerEvents = Exiled.Events.Handlers.Player;
using SCP1162.API;
using Item = Exiled.API.Features.Items.Item;

namespace SCP1162;

public class EventHandler {
  private readonly Plugin _plugin;
  
  public EventHandler(Plugin plugin) {
    _plugin = plugin;
    PlayerEvents.DroppingItem += OnDroppingItem;
  }
  
  ~EventHandler() {
    PlayerEvents.DroppingItem -= OnDroppingItem;
  }

  private void OnDroppingItem(DroppingItemEventArgs ev) {
    if (!ev.IsAllowed) return;
    if (ev.Player.CurrentRoom != Room.Get(_plugin.Config.RoomType)) return;
    if (_plugin.Config.UseHints)
     ev.Player.ShowHint(_plugin.Config.ItemDropMessage, _plugin.Config.ItemDropMessageDuration);
    else
      ev.Player.Broadcast(_plugin.Config.ItemDropMessageDuration, _plugin.Config.ItemDropMessage, Broadcast.BroadcastFlags.Normal, true);
    ev.IsAllowed = false;
    ev.Player.RemoveItem(ev.Item);
    var newItemType = _plugin.Config.Chances[UnityEngine.Random.Range(0, _plugin.Config.Chances.Count)];
    var eventArgs = new UsingScp1162EventArgs(ev.Player, newItemType, ev.Item.Base.ItemTypeId);
    Scp1162Event.OnUsingScp1162(eventArgs);
    var newItem = Item.Create(eventArgs.ItemAfter);
    ev.Player.AddItem(newItem);
    ev.Player.DropItem(newItem);
  }
}