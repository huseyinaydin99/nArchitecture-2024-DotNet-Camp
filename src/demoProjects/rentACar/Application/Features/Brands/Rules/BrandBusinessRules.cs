using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
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

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException("Marka adı mevcut.");
    }

    public void BrandShouldExistWhenRequested(Brand brand)
    {
        if (brand == null) throw new BusinessException("İstenilen marka mevcut değil.");
    }

    public async Task BrandShouldExistsWhenRequested(Brand brand)
    {
        //Brand brand = await _brandRepository.GetAsync(b => b.Id == id);
        if (brand == null) throw new BusinessException("Bu marka mevcut değil.");
    }
}