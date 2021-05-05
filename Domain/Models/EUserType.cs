using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public enum EUserType: byte
    {
        [Description("Veterinario")]
        Vet =0,
        [Description("Dueño de mascota")]
        Owner = 1
    }
}
