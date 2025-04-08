using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM_User.Application.DTO
{
    public class ResponseSuccess
    {
        public bool Success { get; } = true;
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic? Data { get; set; }
    }

    public class ResponseError
    {
        public bool Success { get; } = false;
        public string? Error { get; set; }
    }
}
