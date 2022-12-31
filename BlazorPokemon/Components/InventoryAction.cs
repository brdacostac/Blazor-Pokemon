using BlazorPokemon.Models;

namespace BlazorPokemon.Components
{
    public class InventoryAction
    {
        public string Action { get; set; }
        public int Index { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
