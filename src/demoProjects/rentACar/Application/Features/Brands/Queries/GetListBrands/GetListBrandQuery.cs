using Application.Features.Brands.Models;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrands;

public class GetListBrandQuery : IRequest<BrandListModel>
{
    public PageRequest PageRequest { get; set; }
    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }
        public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            BrandListModel mappedBrandListModel = _mapper.Map<BrandListModel>(brands);
            return mappedBrandListModel;
        }
    }
}