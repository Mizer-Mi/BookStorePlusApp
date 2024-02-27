using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record BookDtoForUpdate : BookDTOValidation
    {
        [Required (ErrorMessage ="Id cannot be empty.")]
        public int Id { get; init; }

    }
}
