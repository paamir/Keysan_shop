using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide
{
    public class SlideCreateModel
    {
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        [MaxLength(255)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        [MaxLength(100)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        [MaxLength(55)]
        public string Title { get; set; }

        [MaxLength(40)]
        public string Header { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        [MaxLength(20)]
        public string ButtonText { get; set; }

        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Link { get; set; }

    }
}
