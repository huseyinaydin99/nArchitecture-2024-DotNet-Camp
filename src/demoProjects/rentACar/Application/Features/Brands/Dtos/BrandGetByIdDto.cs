﻿using System;
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

namespace Application.Features.Brands.Dtos;

public class BrandGetByIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}