using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class UserAcc
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly CreatedDate { get; set; } 

    public int UserGroupId { get; set; }

    public int UserStateId { get; set; }

    [JsonConverter(typeof(GroupConverter))]
    public virtual Group UserGroup { get; set; } = null!;
    [JsonConverter(typeof(StateConverter))]
    public virtual State UserState { get; set; } = null!;
}
