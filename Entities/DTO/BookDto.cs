using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    [Serializable, DataContract]
    public record BookDto()
    {
        [DataMember]
        public int Id { get; init; }
        [DataMember]
        public string Title { get; init; }
        [DataMember]
        public decimal Price { get; init; }
    }
}
