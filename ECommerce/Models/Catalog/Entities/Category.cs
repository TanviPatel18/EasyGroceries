using System;
using System.Collections.Generic;
using System.Text;

using ECommerce.Models.Common;

namespace ECommerce.Models.Catalog.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }

        public string ImageUrl { get; set; }
    }
}
