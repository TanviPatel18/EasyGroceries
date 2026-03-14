using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Catalog.DTOs
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public string ImageUrl { get; set; }
    }
}
