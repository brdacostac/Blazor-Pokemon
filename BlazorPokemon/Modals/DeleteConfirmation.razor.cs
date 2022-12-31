﻿using BlazorPokemon.Services;
using Microsoft.AspNetCore.Components;
using BlazorPokemon.Models;
using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.Extensions.Localization;

namespace BlazorPokemon.Modals
{
    public partial class DeleteConfirmation
    {
        [Inject]
        public IStringLocalizer<DeleteConfirmation> Localizer { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private Pokemon pokemon = new Pokemon();

        protected override async Task OnInitializedAsync()
        {
            // Get the pokemon
            pokemon = await DataService.GetById(Id);
        }

        void ConfirmDelete()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(true));
        }

        void Cancel()
        {
            ModalInstance.CancelAsync();
        }
    }
}
