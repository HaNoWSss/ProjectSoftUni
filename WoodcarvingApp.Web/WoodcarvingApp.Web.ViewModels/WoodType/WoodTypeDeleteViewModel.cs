﻿namespace WoodcarvingApp.Web.ViewModels.WoodType
{
    public class WoodTypeDeleteViewModel
    {
        public Guid Id { get; set; }

        public string WoodTypeName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}