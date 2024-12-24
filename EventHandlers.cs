using UnityEngine;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using PlayerEvents = Exiled.Events.Handlers.Player;
using PlayerRoles;
using SCP1162.API;
using Item = Exiled.API.Features.Items.Item;
using Random = UnityEngine.Random;

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
    if (!(Vector3.Distance(ev.Player.Position, RoleTypeId.Scp173.GetRandomSpawnLocation().Position) <= 8.2f)) return;
    if (_plugin.Config.UseHints)
     ev.Player.ShowHint(_plugin.Config.ItemDropMessage, _plugin.Config.ItemDropMessageDuration);
    else
      ev.Player.Broadcast(_plugin.Config.ItemDropMessageDuration, _plugin.Config.ItemDropMessage, Broadcast.BroadcastFlags.Normal, true);
    ev.IsAllowed = false;
    var oldItem = ev.Item.Base.ItemTypeId;
    ev.Player.RemoveItem(ev.Item);
    var newItemType = _plugin.Config.Chances[Random.Range(0, _plugin.Config.Chances.Count)];
    var eventArgs = new UsingScp1162EventArgs(ev.Player, newItemType, oldItem);
    Scp1162Event.OnUsingScp1162(eventArgs);
    var newItem = Item.Create(eventArgs.ItemAfter);
    ev.Player.AddItem(newItem);
    ev.Player.DropItem(newItem);
  }
}