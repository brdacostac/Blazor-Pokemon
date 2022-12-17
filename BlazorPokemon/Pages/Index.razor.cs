using BlazorPokemon.Modals;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using BlazorPokemon.Models;
using BlazorPokemon.Services;
using Blazored.LocalStorage;


namespace BlazorPokemon.Pages
{
    public partial class Index
    {
        [Inject]
        public IStringLocalizer<Index> Localizer { get; set; }

    }
}
