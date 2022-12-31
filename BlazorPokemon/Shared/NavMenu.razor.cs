using BlazorPokemon.Modals;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Blazored.LocalStorage;


namespace BlazorPokemon.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public IStringLocalizer<NavMenu> Localizer { get; set; }
    }
}
