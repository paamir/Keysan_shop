﻿namespace ShopManagement.Application.Contracts.Slide
{
    public class SlideViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string ButtonText { get; set; }
        public string CreationDate { get; set; }
        public string Link { get; set; }

    }
}