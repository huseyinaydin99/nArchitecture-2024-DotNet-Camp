using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//بسم الله الرحمن الرحيم
/**
 *
 * @author Huseyin_Aydin
 * @since 1994
 * @category DotNet Core nArchitechture, C#.
 *
 */

namespace Domain.Entities;

public class Brand : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public Brand()
    {
        //Models = new List<Model>();
    }

    public Brand(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}